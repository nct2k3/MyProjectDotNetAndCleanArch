using Application.Common.Interfaces.Service;

namespace Infrastructure.Common.Service;

public class DateTimeProvider:IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
}