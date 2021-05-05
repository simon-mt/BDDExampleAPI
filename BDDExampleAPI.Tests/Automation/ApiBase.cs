using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Xunit.Gherkin.Quick;

namespace BDDExampleAPI.Tests.Automation
{
    public class ApiBase : Feature
    {
        protected readonly HttpClient _client;
        protected readonly TestServer _testServer;
        protected readonly WebHostBuilder _builder;

        public ApiBase()
        {
            _builder = new WebHostBuilder().UseStartup<Startup>() as WebHostBuilder;
            _builder.ConfigureAppConfiguration(cfg => cfg.AddJsonFile("appsettings.json", false, false));
            _testServer = new TestServer(_builder);
            _client = _testServer.CreateClient();
        }

        protected HttpContent ConvertObjectToHttpContent(object any)
            => new StringContent(JsonSerializer.Serialize(any), Encoding.Default, "application/json");

        protected async Task<T> ConvertResponseContentToObjectAsync<T>(HttpResponseMessage response)
        {
            var contents = await response.Content.ReadAsStringAsync();
            return JObject.Parse(contents).ToObject<T>();
        }

        public virtual void Dispose()
        {
            _client.Dispose();
            _testServer.Dispose();
        }
    }
}
