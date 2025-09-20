using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DTO
{
    public class CreateCurrencyModel
    {
        [Required(ErrorMessage = "ვალუტის კოდი აუცილებელია")]
        [MaxLength(3, ErrorMessage = "ვალუტის კოდი უნდა იყოს 3 სიმბოლოსგან შემდგარი")]
        public string CurrencyCode { get; set; }
        [Required(ErrorMessage = "ვალუტის სახელი აუცილებელია")]
        public string CurrencyName { get; set; }
        [Required(ErrorMessage = "ვალუტის სახელი (ინგლისურად) აუცილებელია")]
        public string CurrencyNameEn { get; set; }
        public int OrderNumber { get; set; }
    }
}
