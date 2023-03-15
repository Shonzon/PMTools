using Microsoft.EntityFrameworkCore;
using PMTools.DAL;
using PMTools.Interface;
using PMTools.Models;
using PMTools.Models.Authentication.Login;
using PMTools.Models.Project;

namespace PMTools.BLL
{
    public class ProjectBLL : IProject
    {
        ProjectDAL _projectDAL;

        public ProjectBLL(PMDBContext pMDBContext)
        {
            _projectDAL = new ProjectDAL(pMDBContext);
        }

        #region Add Projects
        public Task<Response> AddNewProject(string projectName, string Createdby)
        {
            ProjectModel project = new ProjectModel
            {
                CreatedBy = Createdby,
                ProejectName = projectName,
            };
            int res = _projectDAL.Addnewproject(project);
            if (res == 1)
            {
                return Task.FromResult(new Response
                {
                    Status = "Success",
                    Message = "Project Save Successfully"
                });
            }
            else
            {
                return Task.FromResult(new Response
                {
                    Status = "Error",
                    Message = "Project Save Failed"
                });
            }
        }
        #endregion Add projects

        #region Assign project to Developers
        public Task<Response> AssignDeveloperToProject(ProjectModel project, string createdBy)
        {
            if (project.DeveloperList == null || project.DeveloperList.Count <= 0)
            {
                return Task.FromResult(new Response { Status = "Failed", Message = "Must Assign Developer to project" });
            }
            else
            {
                List<ProjectAssignModel> assignlist = new List<ProjectAssignModel>();
                foreach (var d in project.DeveloperList)
                {
                    assignlist.Add(
                        new ProjectAssignModel
                        {
                            AssignedBy = createdBy,
                            DeveloperId = d.DeveloperId,
                            ProejectId = project.ProejectId.ToString(),
                            AssignedDate = DateTime.Now

                        }); ;
                }
                int res = _projectDAL.AssignDeveloperToProject(assignlist);
                if (res > 0)
                {
                    return Task.FromResult(new Response
                    {
                        Status = "Success",
                        Message = "Project Assigned Successfully"
                    });
                }
                else
                {
                    return Task.FromResult(new Response
                    {
                        Status = "Error",
                        Message = "Project Assigned Failed"
                    });
                }
            }

        }
        #endregion  Assign project to Developers

        #region Get Date List
        public Task<List<RegisterUser>> GetAllDevelopers()
        {
            List<RegisterUser> developerList = new List<RegisterUser>();
            developerList = _projectDAL.GetAllDevelopers();
            return Task.FromResult(developerList);
        }

        public Task<List<ProjectModel>> GetAllProjectList()
        {
            List<ProjectModel> ProjectList = new List<ProjectModel>();
            ProjectList = _projectDAL.GetAllProjectList();
            return Task.FromResult(ProjectList);
        }
        public Task<List<ProjectModel>> GetAllProjectListByDev(string developerid)
        {
            List<ProjectModel> projectbydev = new List<ProjectModel>();
            projectbydev = _projectDAL.GetProjectListByDeveloper(developerid);
            return Task.FromResult(projectbydev);
        }
        #endregion  Get Date List

        #region Task Model
        public Task<Response> AddOrUpdateTask(TaskModel taskList, string createdBy)
        {
            if (_projectDAL.ValidateProjectByDeveloper(createdBy, taskList.ProjectId))
            {
                int res = _projectDAL.AddorUpdateTask(taskList, createdBy);
                if (res > 0)
                {
                    return Task.FromResult(new Response
                    {
                        Status = "Success",
                        Message = "Task Created Successfully"
                    });
                }
                else
                {
                    return Task.FromResult(new Response
                    {
                        Status = "Error",
                        Message = "Task Created Failed"
                    });
                }
            }
            else
            {
                return Task.FromResult(new Response
                {
                    Status = "Error",
                    Message = "This Project is Not assigned with this developer"
                });
            }

        }

        public Task<List<TaskModel>> GetAllTaskListByProject(string ProjectId)
        {
            List<TaskModel> tasklist = new List<TaskModel>();
            tasklist = _projectDAL.GetAllTaskListByproject(ProjectId);
            return Task.FromResult(tasklist);
        }
        public Task<Response> UpdateTaskStatus(string TaskId, string Status, string UpdatedBy){
            int res = _projectDAL.UpdateTaskStaus(TaskId, Status, UpdatedBy);
            if (res > 0)
            {
                return Task.FromResult(new Response
                {
                    Status = "Success",
                    Message = "Task Status Updated Successfully"
                });
            }
            else if (res==-1)
            {
                return Task.FromResult(new Response
                {
                    Status = "Error",
                    Message = "No Task found for the following Id"
                });
            }
            else
            {
                return Task.FromResult(new Response
                {
                    Status = "Error",
                    Message = "Task Status Updated Failed"
                });
            }
        }
        #endregion Task Model
    }
}
