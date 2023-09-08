using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class EmailException : Exception
    {
        public EmailException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
    }
}
