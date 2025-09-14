using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Entities
{
    public class Currency
    {
        public int ID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameEn { get; set; }
        public int OrderNumber { get; set; }
        public bool IsActive { get; set; }
        public List<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();
        public List<Conversion> RecievedCurrencies { get; set; } = new List<Conversion>();
        public List<Conversion> SoldCurrencies { get; set; } = new List<Conversion>();
    }
}
