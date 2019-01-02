using System;

namespace PrjManager.Business.ServiceRequests
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int Priority { get; set; }
        public int ParentTaskId { get; set; }
        public string   ParentTaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string Status { get; set; }
    }
}
