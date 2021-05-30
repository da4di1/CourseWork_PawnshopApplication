using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public class PreciousMetals : Pledge
    {
        public PreciousMetals()
        {
            Random rand = new Random();
            Type = PledgeType.PreciousMetals;
            TypeInString = "A precious metal";
            StartingCost = rand.Next(10000, 50000);
            CurrentCost = StartingCost;
            StandartPercentage = 0.2;
            PercentageForStoring = 0.1;
        }
    }
}
