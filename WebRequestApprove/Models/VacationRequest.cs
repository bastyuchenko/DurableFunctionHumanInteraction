using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebRequestApprove.Models
{
    public class VacationRequest:TableEntity
    {
        public string InstanceId { get; set; }
        public string FullName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Approved { get; set; }
    }
}
