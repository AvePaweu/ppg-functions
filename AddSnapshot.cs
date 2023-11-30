using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Sql;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace FunctionApp1
{
    public class AddSnapshot
    {
        [FunctionName("AddSnapshot")]
        public static async Task Run([TimerTrigger("* */5 5-23 * * *")] TimerInfo myTimer, ILogger log,
            [Sql(commandText: "dbo.Snapshots", connectionStringSetting: "SqlConnectionString")] IAsyncCollector<Snapshot> snapshots)
        {
            var client = new HttpClient();
            var resp = await client.GetStringAsync("https://komunikacja.um.gorzow.pl/vm/main?command=planner&action=v");
            var result = JsonConvert.DeserializeObject<List<Position>>(resp);

            Snapshot newSnap = new()
            {
                date = DateTime.Now,
                tram = Convert.ToByte(result.Count(d => d.type == "Tramwaj")),
                bus = Convert.ToByte(result.Count(d => d.type == "Autobus")),
                data = resp
            };
            try
            {
                await snapshots.AddAsync(newSnap);
                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            }
            catch (System.Exception ex)
            {
                log.LogError($"{ex.Message}***{ex.StackTrace}***{ex.InnerException}");
            }
        }
    }
}
