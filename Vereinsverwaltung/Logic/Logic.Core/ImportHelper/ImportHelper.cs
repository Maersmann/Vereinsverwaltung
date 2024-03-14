using Base.Logic.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.ImportHelper
{
    public class ImportHelper
    {
        public async Task<HttpResponseMessage> PostFile(string filename)
        {
            HttpRequestMessage message = new HttpRequestMessage();
            MultipartFormDataContent content = new MultipartFormDataContent();
            var stream = File.Open(filename, FileMode.Open, FileAccess.Read);
            content.Add(new StreamContent(stream), "file", filename);
            message.Method = HttpMethod.Post;
            message.Content = content;
            message.RequestUri = new Uri(GlobalVariables.BackendServer_URL+ $"/api/Import/Mitglieder/Loading");
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            var client = HttpClientFactory.Create(clientHandler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalVariables.Token);
            return await client.SendAsync(message);
        }

        public async Task<HttpResponseMessage> PostSchnurschiessenFile(string fileName)
        {
            HttpRequestMessage message = new HttpRequestMessage();
            MultipartFormDataContent content = new MultipartFormDataContent();
            var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            content.Add(new StreamContent(stream), "file", fileName);
            message.Method = HttpMethod.Post;
            message.Content = content;
            message.RequestUri = new Uri(GlobalVariables.BackendServer_URL + $"/api/Import/schnurschiessen/Loading");
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            var client = HttpClientFactory.Create(clientHandler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalVariables.Token);
            return await client.SendAsync(message);
        }
    }
}
