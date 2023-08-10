using FluentValidation;

namespace BoostProject.Common.Validation
{
    public class ModelValidator<T> : IModelValidator<T> where T : class
    {
        private readonly IValidator<T> _validator;

        public ModelValidator(
                IValidator<T> validator
            )
        {
            _validator = validator;
        }

        public async Task CheckValidation(T model)
        {
            var result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}
