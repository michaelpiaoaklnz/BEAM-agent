using BeamApi.Models.Requests;
using BeamApi.Models.Responses;

namespace BeamApi.Services;

public class ContactService
{
    public ApiResponse<object> Update(ContactUpdateRequest request)
    {
        var result = new
        {
            customerId = request.CustomerId,
            email = request.Email,
            phoneNumber = request.PhoneNumber,
            status = "Updated"
        };

        return ApiResponse<object>.Success(result, "Contact details updated successfully");
    }
}
