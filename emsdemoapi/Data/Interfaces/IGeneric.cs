using emsdemoapi.Data.Entities;

namespace emsdemoapi.Data.Interfaces
{
    public interface IGeneric<T> where T :class
    {
      
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);   
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveAsync();


        //List<District> GetAllDistricts();
        //District GetDistrictById(int id);
        //public bool AddDistrcit(District district);
        //public bool UpdateDistrict(District district);
        //public bool DeleteDistrict(int id);

    }





    //public interface IGeneric<T> where T : class
    //{
    //    //**Task is return type which is used for asynchronous task (async await).
    //    Task<IEnumerable<T>> GetAllAsync();
    //    Task<T> GetByIdAsync(int id);
    //    Task AddAsync(T entity);
    //    Task UpdateAsync(T entity);
    //    Task DeleteAsync(int id);
    //    Task SaveAsync();

    //}
}
