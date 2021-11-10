using System;
using System.Runtime.Serialization;

namespace JustIoC
{
    public class JustException : Exception
    {
        public JustException()
        {
        }

        public JustException(string message) : base(message)
        {
        }

        public JustException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected JustException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
