using Domain.Common.Entities;

namespace Domain.Common.Ports;

public interface IValidatorEntity<T>
{
    public void Validate(T entity);
}
