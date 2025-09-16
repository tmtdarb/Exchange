using AutoMapper;
using Exchange.Application.CQRS.Currencies.Commands;
using Exchange.Application.DTO;
using Exchange.Application.Mapping;
using Exchange.Domain.Entities;
using Exchange.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.CQRS.Currencies.Commands
{
    public record CreateCurrencyCommand(CurrencyModel model) : IRequest<Unit>;

    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Unit>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;
        public CreateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Currency>(request.model);
            await _currencyRepository.CreateCurrency(model);
            await _currencyRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
