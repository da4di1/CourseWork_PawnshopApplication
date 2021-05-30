using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public class Antiques : Pledge
    {
        public Antiques()
        {
            Random rand = new Random();
            TypeInString = "An antique";
            Type = PledgeType.Antiques;
            StartingCost = rand.Next(10000, 500000);
            CurrentCost = StartingCost;
            StandartPercentage = 0.35;
            PercentageForStoring = 0.2;
        }
    }
}
