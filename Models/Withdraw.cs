namespace BankSystem.Models
{
    public class Withdraw
    {
        public int Id { get; set; }

        public Account account { get; set; }

        public float Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
