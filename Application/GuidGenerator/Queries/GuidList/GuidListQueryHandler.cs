using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GuidGenerator.Queries.GuidList
{
    public class GuidListQueryHandler : IRequestHandler<GuidListQuery, List<Guid>>
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IDateTime _dateTime;

        public GuidListQueryHandler(IGuidGenerator guidGenerator, IDateTime dateTime)
        {
            _guidGenerator = guidGenerator;
            _dateTime = dateTime;
        }
        public Task<List<Guid>> Handle(GuidListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_guidGenerator.GuidGenerateMultiple(_dateTime.Now,request.GuidNumbers).ToList());
        }
    }
}
