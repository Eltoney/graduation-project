using GraduateProject.httpModels;
using GraduateProject.httpModels.requests;
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

    /// <summary>
    /// It's a post request for creating a task to analyse the picture.
    /// It starts with checking the session token which this time sent as cookie which its the best way to add session token to request, then check the file in several ways to ensure the uploaded file is image
    /// Then Save the image and copy it to images folder then notify the python  API with the task 
    /// </summary>
    /// <param name="model">The Uploaded Model</param>
    /// <returns>Returns Response whether success or fail</returns>
    [HttpPost("CreateTask")]
    public async Task<Response> CreateTask([FromForm] UploadImageModel model)
    {
        var file = model.Image;
        var sessionToken = model.SessionToken;
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

    [HttpGet("CheckState/{id}")]
    public Response CheckState(int id)
    {
        var sessionToken = Request.Cookies["sessionToken"];
        var user = _userService.CheckAuthentication(sessionToken);

        if (user == null)
        {
            return new Response()
            {
                IsSuccess = false,
                Message = "Not Authorized"
            };
        }

        return new Response();
    }

    [HttpPost]
    [HttpGet]
    public IActionResult Index()
    {
        return BadRequest();
    }
}