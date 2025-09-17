using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public string Error { get; set; }
        // onsuccess
        public static ApiResponse<T> SuccessResponse(T data, string message = "")
        {
            return new ApiResponse<T> { Success = true, Message = message, Data = data };
        }
        // onfail
        public static ApiResponse<T> FailResponse(string errors, string message = "")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Error = errors
            };
        }
    }
}
