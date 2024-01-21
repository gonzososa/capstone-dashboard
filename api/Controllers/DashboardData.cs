using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CapstoneProject.Data;
using Newtonsoft.Json;
using System.Web.Http;

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
            try {
                log.LogInformation ("Function GetAllData running...");
                var data = await CapstoneData.GetAllData (log);
                var result = JsonConvert.SerializeObject (data);
                log.LogInformation ("Data gathered successfully");
                
                return new OkObjectResult (result);
            }
            catch (System.Exception e) {
                log.LogError (e, "An error occurred while function executing!");
                return new InternalServerErrorResult ();                
            }
        }
    }
}
