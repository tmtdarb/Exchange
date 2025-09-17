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
    public record UpdateCurrencyCommand(CreateCurrencyModel model, int id) : IRequest<CurrencyModel>;
    public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, CurrencyModel>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;
        public UpdateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }
        public async Task<CurrencyModel> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var modelToUpdate = await _currencyRepository.GetCurrencyById(request.id);
            if (modelToUpdate is null)
                throw new InvalidOperationException("ვალუტა ამ მონაცემებით არ არსებობს");
            modelToUpdate.CurrencyName = request.model.CurrencyName;
            modelToUpdate.CurrencyNameEn = request.model.CurrencyNameEn;
            modelToUpdate.CurrencyCode = request.model.CurrencyCode;
            modelToUpdate.OrderNumber = request.model.OrderNumber;
            await _currencyRepository.UpdateCurrency(modelToUpdate);
            await _currencyRepository.SaveChangesAsync();
            return _mapper.Map<CurrencyModel>(modelToUpdate);
        }
    }
}
