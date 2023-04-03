using Sinch.FaxApi;
using System.Text.Json;

namespace Sinch
{   
    //since we dont have  common way of naming regions each product will have to map urls to this enum.
    //Company mandate to deply world wide so we have to support all regions. Question is if we should default to US if there is a non supported region by a product
    public enum SinchRegion { US_EAST, US_WEST,  EU, ASIA, AU, UK, DE}
    public class SinchClient
    {
        private readonly string projectId;
        private readonly string key;
        private readonly string secret;
        private readonly HttpClient httpClient;
        private SinchRegion defaultRegion;
        private string baseUrl = "";
        private FaxesApi _faxes;
        internal static JsonSerializerOptions JsonSerializerOptions = new System.Text.Json.JsonSerializerOptions
                    {
                        WriteIndented = true,
                        AllowTrailingCommas = true,
                        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                    };
        private Smses _smses;

        /// <summary>
        /// Create a instance of the sinch client
        /// </summary>
        /// <param name="projectId">default proejct id to work with</param>
        /// <param name="key">Api key from dashboard</param>
        /// <param name="secret">ap secret from dashboard</param>
        public SinchClient(string projectId, string key, string secret)
        {
            this.projectId = projectId;
            this.key = key;
            this.secret = secret;
            this.httpClient = new HttpClient();
            var authenticationString = $"{key}:{secret}";
            //authenticationString = string.Format(authenticationString, key, secret);
            //todo add oath to http client
            var base64string = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            this.httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64string);

        }
        public SinchClient(string projectId, string key, string secret, SinchRegion defaultRegion) : this(projectId, key, secret)
        {

            this.defaultRegion = defaultRegion;

        }
        public SinchClient(string projectId, string key, string secret, SinchRegion region, string baseUrl) : this(projectId, key, secret, region)
        {
            this.baseUrl = baseUrl;

        }

        public Faxes Faxes
        {
            get
            {
                if (_faxes == null)
                {
                    _faxes = new Faxes(httpClient, projectId);
                }
                return _faxes;
            }
        }

        public Smses Smses
        {
            get
            {
                if (_smses == null)
                {
                    _smses = new Smses();
                }
                return _smses;
            }
        }

    }
}