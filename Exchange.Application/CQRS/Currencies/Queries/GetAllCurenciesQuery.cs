using AutoMapper;
using Exchange.Application.DTO;
using Exchange.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.CQRS.Currencies.Queries
{
    public record GetAllActiveCurenciesQuery : IRequest<List<CurrencyModel>>;
    public class GetAllActiveCurenciesQueryHandler : IRequestHandler<GetAllActiveCurenciesQuery, List<CurrencyModel>>
    {
        private readonly ICurrencyRepository _repository;
        private readonly IMapper _mapper;
        public GetAllActiveCurenciesQueryHandler(ICurrencyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CurrencyModel>> Handle(GetAllActiveCurenciesQuery request, CancellationToken cancellationToken)
        {
            var activeCurrencies = await _repository.GetAllActiveCurrencies();
            return _mapper.Map<List<CurrencyModel>>(activeCurrencies);
        }
    }
}
