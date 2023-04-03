using Sinch.FaxApi.Models;
using Sinch.SmsApi.Models;

namespace Sinch
{
    public class ListSmsResponse : PagingResponse
    {
        public IEnumerable<Sms> Smses { get; set; }
    }
}