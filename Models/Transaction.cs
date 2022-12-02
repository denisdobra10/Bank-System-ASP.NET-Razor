using BankSystem.Data;
using BankSystem.Models.Errors;
using Microsoft.EntityFrameworkCore;

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








        public static int TransferMoney(BankSystemContext _context, Transaction transaction, string pin)
        {
            // Send a response depending on what was happening through the proccess
            /* ERRORS THAT MAY OCCUR:
             * 
             * INSUFFICIENT FUNDS = 101
             * RECIEVER NAME DOESN'T EXIST = 102
             * RECIEVER CARD NUMBER DOESN'T EXIST = 103
             * RECIEVER NAME DOESN'T MATCH THE CARD NUMBER = 104
             * TRANSFER SUMMARY IS TOO SHORT = 105
             * WRONG PIN = 106
             */


            if (transaction.FromUserInitialBalance < transaction.MoneyAmount)
                return 101;

            if (!DatabaseHelper.FindName(_context, transaction.ToUserName))
                return 102;

            if (!DatabaseHelper.FindCardNumber(_context, transaction.ToUserCardNumber))
                return 103;

            if (DatabaseHelper.GetAccountByName(_context, transaction.ToUserName).CardNumber != transaction.ToUserCardNumber)
                return 104;

            if (transaction.Details.Length < ErrorHandler.MINIMUM_TRANS_DETAILS_CHARS)
                return 105;

            if (DatabaseHelper.GetAccountByName(_context, transaction.FromUserName).Pin != pin)
                return 106;



            // Update balance for sender
            //Account updatedSender = DatabaseHelper.GetAccountByName(_context, transaction.FromUserName);
            Account updatedSender = DatabaseHelper.GetUserDataByCardNumber(_context, transaction.FromUserCardNumber).Result;
            updatedSender.Balance = transaction.FromUserAfterBalance;

            //Account updatedReciever = DatabaseHelper.GetAccountByName(_context, transaction.FromUserCardNumber);
            Account updatedReciever = DatabaseHelper.GetUserDataByCardNumber(_context, transaction.ToUserCardNumber).Result;
            updatedReciever.Balance = transaction.ToUserAfterBalance;

            List<Account> updatedAccounts = new List<Account>();
            updatedAccounts.Add(updatedSender);
            updatedAccounts.Add(updatedReciever);

            DatabaseHelper.UpdateAccounts(_context, updatedAccounts);
            DatabaseHelper.UpdateLoggedInAccount(_context);

            return 0;
        }

    }
}
