using BarberShop.Application.AutoMapper;
using BarberShop.Application.UseCases.Faturas.Delete;
using BarberShop.Application.UseCases.Faturas.GetAll;
using BarberShop.Application.UseCases.Faturas.GetById;
using BarberShop.Application.UseCases.Faturas.Register;
using BarberShop.Application.UseCases.Faturas.Reports.Excel;
using BarberShop.Application.UseCases.Faturas.Reports.Pdf;
using BarberShop.Application.UseCases.Faturas.Update;
using BarberShop.Application.UseCases.Users.ChangePassW;
using BarberShop.Application.UseCases.Users.Delete;
using BarberShop.Application.UseCases.Users.GetById;
using BarberShop.Application.UseCases.Users.Login;
using BarberShop.Application.UseCases.Users.Register;
using BarberShop.Application.UseCases.Users.Update;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Application;

public static class DependecyInjectionEstension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }


    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapping>());
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterFaturaUseCase, RegisterFaturaUseCase>();
        services.AddScoped<IGetAllFaturasUseCase, GetAllFaturasUseCase>();
        services.AddScoped<IGetFaturaById, GetFaturaById>();
        services.AddScoped<IDeleteFaturaUseCase, DeleteFaturaUseCase>();
        services.AddScoped<IUpdateFaturaUseCase, UpdateFaturaUseCase>();
        services.AddScoped<IGenerateReportExcelUseCase, GenerateReportExcelUseCase>();
        services.AddScoped<IGenerateReportPdfUseCase, GenerateReportPdfUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, LoginUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUserCase>();
        services.AddScoped<IChangePassWordUseCase, ChangePassWordUseCase>();
    }

}
