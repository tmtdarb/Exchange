using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Entities
{
    public class ExchangeRate
    {
        public int ID { get; set; }
        // for GEL
        public int CurrencyID { get; set; }
        public Currency Currency { get; set; }
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        // rate set time
        public DateTimeOffset CreatedAt { get; set; }
    }
}
