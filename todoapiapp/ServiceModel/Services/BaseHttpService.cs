using TodoApi.ServiceModel.Flow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace TodoApi.ServiceModel.Services
{
    internal class BaseHttpService<TResult>
        where TResult : new()
    {
        internal List<IPrechekFlow> Prechecks = new List<IPrechekFlow>();

        internal Dictionary<string, string> QueryString { get; set; } = new Dictionary<string, string>();

        internal HttpContent Content { get; set; }

        internal string AccessToken { get; set; }

        internal HttpMethod Method { get; set; } = HttpMethod.Get;

        internal string URL { get; set; } = string.Empty;


        internal async Task<ParseResult<TResult>> InvokeAsync()
        {
            // Handle precheck flow
            try
            {
                foreach (var item in this.Prechecks)
                {
                    var validated = await item.Validate();
                    if (validated.Item1 == false)
                    {
                        return new ParseResult<TResult>()
                        {
                            Error =  validated.Item2
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new ParseResult<TResult>()
                {
                    Error = new ErrorData
                    {
                        Message = ex.Message
                    }
                };
            }

            return await this.CallAPIAsync();
        }

        private async Task<ParseResult<TResult>> CallAPIAsync()
        {
            string apiURL = GenerateRequqestURL();

            Debug.WriteLine(apiURL);

            HttpRequestMessage requestParameter = new HttpRequestMessage(Method, new Uri(apiURL));
            requestParameter.Content = Content;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                if (requestParameter.Method == HttpMethod.Post)
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                using (HttpResponseMessage response = await client.SendAsync(requestParameter))
                {
                    var cryptTask = await response.Content.ReadAsByteArrayAsync();
                    string json = Encoding.UTF8.GetString(cryptTask, 0, cryptTask.Length);

                    Debug.WriteLine(json);

                    ParseResult<TResult> result = new ParseResult<TResult>();

                    if (response.IsSuccessStatusCode)
                    {
                        result.Content = JsonConvert.DeserializeObject<TResult>(json);
                    }
                    else
                    {
                        result.Error = ParseErrorData(json);
                    }

                    return result;
                }
            }
        }

        private string GenerateRequqestURL()
        {
            var uriBuilder = new UriBuilder(URL)
            {
                Query = string.Join("&", QueryString.Select(kvp => $"{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}"))
            };

            return uriBuilder.Uri.AbsoluteUri;
        }

        private ErrorData ParseErrorData(string json)
        {
            try
            {
                var errorJson = JObject.Parse(json);
                var errorToken = errorJson["error"];

                return JsonConvert.DeserializeObject<ErrorData>(errorToken.ToString());
            }
            catch (Exception ex)
            {
                return new ErrorData
                {
                    Message = ex.Message
                };
            }
        }
    }
}