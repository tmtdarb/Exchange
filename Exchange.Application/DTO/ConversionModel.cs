using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class ConversionModel
    {
        public int ID { get; set; }
        public int RecievedCurrencyID { get; set; }
        public CurrencyModel RecievedCurrency { get; set; }
        public int SoldCurrencyID { get; set; }
        public CurrencyModel SoldCurrency { get; set; }
        public decimal AmountToBuy { get; set; }
        public decimal AmountToSell { get; set; }
        public DateTimeOffset ConversionDate { get; set; }
        public string Comment { get; set; }
    }
}
