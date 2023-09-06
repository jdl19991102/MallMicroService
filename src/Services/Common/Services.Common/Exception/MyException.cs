using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Exception
{
    public class MyException : System.Exception
    {
        public MyException() { }

        public MyException(string message) : base(message) { }

        public MyException(string message, System.Exception innerException) : base(message, innerException) { }

        public MyException(int code, string message) : base(message)
        {
            Code = code;
        }

        public MyException(int code, string message, System.Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        protected MyException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public int Code { get; set; }
    }
}
