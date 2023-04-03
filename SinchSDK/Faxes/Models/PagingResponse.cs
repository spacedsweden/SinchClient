using Sinch.FaxApi.Models;

namespace Sinch.FaxApi.Models
{
    public class PagingResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItems / (double)PageSize);
            }
        }
        public int TotalItems { get; set; }

    }



    public class FaxListResponse : PagingResponse
    {
        public Fax[]? Faxes { get; set; }
    }

    public class NumbersListResponse : PagingResponse
    {
        public Number[]? PhoneNumbers { get; set; }
    }

    public class Number
    {
    }

    public class WebHookListResponse : PagingResponse
    {
        public Webhook[]? Webhooks { get; set; }
    }

    public class Webhook
    {
    }
}
