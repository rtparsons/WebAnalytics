using HtmlAgilityPack;
using System;
using WebAnalytics.Models;

namespace WebAnalytics.Analysers
{
    public class ExternalResourceCount : IAnalyser
    {
        public int CountScriptResources(GetPageResponse toParse)
        {
            var scriptTags = toParse.HtmlContent.DocumentNode.SelectNodes("//script/@src");

            if (scriptTags!= null) return scriptTags.Count;
            return 0;
        }

        public int CountCssResources(GetPageResponse toParse)
        {
            var cssLinks = toParse.HtmlContent.DocumentNode.SelectNodes("//link");

            if (cssLinks == null) return 0;

            var res = 0;
            foreach (var node in cssLinks)
            {
                if (node.GetAttributeValue("rel", "") == "stylesheet" &&
                    !String.IsNullOrEmpty(node.GetAttributeValue("href", ""))) 
                {
                    res++;
                }
            }

            return res;
        }

        public string GetAsPrintableString(GetPageResponse toParse)
        {
            var scriptCount = CountScriptResources(toParse);
            var cssCount = CountCssResources(toParse);
            return $"Num of scripts: {scriptCount}\n" +
                $"Num of css files: {cssCount}";
        }
    }
}
