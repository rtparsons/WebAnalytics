using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WebAnalytics.Tests
{
    public class PageRetrieverTests
    {
        [Fact]
        public async void GetPageReturns200()
        {
            var pr = InitPageRetriever(HttpStatusCode.OK);
            var res = await pr.GetPage("http://www.testpage.com");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact]
        public async void GetPageReturns404()
        {
            var pr = InitPageRetriever(HttpStatusCode.NotFound);
            var res = await pr.GetPage("http://www.testpage.com");
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }

        [Fact]
        public async void GetPageReturnsPageContent()
        {
            var pr = InitPageRetriever(HttpStatusCode.OK, "testcontent");
            var res = await pr.GetPage("http://www.testpage.com");
            Assert.Equal("testcontent", res.Content);
        }

        private PageRetriever InitPageRetriever(HttpStatusCode responseStatus)
        {
            return InitPageRetriever(responseStatus, "");
        }

        private PageRetriever InitPageRetriever(HttpStatusCode responseStatus, string responseContent)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = responseStatus,
                Content = new StringContent(responseContent)
            };

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            
            var client = new HttpClient(handlerMock.Object);
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            return new PageRetriever(mockFactory.Object);
        }
    }
}
