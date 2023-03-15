using System.ComponentModel.DataAnnotations;

namespace PMTools.Models.Project
{

    public class TaskModel
    {
        [Key]
        public Guid TaskId { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDetails { get; set; }
        public string? ProjectId { get; set; }
        public string? DeveloperId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? TaskDifficulties { get; set; }
        public string? TaskStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class TaskModelLog
    {
        [Key]
        public Guid TaskLogId { get; set; }
        public string? TaskId { get; set; }
        public string? TaskLog { get; set; }
        public string? TaskLogDescriptions { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
