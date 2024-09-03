using Newtonsoft.Json;

namespace QBO_Online
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("x_refresh_token_expires_in")]
        public int RefreshTokenExpiresIn { get; set; }

        [JsonProperty("realmId")]
        public string RealmId { get; set; }
    }

}
