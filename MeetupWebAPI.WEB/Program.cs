using AutoMapper;
using Newtonsoft.Json;
using MeetupWebAPI.BLL.Interfaces;
using MeetupWebAPI.BLL.Profiles;
using MeetupWebAPI.BLL.Services;
using MeetupWebAPI.DAL.EFCore;
using MeetupWebAPI.DAL.Interfaces;
using MeetupWebAPI.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MeetupWebAPI.WEB.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MeetupWebAPI.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<MeetupDbContext>(
    options => options.UseNpgsql(connectionString)
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IRepositoryBase<Meetup>,RepositoryBase<Meetup>>();
builder.Services.AddTransient<IRepositoryBase<User>, RepositoryBase<User>>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IMeetupRepository, MeetupRepository>();



var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MeetupProfile());
    mc.AddProfile(new UserProfile());
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddControllers().AddNewtonsoftJson(
    options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    }
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<JwtAuthorizationMiddleware>();

app.MapControllers();

app.Run();
