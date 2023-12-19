﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Domain.Entities
{
    public class Product:BaseNameableEntity
    {
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string? Description { get; set; }
        //Relational Props
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductColor>? ProductColors { get; set; }

    }
}