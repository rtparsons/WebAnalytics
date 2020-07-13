﻿using HtmlAgilityPack;
using System;
using WebAnalytics.Models;

namespace WebAnalytics.Analysers
{
    public class ExternalResourceCount
    {
        public int CountScriptResources(GetPageResponse toParse)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(toParse.Content);
            var scriptTags = doc.DocumentNode.SelectNodes("//script/@src");

            if (scriptTags!= null) return scriptTags.Count;
            return 0;
        }

        public int CountCssResources(GetPageResponse toParse)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(toParse.Content);
            var cssLinks = doc.DocumentNode.SelectNodes("//link");

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
    }
}