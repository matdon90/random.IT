using Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GuidGenerator.Queries.GuidSingle
{
    public class GuidSingleQueryHandler : IRequestHandler<GuidSingleQuery, Guid>
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IDateTime _dateTime;

        public GuidSingleQueryHandler(IGuidGenerator guidGenerator, IDateTime dateTime)
        {
            _guidGenerator = guidGenerator;
            _dateTime = dateTime;
        }

        public Task<Guid> Handle(GuidSingleQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_guidGenerator.GuidGenerateSingle(_dateTime.Now));
        }
    }
}
