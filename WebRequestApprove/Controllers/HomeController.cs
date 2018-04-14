using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using WebRequestApprove.Models;

namespace WebRequestApprove.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            //var acc = new CloudStorageAccount(
            // new StorageCredentials("storeaccdemo", "+SU0DfTqcs4gcNQDIS47RnBOg2pNLTXftsDmPc9PAan2+AsW5k4FfhlLiS9exOFT9bcAtzJW3ocZqRm4/74D0g=="), false);
            //var tableClient = acc.CreateCloudTableClient();
            //var table = tableClient.GetTableReference("ApproveTable");
            //VacationRequest customer1 = new VacationRequest();
            //customer1.PartitionKey = Guid.NewGuid().ToString();
            //customer1.RowKey = Guid.NewGuid().ToString();
            //customer1.FullName = "Anton Baton";
            //customer1.FromDate = DateTime.Now;
            //customer1.ToDate = DateTime.Now;

            //// Create the TableOperation object that inserts the customer entity.
            //TableOperation insertOperation = TableOperation.Insert(customer1);

            //// Execute the insert operation.
            //await table.ExecuteAsync(insertOperation);

            return View();
        }

        public async Task<IActionResult> Manager()
        {
            var acc = new CloudStorageAccount(
             new StorageCredentials("storeaccdemo", "+SU0DfTqcs4gcNQDIS47RnBOg2pNLTXftsDmPc9PAan2+AsW5k4FfhlLiS9exOFT9bcAtzJW3ocZqRm4/74D0g=="), false);
            var tableClient = acc.CreateCloudTableClient();
            var table = tableClient.GetTableReference("ApproveTable");
            var entities = await table.ExecuteQuerySegmentedAsync(new TableQuery<VacationRequest>(), null);

            return View(entities.Results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Create(VacationRequest item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://df86.azurewebsites.net");
                var httpContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                await client.PostAsync("api/OrcClient", httpContent);
            }
            return View("Index");
        }

        public async Task<IActionResult> Approve(VacationRequest item)
        {
            item.Approved = true;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://df86.azurewebsites.net");
                var httpContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                await client.PostAsync("api/ManagerApproveOrcClient", httpContent);
            }

            return RedirectToAction("Manager");
        }

        public async Task<IActionResult> Reject(VacationRequest item)
        {
            item.Approved = false;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://df86.azurewebsites.net");
                var httpContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                await client.PostAsync("api/ManagerApproveOrcClient", httpContent);
            }
            return RedirectToAction("Manager");
        }
    }
}
