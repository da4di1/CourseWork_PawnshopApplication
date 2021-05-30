using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public class Watches : Pledge
    {
        public Watches()
        {
            Random rand = new Random();
            TypeInString = "A watch";
            Type = PledgeType.Watches;
            StartingCost = rand.Next(10000, 400000);
            CurrentCost = StartingCost;
            StandartPercentage = 0.35;
            PercentageForStoring = 0.2;
        }
    }
}
