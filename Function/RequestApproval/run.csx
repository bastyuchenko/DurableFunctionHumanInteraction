/*
 * Before running this sample, please create a Durable Activity function (default name is "hello")
 */

#r "Microsoft.WindowsAzure.Storage"
#r "Microsoft.Azure.WebJobs.Extensions.DurableTask"
#r "Newtonsoft.Json"

#load "../OrcTrigger1/VacationRequest.csx"

using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

public static async Task Run(VacationRequest eventData, ICollector<string> outputQueueItem, TraceWriter log)
{
    var acc = new CloudStorageAccount(
             new StorageCredentials("storeaccdemo", "+SU0DfTqcs4gcNQDIS47RnBOg2pNLTXftsDmPc9PAan2+AsW5k4FfhlLiS9exOFT9bcAtzJW3ocZqRm4/74D0g=="), false);
            var tableClient = acc.CreateCloudTableClient();
            var table = tableClient.GetTableReference("ApproveTable");
            
            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(eventData);

            // Execute the insert operation.
            await table.ExecuteAsync(insertOperation);
}