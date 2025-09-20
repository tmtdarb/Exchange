using AutoMapper;
using Exchange.Application.DTO;
using Exchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CurrencyModel, Currency>().ForMember(a=>a.ID,opt=>opt.Ignore());
            CreateMap<Currency, CurrencyModel>();

            CreateMap<CreateCurrencyModel, Currency>().ForMember(a => a.ID, opt => opt.Ignore());
            CreateMap<Currency, CreateCurrencyModel>();

            CreateMap<CreateExchangeRateModel, ExchangeRate>();
            CreateMap<ExchangeRate, ExchangeRateModel>();

            CreateMap<CreateConversionModel, Conversion>().ForMember(a => a.ID, opt => opt.Ignore());
            CreateMap<Conversion, ConversionModel>();
        }
    }
}
