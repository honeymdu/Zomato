﻿namespace Zomato.Exceptions.CustomExceptionHandler
{
    public class ResourceNotFoundException:Exception
    {
        public ResourceNotFoundException(string message) : base(message) { }
    }
}
}
