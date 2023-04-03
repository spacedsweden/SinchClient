using Sinch.FaxApi;
using Sinch.FaxApi.Models;
using Sinch.Models;
using Sinch.Utils;
using System.Net.Http.Json;
using System.Text.Json;

namespace Sinch
{
    public class Faxes
    {
        private readonly HttpClient httpClient;
        private readonly string projectId;
        private string baseUrl = @"https://fax.us1tst.fax-api.staging.sinch.com/v3/projects/{0}/";
        public Faxes(HttpClient httpClient, string projectId)
        {
            this.projectId = projectId;
            this.httpClient = httpClient;
            this.httpClient = httpClient;
            this.projectId = projectId;
            baseUrl = string.Format(baseUrl, projectId);
            httpClient.BaseAddress = new Uri(string.Format(baseUrl, projectId));
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
        /// <summary>
        /// Send a fax with and adds the file to the fax, make sure the file exists where the sdk can access it.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Send a fax with a stream as fax
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<Fax> Send(string to, string from, Stream file, string fileName)
        {
            var faxOptions = new FaxOptions
            {
                To = to,
                From = from,

            };
            return await Send(faxOptions, file, fileName);
        }

        /// <summary>
        /// Teh most fleixble method to send a fax, you can add any fax options and a file to the fax
        /// </summary>
        /// <param name="fax"></param>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
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


        #region Services

        public async Task<Service> GetService(string Id)
        {
            var url = $"services/{Id}";
            return await httpClient.GetFromJsonAsync<Service>(url, SinchClient.JsonSerializerOptions);
        }
        public async Task<ListServiceResponse> GetServices()
        {
            var url = $"services";
            var result = await httpClient.GetFromJsonAsync<ListServiceResponse>(url, SinchClient.JsonSerializerOptions);
            return result;
        }
        public async Task<Service> CreateService(Service service)
        {
            var url = $"services";
            var result = await httpClient.PostAsJsonAsync(url, service);
            if (result.IsSuccessStatusCode)
            {
                var serviceResponse = await result.Content.ReadFromJsonAsync<Service>(SinchClient.JsonSerializerOptions);
                return serviceResponse;
            }
            else
            {
                var error = await result.Content.ReadFromJsonAsync<Error>(SinchClient.JsonSerializerOptions);
                throw new Exception(error.Status + ": " + error.Message + "\n full error message:\n" + JsonSerializer.Serialize(error.Details, SinchClient.JsonSerializerOptions));
            }
        }

        public async Task<Service> UpdateService(Service service)
        {
            var url = $"services/{service.Id}";
            var result = await httpClient.PutAsJsonAsync(url, service, SinchClient.JsonSerializerOptions);
            if (result.IsSuccessStatusCode)
            {
                var serviceResponse = await result.Content.ReadFromJsonAsync<Service>(SinchClient.JsonSerializerOptions);
                return serviceResponse;
            }
            else
            {
                var error = await result.Content.ReadFromJsonAsync<Error>(SinchClient.JsonSerializerOptions);
                throw new Exception(error.Status + ": " + error.Message + "\n full error message:\n" + JsonSerializer.Serialize(error.Details, SinchClient.JsonSerializerOptions));
            }
        }

        /// <summary>
        /// Delete the specified services
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteService(string Id)
        {
            var url = $"services/{Id}";
            var result = await httpClient.DeleteAsync(url);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var error = await result.Content.ReadFromJsonAsync<Error>(SinchClient.JsonSerializerOptions);
                throw new Exception(error.Status + ": " + error.Message + "\n full error message:\n" + JsonSerializer.Serialize(error.Details, SinchClient.JsonSerializerOptions));
            }
        }

        #endregion
    }
}