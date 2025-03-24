using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TicketTracking.Api.Filters;
using TicketTracking.Api.Handlers;
using TicketTracking.Domain.Interfaces;
using TicketTracking.Core.Mapping;
using TicketTracking.Core.Services;
using TicketTracking.Core.Services.Interfaces;
using TicketTracking.Core.Validators;
using TicketTracking.Infrastructure;
using TicketTracking.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TicketTrackingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SupportConnection")));

builder.Services.AddMapsterConfiguration();

builder.Services.AddScoped<IReferenceDataService, ReferenceDataService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IReplyService, ReplyService>();
builder.Services.AddScoped<IReplyRepository, ReplyRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IReferenceDataRepository, ReferenceDataRepository>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssemblyContaining<TicketRequestDtoValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJS", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//app.UseHttpsRedirection();
app.UseCors("AllowNextJS");

app.UseAuthorization();

app.MapControllers();

app.Run();
