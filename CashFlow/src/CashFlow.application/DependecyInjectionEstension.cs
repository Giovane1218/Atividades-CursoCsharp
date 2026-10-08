using CashFlow.application.AutoMapper;
using CashFlow.application.UseCases.Expenses.Delete;
using CashFlow.application.UseCases.Expenses.GetAll;
using CashFlow.application.UseCases.Expenses.GetById;
using CashFlow.application.UseCases.Expenses.Register;
using CashFlow.application.UseCases.Expenses.Reports.Excel;
using CashFlow.application.UseCases.Expenses.Reports.Pdf;
using CashFlow.application.UseCases.Expenses.Update;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.application;

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
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
        services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseuseCase>();
        services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
        services.AddScoped<IGenerateReportExcelUseCase, GenerateReportExcelUseCase>();
        services.AddScoped<IGenerateReportPdfUseCase, GenerateReportPdfUseCase>();
    }
}
