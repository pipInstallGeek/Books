using System.Security.AccessControl;
using static Books_Web.SD;

namespace Books_Web.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        
    }
}
