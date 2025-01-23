namespace SampleSentry.API.Common.Base
{
    public class BaseResponse
    {
        public object? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
