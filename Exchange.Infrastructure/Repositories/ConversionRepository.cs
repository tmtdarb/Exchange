using Exchange.Domain.Interfaces;
using Exchange.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Infrastructure.Repositories
{
    public class ConversionRepository : IConversionsRepository
    {
        private readonly ExchangeDbContext _db;
        public ConversionRepository(ExchangeDbContext db)
        {
            _db = db;
        }
    }
}
