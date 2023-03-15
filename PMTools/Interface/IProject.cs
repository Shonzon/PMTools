using PMTools.Models;
using PMTools.Models.Authentication.Login;
using PMTools.Models.Project;

namespace PMTools.Interface
{
    public interface IProject
    {
        public Task<Response> AddNewProject(string projectName, string Createdby);
        public Task<List<ProjectModel>> GetAllProjectList();
        public Task<List<RegisterUser>> GetAllDevelopers();
        public Task<List<ProjectModel>> GetAllProjectListByDev(string developerid);
        public Task<Response> AssignDeveloperToProject(ProjectModel project, string createdBy);
        public Task<Response> AddOrUpdateTask(TaskModel taskList, string createdBy);
        public Task<List<TaskModel>> GetAllTaskListByProject(string ProjectId);
        public Task<Response> UpdateTaskStatus(string TaskId, string Status,string UpdatedBy);
    }
}
