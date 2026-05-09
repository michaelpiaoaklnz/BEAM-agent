using BeamApi.Models.Responses;
using System.Collections.Generic;

namespace BeamApi.Services;

public class CustomersService
{
    public ApiResponse<object> Delete(string customerId, bool hasRelatedOrders, bool hasActiveDisputes)
    {
        if (hasActiveDisputes)
        {
            var result = new
            {
                customerId,
                profileDeleted = false,
                relatedOrdersArchived = false,
                futureInvoicesBlocked = false,
                deletionBlocked = true
            };

            return ApiResponse<object>.Success(result, "Deletion blocked due to active disputes");
        }

        var profileDeleted = true;
        var relatedOrdersArchived = hasRelatedOrders;
        var futureInvoicesBlocked = hasRelatedOrders;

        var result = new
        {
            customerId,
            profileDeleted,
            relatedOrdersArchived,
            futureInvoicesBlocked,
            deletionBlocked = false
        };

        return ApiResponse<object>.Success(result, "Customer deleted successfully");
    }
}
