using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class CalculateExchangeRateRequestModel
    {
        [Required(ErrorMessage = "მისაღები ვალუტის მითითება აუცილებელია")]
        public string CurrencyCodeUserWantToGet { get; set; }
        [Required(ErrorMessage = "გასაყიდი ვალუტის მითითება აუცილებელია")]
        public string CurrencyCodeUserWantToSell { get; set; }
    }
}
