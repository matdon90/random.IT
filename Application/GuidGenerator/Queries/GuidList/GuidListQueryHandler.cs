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

        public GuidListQueryHandler(IGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }
        public Task<List<Guid>> Handle(GuidListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_guidGenerator.GuidGenerateMultiple(request.GuidNumbers).ToList());
        }
    }
}
