namespace API.Models.Identity
{
    public class TokenConfigurationsModel
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}