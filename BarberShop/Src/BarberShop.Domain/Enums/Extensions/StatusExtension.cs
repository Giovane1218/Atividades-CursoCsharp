using BarberShop.Domain.Reports;

namespace BarberShop.Domain.Enums.Extensions;

public static class StatusExtension
{
    public static string StatusToString(this Status status)
    {
        return status switch
        {
            Status.Pago => ResourceReportGenerateMsg.PAID,
            Status.Pendente => ResourceReportGenerateMsg.PENDING,
            Status.Cancelado => ResourceReportGenerateMsg.CANCEL_PAY,
            _ => string.Empty
        };
    }
}
