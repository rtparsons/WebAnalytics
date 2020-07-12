using WebAnalytics.Analysers;
using WebAnalytics.Models;
using Xunit;

namespace WebAnalytics.Tests.Analysers
{
    public class MostFrequentWordsTests
    {
        [Fact]
        public void GetWordDictReturnsEmptyDictGivenNoInput()
        {
            var req = new GetPageResponse {
                Content = ""
            };
            var mfw = new MostFrequentWords();
            var res = mfw.GetWordDict(req);
            Assert.Empty(res);
        }

        [Fact]
        public void GetWordDictReturnsDictWithSingleEntry()
        {
            var req = new GetPageResponse {
                Content = "<html><body><p>testword</p></body></html>"
            };
            var mfw = new MostFrequentWords();
            var res = mfw.GetWordDict(req);
            Assert.Equal(1, res["testword"]);
        }

        [Fact]
        public void GetWordDictPicksUpWordsInMultipleTags()
        {
            var req = new GetPageResponse {
                Content = "<html><body><p>test</p><a>test</a><span>test</span></body></html>"
            };
            var mfw = new MostFrequentWords();
            var res = mfw.GetWordDict(req);
            Assert.Equal(3, res["test"]);
        }

        [Fact]
        public void GetWordDictBreaksDownSentance()
        {
            var req = new GetPageResponse {
                Content = "<html><body><p>this is a test</p></body></html>"
            };
            var mfw = new MostFrequentWords();
            var res = mfw.GetWordDict(req);
            Assert.Equal(4, res.Count);
            Assert.Equal(1, res["test"]);
        }

        [Fact]
        public void GetWordDictRemoveCapitalization()
        {
            var req = new GetPageResponse {
                Content = "<html><body><p>TEsT</p></body></html>"
            };
            var mfw = new MostFrequentWords();
            var res = mfw.GetWordDict(req);
            Assert.Equal(1, res["test"]);
        }
    }
}
