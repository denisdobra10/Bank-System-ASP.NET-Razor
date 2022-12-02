using Microsoft.Build.Framework;

namespace BankSystem.Models
{
    public class Account
    {
        public static Account LoggedInAccount { get; set; }

        public static string Currency = "$";



        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Gender {get; set; }

        public string SecretQuestion { get; set; }

        public string Email { get; set; }

        public string CardNumber { get; set; }

        public string Pin { get; set; }

        public float Balance { get; set; }

        public DateTime AccountCreatedData { get; set; }
    }
}
