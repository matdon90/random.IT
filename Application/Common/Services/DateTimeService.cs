using Application.Common.Interfaces;
using System;

namespace Application.Common.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
