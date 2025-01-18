namespace Shipping.Application.Contracts
{
    public interface ICommonService<T, TI, TU>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(Guid id);
        Task<T> Add(TI insertDto);
        Task<T> Update(TU updateDto);
        Task<T> Delete(Guid id);
    }
}
