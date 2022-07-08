using GraduateProject.httpModels.api.response;
using GraduateProject.models;
using GraduateProject.services;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.webApp.controllers;

[Controller]
[Route("history")]
public class HistoryController : Controller

{
    private IUserService _userService;

    private ITaskService _taskService;

    // GET
    public HistoryController(IUserService userService, ITaskService taskService)
    {
        _userService = userService;
        _taskService = taskService;
    }

    public IActionResult Index()
    {
        var user = CheckSession();
        if (user == null) return RedirectToAction("SignIn", "Account");

        var tasks = _taskService.GetTaskList(user: user, out _);
        ViewBag.tasks = tasks;
        return View("History");
    }

    private User? CheckSession()
    {
        var session = Request.Cookies["session"];


        var user = _userService.CheckAuthentication(session);
        if (user == null)
        {
            ViewBag.IsSigned = false;
            return null;
        }

        ViewBag.IsSigned = true;
        ViewBag.Name = user.FirstName + " " + user.LastName;
        return user;
    }
}