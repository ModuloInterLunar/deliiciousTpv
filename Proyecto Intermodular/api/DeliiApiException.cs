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

    public class UserNotFoundException : DeliiApiException
    {
        public UserNotFoundException() : base() { }
        public UserNotFoundException(string message) : base(message) { }
        protected UserNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
