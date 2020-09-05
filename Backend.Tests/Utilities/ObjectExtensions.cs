using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Backend.Tests.Utilities
{
    public static class ObjectExtensions
    {
        public static StringContent ToStringContent(this object parameter) => new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");
    }
}