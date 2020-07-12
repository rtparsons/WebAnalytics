using WebAnalytics.Analysers;
using WebAnalytics.Models;
using Xunit;

namespace WebAnalytics.Tests.Analysers
{
    public class PageTitleTests
    {
        [Fact]
        public void GetTitleRetunsTitleFromHeadTag()
        {
            var req = new GetPageResponse {
                Content = "<html><head><title>test title</title></head></html>"
            };
            var pt = new PageTitle();
            var res = pt.GetTitle(req);
            Assert.Equal("test title", res);
        }

        [Fact]
        public void GetTitleRetunsEmptyStringWhenNoTitlePresent()
        {
            var req = new GetPageResponse {
                Content = "<html></html>"
            };
            var pt = new PageTitle();
            var res = pt.GetTitle(req);
            Assert.Equal("", res);
        }
    }
}
