using System;
using System.Collections.Generic;
using System.Text;
using WebAnalytics.Models;

namespace WebAnalytics
{
    public class PageRetriever
    {
        public GetPageResponse GetPage(string url)
        {
            return new GetPageResponse
            {
                Status = 200
            };
        }
    }
}
