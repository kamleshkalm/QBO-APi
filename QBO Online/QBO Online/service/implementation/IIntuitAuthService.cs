namespace QBO_Online.service.implementation
{
    public interface IIntuitAuthService
    {
        string GetAuthorizationUrl();
        Task<TokenResponse> GetAccessTokenAsync(string authorizationCode);
        Task<string> GetCompanyInfoAsync(string accessToken, string companyId);
        Task<string> GetDailyUpdatesAsync(string accessToken, string companyId);
        Task<byte[]> GetInvoicePdfAsync(string token,string invoiceId);

    }
}
