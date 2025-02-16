using Microsoft.AspNetCore.Http;

namespace RealEstate.Application.Models
{
    public class ResponseBaseModel
    {
        /// <summary>
        /// Response Status code. Bad Request (400) by default.
        /// </summary>
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        /// <summary>
        /// Response success status. false by default.
        /// </summary>
        public bool Success { get; set; } = false;
        /// <summary>
        /// Response message displayed for developers.
        /// </summary>
        public string? RawResponse { get; set; }
        /// <summary>
        /// Response message displayed for users.
        /// </summary>
        public string? UserMessage { get; set; }
    }

    public abstract class BaseResponseModel<T> : ResponseBaseModel where T : BaseResponseModel<T>, new()
    {
        public static T BadRequest(string message)
        {
            return new T
            {
                Success = false,
                UserMessage = message,
            };
        }

        public static T Ok(string message)
        {
            return new T
            {
                Success = true,
                UserMessage = message
            };

        }
    }
}
