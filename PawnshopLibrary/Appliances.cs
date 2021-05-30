using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public class Appliances : Pledge
    {
        public Appliances()
        {
            Random rand = new Random();
            TypeInString = "An appliance";
            Type = PledgeType.Appliances;
            StartingCost = rand.Next(10000, 40000);
            CurrentCost = StartingCost;
            StandartPercentage = 0.2;
            PercentageForStoring = 0.1;
        }
    }
}
