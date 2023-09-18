using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Email.MVC
{
    public class EmailService : IEmailService
    {
        protected readonly HttpClient httpClient;

        protected readonly IConfiguration _configuration;

        protected string baseUri;

        public static string ListEmails => "api/email/list";

        public static string SaveEmails => "api/email/save";

        public EmailService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            baseUri = _configuration["EmailUrl"];
        }

        public async Task<List<EmailModel>> ListEmailsAsync()
        {
            string requestUri = string.Format(new Uri(new Uri(baseUri), ListEmails).ToString());

            var jsonResult = await httpClient.GetStringAsync(requestUri);

            return JsonConvert.DeserializeObject<List<EmailModel>>(jsonResult);
        }

        public async Task<EmailModel> SaveEmailAsync(EmailModel model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            string requestUri = string.Format(new Uri(new Uri(baseUri), SaveEmails).ToString());

            HttpResponseMessage httpResponse = await httpClient.PostAsync(requestUri, httpContent);

            if (!httpResponse.IsSuccessStatusCode)
            {
                var error = new { httpResponse.StatusCode, httpResponse.ReasonPhrase };
                var errorJson = JsonConvert.SerializeObject(error);
                throw new HttpRequestException(errorJson);
            }
            var jsonOut = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<EmailModel>(jsonOut);
        }



    }
}
