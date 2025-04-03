using LeadManagement.Application.Handlers;
using LeadManagement.Application.Mappings;
using LeadManagement.Domain.Interfaces;
using LeadManagement.Infrastructure.Data;
using LeadManagement.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LeadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(AcceptLeadHandler).Assembly);
builder.Services.AddMediatR(typeof(CreateLeadHandler).Assembly);
builder.Services.AddMediatR(typeof(DeclineLeadHandler).Assembly);
builder.Services.AddAutoMapper(typeof(LeadProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }