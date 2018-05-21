using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Learning.EntityFrameworkCore.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public string Remark { get; set; }
    }
}
