using BankSystem.Data;
using BankSystem.Models.Errors;

namespace BankSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string FromUserName { get; set; }

        public string FromUserCardNumber { get; set; }

        public string ToUserName { get; set; }

        public string ToUserCardNumber  { get; set; }

        public DateTime Date { get; set; }

        public float MoneyAmount { get; set; }

        public float FromUserInitialBalance { get; set; }

        public float FromUserAfterBalance { get; set; }

        public float ToUserInitialBalance { get; set; } 

        public float ToUserAfterBalance { get; set; }

        public string Details { get; set; }








        public static int TransferMoney(BankSystemContext _context, Transaction transaction)
        {
            // Send a response depending on what was happening through the proccess
            /* ERRORS THAT MAY OCCUR:
             * 
             * INSUFFICIENT FUNDS = 101
             * RECIEVER NAME DOESN'T EXIST = 102
             * RECIEVER CARD NUMBER DOESN'T EXIST = 103
             * RECIEVER NAME DOESN'T MATCH THE CARD NUMBER = 104
             * TRANSFER SUMMARY IS TOO SHORT = 105
             */


            if (transaction.FromUserInitialBalance < transaction.MoneyAmount)
                return 101;

            if (!DatabaseHelper.FindName(_context, transaction.ToUserName))
                return 102;

            if (!DatabaseHelper.FindCardNumber(_context, transaction.ToUserCardNumber))
                return 103;

            if (DatabaseHelper.AccountInfo(_context, transaction.ToUserName).CardNumber != transaction.ToUserCardNumber)
                return 104;

            if (transaction.Details.Length < ErrorHandler.MINIMUM_TRANS_DETAILS_CHARS)
                return 105;



            return 0;
        }

    }
}
