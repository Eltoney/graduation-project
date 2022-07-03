using GraduateProject.httpModels.api.requests;
using GraduateProject.httpModels.webAppModels;
using GraduateProject.services;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.webApp.controllers;

[Controller]
[Route("account")]
public class AccountController : Controller
{
    private IUserService _userService;

    public AccountController(IUserService userService)
    {
        this._userService = userService;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("login")]
    public IActionResult SignIn()
    {
        if (CheckIfAlreadySignedIn())
        {
            return RedirectToAction("Index", "Home");
        }

        foreach (var (key, value) in ModelState)
        {
            Console.WriteLine(key + " " + value);
        }

        ModelState.Clear();
        return View("account/SignIn");
    }

    [HttpPost("login")]
    public IActionResult SignIn(SignInModel model)
    {
        if (CheckIfAlreadySignedIn())
        {
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Message = "Please Enter Valid Data";
            return View("account/SignIn");
        }

        var r = _userService.Authenticate(
            new AuthenticateRequest() {UserName = model.UserName, Password = model.Password}, out var m);
        if (r == null)
        {
            ViewBag.Message = m;
            return View("account/SignIn");
        }

        addSession(r.Token);
        return RedirectToAction("Index", "Home");
    }


    [HttpGet("register")]
    public IActionResult Register()
    {
        ModelState.Clear();
        if (CheckIfAlreadySignedIn())
        {
            return RedirectToAction("Index", "Home");
        }

        return View("account/Register");
    }

    [HttpPost("register")]
    public IActionResult Register(AuthenticateRequest request)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Message = "Please Enter All Data Correctly";
            return View("account/Register");
        }

        var result = _userService.Register(request, out var m);
        if (result == null)
        {
            ViewBag.Message = m;
            return View("account/Register");
        }

        addSession(result.Token);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("logout")]
    public IActionResult SignOut()
    {
        if (CheckIfAlreadySignedIn())
        {
            _userService.SignOut(Request.Cookies["session"]);
            Response.Cookies.Append("session", "", new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            });
        }

        return RedirectToAction("Index", "Home");
    }

    private bool CheckIfAlreadySignedIn()
    {
        var sessionId = Request.Cookies["session"];


        return sessionId != null && _userService.CheckAuthentication(sessionId) != null;
    }

    private void addSession(string token)
    {
        Response.Cookies.Append("session", token, new CookieOptions() {Expires = DateTimeOffset.Now.AddHours(7)});
    }


}