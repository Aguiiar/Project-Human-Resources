using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DTOs;
using EmployeeApi.Application.Interfaces;




namespace EmployeeApi.Infrastructure.Http
{
    public class DepartmentClient : IDepartmentClient
    {
        private readonly HttpClient _httpClient;

        public DepartmentClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DepartmentExists(Guid departmentId)
        {
            var response = await _httpClient.GetAsync($"/api/department/{departmentId}");

            return response.IsSuccessStatusCode;
        }



        public async Task<DepartmentDto> GetDepartmentById(Guid departmentId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/department/{departmentId}");
                if (!response.IsSuccessStatusCode) return null;

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var department = await response.Content.ReadFromJsonAsync<DepartmentDto>(options);

                return department;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ao buscar");
                return null;
            }


        }

        public async Task DecrementEmployee(Guid departmentId)
        {
            var response = await _httpClient.PostAsync($"/api/department/{departmentId}/decrement", null);
            if (!response.IsSuccessStatusCode) throw new Exception("Error decrement");
        }


        public async Task IncrementEmployee(Guid departmentId)
        {
            var response = await _httpClient.PostAsync($"/api/department/{departmentId}/increment", null);

            if (!response.IsSuccessStatusCode) throw new Exception("Error increment");
        }




    }
}
