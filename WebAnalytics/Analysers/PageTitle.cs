using HtmlAgilityPack;
using WebAnalytics.Models;

namespace WebAnalytics.Analysers
{
    public class PageTitle : IAnalyser
    {
        public string GetAsPrintableString(GetPageResponse toParse)
        {
            var title = GetTitle(toParse);
            return $"Page title: {title}";
        }

        public string GetTitle(GetPageResponse toParse) {
            var doc = new HtmlDocument();
            doc.LoadHtml(toParse.Content);
            var titleNode = doc.DocumentNode.SelectSingleNode("//head/title");
            if (titleNode != null) return titleNode.InnerText;
            return "";
        }
    }
}
