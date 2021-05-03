using AutoMapper;
using Kinetix.Dto;
using Kinetix.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinetix.Business.Mapping
{
    public class Mappings :Profile
    {
        public Mappings()
        {
            CreateMap<Order, OrderModelDto>();
            CreateMap<OrderModelDto, Order>();
            CreateMap<Article, ArticleModelDto>();
            CreateMap<ArticleModelDto, Article>();
            CreateMap<List<ArticleModelDto>, List<Article>>();
        }
    }
}
