using HtmlAgilityPack;
using System.Net;

namespace WebAnalytics.Models
{
    public class GetPageResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public HtmlDocument HtmlContent { get; private set; }

        private string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                HtmlContent = new HtmlDocument();
                HtmlContent.LoadHtml(_content);
            }
        }
    }
}
