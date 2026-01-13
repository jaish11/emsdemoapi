using emsdemoapi.Data.Entities;

namespace emsdemoapi.Data.Interfaces
{
    public interface ICountry
    {
        List<Country> GetAllCountry();
        Country GetCountryById(int id);
        bool AddCountry(Country country);
        bool DeleteCountry(int id);
        bool UpdateCountry(Country country);
    }
}
