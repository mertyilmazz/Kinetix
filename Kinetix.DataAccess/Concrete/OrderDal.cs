using Kinetix.DataAccess.Abstract;
using Kinetix.DataAccess.BaseRepository;
using Kinetix.DataAccess.Context;
using Kinetix.Entity;
using System.Threading.Tasks;

namespace Kinetix.DataAccess.Concrete
{
    public class OrderDal : BaseRepository<Order>, IOrderDal
    {
        private readonly KinetixDbContext _context;
        private readonly IArticleDal _articleDal;

        public OrderDal(KinetixDbContext context, IArticleDal articleDal) : base(context)
        {
            _context = context;
            _articleDal = articleDal;
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);

            var articles = order.Articles;

            foreach (var item in articles)
            {
                var article = await _articleDal.GetAsync(f => f.EanCode == item.EanCode);
                article.Quantity -= item.Quantity;
            }
            await _context.SaveChangesAsync();
        }
    }
}
