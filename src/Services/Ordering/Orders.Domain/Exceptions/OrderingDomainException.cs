using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domain.Exceptions
{
    [Serializable]
    public class OrderingDomainException : System.Exception
    {
        public OrderingDomainException() { }

        public OrderingDomainException(string message) : base(message) { }

        public OrderingDomainException(string message, System.Exception innerException) : base(message, innerException) { }

        public OrderingDomainException(int code, string message) : base(message)
        {
            Code = code;
        }

        public OrderingDomainException(int code, string message, System.Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        protected OrderingDomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public int Code { get; private set; }
    }
}
