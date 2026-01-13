using EmsDemoBlazer.Models;

namespace EmsDemoBlazer.Services
{
    public class StudentApiService
    {
        private readonly HttpClient _http;
        public StudentApiService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<Student>> GetAll()
        {
            return await _http.GetFromJsonAsync<List<Student>>("api/Students");
        }
        public async Task<Student> GetById(int id)
        {
            return await _http.GetFromJsonAsync<Student>($"api/Students/{id}");
        }
        public async Task Add(Student student)
        {
            await _http.PostAsJsonAsync("api/Students", student);
        }
        public async Task Update(Student student)
        {
            await _http.PutAsJsonAsync("api/Students", student);
        }
        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"api/Students/{id}");
        }
    }

}
