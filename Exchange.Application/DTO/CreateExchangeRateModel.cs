using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class CreateExchangeRateModel
    {
        [Required(ErrorMessage = "ვალუტის მითითება აუცილებელია")]
        public int CurrencyID { get; set; }
        [Required(ErrorMessage = "ყიდვის კურსის მითითება აუცილებელია")]
        public decimal BuyRate { get; set; }
        [Required(ErrorMessage = "გაყიდვის კურსის მითითება აუცილებელია")]
        public decimal SellRate { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
