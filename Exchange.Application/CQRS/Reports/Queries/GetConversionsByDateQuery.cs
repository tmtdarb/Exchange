using AutoMapper;
using Exchange.Application.DTO;
using Exchange.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.CQRS.Reports.Queries
{
    public record GetConversionsByDateQuery(DateTime from,DateTime to):IRequest<List<ConversionModel>>;
    public class GetConversionsByDateQueryHandler : IRequestHandler<GetConversionsByDateQuery,List<ConversionModel>>
    {
        private readonly IConversionsRepository _conversionRepository;
        private readonly IMapper _mapper;
        public GetConversionsByDateQueryHandler(IConversionsRepository conversionRepository, IMapper mapper)
        {
            _conversionRepository = conversionRepository;
            _mapper = mapper;
        }

        public async Task<List<ConversionModel>> Handle(GetConversionsByDateQuery request, CancellationToken cancellationToken)
        {
            var result = await _conversionRepository.GetAllConversionsByDate(request.from, request.to);
            if (result == null)
                throw new InvalidOperationException("ამ თარიღებში კონვერტაციები არ მოიძებნა");
            return _mapper.Map<List<ConversionModel>>(result);
        }
    }
}
