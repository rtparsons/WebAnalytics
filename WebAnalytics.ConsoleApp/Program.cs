using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebAnalytics.Analysers;

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

            var analysers = serviceProvider.GetServices<IAnalyser>();
            foreach (var analyser in analysers)
            {
                Console.WriteLine(analyser.GetAsPrintableString(res));
            }
        }

        private static ServiceProvider SetupDI()
        {
            var serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .AddSingleton<IPageRetriever, PageRetriever>()
                .AddSingleton<IAnalyser, PageTitle>()
                .AddSingleton<IAnalyser, ExternalResourceCount>()
                .AddSingleton<IAnalyser, MostFrequentWords>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
