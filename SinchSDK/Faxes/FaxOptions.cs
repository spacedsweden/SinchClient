using Sinch.FaxApi.Models;

namespace Sinch.FaxApi
{
    public class FaxOptions
    {
        public string To { get;  set; }
        public string From { get;  set; }
        public ImageConversionMethod ImageConversionMethod { get; set; }
    }
}