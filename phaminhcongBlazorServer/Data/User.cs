using System.ComponentModel.DataAnnotations;

namespace phaminhcongBlazorServer.Data
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Vui long nhap ten")]
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserPassword { get; set; }
        public string UserAvatar { get; set; }
        public int IsDeleted { get; set; }

        public User()
        {

        }
        public User(int userId,string userName,string userEmail,string userPhoneNumber, string userPassword,string userAvatar, int isDeleted )
        {
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
            UserPhoneNumber = userPhoneNumber;
            UserPassword = userPassword;
            IsDeleted = isDeleted;
            UserAvatar=userAvatar;
        }
    }
}
