using Application.Common.Interfaces.Service;
namespace Infrastructure.Service;

public class DataTimeProvider:IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

}
