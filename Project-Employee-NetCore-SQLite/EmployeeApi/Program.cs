using EmployeeApi.Middlewares;
using EmployeeApi.Application.Interfaces;
using EmployeeApi.Domain.Services;
using EmployeeApi.Infrastructure.Data;
using EmployeeApi.Infrastructure.Http;
using EmployeeApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using EmployeeApi.ApplicationInterfaces;



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=Employees.db"));

builder.Services.AddScoped<IEmployee, EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();




builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy
            .WithOrigins("http://localhost:3000") 
            .AllowAnyHeader()
            .AllowAnyMethod());
});



builder.Services.AddHttpClient<IDepartmentClient, DepartmentClient>(Client =>
{
    Client.BaseAddress = new Uri("https://localhost:7025/");
});
builder.Services.AddScoped<EmployeeService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.UseCors("AllowReact");
app.UseAuthorization();





app.Run();



