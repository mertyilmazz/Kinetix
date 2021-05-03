using System;
using System.Collections.Generic;
using System.Text;

namespace Kinetix.Dto
{
    public class OrderModelDto
    {
        public OrderModelDto()
        {
            Articles = new List<ArticleModelDto>();
        }

        public string FileType { get; set; }
        public long Id { get; set; }
        public string OrderDate { get; set; }
        public long BuyerCode { get; set; }
        public long SupplierCode { get; set; }
        public string FreeTextComment { get; set; }
        public List<ArticleModelDto> Articles { get; set; }
    }
}
