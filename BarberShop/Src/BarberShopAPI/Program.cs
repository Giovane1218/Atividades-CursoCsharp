using BarberShop.Application;
using BarberShop.Application.UseCases.Faturas.Reports.Pdf.Fonts;
using BarberShop.API.Filtros;
using BarberShop.API.Middleware;
using BarberShop.Infraestrutura;
using PdfSharp.Fonts;
using PdfSharp;

GlobalFontSettings.FontResolver = new FontHelper();
PdfSharpLicense.SetAgplLicense();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "BarberShop API V1");
        options.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<CultureMiddleware>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
