using Zeta.CodebaseExpress.Application.Services.DateAndTime;

namespace Zeta.CodebaseExpress.Infrastructure.DateAndTime;

public class DateAndTimeService : IDateAndTimeService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
