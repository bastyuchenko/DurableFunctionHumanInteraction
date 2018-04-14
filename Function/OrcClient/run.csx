#r "Microsoft.Azure.WebJobs.Extensions.DurableTask"
#r "Newtonsoft.Json"

#load "../OrcTrigger1/VacationRequest.csx"

using System.Net;
using Microsoft.Azure.WebJobs;

public static async Task<HttpResponseMessage> Run(
    HttpRequestMessage req,
    DurableOrchestrationClient starter, 
    TraceWriter log)
{
    // Function input comes from the request content.
    VacationRequest eventData = await req.Content.ReadAsAsync<VacationRequest>();
    string instanceId = await starter.StartNewAsync("OrcTrigger1", eventData);
    log.Info($"Func info ='{eventData.FullName}'.");
    log.Info($"Started orchestration with ID ='{instanceId}'.");

    return starter.CreateCheckStatusResponse(req, instanceId);
}