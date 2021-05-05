using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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
            message.RequestUri = new Uri("https://localhost:5001/api/ImportMitglieder/Mitglieder/Loading");
            var client = HttpClientFactory.Create();
            return await client.SendAsync(message);
        }
    }
}
