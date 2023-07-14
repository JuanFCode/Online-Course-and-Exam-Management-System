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


// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {

            // Si quisiera que solo fuera permitido para un solo domino espesifico seria 
            // builder.WithOrigins("www.juanjimenez.com")
            builder.AllowAnyOrigin() // Esto permite que puedan acceder a cualquier dominio
                   .AllowAnyMethod()
                   .AllowAnyHeader();
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

app.UseAuthorization();

// Habilitar CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
