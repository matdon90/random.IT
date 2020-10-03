using System;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IGuidGenerator
    {
        Guid GuidGenerateSingle(DateTime date);
        IEnumerable<Guid> GuidGenerateMultiple(DateTime date, int numberOfGuid);
    }
}
