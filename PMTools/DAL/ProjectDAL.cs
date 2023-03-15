using Microsoft.EntityFrameworkCore.Internal;
using PMTools.Models;
using PMTools.Models.Authentication.Login;
using PMTools.Models.Project;
using System.Collections.Generic;

namespace PMTools.DAL
{
    public class ProjectDAL
    {
        private readonly PMDBContext _context;
        public ProjectDAL(PMDBContext context)
        {
            _context = context;
        }
        #region Project Model
        public int Addnewproject(ProjectModel project) {
            var projectname = new ProjectModel { ProejectName = project.ProejectName, CreatedBy = project.CreatedBy,CreatedDate=DateTime.Now };
            _context.Add<ProjectModel>(projectname);
            return _context.SaveChanges();
        }
        
        public List<ProjectModel> GetAllProjectList() {
            List<ProjectModel>  allProjectlist= _context.ProjectTable.ToList<ProjectModel>();
            return allProjectlist;
        }
        public List<ProjectModel> GetProjectListByDeveloper(string developerid)
        {
            List<ProjectModel> projectbydeveoper = (from pa in _context.ProjectAssignTable
                                                 join pt in _context.ProjectTable on pa.ProejectId equals pt.ProejectId.ToString()
                                                 where pa.DeveloperId == developerid
                                                 select new ProjectModel
                                                 {
                                                     ProejectId = pt.ProejectId,
                                                     ProejectName = pt.ProejectName,
                                                 }).ToList();
            return projectbydeveoper;
        }
        public bool ValidateProjectByDeveloper(string developerid,string projectid)
        {
            bool valid = _context.ProjectAssignTable.Any(x=>x.DeveloperId==developerid && x.ProejectId==projectid);
            return valid;
        }
        public List<RegisterUser> GetAllDevelopers() {
            List<RegisterUser> developerList = (from usr in _context.Users
                                                join ur in _context.UserRoles on usr.Id equals ur.UserId
                                                join r in _context.Roles on ur.RoleId equals r.Id
                                                where r.Name=="Developer" 
                                                select new RegisterUser
                                                {
                                                    Id = usr.Id,
                                                    UserName=usr.UserName,
                                                    Email=usr.Email,
                                                }).ToList();
            return developerList;
        }

        public int AssignDeveloperToProject(List<ProjectAssignModel> assignlist)
        {
            foreach (var pa in assignlist)
            {
                _context.Add<ProjectAssignModel>(pa);
            }
            return _context.SaveChanges();
        }
        #endregion project Model
        #region Task Model
        public int AddorUpdateTask(TaskModel task,string developerid)
        {
            var findtask = _context.TaskTable.Any(t => t.TaskId==task.TaskId);
            if (findtask)
            {
                var uptask=_context.TaskTable.Where(x => x.TaskId==task.TaskId).First();
                uptask.StartDate = task.StartDate;
                uptask.EndDate = task.EndDate;
                uptask.TaskDetails = task.TaskDetails;
                uptask.TaskDifficulties = task.TaskDifficulties;
                var taskid = uptask.TaskId;

                _context.Add<TaskModelLog>(new TaskModelLog { TaskId = taskid.ToString(), TaskLog = "Task Information Updated", TaskLogDescriptions = "Task Updated Syuccessfully", CreatedBy = developerid, CreatedDate = DateTime.Now });
            }
            else
            {
                var newtask = new TaskModel
                {
                    TaskId = task.TaskId,
                    ProjectId = task.ProjectId,
                    DeveloperId = developerid,
                    StartDate = task.StartDate,
                    EndDate = task.EndDate,
                    TaskName = task.TaskName,
                    TaskDetails = task.TaskDetails,
                    TaskDifficulties=task.TaskDifficulties,
                    CreatedBy = developerid,
                    CreatedDate = DateTime.Now
                };
                _context.Add<TaskModel>(newtask);
                var taskid = newtask.TaskId;

                _context.Add<TaskModelLog>(new TaskModelLog { TaskId=taskid.ToString(),TaskLog="New Task Created",TaskLogDescriptions="Task Created Syuccessfully",CreatedBy=developerid,CreatedDate=DateTime.Now});
            }
            
            return _context.SaveChanges();
        }
        public List<TaskModel> GetAllTaskListByproject(string projectid)
        {
            List<TaskModel> tasklist = (from tk in _context.TaskTable
                                        join pr in _context.ProjectTable on tk.ProjectId equals pr.ProejectId.ToString()
                                                join us in _context.Users on tk.DeveloperId equals us.Id
                                                where tk.ProjectId == projectid
                                        select new TaskModel
                                        {
                                          TaskId=tk.TaskId,
                                          TaskName=tk.TaskName,
                                          TaskDetails=tk.TaskDetails,
                                          DeveloperId=us.UserName,
                                          ProjectId=pr.ProejectName,
                                          CreatedDate=tk.CreatedDate,
                                          StartDate=tk.StartDate,
                                          EndDate=tk.EndDate,
                                          TaskStatus=tk.TaskStatus,
                                          TaskDifficulties=tk.TaskDifficulties
                                        }).ToList();
            return tasklist;
        }
        public int UpdateTaskStaus(string taskId,string Status, string developerid)
        {
            var findtask = _context.TaskTable.Any(t => t.TaskId.ToString() == taskId);
            if (findtask)
            {
                var uptask = _context.TaskTable.Where(x => x.TaskId.ToString() == taskId).First();
                uptask.TaskStatus = Status;
                var taskid = uptask.TaskId;

                _context.Add<TaskModelLog>(new TaskModelLog { TaskId = taskid.ToString(), TaskLog = "Task Staus Updated=> "+Status, TaskLogDescriptions = "Task status Updated Syuccessfully", CreatedBy = developerid, CreatedDate = DateTime.Now });
                return _context.SaveChanges();
            }
            else
            {
                return -1;
            }

            
        }
        #endregion Task Model

    }
}
