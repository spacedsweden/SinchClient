using Sinch.FaxApi.Models;

namespace Sinch
{
    public class ListServiceResponse : PagingResponse
    {
        public IEnumerable<Service> Services { get; set; }
    }
}