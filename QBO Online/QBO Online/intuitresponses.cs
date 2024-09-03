namespace QBO_Online
{
    using Newtonsoft.Json;

    public class IntuitResponse
    {
        [JsonProperty("?xml")]
        public XmlInfo Xml { get; set; }

        [JsonProperty("IntuitResponse")]
        public IntuitResponseData IntuitResponseData { get; set; }
    }

    public class XmlInfo
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("@encoding")]
        public string Encoding { get; set; }

        [JsonProperty("@standalone")]
        public string Standalone { get; set; }
    }

    public class IntuitResponseData
    {
        [JsonProperty("@xmlns")]
        public string XmlNamespace { get; set; }

        [JsonProperty("@time")]
        public string Time { get; set; }

        [JsonProperty("QueryResponse")]
        public QueryResponse QueryResponse { get; set; }
    }

    public class QueryResponse
    {
        [JsonProperty("@startPosition")]
        public string StartPosition { get; set; }

        [JsonProperty("@maxResults")]
        public string MaxResults { get; set; }

        [JsonProperty("@totalCount")]
        public string TotalCount { get; set; }

        [JsonProperty("Invoice")]
        public List<Invoice> Invoices { get; set; }
    }

    public class Invoice
    {
        [JsonProperty("@domain")]
        public string Domain { get; set; }

        [JsonProperty("@sparse")]
        public string Sparse { get; set; }

        public string Id { get; set; }
        public string SyncToken { get; set; }

        [JsonProperty("MetaData")]
        public MetaData MetaData { get; set; }

        public string DocNumber { get; set; }
        public string TxnDate { get; set; }

        [JsonProperty("CurrencyRef")]
        public CurrencyRef CurrencyRef { get; set; }

        [JsonProperty("LinkedTxn")]
        public LinkedTxn LinkedTxn { get; set; }

        [JsonProperty("Line")]
        public List<Line> Lines { get; set; }

        public string TotalAmt { get; set; }
    }

    public class MetaData
    {
        public string CreateTime { get; set; }
        public string LastUpdatedTime { get; set; }
    }

    public class CurrencyRef
    {
        [JsonProperty("@name")]
        public string Name { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class LinkedTxn
    {
        public string TxnId { get; set; }
        public string TxnType { get; set; }
    }

    public class Line
    {
        public string Id { get; set; }
        public string LineNum { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }

        [JsonProperty("LinkedTxn")]
        public LinkedTxn LinkedTxn { get; set; }

        [JsonProperty("DetailType")]
        public string DetailType { get; set; }

        [JsonProperty("SalesItemLineDetail")]
        public SalesItemLineDetail SalesItemLineDetail { get; set; }
    }

    public class SalesItemLineDetail
    {
        [JsonProperty("ItemRef")]
        public ItemRef ItemRef { get; set; }

        public string UnitPrice { get; set; }
        public string Qty { get; set; }

        [JsonProperty("TaxCodeRef")]
        public string TaxCodeRef { get; set; }
    }

    public class ItemRef
    {
        [JsonProperty("@name")]
        public string Name { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

}
