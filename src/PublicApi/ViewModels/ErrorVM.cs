using Newtonsoft.Json;

namespace PublicApi.ViewModels
{
    public class ErrorVM
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Path { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}