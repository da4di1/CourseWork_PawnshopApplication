using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawnshopLibrary;

namespace PawnshopApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Pawnshop pawnshop = new Pawnshop();
            bool alive = true;
            while (alive)
            {
                bool customerOnline = true;
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter your name(use only Latin letters): ");
                string name = Console.ReadLine();
                bool wrongNameFormat = false;
                for (int i = 0; i < name.Length; i++)
                {
                    if (((int)name[i] < 65) || ((int)name[i] > 90 && (int)name[i] < 97) || ((int)name[i] > 122))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nUnavailable format. Use only Latin letters!\n");
                        Console.ForegroundColor = color;
                        wrongNameFormat = true;
                        break;
                    }
                }
                if (wrongNameFormat)
                    continue;
                while (customerOnline)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\n1.Sell something. \t 2.Leave something on bail. \t\t 3.Buy something." +
                    "\n4.Buy back a pledge. \t 5.See stuff available for Buying. \t 6.Leave the pawnshop.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("What do you want to do: ");
                    Console.ForegroundColor = color;

                    try
                    {
                        int command = Convert.ToInt32(Console.ReadLine());

                        switch (command)
                        {
                            case 1:
                                Sell(pawnshop);
                                break;
                            case 2:
                                LeaveOnBail(pawnshop, name);
                                break;
                            case 3:
                                Buy(pawnshop);
                                break;
                            case 4:
                                BuyBack(pawnshop, name);
                                break;
                            case 5:
                                SeeAvailableForBuying(pawnshop);
                                break;
                            case 6:
                                customerOnline = false;
                                continue;
                            default:
                                color = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nInvalid number. Choose another one.\n");
                                Console.ForegroundColor = color;
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nEnter a number!\n");
                        Console.ForegroundColor = color;
                    }
                    catch (PawnshopOperationProblemException exc)
                    {
                        color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n{exc.Message}\n");
                        Console.ForegroundColor = color;
                    }
                    catch (NullReferenceException exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }
                bool numberNotEntered = true;
                while (numberNotEntered)
                {
                    try
                    {
                        Console.WriteLine("\nDo you want to close the program? 1.Yes; 2.No.");
                        int option = Convert.ToInt32(Console.ReadLine());
                        if (option == 1)
                        {
                            numberNotEntered = false;
                            alive = false;
                            continue;
                        }
                        else if (option == 2)
                        { 
                            SkipMonths(pawnshop);
                            pawnshop.CheckValidity();
                            numberNotEntered = false;
                        }
                        else
                        {
                            color = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid number. Choose another one.");
                            Console.ForegroundColor = color;
                        }
                    }
                    catch (FormatException)
                    {
                        color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nEnter a number!");
                        Console.ForegroundColor = color;
                    }
                    catch (PawnshopOperationProblemException exc)
                    {
                        color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n{exc.Message}");
                        Console.ForegroundColor = color;
                    }
                    catch (NullReferenceException exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }
            }
        }



        private static PledgeType ChooseTypeOfPledge()
        {
            PledgeType pledgeType = PledgeType.PreciousMetals;

            bool flag = true;

            while (flag)
            {
                int type = Convert.ToInt32(Console.ReadLine());

                switch (type)
                {
                    case 1:
                        pledgeType = PledgeType.PreciousMetals;
                        flag = false;
                        break;
                    case 2:
                        pledgeType = PledgeType.Jewellery;
                        flag = false;
                        break;
                    case 3:
                        pledgeType = PledgeType.Antiques;
                        flag = false;
                        break;
                    case 4:
                        pledgeType = PledgeType.Appliances;
                        flag = false;
                        break;
                    case 5:
                        pledgeType = PledgeType.Watches;
                        flag = false;
                        break;
                    case 6:
                        pledgeType = PledgeType.Cars;
                        flag = false;
                        break;
                    default:
                        ConsoleColor color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid number. Choose another one:");
                        Console.ForegroundColor = color;
                        break;
                }
            }
            return pledgeType;
        }


        private static void SeeAvailableForBuying(Pawnshop pawnshop)
        {
            int[] availableForBuying = pawnshop.ShowAvailableForBuying();
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nStuff available for buying:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Precious Metals: {availableForBuying[0]};" +
                $"\nJewellery: {availableForBuying[1]};" +
                $"\nAntiques: {availableForBuying[2]};" +
                $"\nAppliances: {availableForBuying[3]};" +
                $"\nWatches: {availableForBuying[4]};" +
                $"\nCars: {availableForBuying[5]};\n");
            Console.ForegroundColor = color;
        }


        private static void ShowLossAndProfit(Pawnshop pawnshop)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nProfit of the pawnshop: {pawnshop.Profit:f2} UAH");
            Console.WriteLine($"Loss of the pawnshop: {pawnshop.Loss:f2} UAH\n");
            Console.ForegroundColor = color;
        }


        private static void SkipMonths(Pawnshop pawnshop)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEnter an amount of months you want to skip: ");
            Console.ForegroundColor = color;
            int monthsToSkip = Convert.ToInt32(Console.ReadLine());
            if (monthsToSkip < 0)
                throw new PawnshopOperationProblemException("The amount of months to skip can't be less than zero.");
            Console.WriteLine();

            pawnshop.monthsPassed += monthsToSkip;

            for (int i = 0; i < monthsToSkip; i++)
                pawnshop.AddPercentageToCost();
            Console.WriteLine($"{monthsToSkip} months have passed\n");
        }


        private static void Sell(Pawnshop pawnshop)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nChoose a type of pledge you want to sell:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("1.Percious metals, 2.Jewellery, 3.Antiques, 4.Appliances, 5.Watches, 6.Cars");
            Console.ForegroundColor = color;

            PledgeType pledgeType = ChooseTypeOfPledge();
            pawnshop.Sell(pledgeType, 
                SoldHandler, LeftOnBailHandler, BoughtHandler, BoughtBackHandler, TermExpiredHandler);
            ShowLossAndProfit(pawnshop);
        }


        private static void LeaveOnBail(Pawnshop pawnshop, string name)
        {
            ConsoleColor color = Console.ForegroundColor;
            pawnshop.CheckCustomerOnBlackListed(name);
            bool customerResticted = pawnshop.CheckCustomerOnRestriction(name, out double additionalPercentage);
            if (customerResticted)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nYou are on the restricted list. Your interest rate is increased by {additionalPercentage}%." +
                    $"\nIf you don't buy back your pledge, you will be added to the black list!");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nChoose a type of pledge you want to leave:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("1.Percious metals, 2.Jewellery, 3.Antiques, 4.Appliances, 5.Watches, 6.Cars");
            Console.ForegroundColor = color;

            PledgeType pledgeType = ChooseTypeOfPledge();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nChoose a storage term:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("1.A month,   2.Three months,   3.Six months,   4.A year");
            Console.ForegroundColor = color;

            int storageTerm = 0;

            bool flag = true;

            while (flag)
            {
                int type = Convert.ToInt32(Console.ReadLine());

                switch (type)
                {
                    case 1:
                        storageTerm = 1;
                        flag = false;
                        break;
                    case 2:
                        storageTerm = 3;
                        flag = false;
                        break;
                    case 3:
                        storageTerm = 6;
                        flag = false;
                        break;
                    case 4:
                        storageTerm = 12;
                        flag = false;
                        break;
                    default:
                        color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid number. Choose another one:");
                        Console.ForegroundColor = color;
                        break;
                }
            }
            pawnshop.LeaveOnBail(pledgeType, storageTerm, name, 
                SoldHandler, LeftOnBailHandler, BoughtHandler, BoughtBackHandler, TermExpiredHandler);
            ShowLossAndProfit(pawnshop);
        }


        private static void Buy(Pawnshop pawnshop)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nChoose a type of pledge you want to buy:");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("1.Percious metals, 2.Jewellery, 3.Antiques, 4.Appliances, 5.Watches, 6.Cars");
            Console.ForegroundColor = color;

            PledgeType pledgeType = ChooseTypeOfPledge();
            pawnshop.Buy(pledgeType);
            ShowLossAndProfit(pawnshop);
        }


        private static void BuyBack(Pawnshop pawnshop, string name)
        {
            pawnshop.CheckCustomerOnHavingStuffInStorage(name);
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEnter id of your receipt: ");
            Console.ForegroundColor = color;

            int id = Convert.ToInt32(Console.ReadLine());

            pawnshop.BuyBack(id, name);
            ShowLossAndProfit(pawnshop);
        }



        private static void SoldHandler(object sender, OperationEventArgs arg)
        {
            Console.WriteLine(arg.Message);
        }

        private static void LeftOnBailHandler(object sender, OperationEventArgs arg)
        {
            Console.WriteLine(arg.Message);
        }

        private static void BoughtHandler(object sender, OperationEventArgs arg)
        {
            Console.WriteLine(arg.Message);
        }

        private static void BoughtBackHandler(object sender, OperationEventArgs arg)
        {
            Console.WriteLine(arg.Message);
        }

        private static void TermExpiredHandler(object sender, OperationEventArgs arg)
        {
            Console.WriteLine(arg.Message);
        }
    }
}
