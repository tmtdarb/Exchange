using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class ExchangeRateModel
    {
        public int ID { get; set; }
        public int CurrencyID { get; set; }
        public CurrencyModel Currency { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
