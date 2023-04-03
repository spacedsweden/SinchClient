using Sinch.FaxApi.Models;

namespace Sinch.FaxApi
{
    public class ListFaxResponse: PagingResponse
    {
        
        public IEnumerable<Fax> Faxes { get; set; }    

    }
}