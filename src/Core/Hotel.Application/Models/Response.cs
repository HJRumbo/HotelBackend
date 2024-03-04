namespace Hotel.Core.Application.Models
{
    public class Response<T>
    {
        public Response()
        {

        }

        public Response(string message)
        {
            Successed = false;
            Message = message;
        }

        public Response(T data, string? message = null)
        {
            Successed = true;
            Message = message;
            Data = data;
        }

        public bool Successed { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }
    }
}
