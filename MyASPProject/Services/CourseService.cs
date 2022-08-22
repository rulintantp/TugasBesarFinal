using MyASPProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace MyASPProject.Services
{
    public class CourseService : ICourse
    {
        public async Task<bool> Delete(int id, string token)
        { 
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");   //Add Authorization
                using (var response = await httpClient.DeleteAsync($"https://localhost:6001/api/Course/{id}" ))  //call Backend URL
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

        public async Task<IEnumerable<Course>> GetAll(int pageNumber, string token)
        {
            List<Course> lstCourse = new List<Course>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");   //Add Authorization
                using (var response = await httpClient.GetAsync("https://localhost:6001/api/Course?pageNumber=" + pageNumber))  //call Backend URL
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)  //Kalau sukses get data 
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();  //pengambilan data dalam bentuk json   
                        lstCourse = JsonConvert.DeserializeObject<List<Course>>(apiResponse); //converting data dalam bentuk json ke c#
                    }
                    else  //kalau tidak suskses mengambil data
                    {
                        throw new Exception("Gagal retrieve data"); //return kalimat "Gagal retrieve data"
                    }
                }
            }
            return lstCourse; //return to controller
        }

        public async Task<Course> GetById(int id)
        {
            Course course = new Course();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:6001/api/Course/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        course = JsonConvert.DeserializeObject<Course>(apiResponse);
                    }
                }
            }
            return course;
        }

        public async Task<IEnumerable<Course>> GetByName(string name, string token)
        {
            List<Course> courses = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:6001/api/Course/ByName?name={name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);
                }
            }
            return courses;
        }

        public async Task<Course> Insert(Course obj, string token)
        {

            Course course = new Course();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");

                StringContent content =
                    new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:6001/api/Course", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        course = JsonConvert.DeserializeObject<Course>(apiResponse);
                    }
                }
            }
            return course;
        }

        public async Task<Course> Update(Course obj, string token)
        {
            Course course = await GetById(obj.CourseID);
            if (course == null)
                throw new Exception($"Data Course dengan id {obj.CourseID} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:6001/api/Course", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    course = JsonConvert.DeserializeObject<Course>(apiResponse);
                }
            }
            return course;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentByCourseId(int id, string token)
        {
            List<Enrollment> lstEnrollment = new List<Enrollment>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");   //Add Authorization
                using (var response = await httpClient.GetAsync($"https://localhost:6001/api/Course/GetEnrollmentByCourseId?id={id}"))  //call Backend URL
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
