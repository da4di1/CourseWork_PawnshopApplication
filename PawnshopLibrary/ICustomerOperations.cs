using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public interface ICustomerOperations
    {
        void OnSold();
        void OnLeftOnBail();
        void OnBought();
        void OnBoughtBack();
    }
}
