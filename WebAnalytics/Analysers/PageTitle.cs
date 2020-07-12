using System.Linq;
using HtmlAgilityPack;
using WebAnalytics.Models;

namespace WebAnalytics.Analysers
{
    public class PageTitle
    {
        public string GetTitle(GetPageResponse toParse) {
            var doc = new HtmlDocument();
            doc.LoadHtml(toParse.Content);
            var titleNode = doc.DocumentNode.SelectSingleNode("//head/title");
            if (titleNode != null) return titleNode.InnerText;
            return "";
        }
    }
}
