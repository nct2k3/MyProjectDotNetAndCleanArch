namespace Application.Common.Interfaces.Service;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    
}