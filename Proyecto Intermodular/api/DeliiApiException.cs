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

    public class WrongCredentialsException : DeliiApiException
    {
        public WrongCredentialsException() : base() { }
        public WrongCredentialsException(string message) : base(message) { }
        protected WrongCredentialsException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class AlreadyInUseException : DeliiApiException
    {
        public AlreadyInUseException() : base() { }
        public AlreadyInUseException(string message) : base(message) { }
        protected AlreadyInUseException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
