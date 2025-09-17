using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class CreateCurrencyModel
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameEn { get; set; }
        public int OrderNumber { get; set; }
    }
}
