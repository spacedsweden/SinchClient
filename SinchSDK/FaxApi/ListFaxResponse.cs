using Sinch.FaxApi.Models;

namespace Sinch.FaxApi
{
    public class ListFaxResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<Fax> Faxes { get; set; }    

    }
}