using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMTools.Interface;
using PMTools.BLL;
using PMTools.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PMTools.Models.Project;
using PMTools.Models.Authentication.Login;

namespace PMTools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        IProject _project;

        public ProjectController(PMDBContext pMDBContext)
        {
            _project = new ProjectBLL(pMDBContext);
        }
        #region Project Details
        [HttpPost]
        [Route("AddNewProject")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Admin")]
        public async Task<IActionResult> AddNewProject(string projectName)
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity!=null)
            {
                string? createid = identity.Name;
                Response _result = await _project.AddNewProject(projectName, createid);
                if (_result.Status.Equals("Success"))
                {
                    return StatusCode(StatusCodes.Status200OK, _result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, _result);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status="Error",Message="Something went wrong No User Found"});
            }
            
        }
        [HttpPost]
        [Route("GetAllProjects")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllProjects()
        {
            List<ProjectModel> projectList = await _project.GetAllProjectList();
            return StatusCode(StatusCodes.Status200OK, projectList);
        }
        [HttpPost]
        [Route("AssignProjectToDeveloper")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> AssignProjectToDeveloper([FromBody] ProjectModel project)
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity != null)
            {
                string? createid = identity.Name;
                Response _result = await _project.AssignDeveloperToProject(project, createid);
                if (_result.Status.Equals("Success"))
                {
                    return StatusCode(StatusCodes.Status200OK, _result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, _result);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Something went wrong No User Found" });
            }
        }
        [HttpGet]
        [Route("GetAllDevelopers")]
        public async Task<IActionResult> GetAllDevelopers()
        {
            List<RegisterUser> developerlist = await _project.GetAllDevelopers();
            return StatusCode(StatusCodes.Status200OK, developerlist);
        }
        [HttpPost]
        [Route("ProjectListByDeveloper")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Developer")]
        public async Task<IActionResult> ProjectListByDeveloper()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string createid = identity.Name;
            List<ProjectModel> projectList = await _project.GetAllProjectListByDev(createid);
            return StatusCode(StatusCodes.Status200OK, projectList);
        }

        #endregion project details
        #region Task Details
        [HttpPost]
        [Route("AddOrUpdateTask")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Developer")]
        public async Task<IActionResult> AddOrUpdateTask([FromBody] TaskModel TaskList)
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity != null)
            {
                string? createid = identity.Name;
                Response _result = await _project.AddOrUpdateTask(TaskList, createid);
                if (_result.Status.Equals("Success"))
                {
                    return StatusCode(StatusCodes.Status200OK, _result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, _result);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Something went wrong No User Found" });
            }
        }
        [HttpPost]
        [Route("GetAllTaskListByProject")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Developer,Admin")]
        public async Task<IActionResult> GetAllTaskListByProject (string  ProjectId)
        {
            List<TaskModel> tasks = await _project.GetAllTaskListByProject(ProjectId);
            return StatusCode(StatusCodes.Status200OK, tasks);
        }
        [HttpPost]
        [Route("UpdateTaskStatus")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Developer")]
        public async Task<IActionResult> UpdateTaskStatus(string TaskId,string Status)
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity != null)
            {
                string? createid = identity.Name;
                Response _result = await _project.UpdateTaskStatus(TaskId, Status,createid);
                if (_result.Status.Equals("Success"))
                {
                    return StatusCode(StatusCodes.Status200OK, _result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, _result);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Something went wrong No User Found" });
            }
        }
        #endregion Task Details
    }
}
