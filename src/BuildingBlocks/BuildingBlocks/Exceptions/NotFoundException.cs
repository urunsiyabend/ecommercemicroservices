﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }

        public NotFoundException(string name, object key, string message) : base($"Entity \"{name}\" ({key}) was not found. {message}")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
