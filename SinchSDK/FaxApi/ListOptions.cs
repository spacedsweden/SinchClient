using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sinch.FaxApi
{
    public class ListOptions
    {
        /// <summary>
        /// Filter calls based on createTime. You make the query more precise, fewer results will be returned. For example, 2021-02-01 will return all calls from the first of February 2021, and 2021-02-01T14:00:00Z will return all calls 14:00 for the 00 minute on the first of February.
        /// This field also supports <= and >= to search for calls in a range ?createTime>=2021-10-01&createTime>=2021-10-30 to get a list if calls for october 2021 It is also possible to submit partial dates for example createTime = 2021 - 02 will return all calls for February
        /// If not value is submitted, the default value is last 7 days from the current date and time `(Date.UTCNow().addDays(-7) )``.
        /// </summary>
        public string? CreateTime { get; set; }
        public Models.Direction? Direction { get; set; }
        public Models.Status? Status { get; set; }
        [PhoneNumber]
        public string? To { get; set; }
        [PhoneNumber]
        public string? From { get; set; }
        public int PageSize { get; set; } = 100;
        public int Page { get; set; } = 1;
    }
}