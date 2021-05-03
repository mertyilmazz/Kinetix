using Kinetix.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinetix.Entity
{
    public class Stock : IEntity
    {
        public int Id { get; set; }
        public long EanCode { get; set; }
        public int Quantity { get; set; }
    }
}
