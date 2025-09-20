using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class CreateConversionModel
    {
        [Required(ErrorMessage = "მისაღები ვალუტის მითითება აუცილებელია")]
        public string RecievedCurrencyCode { get; set; }
        [Required(ErrorMessage = "გასაყიდი ვალუტის მითითება აუცილებელია")]
        public string SoldCurrencyCode { get; set; }
        [Required(ErrorMessage = "მისაღები თანხის მითითება აუცილებელია")]
        public decimal AmountToBuy { get; set; }
        [Required(ErrorMessage = "გასაყიდი თანხის მითითება აუცილებელია")]
        public decimal AmountToSell { get; set; }
        public string Comment { get; set; }
    }
}
