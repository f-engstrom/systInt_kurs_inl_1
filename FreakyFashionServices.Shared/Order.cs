﻿using System.Collections.Generic;
using FreakyFashionServices.Catalog.Data.Models;

namespace FreakyFashionServices.Shared
{
    public class RabbitOrderMessage
    {
        public string CustomIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}