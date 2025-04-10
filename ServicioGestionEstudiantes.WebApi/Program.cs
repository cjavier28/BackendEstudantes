using CapaDatos;
using Microsoft.EntityFrameworkCore;
using SGEU.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("angularApp", builder =>
//    {
//        builder.WithOrigins("http://localhost:4200")
//        //builder.WithOrigins("https://pagos.aliatesp.com:470")
//        .SetIsOriginAllowedToAllowWildcardSubdomains()
//        .AllowAnyMethod()
//        .AllowAnyHeader();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("angularApp",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Adición Contexto de base de datos
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SGEUConnStr")));

// Adición de AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Adición de Servicios
builder.Services.AddScoped<ProgramaService>();
builder.Services.AddScoped<MateriaService>();
builder.Services.AddScoped<EstudianteService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("angularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
