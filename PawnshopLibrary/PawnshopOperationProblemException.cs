using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopLibrary
{
    public class PawnshopOperationProblemException : Exception
    {
        public PawnshopOperationProblemException() : base() { }

        public PawnshopOperationProblemException(string str) : base(str) { }

        public PawnshopOperationProblemException(string str, Exception inner) : base(str, inner) { }

        protected PawnshopOperationProblemException(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) : base(si, sc) { }
    }
}
