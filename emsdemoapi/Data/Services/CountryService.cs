using Dapper;
using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace emsdemoapi.Data.Services
{
    public class CountryService : ICountry
    {
        //declare
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CountryService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlCon");
        }

        public bool AddCountry(Country country)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", country.Name);

                connection.Execute("sp_AddCountry", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public bool DeleteCountry(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                connection.Execute("sp_deleteCountrybyId", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public List<Country> GetAllCountry()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var countries = connection.Query<Country>("sp_GetCountries", commandType: CommandType.StoredProcedure).ToList();
                return countries;
            }
        }

        public Country GetCountryById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var country = connection.QueryFirstOrDefault<Country>("sp_GetCountryById", new { Id = id },commandType: CommandType.StoredProcedure);
                return country;


            }
        }

        public bool UpdateCountry(Country country)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", country.Id);
                parameters.Add("@Name", country.Name);

                connection.Execute("sp_UpdateCountry", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
    }
}
