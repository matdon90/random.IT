using MediatR;
using System;

namespace Application.GuidGenerator.Queries.GuidSingle
{
    public class GuidSingleQuery : IRequest<string> 
    {
        public bool IsUppercase { get; set; }
        public GuidSingleQuery(bool isUppercase)
        {
            IsUppercase = isUppercase;
        }
    }
}
