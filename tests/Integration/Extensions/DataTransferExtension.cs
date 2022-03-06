using Listening.Core.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Extensions
{
    public static class DataTransferExtension
    {
        private static readonly JsonSerializerSettings _settings;

        static DataTransferExtension()
        {
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public static string Serialize(this IDto dto)
        {
            return JsonConvert.SerializeObject(dto, _settings);
        }

        public static string Serialize(this IViewModel viewModel)
        {
            return JsonConvert.SerializeObject(viewModel, _settings);
        }

        public static string Serialize(this string[] stringArray)
        {
            return JsonConvert.SerializeObject(stringArray, _settings);
        }

        public static HttpContent AsContent(this string str)
        {
            return new StringContent(str, Encoding.UTF8, "application/json");
        }

        public static async Task<T> Deserialize<T>(this HttpResponseMessage httpContent)
        {
            return JsonConvert.DeserializeObject<T>(
                await httpContent.Content.ReadAsStringAsync());
        }
    }
}