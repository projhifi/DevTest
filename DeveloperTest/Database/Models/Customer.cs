﻿using System;
using System.Collections.Generic;

namespace DeveloperTest.Database.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerType { get; set; }

        public List<Job> Jobs { get; set; }
    }
}
