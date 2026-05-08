namespace BeamApi.Models.Requests;

public class PurchaseApprovalRequest
{
    public string PurchaseOrderId { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public bool ManagerApproved { get; set; }

    public bool FinanceApproved { get; set; }
}