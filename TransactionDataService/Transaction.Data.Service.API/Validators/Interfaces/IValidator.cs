using Transaction.Data.Service.API.Validators.Models;

namespace Transaction.Data.Service.API.Validators.Interfaces
{
    public interface IValidator<T>
    {
        public ValidationResult Validate(T request);
    }
}
