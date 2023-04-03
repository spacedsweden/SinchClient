using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Sinch.SmsApi.Models;
using Sinch.Utils;

namespace Sinch
{

    public class Smses
    {
        private readonly HttpClient httpClient; //due to differetn auth we cant reuse the http client
        private readonly string projectId;

        private string baseUrl = @"https://${0}.sms.api.sinch.com/xms/v1/${1}/";
        public Smses()
        {
            httpClient = new HttpClient();
        }


        /// <summary>
        /// Workaround until SMS supports projectId, then we can reuse the http client
        /// </summary>
        /// <param name="serviceUrl">url of serivce<
        /// 
        /// /param>
        /// <param name="servicePlanId"></param>
        /// <param name="smsApiToken"></param>
        /// <example>
        /// configure("http://us.sms.api.sinch.com/xms/v1/diuouadkjfejkjlsefea/", "idfuidusfisudfke!@##")
        /// </example>
        public void Configure(string serviceUrl, string smsApiToken)
        {
            httpClient.BaseAddress = new Uri(serviceUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", smsApiToken);
        }
        #region Send

        public async Task<Sms> Send(string To, string body, string From)
        {
            return await Send(new SmsOptions
            {
                To = new string[] { To },
                Body = body,
                From = From
            });
        }

        public async Task<Sms> Send(SmsOptions options)
        {
            var result = await httpClient.PostAsJsonAsync<SmsOptions>("batches", options);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<Sms>();
            }
            else
            {
                throw new Exception("Unable to send sms" + await result.Content.ReadAsStringAsync());
            }
        }
        #endregion

        #region list messages

        public async Task<ListSmsResponse> List(SmsListOptions listOptions)
        {
            var url = $"/batches/";
            listOptions.ToDictionary().ToList().ForEach(x => url += $"{x.Key}={Uri.EscapeDataString(x.Value)}&");
            try
            {
                var response = await httpClient.GetFromJsonAsync<ListSmsResponse>(url);
                return response;
            }
            catch (Exception)
            {
                throw new Exception("Failed to fetch smses");
            }
        }
        #endregion
    }
}