using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using WebAnalytics.Models;

namespace WebAnalytics.Analysers
{
    public class MostFrequentWords : IAnalyser
    {
        private readonly char[] _wordSplitChars = new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')' };
        private readonly char[] _charsToRemove = new char[] { '\n', '|' };
        private readonly string[] _wordsToFilter = new string[] { "+", "-" };

        public Dictionary<string, int> GetWordDict(GetPageResponse toParse)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(toParse.Content);
            var wordDict = new Dictionary<string, int>();

            foreach (HtmlNode node in GetAllTextNodes(doc))
            {
                var words = node.InnerText.Split(_wordSplitChars, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    var cleanedWord = CleanWord(word);
                    AddWordToDict(wordDict, cleanedWord);
                }
            }
            return wordDict.OrderByDescending(x => x.Value).ToDictionary(y => y.Key, y => y.Value);
        }

        private IEnumerable<HtmlNode> GetAllTextNodes(HtmlDocument doc)
        {
            if (doc.DocumentNode != null)
            {
                var textNodes = doc.DocumentNode.SelectNodes("//text()");
                if (textNodes != null) return textNodes.AsEnumerable();
            }
            return new List<HtmlNode>();
        }

        private string CleanWord(string word)
        {
            word = word.ToLowerInvariant();

            foreach (var c in _charsToRemove)
            {
                word = word.Replace(c.ToString(), "");
            }

            if (_wordsToFilter.Contains(word)) word = "";
            
            return word;
        }

        private void AddWordToDict(Dictionary<string, int> wordDict, string word)
        {
            if (String.IsNullOrEmpty(word)) return;
            
            if (wordDict.ContainsKey(word))
            {
                wordDict[word]++;
            }
            else
            {
                wordDict.Add(word, 1);
            }
        }

        public string GetAsPrintableString(GetPageResponse toParse)
        {
            var words = GetWordDict(toParse);
            var sb = new StringBuilder();
            sb.AppendLine($"Most frequent words:\n");
            foreach (var kv in words)
            {
                sb.AppendLine($"{kv.Key}: {kv.Value}");
            }
            return sb.ToString();
        }
    }
}
