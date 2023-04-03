using Sinch.FaxApi.Models;
using Sinch.Models;
using Sinch.Utils;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public async Task<ListFaxResponse> List(ListOptions listOptions)
        {
            var url = $"/faxes/";
            listOptions.ToDictionary().ToList().ForEach(x => url += $"{x.Key}={Uri.EscapeDataString(x.Value)}&");
            try
            {
                var response = await httpClient.GetFromJsonAsync<ListFaxResponse>(url);
                return response;
            }
            catch (Exception)
            {

                throw new Exception("Failed to fetch faxes");
            }
        }

        public async Task<bool> Delete(string id)
        {
            var url = $"/faxes/{id}";
            var response = await httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
        public async Task<Fax> Get(string id)
        {
            var url = $"/faxes/{id}";
            try
            {
                var response = await httpClient.GetFromJsonAsync<Fax>(url, SinchClient.JsonSerializerOptions);

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get fax", ex);
            }

        }

        /// <summary>
        /// Dowloads and saves the pdf file to the specified path
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> DownloadPdf(string id, string filename)
        {
            var stream = await DownloadPdf(id);
            using (var fileStream = File.Create(filename))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                stream.Close();
            }
            return filename;
        }
        /// <summary>
        /// Returns a stream with the pdf file 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Stream> DownloadPdf(string id)
        {
            var url = $"/faxes/{id}/file.pdf";
            try
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<Error>(SinchClient.JsonSerializerOptions);
                    throw new Exception(error.Message);
                }



            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get fax", ex);
            }

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
            var url = $"faxes";
            var content = new MultipartFormDataContent();
            if (file != null)
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentException("fileName is required when file is provided");
                content.Add(new StreamContent(file), "file", fileName);
            }
            foreach (var data in fax.ToDictionary())
            {
                content.Add(new StringContent(data.Value), data.Key);
            }
            var result = await httpClient.PostAsync(url, content);
            if (result.IsSuccessStatusCode)
            {
                var faxResponse = await result.Content.ReadFromJsonAsync<Fax>(SinchClient.JsonSerializerOptions);
                return faxResponse;
            }
            else
            {
                var error = await result.Content.ReadFromJsonAsync<Error>(SinchClient.JsonSerializerOptions);
                throw new Exception(error.Status + ": " + error.Message + "\n full error message:\n" + JsonSerializer.Serialize(error.Details, SinchClient.JsonSerializerOptions));
            }

        }
    }
}