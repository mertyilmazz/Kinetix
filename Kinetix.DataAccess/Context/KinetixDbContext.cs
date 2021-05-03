using Kinetix.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kinetix.DataAccess.Context
{
    public class KinetixDbContext : DbContext
    {
        public KinetixDbContext(DbContextOptions options) : base(options)
        {
            if (!Articles.Any())
            {
                LoadArticle();
            }
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Stock> Stocks { get; set; }


        public void LoadArticle()
        {
            var articleList = new List<Article>();

            Article art = new Article
            {
                Description = "Computer Monitor",
                UnitPrice = 12335,
                Quantity = 100,
                EanCode = 8712345678906,
               
            };
            articleList.Add(art);

            Article art1 = new Article
            {
                Description = "Keyboard",
                UnitPrice = 56,
                Quantity = 200,
                EanCode = 8712345678913,
              
            };
            articleList.Add(art1);

            Article art2 = new Article
            {
                Description = "Mouse",
                UnitPrice = 1290,
                Quantity = 500,
                EanCode = 8712345678920              
            };
            articleList.Add(art2);

            Articles.AddRange(articleList);
            this.SaveChanges();       
        }
    }
}
