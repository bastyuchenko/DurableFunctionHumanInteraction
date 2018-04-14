#r "Microsoft.Azure.WebJobs.Extensions.DurableTask"
#r "Newtonsoft.Json"

#load "../OrcTrigger1/VacationRequest.csx"

using System.Net;

public static async Task Run(
    HttpRequestMessage req,
    DurableOrchestrationClient starter,
    TraceWriter log)
{ 
    VacationRequest eventData = await req.Content.ReadAsAsync<VacationRequest>();
    await starter.RaiseEventAsync(eventData.InstanceId, "ApprovalEvent", eventData.Approved);
}