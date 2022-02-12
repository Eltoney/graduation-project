using GraduateProject.httpModels;
using GraduateProject.httpModels.response;
using GraduateProject.services;
using GraduateProject.utils;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : Controller
{
    private readonly ITaskService _taskService;
    private readonly IUserService _userService;


    public TaskController(IUserService userService, ITaskService taskService)
    {
        _userService = userService;
        _taskService = taskService;
    }

    [HttpPost("CreateTask")]
    public async Task<Response> CreateTask(IFormFile file)
    {
        var sessionToken = Request.Cookies["sessionToken"];
        var user = _userService.CheckAuthentication(sessionToken);

        if (user == null)
        {
            System.IO.File.Delete(file.FileName);
            return new Response()
            {
                IsSuccess = false,
                Message = "Not Authorized"
            };
        }

        var ext = Path.GetExtension(file.FileName);
        if (!CommonUtils.CheckStrings(file.ContentType) || CommonUtils.CheckImageExt(ext))
        {
            System.IO.File.Delete(file.FileName);
            return new Response
            {
                IsSuccess = false,
                Message = "Upload File Must Be Image"
            };
        }

        var guid = Convert.ToString(Guid.NewGuid());
        var imageLocation = Path.Combine("images", $"{guid}.{ext}");
        await file.CopyToAsync(new FileStream(imageLocation, FileMode.Create, FileAccess.ReadWrite));
        int taskId = _taskService.CreateTask(imageLocation, user, out string message);
        return new Response()
        {
            IsSuccess = taskId != -1,
            Message = message,
            Result = new CreateTaskResponse() {TaskID = taskId}
        };
    }

    [HttpPost("CheckState")]
    public Response CheckState()
    {
        return null;
    }

    [HttpPost]
    [HttpGet]
    public IActionResult Index()
    {
        return BadRequest();
    }
}