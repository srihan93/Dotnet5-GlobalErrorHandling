using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalErrorHandling.Exceptions
{
    public class CustomBadRequest : Exception
    {
        public CustomBadRequest(string message) : base(message)
        {
        }
    }
}
