using MyASPProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace MyASPProject.Services
{
    public class EnrollmentService : IEnrollment
    {
        public async Task<bool> Delete(int id, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");   //Add Authorization
                using (var response = await httpClient.DeleteAsync($"https://localhost:6001/api/Enrollment/{id}"))  //call Backend URL
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)  //Kalau sukses get data 
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll(int pageNumber, string token)
        {
            List<Enrollment> lstEnrollment = new List<Enrollment>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");   //Add Authorization
                using (var response = await httpClient.GetAsync("https://localhost:6001/api/Enrollment?pageNumber=" + pageNumber))  //call Backend URL
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)  //Kalau sukses get data 
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();  //pengambilan data dalam bentuk json   
                        lstEnrollment = JsonConvert.DeserializeObject<List<Enrollment>>(apiResponse); //converting data dalam bentuk json ke c#
                    }
                    else  //kalau tidak suskses mengambil data
                    {
                        throw new Exception("Gagal retrieve data"); //return kalimat "Gagal retrieve data"
                    }
                }
            }
            return lstEnrollment; //return to controller
        }

        public async Task<IEnumerable<Enrollment>> GetByName(string name, string token)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:6001/api/Enrollment/ByName?name={name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    enrollments = JsonConvert.DeserializeObject<List<Enrollment>>(apiResponse);
                }
            }
            return enrollments;
        }

        public async Task<Enrollment> Insert(Enrollment obj, string token)
        {

            Enrollment enrollment = new Enrollment();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");

                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:6001/api/Enrollment", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        enrollment = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                    }
                }
            }
            return enrollment;
        }

        public Task<Enrollment> Update(Enrollment obj, string token)
        {
            throw new NotImplementedException();
        }
    }
}
