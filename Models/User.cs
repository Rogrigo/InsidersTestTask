using System.ComponentModel.DataAnnotations;

namespace InsidersTestTask.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurename { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserCrypto> UserCryptos { get; set; }
    }
}
