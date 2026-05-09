namespace BeamApi.Models.Requests;

public class PaymentCallbackRequest
{
    public string TransactionReference { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public int ExistingLedgerEntries { get; set; }

    public int ExistingNotifications { get; set; }
}
