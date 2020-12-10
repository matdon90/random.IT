using Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.GuidGenerator.Queries.GuidSingle
{
    public class GuidSingleQueryHandler : IRequestHandler<GuidSingleQuery, string>
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IDateTime _dateTime;

        public GuidSingleQueryHandler(IGuidGenerator guidGenerator, IDateTime dateTime)
        {
            _guidGenerator = guidGenerator;
            _dateTime = dateTime;
        }

        public Task<string> Handle(GuidSingleQuery request, CancellationToken cancellationToken)
        {
            var response = request.IsUppercase 
                ? 
                _guidGenerator.GuidGenerateSingle(_dateTime.Now).ToString().ToUpper() 
                :
                _guidGenerator.GuidGenerateSingle(_dateTime.Now).ToString();

            return Task.FromResult(response);
        }
    }
}
