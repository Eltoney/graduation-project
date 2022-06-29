using GraduateProject.contexts;
using GraduateProject.httpModels;
using GraduateProject.httpModels.api.requests;
using GraduateProject.httpModels.api.response;
using GraduateProject.models;
using GraduateProject.services;
using GraduateProject.utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

[ApiController()]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IUserService _userService;


    public AuthController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("login")]
    public Response Login([FromBody] AuthenticateRequest user)
    {
        var result = _userService.Authenticate(user, out string message);

        return new Response()
        {
            IsSuccess = result != null,
            Result = result,
            Message = message
        };
    }

    [HttpPost("register")]
    public Response Register([FromBody] AuthenticateRequest user)
    {
        Console.WriteLine(user.ToString());

        var result = _userService.Register(user, out string message);
        return new Response()
        {
            Result = result,
            IsSuccess = result != null,
            Message = message
        };
    }

    [HttpGet()]
    [HttpPost()]
    public IActionResult Index()
    {
        return BadRequest();
    }
}