using MediatR;
using System;
using System.Collections.Generic;

namespace Application.GuidGenerator.Queries.GuidList
{
    public class GuidListQuery : IRequest<List<Guid>>
    {
        public GuidListQuery()
        {

        }
        public GuidListQuery(int numberOfGuids)
        {
            GuidNumbers = numberOfGuids;
        }
        public int GuidNumbers { get; set; }
    }
}
