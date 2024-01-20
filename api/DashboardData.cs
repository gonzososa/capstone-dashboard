using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace api
{
    public static class DashboardData
    {
        [FunctionName("DashboardData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {            
            Dictionary<string, string> headers = new Dictionary<string, string>();

            foreach (var key in req.Headers.Keys) {
                if (req.Headers.TryGetValue (key, out StringValues val)) {
                    headers.Add (key, val.ToString());
                } 
            }

            var resp = JsonConvert.SerializeObject(headers);

            return new OkObjectResult(resp);
        }
    }
}
