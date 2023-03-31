using Sinch.FaxApi.Models;
using Sinch.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Sinch.FaxApi
{
    public class Faxes
    {
        private readonly HttpClient httpClient;
        private readonly string projectId;
        public Faxes(HttpClient httpClient, string projectId)
        {
            this.projectId = projectId;
            this.httpClient = httpClient;
        }

        public async Task<bool> Delete(string id)
        {

            return true;
        }
        public async Task<Fax> Get(string id)
        {
            var url = $"/faxes/{id}";
            var response = await httpClient.GetAsync(url);
            
            return new Fax();
        }
        public async Task<Fax> Send(string to, string from, string filePath)
        {
            var faxOptions = new FaxOptions
            {
                To = to,
                From = from,

            };
            var stream = File.Open(filePath, FileMode.Open);
            return await Send(faxOptions, stream, Path.GetFileName(stream.Name));
        }

        public async Task<Fax> Send(string to, string from, Stream file, string fileName)
        {
            var faxOptions = new FaxOptions
            {
                To = to,
                From = from,

            };
            return await Send(faxOptions, file, fileName);
        }

        public async Task<Fax> Send(FaxOptions fax, Stream? file, string? fileName)
        {
            var url = $"/faxes";
            var content = new MultipartFormDataContent();
            if (file != null ) {
                if (string.IsNullOrEmpty(fileName) )
                    throw new ArgumentException("fileName is required when file is provided");
                content.Add(new StreamContent(file), "file", fileName);
            }
            content.Add(new StringContent(JsonSerializer.Serialize(fax, SinchClient.JsonSerializerOptions)), "fax");
            var result = await httpClient.PatchAsync(url, content);
            if (result.IsSuccessStatusCode)
            {
                var faxResponse = await result.Content.ReadFromJsonAsync<Fax>(SinchClient.JsonSerializerOptions);
                return faxResponse;
            }
            else { 
                var error = await result.Content.ReadFromJsonAsync<Error>(SinchClient.JsonSerializerOptions);
                throw new Exception(error.Status + ": " + error.Message +"\n full error message:\n" + JsonSerializer.Serialize(error.Details, SinchClient.JsonSerializerOptions));
            }
            
        }
    }
}