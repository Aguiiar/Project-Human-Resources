using DepartmentApi.Application.Interfaces;
using DepartmentApi.Domain.Services;
using DepartmentApi.Infrastructure.Data;
using DepartmentApi.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=Employees.db"));

//Temos que inserir duas injetions da services aqui para funcionar ela
builder.Services.AddScoped<IDepartment, DepartmentRepository>();
builder.Services.AddScoped<DepartmentService>();


//Para funcionar no front

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy
            .WithOrigins("http://localhost:3000") // URL do frontend
            .AllowAnyHeader()
            .AllowAnyMethod());
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

//app.UseMiddleware<ExceptionMiddleware>();

//Temos que inserir para nossas middleware funcionarem depois do var app que está acima
//app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
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



