using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public class Pledge : ICustomerOperations
    {
        protected string TypeInString { get; set; }
        public string OwnerName { get; set; }
        public PledgeType Type { get; protected set; }
        public double StartingCost { get; protected set; }
        public double CurrentCost { get; set; }
        public int Id { get; set; }
        public double StandartPercentage { get; protected set; }
        public double PercentageForStoring { get; set; }
        public int StorageTerm { protected get; set; }
        public int EdgeOfStorage { get; set; }

        public event OperationStateHandler Sold;
        public event OperationStateHandler LeftOnBail;
        public event OperationStateHandler Bought;
        public event OperationStateHandler BoughtBack;
        public event OperationStateHandler TermExpired;


        public void OnSold()
        {
            if (Sold != null)
                Sold(this, new OperationEventArgs($"\n{TypeInString} was sold to pawnshop for {StartingCost:f2} UAH."));
        }


        public void OnLeftOnBail()
        {
            if (LeftOnBail != null)
                LeftOnBail(this, new OperationEventArgs($"\n{TypeInString} was left in pawnshop for {StorageTerm} month(s) " +
                $"with growing interest rate which is equal to {PercentageForStoring}%." +
                $"\nStarting cost: {StartingCost:f2} UAH. Receipt id: {Id}."));
        }


        public void OnBought()
        {
            if (Bought != null)
                Bought(this, new OperationEventArgs($"\nThe pawnshop sold {TypeInString} for {CurrentCost:f2} UAH."));
        }


        public void OnBoughtBack()
        {
            if (BoughtBack != null)
                BoughtBack(this, new OperationEventArgs($"\nThe owner took {TypeInString} with id: {Id} from storage." +
                    $" The debt of {CurrentCost:f2} UAH was paid off."));
        }


        public void OnTermExpired(string typeOfRestriction)
        {
            if (TermExpired != null)
                TermExpired(this, new OperationEventArgs($"The term of storage of {TypeInString} " +
                    $"with id: {Id} has expired. The stuff is now available for every customer to buy." +
                    $" \nCurrent cost is {(StartingCost * (1 + StandartPercentage)):f2}. {OwnerName} was added to the {typeOfRestriction}.\n"));
        }
    }
}
