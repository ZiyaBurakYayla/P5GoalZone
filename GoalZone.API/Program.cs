using GoalZone.API.Data;
using GoalZone.API.Mappings;
using GoalZone.API.Repositories;
using GoalZone.API.Repositories.InterFaces;
using GoalZone.API.Services;
using GoalZone.API.Services.AuthServices;
using GoalZone.API.Services.RefereeServices;
using GoalZone.API.Services.SeasonServices;
using GoalZone.API.Services.StandingServices;
using GoalZone.API.Services.TeamServices;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GoalZoneDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql =>
        {
            sql.MigrationsAssembly("GoalZone.API");
            sql.CommandTimeout(60);
            sql.EnableRetryOnFailure(3);
        }));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
builder.Services.AddScoped<IRefereeRepository, RefereeRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IStandingService, StandingService>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<IRefereeService, RefereeService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "GoalZone API", Version = "v1" });
});

builder.Services.AddCors(opts =>
    opts.AddPolicy("WebUI", p =>
        p.WithOrigins(
            builder.Configuration["WebUIUrl"] ?? "https://localhost:7002",
            "http://localhost:5002"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errApp =>
{
    errApp.Run(async ctx =>
    {
        var ex = ctx.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        ctx.Response.StatusCode = 500;
        ctx.Response.ContentType = "application/json";
        await ctx.Response.WriteAsJsonAsync(new
        {
            error = ex?.Error?.Message,
            detail = ex?.Error?.InnerException?.Message,
            stack = ex?.Error?.StackTrace
        });
    });
});

app.UseCors("WebUI");
app.UseAuthorization();
app.MapControllers();
app.Run();
