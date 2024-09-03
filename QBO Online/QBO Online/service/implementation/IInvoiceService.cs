namespace QBO_Online.service.implementation
{
    public interface IInvoiceService
    {
        Task<string> CreateInvoiceAsync(string accessToken, string companyId, updatedInvoiceData invoiceDto);
        Task SendInvoiceAsync(string accessToken, string companyId, string invoiceId);
    }
}
