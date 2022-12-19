using AutoMapper;
using mantenimiento_api.Controllers.Profile;
using mantenimiento_api.Models;
using mantenimiento_api.Services;
using mantenimiento_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

//Mapping
builder.Services.AddAutoMapper(typeof(WorkOrderProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
