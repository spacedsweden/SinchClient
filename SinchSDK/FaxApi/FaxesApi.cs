using System.Text;
using Sinch.Models;
using Sinch.FaxApi.Models;
using System.Security.Cryptography;

namespace Sinch.FaxApi
{
    public class FaxesApi
    {
        private readonly HttpClient httpClient;
        private readonly string projectId;
        private string baseUrl = @"https://fax.us1tst.fax-api.staging.sinch.com/v3/projects/{0}/";
        private Faxes faxes;
        public FaxesApi(HttpClient httpClient, string projectId)
        {
            this.httpClient = httpClient;
            this.projectId = projectId;
            baseUrl = string.Format(baseUrl, projectId);
            this.faxes = new Faxes(httpClient, projectId);
            httpClient.BaseAddress = new Uri(string.Format(baseUrl, projectId));
        }

        
        public Faxes Faxes
        {
            get
            {
                if (faxes == null)
                    faxes = new Faxes(httpClient, projectId);
                return new Faxes(httpClient, projectId);
            }
        }
        public Services Services { get; set; }

    }
}