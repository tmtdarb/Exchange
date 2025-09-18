using AutoMapper;
using Exchange.Application.DTO;
using Exchange.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.CQRS.ExchangeRates.Queries
{
    public record GetAllLatestExchangeRateQuery() : IRequest<List<ExchangeRateModel>>;
    public class GetAllExchangeRateQueryHandler : IRequestHandler<GetAllLatestExchangeRateQuery, List<ExchangeRateModel>>
    {
        private readonly IExchangeRatesRepository _exchangeRatesRepository;
        private readonly IMapper _mapper;
        public GetAllExchangeRateQueryHandler(IExchangeRatesRepository exchangeRatesRepository, IMapper mapper)
        {
            _exchangeRatesRepository = exchangeRatesRepository;
            _mapper = mapper;
        }
        public async Task<List<ExchangeRateModel>> Handle(GetAllLatestExchangeRateQuery request, CancellationToken cancellationToken)
        {
            var result = await _exchangeRatesRepository.GetAllLatestExchangeRates();
            if (result is null)
                throw new InvalidOperationException("არ მოიძებნა ვალუტის კურსები");
            return _mapper.Map<List<ExchangeRateModel>>(result);
        }
    }
}
