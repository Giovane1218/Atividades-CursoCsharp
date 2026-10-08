using CashFlow.API.Filtros;
using CashFlow.API.Middleware;
using CashFlow.application;
using CashFlow.application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Infraestrutura;
using PdfSharp.Fonts;

GlobalFontSettings.FontResolver = new FontHelper();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddMvc(options => options.Filters.Add(typeof(ExeptionFilter)));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

 var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CashFlow API V1");
        options.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
