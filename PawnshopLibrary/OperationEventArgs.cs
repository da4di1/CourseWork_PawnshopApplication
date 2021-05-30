using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public delegate void OperationStateHandler(object sender, OperationEventArgs arg);


    public class OperationEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public OperationEventArgs(string message)
        {
            Message = message;
        }
    }
}
