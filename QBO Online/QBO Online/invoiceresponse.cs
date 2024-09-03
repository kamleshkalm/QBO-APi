using System.ComponentModel.DataAnnotations;

namespace QBO_Online
{   
    public class InvoiceDto
    {
        [Required]
        public List<LineItemDto>? Line { get; set; }
        [Required]
        public CustomerReference? CustomerRef { get; set; }
    }

    public class LineItemDto
    {
        public string? DetailType { get; set; }
        public decimal Amount { get; set; }
        public SalesItemLineDetailsAll? SalesItemLineDetail { get; set; }
    }

    public class SalesItemLineDetailsAll
    {
        public ItemReference? ItemRef { get; set; }
    }

    public class ItemReference
    {
        public string? value { get; set; }
        public string? name { get; set; }
    }

    public class CustomerReference
    {
        public string? value { get; set; }
        public string? name { get; set; }
    }

    public class CreateInvoiceRequest
    {
        public InvoiceDto InvoiceDto { get; set; }
        public string AccessToken { get; set; }
    }

    public class updatedInvoiceData
    {
        public string AccessToken { get; set; }
        public decimal Amount { get; set; }
        public string Product_name { get; set; }
        public string product_price { get; set; }
        public string Customer_ref { get; set;}
        //public string Customer_name { get; set;}
        //public string Email { get; set;}

    }
}
