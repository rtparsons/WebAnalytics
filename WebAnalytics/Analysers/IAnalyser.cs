using WebAnalytics.Models;

namespace WebAnalytics.Analysers
{
    public interface IAnalyser
    {
        string GetAsPrintableString(GetPageResponse toParse);
    }
}
