using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreateLanguage;
using LingoLabs.Infrastructure.Data;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructureToDI(builder.Configuration);
builder.Services.AddMediatR(new[] { typeof(CreateLanguageCommandHandler).Assembly });

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

