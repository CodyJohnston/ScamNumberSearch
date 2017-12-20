namespace ScamNumberSearch
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Google.Apis.Customsearch.v1;
    using Google.Apis.Customsearch.v1.Data;
    using Google.Apis.Services;

    using Extensions;

    class Program
    {
        private const string ApiKey = "<YOUR API KEY HERE>";
        private const string SearchEngineId = "<YOUR SEARCH ENGINE ID HERE>";

        private static SimpleLogger logger = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Spotify Scam Phone Number Grabber");
            Console.WriteLine();
            Console.WriteLine("If you are insterested in getting this all working, there are");
            Console.WriteLine("a few things that you will need.");
            Console.WriteLine();
            Console.WriteLine("    1. Create a new project in Google Developer Console, you'll need to get your API key there.");
            Console.WriteLine("    2. Follow this guide to get your Search Engine Id:");
            Console.WriteLine(        "https://developers.google.com/custom-search/docs/overview");
            Console.WriteLine();
            Console.WriteLine("By: @AdwareHunter (Cody Johnston)");
            Console.WriteLine("https://github.com/CodyJohnston/ScamNumberSearch");

            try
            {
                logger = new SimpleLogger();

                var query = "microsoft OR help OR support OR desk AND phone";
                query = $"{query} daterange:{(int)DateTime.Now.AddDays(-1).ToJulianDate()}-{(int)DateTime.Now.ToJulianDate()}";

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
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = ApiKey });
            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = SearchEngineId;

            Console.WriteLine("Start...");
            IList<Result> paging = new List<Result>();
            var count = 1;
            try
            {
                while (paging != null)
                {
                    Console.WriteLine($"Page {count}");

                    listRequest.Start = count + 10;

                    paging = listRequest.Execute().Items;

                    if (paging != null)
                    {
                        foreach (var item in paging)
                        {
                            var strippedPhoneNumber = Regex.Replace(item.Title, "[^0-9]+", string.Empty);
                            Console.WriteLine($"Title : {item.Title}" + Environment.NewLine + 
                                $"Link : {item.Link}" + Environment.NewLine + 
                                $"StrippedNumber: {strippedPhoneNumber}" + 
                                Environment.NewLine);

                            logger?.Log(strippedPhoneNumber);
                        }
                    }
                        
                    count++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting more results: {ex.Message}");
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("Done.");
            Console.WriteLine($"{count} results found.");
        }
    }
}
