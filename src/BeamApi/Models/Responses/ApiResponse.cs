namespace BeamApi.Models.Responses;

public class ApiResponse<T>
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T? data, string message) =>
        new()
        {
            Succeeded = true,
            Message = message,
            Data = data,
            Errors = null
        };

    public static ApiResponse<T> Failure(List<string> errors, string message) =>
        new()
        {
            Succeeded = false,
            Message = message,
            Errors = errors,
            Data = default
        };
}