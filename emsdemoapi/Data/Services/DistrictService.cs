using Dapper;
using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace emsdemoapi.Data.Services
{
    public class DistrictService:IDistrict
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DistrictService(IConfiguration configuration) { 
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlCon");
        }

        public bool AddDistrcit(District district)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name",district.Name);
                parameters.Add("@StateId", district.StateId);
                parameters.Add("@CountryId", district.CountryId);

                connection.Execute("sp_AddDistricts", parameters, commandType: CommandType.StoredProcedure);

                return true;
            }

        }

        public bool DeleteDistrict(int id)
        {
           using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                connection.Execute("sp_DeleteDistrictsByID",parameters,commandType:CommandType.StoredProcedure);
                return true;
            }
        }

        public List<District> GetAllDistricts()
        {
            using(var connection =  new SqlConnection(_connectionString))
            {
                var states = connection.Query<District>("sp_GetDistricts", commandType: CommandType.StoredProcedure).ToList();
                return states;
            }
        }

        public District GetDistrictById(int id)
        {
           using(var connection =  new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var state = connection.QueryFirstOrDefault<District>("sp_GetDistrictById",parameters,commandType: CommandType.StoredProcedure);
                return state;
            }
        }

        public bool UpdateDistrict(District district)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", district.Id);
                parameters.Add("@Name", district.Name);
                parameters.Add("@CountryId", district.CountryId);
                parameters.Add("@StateId", district.StateId);

                connection.Execute("sp_UpdateDistricts", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
    }
}
