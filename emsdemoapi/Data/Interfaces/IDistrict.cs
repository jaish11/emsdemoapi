using emsdemoapi.Data.Entities;

namespace emsdemoapi.Data.Interfaces
{
    public interface IDistrict
    {
        List<District> GetAllDistricts();
        District GetDistrictById(int id);
        public bool AddDistrcit(District district);
        public bool UpdateDistrict(District district);
        public bool DeleteDistrict(int id);
    }
}
