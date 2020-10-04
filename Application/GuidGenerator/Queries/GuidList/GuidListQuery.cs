using MediatR;
using System;
using System.Collections.Generic;

namespace Application.GuidGenerator.Queries.GuidList
{
    public class GuidListQuery : IRequest<List<Guid>>
    {
        public int GuidNumbers { get; set; }
    }
}
