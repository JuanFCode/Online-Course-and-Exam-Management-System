global using Microsoft.EntityFrameworkCore;
using Online_Course_and_Exam_Management_System.Models;

var builder = WebApplication.CreateBuilder(args);

//Conexion base de datos
builder.Services.AddEntityFrameworkNpgsql()
           .AddDbContext<PostgresContext>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("CursoExamenConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
