using AutoMapper;
using Exchange.Application.DTO;
using Exchange.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.CQRS.Conversions.Queries
{
    public record GetAllConversionsQuery : IRequest<List<ConversionModel>>;
    public class GetAllConversionsQueryHandler : IRequestHandler<GetAllConversionsQuery, List<ConversionModel>>
    {
        private readonly IConversionsRepository _repository;
        private readonly IMapper _mapper;
        public GetAllConversionsQueryHandler(IConversionsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<ConversionModel>> Handle(GetAllConversionsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllConversions();
            if (result == null)
                throw new InvalidOperationException("კონვერტაციები ვერ მოიძებნა");
            return _mapper.Map<List<ConversionModel>>(result);
        }
    }
}
