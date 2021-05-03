using Kinetix.DataAccess.Abstract;
using Kinetix.DataAccess.BaseRepository;
using Kinetix.DataAccess.Context;
using Kinetix.Entity;

namespace Kinetix.DataAccess.Concrete
{
    public class ArticleDal : BaseRepository<Article>, IArticleDal
    {
        private readonly KinetixDbContext _context;

        public ArticleDal(KinetixDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
