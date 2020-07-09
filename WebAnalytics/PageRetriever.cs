using System.Net.Http;
using System.Threading.Tasks;
using WebAnalytics.Models;

namespace WebAnalytics
{
    public class PageRetriever
    {
        private readonly HttpClient _client;

        public PageRetriever(HttpClient client)
        {
            _client = client;
        }

        public async Task<GetPageResponse> GetPage(string url)
        {
            var res = await _client.GetAsync(url);
            return new GetPageResponse
            {
                StatusCode = res.StatusCode
            };
        }
    }
}
