using Kinetix.DataAccess.Abstract;
using Kinetix.DataAccess.BaseRepository;
using Kinetix.DataAccess.Context;
using Kinetix.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinetix.DataAccess.Concrete
{
    public class StockDal : BaseRepository<Stock>, IStockDal
    {
        private readonly KinetixDbContext _context;

        public StockDal(KinetixDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
