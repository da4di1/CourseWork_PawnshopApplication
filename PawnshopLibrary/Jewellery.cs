using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public class Jewellery : Pledge
    {
        public Jewellery()
        {
            Random rand = new Random();
            TypeInString = "A jewel";
            Type = PledgeType.Jewellery;
            StartingCost = rand.Next(10000, 100000);
            CurrentCost = StartingCost;
            StandartPercentage = 0.3;
            PercentageForStoring = 0.2;
        }
    }
}
