using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using WebAnalytics.Models;

namespace WebAnalytics.Analysers
{
    public class MostFrequentWords
    {
        private readonly char[] _wordSplitChars = new char[] { '.', '?', '!', ' ', ';', ':', ',' };

        public Dictionary<string, int> GetWordDict(GetPageResponse toParse) {
            var doc = new HtmlDocument();
            doc.LoadHtml(toParse.Content);
            var wordDict = new Dictionary<string, int>();

            foreach (HtmlNode node in GetAllTextNodes(doc))
            {
                var words = node.InnerText.Split(_wordSplitChars, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words) {
                    var cleanedWord = word.ToLowerInvariant();
                    if (wordDict.ContainsKey(cleanedWord)) {
                        wordDict[cleanedWord]++; 
                    } else {
                        wordDict.Add(cleanedWord, 1); 
                    }
                }
            }

            return wordDict;
        }

        private IEnumerable<HtmlNode> GetAllTextNodes(HtmlDocument doc) {
            if (doc.DocumentNode != null) {
                var textNodes = doc.DocumentNode.SelectNodes("//text()");
                if (textNodes != null) return textNodes.AsEnumerable();
            }
            return new List<HtmlNode>();
        }
    }
}
