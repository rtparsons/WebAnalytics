using System.Threading.Tasks;
using WebAnalytics.Models;

namespace WebAnalytics
{
    public interface IPageRetriever
    {
        Task<GetPageResponse> GetPage(string url);
    }
}
