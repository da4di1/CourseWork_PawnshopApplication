using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public enum PledgeType
    {
        PreciousMetals,
        Jewellery,
        Antiques,
        Appliances,
        Watches,
        Cars
    }


    public class Pawnshop
    {
        private int idCounter = 0;
        private double additionalPercentage = 0.1;
        public int monthsPassed = 0;

        public double Profit { get; private set; }
        public double Loss { get; private set; }

        private List<Pledge> availableForSell = new List<Pledge>();
        private List<Pledge> inStorage = new List<Pledge>();

        private List<string> customerBlackList = new List<string>();
        private List<string> customerRestrictedList = new List<string>();


        private Pledge CreateNewPledgeTypeObj(PledgeType pledgeType)
        {
            Pledge newPledge = null;
            switch (pledgeType)
            {
                case PledgeType.PreciousMetals:
                    newPledge = new PreciousMetals();
                    break;
                case PledgeType.Jewellery:
                    newPledge = new Jewellery();
                    break;
                case PledgeType.Antiques:
                    newPledge = new Antiques();
                    break;
                case PledgeType.Appliances:
                    newPledge = new Appliances();
                    break;
                case PledgeType.Watches:
                    newPledge = new Watches();
                    break;
                case PledgeType.Cars:
                    newPledge = new Cars();
                    break;
            }
            return newPledge;
        }


        public void Sell(PledgeType pledgeType, OperationStateHandler SoldHandler, 
            OperationStateHandler LeftOnBailHandler, OperationStateHandler BoughtHandler, 
            OperationStateHandler BoughtBackHandler, OperationStateHandler TermExpiredHandler)
        {
            Pledge newPledge = CreateNewPledgeTypeObj(pledgeType);
            availableForSell.Add(newPledge);

            newPledge.Sold += SoldHandler;
            newPledge.LeftOnBail += LeftOnBailHandler;
            newPledge.Bought += BoughtHandler;
            newPledge.BoughtBack += BoughtBackHandler;
            newPledge.TermExpired += TermExpiredHandler;

            Loss += newPledge.StartingCost;

            newPledge.OnSold();
        }


        public void LeaveOnBail(PledgeType pledgeType, int storageTerm, string name, OperationStateHandler SoldHandler,
            OperationStateHandler LeftOnBailHandler, OperationStateHandler BoughtHandler,
            OperationStateHandler BoughtBackHandler, OperationStateHandler TermExpiredHandler)
        {
            Pledge newPledge = CreateNewPledgeTypeObj(pledgeType);
            if (customerRestrictedList.Exists(x => x == name))
                newPledge.PercentageForStoring += additionalPercentage;
            newPledge.OwnerName = name;
            newPledge.StorageTerm = storageTerm;
            newPledge.EdgeOfStorage = storageTerm + monthsPassed;
            newPledge.Id = idCounter;
            inStorage.Add(newPledge);

            newPledge.Sold += SoldHandler;
            newPledge.LeftOnBail += LeftOnBailHandler;
            newPledge.Bought += BoughtHandler;
            newPledge.BoughtBack += BoughtBackHandler;
            newPledge.TermExpired += TermExpiredHandler;

            newPledge.OnLeftOnBail();

            idCounter += 1;
            Loss += newPledge.StartingCost;
        }


        public void Buy(PledgeType pledgeType)
        {
            bool flag = availableForSell.Exists(x => x.Type == pledgeType);

            if (!flag)
                throw new PawnshopOperationProblemException("This type of items is not available for selling.");

            int idOfThingToBuy = 0;
            for (int i = 0; i < availableForSell.Count; i++)
            {
                if (availableForSell[i].Type == pledgeType)
                {
                    idOfThingToBuy = i;
                    break;
                }
            }

            double cost = availableForSell[idOfThingToBuy].CurrentCost * (1 + availableForSell[idOfThingToBuy].StandartPercentage);
            availableForSell[idOfThingToBuy].CurrentCost = cost;
            Profit += cost;

            availableForSell[idOfThingToBuy].OnBought();

            availableForSell.RemoveAt(idOfThingToBuy);
        }


        public void BuyBack(int id, string name)
        {
            bool flag = inStorage.Exists(x => (x.Id == id && x.OwnerName == name));

            if (!flag)
                throw new PawnshopOperationProblemException("You didn't left anything with this id.");

            int idOfThingToBuyBack = 0;
            for (int i = 0; i < inStorage.Count; i++)
            {
                if (inStorage[i].Id == id)
                {
                    idOfThingToBuyBack = i;
                    break;
                }
            }

            double cost = inStorage[idOfThingToBuyBack].CurrentCost;
            Profit += cost;

            inStorage[idOfThingToBuyBack].OnBoughtBack();

            inStorage.RemoveAt(idOfThingToBuyBack);
        }


        public int[] ShowAvailableForBuying()
        {
            int[] availableForBuying = new int[6];
            for (int i = 0; i < availableForSell.Count; i++)
            {
                int idOfTypeToIncrease = (int)availableForSell[i].Type;
                availableForBuying[idOfTypeToIncrease] += 1;
            }
            return availableForBuying;
        }


        public void CheckValidity()
        {
            string typeOfRestriction;
            Pledge pledgeToBeAvailableForSell;
            for (int i = 0; i < inStorage.Count; i++)
            {
                if (inStorage[i].EdgeOfStorage <= monthsPassed)
                {
                    pledgeToBeAvailableForSell = inStorage[i];

                    if (customerRestrictedList.Exists(x => x == pledgeToBeAvailableForSell.OwnerName))
                    {
                        customerBlackList.Add(pledgeToBeAvailableForSell.OwnerName);
                        customerRestrictedList.Remove(pledgeToBeAvailableForSell.OwnerName);
                        typeOfRestriction = "black list";
                    }
                    else
                    {
                        customerRestrictedList.Add(pledgeToBeAvailableForSell.OwnerName);
                        typeOfRestriction = "list of restricted customers";
                    }

                    pledgeToBeAvailableForSell.CurrentCost = pledgeToBeAvailableForSell.StartingCost;
                    availableForSell.Add(pledgeToBeAvailableForSell);
                    pledgeToBeAvailableForSell.OnTermExpired(typeOfRestriction);
                    inStorage.RemoveAt(i);
                    i--;
                }
            }
        }


        public void AddPercentageToCost()
        {
            for (int i = 0; i < inStorage.Count; i++)
            {
                inStorage[i].CurrentCost *= (1 + inStorage[i].PercentageForStoring);
            }
        }

        
        public void CheckCustomerOnBlackListed(string name)
        {
            if (customerBlackList.Exists(x => x == name))
                throw new PawnshopOperationProblemException("You are on the black list of the pawnshop. You can't leave stuff on bail!");
        }

        
        public bool CheckCustomerOnRestriction(string name, out double percentageToAdd)
        {
            percentageToAdd = additionalPercentage;
            if (customerRestrictedList.Exists(x => x == name))
                return true;
            return false;
        }


        public void CheckCustomerOnHavingStuffInStorage(string name)
        {
            bool flag = inStorage.Exists(x => x.OwnerName == name);

            if (!flag)
                throw new PawnshopOperationProblemException("You didn't left anything for storage.");
        }

    }
}
