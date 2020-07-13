using System.Xml.Serialization;
using WebAnalytics.Analysers;
using WebAnalytics.Models;
using Xunit;

namespace WebAnalytics.Tests.Analysers
{
    public class ExternalResourceCountTests
    {
        [Fact]
        public void CountScriptResourcesWhenNonePresent()
        {
            var res = DoCountScriptResourcesRequest("<html><body></body></html>");
            Assert.Equal(0, res);
        }

        [Fact]
        public void CountScriptResourcesWhenTwoPresent()
        {
            var res = DoCountScriptResourcesRequest("<html><body><script src=\"\"></script><script src=\"\"></script></body></html>");
            Assert.Equal(2, res);
        }

        [Fact]
        public void CountScriptResourcesScriptWithNoSourceNotCounted()
        {
            var res = DoCountScriptResourcesRequest("<html><body><script></script></body></html>");
            Assert.Equal(0, res);
        }

        [Fact]
        public void CountCssResourcesWhenNonePresent()
        {
            var res = DoCountCssResourcesRequest("<html><body></body></html>");
            Assert.Equal(0, res);
        }

        [Fact]
        public void CountCssResourcesWhenTwoPresent()
        {
            var res = DoCountCssResourcesRequest("<html><body><link rel=\"stylesheet\" href=\"#\" /><link rel=\"stylesheet\" href=\"#\" /></body></html>");
            Assert.Equal(2, res);
        }

        private int DoCountScriptResourcesRequest(string testHtml)
        {
            var req = new GetPageResponse
            {
                Content = testHtml
            };
            var erc = new ExternalResourceCount();
            return erc.CountScriptResources(req);
        }

        private int DoCountCssResourcesRequest(string testHtml)
        {
            var req = new GetPageResponse
            {
                Content = testHtml
            };
            var erc = new ExternalResourceCount();
            return erc.CountCssResources(req);
        }
    }
}
