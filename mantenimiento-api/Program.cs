using AutoMapper;
using mantenimiento_api.Controllers.Profile;
using mantenimiento_api.Models;
using mantenimiento_api.Services;
using mantenimiento_api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MantenimientoApiContext>(
    options => options.UseSqlServer("name=ConnectionStrings:MantenimientoApi"));
builder.Services.AddControllersWithViews();

//Servicios
var mapperConfig = new MapperConfiguration((m) => {
    m.AddProfile(new WorkOrderProfile());
    m.AddProfile(new UserProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMvc();

builder.Services.AddScoped<IWorkOrdersServices, WorkOrdersServices>();
builder.Services.AddScoped<IUsersServices, UsersServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();

//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

////Mapping
builder.Services.AddAutoMapper(typeof(WorkOrderProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
