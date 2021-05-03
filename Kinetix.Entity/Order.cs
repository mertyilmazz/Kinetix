using Kinetix.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinetix.Entity
{
    public class Order : IEntity
    {   
        public string FileType { get; set; }
       
        public long Id { get; set; }
        public string OrderDate { get; set; }
        public long BuyerCode { get; set; }
        public long SupplierCode { get; set; }
        public string FreeTextComment { get; set; } 
        public List<Article> Articles { get; set; }
    }
}
