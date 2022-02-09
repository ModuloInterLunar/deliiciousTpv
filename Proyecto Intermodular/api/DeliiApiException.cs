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

    public class ItemNotFoundException : DeliiApiException
    {
        public ItemNotFoundException() : base() { }
        public ItemNotFoundException(string message) : base(message) { }
        protected ItemNotFoundException(System.Runtime.Serialization.SerializationInfo info,
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

    public class NotEnoughStockException : DeliiApiException
    {
        public NotEnoughStockException() : base() { }
        public NotEnoughStockException(string message) : base(message) { }
        protected NotEnoughStockException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
