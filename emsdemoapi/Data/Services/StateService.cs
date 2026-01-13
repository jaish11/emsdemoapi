using Dapper;
using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace emsdemoapi.Data.Services
{
    public class StateService : IState
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public StateService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlCon");
        }
        public bool AddState(State state)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", state.Name);
                parameters.Add("CountryId", state.CountryId);

                connection.Execute("sp_AddStates", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public bool DeleteState(int id)
        {
           using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                connection.Execute("sp_DeleteStateByID", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }

        public List<State> GetAllState()
        {
            //sp_GetStates
            using(var connection = new SqlConnection(_connectionString))
            {
                var states = connection.Query<State>("sp_GetStates", commandType: CommandType.StoredProcedure).ToList();
                return states;
            }
        }

        public State GetStateById(int id)
        {
            using(var connection =  new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var state = connection.QueryFirstOrDefault<State>("sp_GetStateBgyId", parameters, commandType: CommandType.StoredProcedure);
                return state;
            }
        }

        public bool UpdateState(State state)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id",state.Id);
                parameters.Add("@Name", state.Name);
                parameters.Add("@CountryId", state.CountryId);

                connection.Execute("sp_UpdateStates", parameters, commandType: CommandType.StoredProcedure);
                return true;
            }
        }
    }
}
