using BarberShop.API.Filtros;
using BarberShop.API.Middleware;
using BarberShop.API.Token;
using BarberShop.Application;
using BarberShop.Application.UseCases.Faturas.Reports.Pdf.Fonts;
using BarberShop.Infraestrura.Migrations;
using BarberShop.Infraestrura.Security.Token;
using BarberShop.Infraestrutura;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using PdfSharp;
using PdfSharp.Fonts;
using System.Text;


GlobalFontSettings.FontResolver = new FontHelper();
PdfSharpLicense.SetAgplLicense();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text imput below.
                      Example: 'Bearer 12234abcdef'",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
    });
    config.AddSecurityRequirement(openApiDocument =>new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", openApiDocument)] = []
    });
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
builder.Services.AddHttpContextAccessor();

var siginingKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey");
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(siginingKey!))

    };
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await MigrationDatabase();
app.Run();

async Task MigrationDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();

    await DatabaseMigration.MigrationDatabase(scope.ServiceProvider);
}
