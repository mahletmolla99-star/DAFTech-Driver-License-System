namespace DAFTech.DriverLicenseSystem.Api.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }

        public ApiResponse(bool success, string message, T data = default, string error = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Error = error;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> ErrorResponse(string message, string error = null)
        {
            return new ApiResponse<T>(false, message, error: error);
        }
    }
}
