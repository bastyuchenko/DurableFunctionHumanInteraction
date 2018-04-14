#r "Microsoft.Azure.WebJobs.Extensions.DurableTask"
#r "Newtonsoft.Json"

#load "../OrcTrigger1/VacationRequest.csx"

using Newtonsoft.Json;

public static string Run(VacationRequest eventData, ICollector<string> outputQueueItem, TraceWriter log)
{
    string eventDataJson = JsonConvert.SerializeObject(eventData);
    outputQueueItem.Add("Name passed to the function: " + eventDataJson);
    return $"Hello {eventDataJson}!"; 
}