using System;
using System.Collections.Generic;
using System.Text;

namespace Kinetix.Dto
{
    public class ArticleModelDto
    {
        public int Id { get; set; }
        public long EanCode { get; set; }
        public string Description { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
