using Domain.Common.Ports;

namespace Domain.Common.Entities;

public abstract class Entity
{
    public int Id { get; private set; }
    public bool Valid { get; private set; }
    public bool Invalid => !Valid;

    public Dictionary<string, string> Errors = new();

    public void AddError(string key, string message)
    {
        Errors.Add(key, message);
    }

    public Dictionary<string, string> GetErrors()
    {
        return Errors;
    }

    public bool HasErrors()
    {
        return Errors.Count > 0;
    }

    public bool Validar<TModel>(TModel model, IValidatorEntity<TModel> validador)
    {
        validador.Validate(model);
        return this.Valid = !this.HasErrors();
    }
}