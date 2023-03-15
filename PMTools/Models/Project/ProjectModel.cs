using PMTools.Models.Authentication.Login;
using System.ComponentModel.DataAnnotations;

namespace PMTools.Models.Project
{
    public class ProjectModel
    {
        [Key]
        public Guid ProejectId { get; set; }
        public string? ProejectName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<ProjectAssignModel>? DeveloperList { get; set; }
       
    }

    public class ProjectAssignModel
    {
        [Key]
        public Guid AssignId { get; set; }
        public string? ProejectId { get; set; }
        public string? DeveloperId { get; set; }
        public string? AssignedBy { get; set; }
        public DateTime? AssignedDate { get; set; }
    }
}
