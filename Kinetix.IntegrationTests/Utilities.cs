using Kinetix.DataAccess.Context;
using Kinetix.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinetix.IntegrationTests
{
    public class Utilities
    {
        public static void InitializeDbForTests(KinetixDbContext db)
        {

            db.Articles.AddRange(GetArticles());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(KinetixDbContext db)
        {
            db.Articles.RemoveRange(GetArticles());
            InitializeDbForTests(db);
        }

        public static List<Article> GetArticles()
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

            return articleList;

        }
    }
}
