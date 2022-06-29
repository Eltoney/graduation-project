using GraduateProject.services;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.webApp.controllers;

[Controller]
public class HomeController : Controller
{
    private IUserService _userService;

    // GET
    public HomeController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [HttpGet]
    public IActionResult Index()
    {
        CheckSession();
        return View("Index");
    }

    [HttpGet("features")]
    public IActionResult Features()
    {
        CheckSession();
        return View("Features");
    }

    [HttpGet("aboutus")]
    public IActionResult AboutUs()
    {
        CheckSession();
        return View("About");
    }

    [HttpGet("contactus")]
    public IActionResult ContactUs()
    {
        CheckSession();
        return View("ContactUs");
    }

    private void CheckSession()
    {
        var session = Request.Cookies["session"];


        var user = _userService.CheckAuthentication(session);
        if (user == null)
        {
            ViewBag.IsSigned = false;
            return;
        }

        ViewBag.IsSigned = true;
        ViewBag.Name = user.FirstName + " " + user.LastName;
    }
}