using Xunit;

namespace WebAnalytics.Tests
{
    public class PageRetrieverTests
    {
        [Fact]
        public void GetPageReturns200()
        {
            var pr = new PageRetriever();
            var res = pr.GetPage("http://www.testpage.com");
            Assert.Equal(200, res.Status);
        }
    }
}
