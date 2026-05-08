using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class CustomersService
{
    public ApiResponse<object> Delete(CustomerDeleteRequest request)
    {
        // Original T28 behavior:
        // deleting a customer removes customer profile only.
        return ApiResponse<object>.Success(
            new
            {
                customerId = request.CustomerId,
                profileDeleted = true,
                relatedOrdersArchived = false,
                futureInvoicesBlocked = false,
                deletionBlocked = false
            },
            "Customer deleted");
    }
}