using AutoMapper;
using Exchange.Application.DTO;
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
    public record UpdateCurrencyCommand(CurrencyModel model) : IRequest<Unit>;
    public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Unit>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;
        public UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var modelToUpdate = await _currencyRepository.GetCurrencyById(request.model.ID);
            modelToUpdate.CurrencyName = request.model.CurrencyName;
            modelToUpdate.CurrencyNameEn = request.model.CurrencyNameEn;
            modelToUpdate.CurrencyCode = request.model.CurrencyCode;
            modelToUpdate.OrderNumber = request.model.OrderNumber;
            modelToUpdate.IsActive = request.model.IsActive;
            await _currencyRepository.UpdateCurrency(modelToUpdate);
            await _currencyRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
