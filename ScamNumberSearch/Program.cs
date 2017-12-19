using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;

namespace ScamNumberSearch
{
    class Program
    {
        private const string apiKey = "AIzaSyAQWvZ9zO5UwgNuO0so8f3rbs6J_5J4TFo";
        private const string searchEngineId = "012298178512664359617:kzs9jucw0ta";

        static void Main(string[] args)
        {
            Console.WriteLine("Spotify Scam Phone Number Grabber");
            Console.WriteLine();
            Console.WriteLine("By: @AdwareHunter");

            try
            {
                var query = "microsoft OR help OR support OR desk AND phone";
                new Program().Run(query).Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            Console.ReadKey();
        }

        private async Task Run(string query)
        {
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = searchEngineId;

            Console.WriteLine("Start...");
            IList<Result> paging = new List<Result>();
            var count = 0;
            while (paging != null)
            {
                Console.WriteLine($"Page {count}");
                listRequest.Start = count * 10 + 1;
                paging = listRequest.Execute().Items;
                if (paging != null)
                    foreach (var item in paging)
                        Console.WriteLine("Title : " + item.Title + Environment.NewLine + "Link : " + item.Link +
                                          Environment.NewLine + $"StrippedNumber: {Regex.Replace(item.Title, "[^0-9]+", string.Empty)}" + Environment.NewLine);
                count++;
            }
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
