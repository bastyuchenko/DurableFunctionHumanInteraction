/*
 * Before running this sample, please create a Durable Activity function (default name is "hello")
 */

#r "Microsoft.Azure.WebJobs.Extensions.DurableTask"

#load "../OrcTrigger1/VacationRequest.csx"

using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

public static async Task Run(DurableOrchestrationContext ctx)
{
    VacationRequest eventData = ctx.GetInput<VacationRequest>();
    eventData.InstanceId = ctx.InstanceId;
    eventData.PartitionKey = Guid.NewGuid().ToString();
    eventData.RowKey = Guid.NewGuid().ToString();
    eventData.Timestamp = DateTime.Now;
    await ctx.CallActivityAsync<string>("RequestApproval", eventData);

    eventData.Approved = await ctx.WaitForExternalEvent<bool>("ApprovalEvent");
    
    await ctx.CallActivityAsync("ProcessApproval", eventData);
}