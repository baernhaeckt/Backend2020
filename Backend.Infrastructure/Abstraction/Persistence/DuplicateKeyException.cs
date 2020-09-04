﻿using System;

namespace Backend.Infrastructure.Abstraction.Persistence
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException()
        {
        }

        public DuplicateKeyException(string message)
            : base(message)
        {
        }

        public DuplicateKeyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}