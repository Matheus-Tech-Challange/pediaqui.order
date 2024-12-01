namespace Pediaqui.Payment.Models;

public enum PaymentStatus
{
    PENDING,
    APPROVED,
    FAIL
}

public static class PaymentExtension
{
    public static PaymentStatus ToPaymentEnum(this string paymentStatus)
    {
        switch (paymentStatus)
        {
            case nameof(PaymentStatus.PENDING):
                return PaymentStatus.PENDING;
            case nameof(PaymentStatus.APPROVED):
                return PaymentStatus.APPROVED;
            case nameof(PaymentStatus.FAIL):
                return PaymentStatus.FAIL;
            default:
                throw new ArgumentOutOfRangeException(nameof(paymentStatus));
        }
    }
}
