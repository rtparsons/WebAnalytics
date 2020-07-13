using System.Net.Http;
using System.Threading.Tasks;
using WebAnalytics.Models;

namespace WebAnalytics
{
    public class PageRetriever : IPageRetriever
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PageRetriever(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetPageResponse> GetPage(string url)
        {
            var client = _httpClientFactory.CreateClient();
            var res = await client.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            return new GetPageResponse
            {
                StatusCode = res.StatusCode,
                Content = content
            };
        }
    }
}
