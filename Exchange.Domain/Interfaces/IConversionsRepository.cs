using Exchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Interfaces
{
    public interface IConversionsRepository
    {
        Task AddConversion(Conversion model);
        Task<List<Conversion>> GetAllConversions();
        Task<List<Conversion>> GetAllConversionsByDate(DateTime from, DateTime to);
        Task<List<Conversion>> GetAllSuspiciousConversions(DateTime from, DateTime to);
        Task<Conversion> GetConversionByID(int id);
        Task SaveChanges();
    }
}
