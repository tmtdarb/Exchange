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
    public record GetSuspiciousConversionsByDateQuery(DateTime from, DateTime to) : IRequest<List<ConversionModel>>;
    public class GetSuspiciousConversionsByDateQueryHandler : IRequestHandler<GetSuspiciousConversionsByDateQuery, List<ConversionModel>>
    {
        private readonly IConversionsRepository _conversionRepository;
        private readonly IMapper _mapper;
        public GetSuspiciousConversionsByDateQueryHandler(IConversionsRepository conversionRepository, IMapper mapper)
        {
            _conversionRepository = conversionRepository;
            _mapper = mapper;
        }
        public async Task<List<ConversionModel>> Handle(GetSuspiciousConversionsByDateQuery request, CancellationToken cancellationToken)
        {
            var result = await _conversionRepository.GetAllSuspiciousConversions(request.from, request.to);
            if (result == null)
                throw new InvalidOperationException("მოცემულ პერიოდში საეჭვო ტრანზაქციები არ მოიძებნა");
            return _mapper.Map<List<ConversionModel>>(result);
        }
    }
}
