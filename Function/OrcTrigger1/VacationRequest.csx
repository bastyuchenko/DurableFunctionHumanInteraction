#r "Microsoft.WindowsAzure.Storage"

using Microsoft.WindowsAzure.Storage.Table;

public class VacationRequest: TableEntity
    {
        public string InstanceId { get; set; }
        public string FullName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Approved { get; set; }
    }