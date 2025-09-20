using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class CalculateAmountModel
    {
        public decimal AmountToGet { get; set; }
        public decimal AmountToSell { get; set; }
        public string CurrencyCodeUserWantToGet { get; set; }
        public string CurrencyCodeUserWantToSell { get; set; }
    }
}
