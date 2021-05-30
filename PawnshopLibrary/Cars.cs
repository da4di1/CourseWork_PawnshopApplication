using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public class Cars : Pledge
    {
        public Cars()
        {
            Random rand = new Random();
            TypeInString = "A car";
            Type = PledgeType.Cars;
            StartingCost = rand.Next(80000, 500000);
            CurrentCost = StartingCost;
            StandartPercentage = 0.4;
            PercentageForStoring = 0.22;
        }
    }
}
