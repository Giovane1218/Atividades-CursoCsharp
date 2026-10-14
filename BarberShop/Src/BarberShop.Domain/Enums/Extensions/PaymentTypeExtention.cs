using BarberShop.Domain.Reports;

namespace BarberShop.Domain.Enums.Extensions;

public static class PaymentTypeExtention
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportGenerateMsg.CASH,
            PaymentType.CreditCard => ResourceReportGenerateMsg.CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportGenerateMsg.DEBIT_CARD,
            PaymentType.EletronicTranfer => ResourceReportGenerateMsg.TRANSFER,
            PaymentType.Pix => ResourceReportGenerateMsg.PIX,
            _ => string.Empty
        };
    }
}
