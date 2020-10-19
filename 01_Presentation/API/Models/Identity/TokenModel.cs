namespace API.Models.Identity
{
    public class TokenModel
    {
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public UserModel User { get; set; }

        public TokenModel() { }

        public TokenModel(string created, string expiration, string accessToken, UserModel user)
        {
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            User = user;
        }
    }
}