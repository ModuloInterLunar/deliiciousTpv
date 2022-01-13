using System;

namespace Proyecto_Intermodular.api
{
    public class DeliiApiException : Exception
    {
        public DeliiApiException() : base() { }
        public DeliiApiException(string message) : base(message) { }
        protected DeliiApiException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base (info, context) { }
    }

    public class InvalidCredentialsException : DeliiApiException
    {
        public InvalidCredentialsException() : base() { }
        public InvalidCredentialsException(string message) : base(message) { }
        protected InvalidCredentialsException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
