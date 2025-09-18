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
    public record GetExchangeRateByIdQuery(int id) : IRequest<ExchangeRateModel>;
    public class GetExchangeRateByIdQueryHandler : IRequestHandler<GetExchangeRateByIdQuery, ExchangeRateModel>
    {
        private readonly IExchangeRatesRepository _exchangeRatesRepository;
        private readonly IMapper _mapper;
        public GetExchangeRateByIdQueryHandler(IExchangeRatesRepository exchangeRatesRepository, IMapper mapper)
        {
            _exchangeRatesRepository = exchangeRatesRepository;
            _mapper = mapper;
        }
        public async Task<ExchangeRateModel> Handle(GetExchangeRateByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _exchangeRatesRepository.GetExchangeRateById(request.id);
            if (result is null)
                throw new InvalidOperationException("ვალუტის კურსი ამ აიდით ვერ მოიძებნა");
            return _mapper.Map<ExchangeRateModel>(result);
        }
    }
}
