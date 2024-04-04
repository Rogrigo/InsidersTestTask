using System.ComponentModel.DataAnnotations;

namespace InsidersTestTask.Models
{
    public class UserCrypto
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string CryptoId { get; set; }
        public CryptoInfo CryptoInfo { get; set; }
    }
}
