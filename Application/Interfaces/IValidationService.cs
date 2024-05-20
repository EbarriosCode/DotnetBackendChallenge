namespace Application.Interfaces
{
    public interface IValidationService<T>
    {
        void Validate(T entity);
    }
}
