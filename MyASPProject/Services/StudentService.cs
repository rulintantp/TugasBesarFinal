using MyASPProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace MyASPProject.Services
{
    public class StudentService : IStudent
    {

        public async Task<bool> Delete(int id, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");   //Add Authorization
                using (var response = await httpClient.DeleteAsync($"https://localhost:6001/api/Students/{id}"))  //call Backend URL
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

        public async Task<IEnumerable<Student>> GetAll(int pageNumber, string token)
        {
            List<Student> lstStudent = new List<Student>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");   //Add Authorization
                using (var response = await httpClient.GetAsync($"https://localhost:6001/api/Students?pageNumber=" + pageNumber))  //Call Backend URL
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)  //Kalau sukses get data 
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();  //Pengambilan data dalam bentuk json   
                        lstStudent = JsonConvert.DeserializeObject<List<Student>>(apiResponse); //Converting data dalam bentuk json ke c#
                    }
                    else  //kalau tidak suskses mengambil data
                    {
                        throw new Exception("Gagal retrieve data"); //Return kalimat "Gagal retrieve data"
                    }
                }
            }
            return lstStudent; //return to controller
        }

        public async Task<Student> GetById(int id)
        {
            Student student = new Student();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:6001/api/Student/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<Student>(apiResponse);
                    }
                }
            }
            return student;
        }

        public async Task<IEnumerable<Student>> GetByName(string name, string token)
        {
            List<Student> students = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:6001/api/Students/ByName?name={name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return students;
        }


        public async Task<Student> Insert(Student obj, string token)
        {

            Student students = new Student();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");

                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:6001/api/Students", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        students = JsonConvert.DeserializeObject<Student>(apiResponse);
                    }
                }
            }
            return students;
        }

        public async Task<Student> Update(Student obj, string token)
        {
            Student student = await GetById(obj.ID);
            if (student == null)
                throw new Exception($"Data Student dengan id {obj.ID} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:6001/api/Students", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return student;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentByID(int id, string token)
        {
            List<Enrollment> lstEnrollment = new List<Enrollment>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");   //Add Authorization
                using (var response = await httpClient.GetAsync($"https://localhost:6001/api/Students/GetEnrollmentByID?id={id}"))  //call Backend URL
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
    }
}

