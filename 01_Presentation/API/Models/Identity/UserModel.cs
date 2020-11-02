using System;

namespace API.Models.Identity
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ProfileAccess { get; set; }

        public UserModel() { }

        public UserModel(string id, string userID, string userName, string profileAccess, string password = null)
        {
            Id = id;
            UserID = userID;
            UserName = userName;
            Password = password;
            ProfileAccess = profileAccess.ToUpper();
        }

        public void Validate()
        {
            if(string.IsNullOrEmpty(UserID))
                throw new UnauthorizedAccessException("É necessário informar o e-mail");

            if(string.IsNullOrEmpty(Password))
                throw new UnauthorizedAccessException("É necessário informar a senha");
        }
    }
}