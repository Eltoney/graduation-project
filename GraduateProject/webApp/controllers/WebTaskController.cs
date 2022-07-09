using GraduateProject.httpModels.webAppModels;
using GraduateProject.models;
using GraduateProject.services;
using GraduateProject.utils;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.webApp.controllers;

[Controller]
[Route("task")]
public class WebTaskController : Controller
{
    private static readonly string[] Exts = {"jpg", "gif", "png"};
    private ITaskService _taskService;

    private IUserService _userService;

    // GET
    public WebTaskController(ITaskService taskService, IUserService userService)
    {
        _taskService = taskService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return CheckSession() == null ? RedirectToAction("SignIn", "Account") : RedirectToAction("Create");
    }

    [HttpGet("createTask")]
    public IActionResult Create()
    {
        if (CheckSession() == null)
            return RedirectToAction("SignIn", "Account");

        return View("CreateTask");
    }

    [HttpPost("createTask")]
    public async Task<IActionResult> Create(CreateTaskModel model)
    {
        var user = CheckSession();
        if (user == null)
        {
            return RedirectToAction("SignIn", "Account");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Message = "Please Upload File";
            return View("CreateTask");
        }

        var file = model.File;
        var name = file.FileName;
        var ext = Path.GetExtension(name);

        if (CommonUtils.CheckImageExt(ext))
        {
            ViewBag.Message = "Sorry.. we accept only images";
            return View("CreateTask");
        }

        var guid = Convert.ToString(Guid.NewGuid());
        var imageLocation = Path.Combine("images", $"{guid}.{ext}");

        await file.CopyToAsync(new FileStream(imageLocation, FileMode.Create, FileAccess.ReadWrite));

        var taskId = _taskService.CreateTask(Path.GetFullPath(imageLocation),model.Gender, user, out string message);
        if (taskId != -1)
        {
            ViewBag.sessionToken=Request.Cookies["session"];
            ViewBag.taskID=taskId;
            ViewBag.SuccessMessage = "Task Added Successfully: Please wait till Detection is done";
        }
        else
            ViewBag.Message = message;

        return View("CreateTask");
    }

    [HttpGet("getTasks")]
    public IActionResult CheckTaskState()
    {
        //TODO: Needs  a UI and will operate with javascript and Check Task API
        return View();
    }

    private User? CheckSession()
    {
        var session = Request.Cookies["session"];

        var user = _userService.CheckAuthentication(session);
        if (user != null) return user;
        Response.Cookies.Delete("session");
        return null;
    }

    private static bool CheckExt(string ext)
    {
        return Exts.Contains(ext);
    }
}