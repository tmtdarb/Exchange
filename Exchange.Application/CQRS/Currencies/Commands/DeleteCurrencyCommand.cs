using Exchange.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.CQRS.Currencies.Commands
{
    public record DeleteCurrencyCommand(int id) : IRequest<Unit>;
    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, Unit>
    {
        private readonly ICurrencyRepository _repo;
        public DeleteCurrencyCommandHandler(ICurrencyRepository repo)
        {
            _repo = repo;
        }
        public async Task<Unit> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currToDelete = await _repo.GetCurrencyById(request.id);
            if (currToDelete == null)
                throw new InvalidOperationException("ვალუტა ამ კოდით ვერ მოიძებნა");
            currToDelete.IsActive = false;
            await _repo.UpdateCurrency(currToDelete);
            await _repo.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
