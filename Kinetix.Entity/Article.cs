using Kinetix.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinetix.Entity
{
    public class Article  : IEntity
    {

        public long Id { get; set; }
        public long EanCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
