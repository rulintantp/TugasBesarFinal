using MyASPProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace MyASPProject.Services
{
    public class SamuraiServices : ISamurai
    {
        public async Task Delete(int id)
        {
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"http://localhost:5001/api/Samurais/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

        public async Task<IEnumerable<Samurai>> GetAll()
        {
            List<Samurai> samurais = new List<Samurai>();
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/Samurais"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<Samurai>>(apiResponse);
                }
            }
            return samurais;
        }

        public async Task<Samurai> GetById(int id)
        {
            Samurai samurai = new Samurai();
            using(var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync($"http://localhost:5001/api/Samurais/{id}"))
                {
                    if(response.StatusCode==System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Samurai>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public async Task<IEnumerable<SamuraiWithQuotes>> GetSamuraiWithQuotes()
        {
            List<SamuraiWithQuotes> samurais = new List<SamuraiWithQuotes>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/Samurais/WithQuotes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<SamuraiWithQuotes>>(apiResponse);
                }
            }
            return samurais;
        }

        public async Task<IEnumerable<Samurai>> GetByName(string name)
        {
            List<Samurai> samurais = new List<Samurai>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5001/api/Samurais/ByName?name={name}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<Samurai>>(apiResponse);
                }
            }
            return samurais;
        }

        public async Task<Samurai> Insert(Samurai obj)
        {
            Samurai samurai = new Samurai();
            using (var httpClient = new HttpClient())
            {
                StringContent content = 
                    new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:5001/api/Samurais", content)) 
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Samurai>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public async Task<Samurai> Update(Samurai obj)
        {
            Samurai samurai = await GetById(obj.Id);
            if (samurai == null)
                throw new Exception($"Data Samurai dengan id {obj.Id} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj), 
                  Encoding.UTF8, "application/json");
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("http://localhost:5001/api/Samurais",content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurai = JsonConvert.DeserializeObject<Samurai>(apiResponse);
                }
            }
            return samurai;
        }

        public async Task<IEnumerable<Weather>> GetAllWeather(string token)
        {
            List<Weather> lstWeather = new List<Weather>();
            using(var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.GetAsync("https://localhost:6001/WeatherForecast")) 
                    {
                    if(response.StatusCode==System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        lstWeather = JsonConvert.DeserializeObject<List<Weather>>(apiResponse);
                    }
                    else
                    {
                        throw new Exception("Gagal retrieve data");
                    }
                }
            }
            return lstWeather;
        }
    }
}
