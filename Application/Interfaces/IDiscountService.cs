namespace Application.Interfaces
{
    public interface IDiscountService
    {
        Task<int> GetDiscountAsync(int productId);
    }
}
