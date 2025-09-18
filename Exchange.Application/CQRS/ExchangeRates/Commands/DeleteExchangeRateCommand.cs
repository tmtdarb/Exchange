using Exchange.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.CQRS.ExchangeRates.Commands
{
    public record DeleteExchangeRateCommand(int id) : IRequest<Unit>;
    public class DeleteExchangeRateCommandHandler : IRequestHandler<DeleteExchangeRateCommand, Unit>
    {
        private readonly IExchangeRatesRepository _repository;
        public DeleteExchangeRateCommandHandler(IExchangeRatesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(DeleteExchangeRateCommand request, CancellationToken cancellationToken)
        {
            var er = await _repository.GetExchangeRateByCurrencyId(request.id);
            if (er == null)
                throw new InvalidOperationException("ვალუტის კურსი ასეთი აიდით ვერ მოიძებნა");
            await _repository.DeleteExchangeRateById(request.id);
            await _repository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
