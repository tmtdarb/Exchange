using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class CurrencyModel
    {
        public int ID { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameEn { get; set; }
        public int OrderNumber { get; set; }
        public bool IsActive { get; set; }
        public List<ExchangeRateModel> ExchangeRates { get; set; } = new List<ExchangeRateModel>();
        public List<ConversionModel> RecievedCurrencies { get; set; } = new List<ConversionModel>();
        public List<ConversionModel> SoldCurrencies { get; set; } = new List<ConversionModel>();
    }
}
