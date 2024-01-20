using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CapstoneProject.Data;
using Newtonsoft.Json;

namespace CapstoneProject
{
    public static class DashboardData
    {
        [FunctionName("GetAllData")]
        public static async Task<IActionResult> Run (
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] 
            HttpRequest req,
            ILogger log)
        {            
            log.LogInformation ($"Function GetAllData running...");
            var data = await CapstoneData.GetAllData ();
            var result = JsonConvert.SerializeObject (data);
            return new OkObjectResult (result);
        }
    }
}
