using System;

namespace GlobalErrorHandling.Exceptions
{
    public class ItemNotFound : Exception
    {
        public ItemNotFound(string message) : base(message)
        {
        }
    }
}