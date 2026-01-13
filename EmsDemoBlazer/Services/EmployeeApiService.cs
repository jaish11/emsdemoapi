using EmsDemoBlazer.Models;

namespace EmsDemoBlazer.Services
{
    public class EmployeeApiService
    {
        private readonly HttpClient _http;
        public EmployeeApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _http.GetFromJsonAsync<List<Employee>>("api/Employees");
        }
        public async Task<Employee> GetById(int id)
        {
            return await _http.GetFromJsonAsync<Employee>($"api/Employees/{id}");
        }
        /*
         //in this when in post API we are returning employee then we can use this method 
        public async Task<Employee> Create(Employee employee)
        {
            var response = await _http.PostAsJsonAsync("api/employee", employee);
            return await response.Content.ReadFromJsonAsync<Employee>();
        }*/

        public async Task Add(Employee employee)
        {
            await _http.PostAsJsonAsync("api/Employees", employee);
        }

        public async Task Update(Employee employee)
        {
            await _http.PutAsJsonAsync($"api/Employees", employee);
        }
        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"api/Employees/{id}");
        }
    }
}
