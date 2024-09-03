using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QBO_Online.service.implementation;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace QBO_Online.service
{
    public class IntuitAuthService: IIntuitAuthService
    {
        private readonly IConfiguration _configuration;

        public IntuitAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetAuthorizationUrl()
        {
            var clientId = _configuration["Intuit:ClientId"];
            var redirectUri = _configuration["Intuit:RedirectUri"];
            var state = Guid.NewGuid().ToString();

            var x= $"https://appcenter.intuit.com/connect/oauth2?client_id={clientId}&response_type=code&redirect_uri={redirectUri}&scope=com.intuit.quickbooks.accounting&state={state}";
            return x;
        }

        public async Task<TokenResponse> GetAccessTokenAsync(string authorizationCode)
        {          
            var clientId = _configuration["Intuit:ClientId"];
            var clientSecret = _configuration["Intuit:ClientSecret"];
            var redirectUri = _configuration["Intuit:RedirectUri"];

            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer");

            var base64string = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64string);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", authorizationCode),
                new KeyValuePair<string, string>("redirect_uri", redirectUri)
            });

            request.Content = content;

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                throw new Exception($"Error fetching access token: {errorContent}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            return new TokenResponse
            {
                AccessToken = data.access_token,
                RefreshToken = data.refresh_token,
                ExpiresIn = data.expires_in
            };
        }
        public async Task<string> GetCompanyInfoAsync(string accessToken, string companyId)
        {
            var client = new RestClient($"https://sandbox-quickbooks.api.intuit.com/v3/company/{companyId}/companyinfo/{companyId}?minorversion=73");
            var request = new RestRequest(Method.Get.ToString());
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AddHeader("Accept", "application/json");

            RestResponse response = await client.ExecuteAsync(request);
            return response.Content;
        }
        public async Task<string> GetDailyUpdatesAsync(string accessToken, string realmId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync($"https://sandbox-quickbooks.api.intuit.com/v3/company/{realmId}/query?query=SELECT * FROM Invoice where Metadata.CreateTime > '2024-08-12'");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            //convert xml to json
            //XDocument xmlDocument = XDocument.Parse(content);
            //var xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(content);
            //var json = JsonConvert.SerializeXNode(xmlDocument, Newtonsoft.Json.Formatting.Indented);
            //Convert xml to json
            //XElement xmlElement = XElement.Parse(content);
            //string jsondata = JsonConvert.SerializeXNode(xmlElement);
            //IntuitResponse responsedata = JsonConvert.DeserializeObject<IntuitResponse>(json);
            return content;
        }
        public async Task<byte[]> GetInvoicePdfAsync(string token,string invoiceId)
        {
            var accessToken = token;
            var client = new HttpClient();
            var realmId = "9341452858998543";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
            var requestUrl = $"https://sandbox-quickbooks.api.intuit.com/v3/company/{realmId}/invoice/{invoiceId}/pdf?minorversion=73";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                // Handle error response
                throw new HttpRequestException($"Error fetching PDF: {response.ReasonPhrase}");
            }
        }
    }
}
