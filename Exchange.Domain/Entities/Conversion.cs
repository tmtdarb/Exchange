using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Entities
{
    public class Conversion
    {
        public int ID { get; set; }
        public int RecievedCurrencyID { get; set; }
        public Currency RecievedCurrency { get; set; }
        public int SoldCurrencyID { get; set; }
        public Currency SoldCurrency { get; set; }
        public decimal AmountToBuy { get; set; }
        public decimal AmountToSell { get; set; }
        // conversion done
        public decimal AmountToSellInGel { get; set; }
        public DateTimeOffset ConversionDate { get; set; }
        public string Comment { get; set; }

    }
}
