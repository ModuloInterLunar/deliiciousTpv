using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Intermodular
{
    public class DeliiApiException : Exception
    {
        public DeliiApiException() : base() { }
        public DeliiApiException(string message) : base(message) { }

        protected DeliiApiException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base (info, context) { }

    }
}
