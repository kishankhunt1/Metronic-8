using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Metronic_8.Areas.SEC_User.Models
{
    public class SEC_UserModel
    {
        public int UserID { get; set; }
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
