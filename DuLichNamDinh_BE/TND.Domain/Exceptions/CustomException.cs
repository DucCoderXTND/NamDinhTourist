﻿namespace TND.Domain.Exceptions
{
    public class CustomException(string message) : Exception(message)
    {
        public virtual string Title => "Exception";
    }
}
