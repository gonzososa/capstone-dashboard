using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CapstoneProject.Data;
using Newtonsoft.Json;
using System.Web.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System;

namespace CapstoneProject
{
    public static class GetDataByDates
    {
        [FunctionName("GetDataByDates")]
        public static async Task<IActionResult> Run (
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] 
            HttpRequest req,
            ILogger log)
        {
            try {
                log.LogInformation ("Funcion GetDataByDates running...");

                if (!req.Query.TryGetValue ("startDate", out StringValues startDateParam)) {
                    return new BadRequestResult ();
                }

                if (!req.Query.TryGetValue ("endDate", out StringValues endDateParam)) {
                    return new BadRequestResult ();
                }

                if (!req.Query.TryGetValue ("summarize", out StringValues summarizeParam)) {
                    return new BadRequestResult (); 
                }
                
                var aux = startDateParam.FirstOrDefault ();
                if (!DateTime.TryParse (aux, out DateTime start)) {
                    return new BadRequestResult ();
                }
                
                aux = endDateParam.FirstOrDefault ();
                if (!DateTime.TryParse (aux, out DateTime end)) {
                    return new BadRequestResult ();
                }

                aux = summarizeParam.FirstOrDefault ();
                if (!bool.TryParse (aux, out bool summarize)) {
                    return new BadRequestResult ();
                }

                end = end.AddHours(23).AddMinutes(59);
                var data = await CapstoneData.GetDataByDates (start, end, summarize, log);
                var result = JsonConvert.SerializeObject (data);
                log.LogInformation ("Data summarized gathered successfully.");

                return new OkObjectResult (data);
            }
            catch (Exception e) {
                log.LogError (e, "An error occurred while function GetDataByDates executing!");
                return new InternalServerErrorResult ();
            }
        }
    }
}