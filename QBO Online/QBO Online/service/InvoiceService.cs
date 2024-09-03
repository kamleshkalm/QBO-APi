using Newtonsoft.Json;
using QBO_Online.service.implementation;
using System.Net.Http.Headers;
using System.Text;

namespace QBO_Online.service
{
    public class InvoiceService:IInvoiceService
    {
        private readonly HttpClient _httpClient;

        public InvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreateInvoiceAsync(string accessToken, string companyId, updatedInvoiceData invoiceDto)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invoiceDto.AccessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = $"https://sandbox-quickbooks.api.intuit.com/v3/company/{companyId}/invoice?minorversion=73";

            var invoiceData = new InvoiceDto
            {
                Line = new List<LineItemDto>
                {
                    new LineItemDto
                    {
                        DetailType = "SalesItemLineDetail",
                        Amount = invoiceDto.Amount,
                        SalesItemLineDetail = new SalesItemLineDetailsAll
                        {
                            ItemRef = new ItemReference
                            {
                                value = "1",
                                name = invoiceDto.Product_name
                            }
                        }
                    }
                },
                CustomerRef = new CustomerReference
                {
                    value = "11",
                    name = "rahul"
                }
            };
            var invoiceJson = JsonConvert.SerializeObject(invoiceData);

            var content = new StringContent(invoiceJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(requestUri, content);

            if (response.IsSuccessStatusCode)
            {
                var invoiceResponse = await response.Content.ReadAsStringAsync();
                return invoiceResponse; 
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to create invoice. Status code: {response.StatusCode}. Details: {errorContent}");
            }
        }

        public async Task SendInvoiceAsync(string accessToken, string companyId, string invoiceId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUri = $"https://quickbooks.api.intuit.com/v3/company/{companyId}/invoice/{invoiceId}/send";
            var response = await _httpClient.PostAsync(requestUri, null);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to send invoice. Status code: {response.StatusCode}. Details: {errorContent}");
            }
        }
    }
}
