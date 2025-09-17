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
    public record CreateCurrencyCommand(CreateCurrencyModel model) : IRequest<CurrencyModel>;

    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, CurrencyModel>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;
        public CreateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }
        public async Task<CurrencyModel> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var exist = await _currencyRepository.GetCurrencyByCode(request.model.CurrencyCode);
            if(exist !=null)
                throw new InvalidOperationException("ვალუტა ამ კოდით უკვე არსებობს!");

            var model = _mapper.Map<Currency>(request.model);
            await _currencyRepository.CreateCurrency(model);
            await _currencyRepository.SaveChangesAsync();
            return _mapper.Map<CurrencyModel>(model);
        }
    }
}
