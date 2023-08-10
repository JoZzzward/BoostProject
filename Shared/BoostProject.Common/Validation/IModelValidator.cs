namespace BoostProject.Common.Validation;

public interface IModelValidator<T> where T : class
{
    Task CheckValidation(T model);
}