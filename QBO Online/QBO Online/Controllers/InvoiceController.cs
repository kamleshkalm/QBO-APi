using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QBO_Online.service.implementation;
using System.Net.Http.Headers;
using System.Text;

namespace QBO_Online.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("createInvoice")]
        public async Task<IActionResult> CreateAndSendInvoice([FromBody] updatedInvoiceData invoiceDto)
        {
            try
            {
                var companyId = "9341452858998543";// Enter your realmId which you have inside of QBO App. 
                var invoiceResponse = await _invoiceService.CreateInvoiceAsync(invoiceDto.AccessToken, companyId,invoiceDto);
                var invoiceId = ExtractInvoiceId(invoiceResponse);
                return Ok("Invoice created successfully.");
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }       

        private string ExtractInvoiceId(string invoiceResponse)
        {
            // Parse the JSON response to get the InvoiceId
            dynamic invoice = JObject.Parse(invoiceResponse);
            return invoice.Id.ToString();
        }

    }
}
