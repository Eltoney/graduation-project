using GraduateProject.contexts;
using GraduateProject.models;
using GraduateProject.services;
using GraduateProject.utils;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

Directory.CreateDirectory("images");
var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;

    services.Configure<RazorViewEngineOptions>(o =>
    {
        o.ViewLocationFormats.Add("/webApp/Pages/{0}" + RazorViewEngine.ViewExtension);
    });


    services.AddDbContext<DetectionProjectContext>(optionsAction =>
    {
        optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("databaseConnection"));
    });

    services.AddCors();

    //use Swagger
    services.AddControllers();
    services.AddRazorPages();
    services.AddSwaggerGen();

    //Setup Configuration
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    {
        services.AddScoped<IUserService, IUserService.UserService>();
        services.AddScoped<ITaskService, TaskService>();
    }
}

var app = builder.Build();

// configure HTTP request pipeline
{
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

     app.MapControllers();
}

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.MapSwagger();
app.UseSwaggerUI();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("api", "api/{controller=Home}/{action=Index}/{id?}");

app.Run();