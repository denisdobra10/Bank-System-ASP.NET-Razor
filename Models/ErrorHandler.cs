namespace BankSystem.Models.Errors
{
    public class ErrorHandler
    {
        // TRANSACTION ERRORS

        public static int MINIMUM_TRANS_DETAILS_CHARS = 5;

        public static Dictionary<int, string> transferErrors = new Dictionary<int, string>()
        {
            {101, "INSUFFICIENT FUNDS" },
            {102, "RECIEVER NAME DOESN'T EXIST" },
            {103, "RECIEVER CARD NUMBER DOESN'T EXIST" },
            {104, "RECIEVER NAME DOESN'T MATCH THE CARD NUMBER" },
            {105, "TRANSFER SUMMARY IS TOO SHORT" }
        };

        // END TRANSACTION ERRORS

    }
}
