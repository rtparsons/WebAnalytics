﻿using System;
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

            var pr = new PageRetriever(new System.Net.Http.HttpClient());
            var res = await pr.GetPage(page);

            var titleAnalysis = new Analysers.PageTitle().GetTitle(res);
            var wordAnalysis = new Analysers.MostFrequentWords().GetWordDict(res);

            Console.WriteLine($"Page title: {titleAnalysis}");
            Console.WriteLine($"Most frequent words:\n");
            foreach(var kv in wordAnalysis)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value}");
            }
        }
    }
}
