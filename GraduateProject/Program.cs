using GraduateProject.contexts;
using GraduateProject.models;
using GraduateProject.services;
using GraduateProject.utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



{
    var services = builder.Services;
    
    services.AddDbContext<DetectionProjectContext>(optionsAction =>
    {
        optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("databaseConnection"));
    });

    services.AddCors();

    //use Swagger
    services.AddControllers();
    services.AddSwaggerGen();

    //Setup Configuration
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    //Scopes Area; using for add services to controllers 
    {
        services.AddScoped<IUserService, IUserService.UserService>();
    }
}

var app = builder.Build();
//app.Host.ConfigureWebHostDefaults(b => { b.ConfigureKestrel(sb => { sb.AddServerHeader = false; }); });
//Using Swwagger
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// configure HTTP request pipeline
{
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // app.UseResponseCaching();
    // app.UseResponseCompression();
    // app.UseStaticFiles();
    //
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



app.Run();