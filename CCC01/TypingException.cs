using System;
using System.Runtime.Serialization;

namespace CCC01
{
    [Serializable]
    internal class TypingException : Exception
    {
        public TypingException()
        {
        }

        public TypingException(string message) : base(message)
        {
        }

        public TypingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}