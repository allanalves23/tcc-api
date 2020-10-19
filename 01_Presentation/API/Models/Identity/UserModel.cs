namespace API.Models.Identity
{
    public class UserModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserModel() { }

        public UserModel(string userID, string userName, string password = null)
        {
            UserID = userID;
            UserName = userName;
            Password = password;
        }
    }
}