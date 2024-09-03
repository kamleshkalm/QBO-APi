using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QBO_Online.service;
using QBO_Online.service.implementation;

namespace QBO_Online.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IIntuitAuthService _authService;

        public HomeController(IIntuitAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet("authorize")]
        public IActionResult Authorize()
        {
            var authUrl = _authService.GetAuthorizationUrl();
            return Redirect(authUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code)
        {
            var accessToken = await _authService.GetAccessTokenAsync(code);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Domain = "localhost"
            };
            Response.Cookies.Append("access_token", accessToken.AccessToken, cookieOptions);
            // Store access token securely (e.g., in a database or secure storage)
            var redirectUrl = "http://localhost:4200/home"; // Enter your url which page you want to go. 
            return Redirect(redirectUrl);
        }

        [HttpGet("companyinfo")]
        public async Task<IActionResult> GetCompanyInfo([FromQuery] string accessToken)
        {
            var companyId = "9341452858998543";// Enter your realmId which you have inside of QBO App. 
            var companyInfo = await _authService.GetCompanyInfoAsync(accessToken, companyId);
            return Ok(companyInfo);
        }
        [HttpGet("dailyupdates")]
        public async Task<IActionResult> GetDailyUpdates([FromQuery] string accessTokenfromcallback)
        {
            var accessToken = accessTokenfromcallback; 
            var realmId = "9341452858998543"; // Enter your realmId which you have inside of QBO App. 

            var updates = await _authService.GetDailyUpdatesAsync(accessToken, realmId);
            return Content(updates, "application/xml");
        }
        [HttpGet("pdf")]
        public async Task<IActionResult> GetInvoicePdf([FromQuery] string token,string invoiceId)
        {
            try
            {
                var pdfBytes = await _authService.GetInvoicePdfAsync(token,invoiceId);
                return File(pdfBytes, "application/pdf", $"invoice_{invoiceId}.pdf");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching PDF: {ex.Message}");
            }
        }       
    }
}
