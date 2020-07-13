using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WebAnalytics.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Please enter the page you wish to analyse:");
            var page = Console.ReadLine();
            Console.WriteLine("Running...\n");

            var serviceProvider = SetupDI();
            var pr = serviceProvider.GetService<IPageRetriever>();
            var res = await pr.GetPage(page);

            var titleAnalysis = new Analysers.PageTitle().GetTitle(res);
            var scriptAnalysis = new Analysers.ExternalResourceCount().CountScriptResources(res);
            var cssAnalysis = new Analysers.ExternalResourceCount().CountCssResources(res);
            var wordAnalysis = new Analysers.MostFrequentWords().GetWordDict(res);

            Console.WriteLine($"Page title: {titleAnalysis}");
            Console.WriteLine($"Num of scripts: {scriptAnalysis}");
            Console.WriteLine($"Num of css files: {cssAnalysis}");
            Console.WriteLine($"Most frequent words:\n");
            foreach(var kv in wordAnalysis)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value}");
            }
        }

        private static ServiceProvider SetupDI()
        {
            var serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .AddSingleton<IPageRetriever, PageRetriever>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
