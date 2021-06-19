using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalErrorHandling.Exceptions
{
    public class ItemNotFound : Exception
    {
        public ItemNotFound(string message) : base(message)
        {
        }
    }
}
