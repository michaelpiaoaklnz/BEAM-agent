using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class CustomersService
{
    public ApiResponse<object> Delete(CustomerDeleteRequest request)
    {
        if (request.HasActiveDisputes)
        {
            var blocked = new
            {
                customerId = request.CustomerId,
                profileDeleted = false,
                relatedOrdersArchived = false,
                futureInvoicesBlocked = false,
                deletionBlocked = true
            };

            return new ApiResponse<object>
            {
                Succeeded = false,
                Message = "Customer deletion blocked by active disputes",
                Errors = new List<string> { "Customer deletion is blocked by active disputes." },
                Data = blocked
            };
        }

        var result = new
        {
            customerId = request.CustomerId,
            profileDeleted = true,
            relatedOrdersArchived = request.HasRelatedOrders,
            futureInvoicesBlocked = true,
            deletionBlocked = false
        };

        return ApiResponse<object>.Success(result, "Customer deleted successfully");
    }
}
