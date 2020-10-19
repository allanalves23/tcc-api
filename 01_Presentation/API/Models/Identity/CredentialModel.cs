namespace API.Models.Identity
{
    public class CredentialModel
    {
        public bool IsOk { get; set; }
        public UserModel User { get; set; }

        public CredentialModel(bool isOk = false, UserModel user = null)
        {
            IsOk = isOk;
            User = user;
        }
    }
}