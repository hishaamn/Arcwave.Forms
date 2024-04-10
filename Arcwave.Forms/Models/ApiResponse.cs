namespace Arcwave.Forms.Models
{
    public class ApiResponse<T> where T : class
    {
        public string StatusCode { get; set; }

        public T Result { get; set; }
    }
}