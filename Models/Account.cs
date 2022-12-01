using Microsoft.Build.Framework;

namespace BankSystem.Models
{
    public class Account
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string SecretQuestion { get; set; }
    }
}
