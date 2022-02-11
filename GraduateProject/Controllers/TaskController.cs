using GraduateProject.httpModels;
using GraduateProject.httpModels.response;
using GraduateProject.services;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : Controller
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost("CreateTask")]
    public Response CreateTask([FromBody] Request request)
    {
        return null;
    }

    [HttpPost("CheckState")]
    public Response CheckState()
    {
    }

    [HttpPost]
    [HttpGet]
    public IActionResult Index()
    {
        return BadRequest();
    }
}