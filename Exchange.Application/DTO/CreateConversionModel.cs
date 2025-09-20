using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class CreateConversionModel
    {
        public string RecievedCurrencyCode { get; set; }
        public string SoldCurrencyCode { get; set; }
        public decimal AmountToBuy { get; set; }
        public decimal AmountToSell { get; set; }
        public string Comment { get; set; }
    }
}
