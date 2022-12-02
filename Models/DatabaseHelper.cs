using BankSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Drawing;

namespace BankSystem.Models
{
    public static class DatabaseHelper
    {
        // PUBLIC DATABASE QUERY FUNCTIONS

        // METHOD TO CHECK IF CERTAIN ELEMENT DOES EXISTS IN A SPECIFIC TABLE
        //private static async Task<bool> CardNumberExists(BankSystemContext _context, string cardNumber)
        //{
        //    var account = await _context.Account.FirstOrDefaultAsync(m => m.CardNumber == cardNumber);

        //    if (account == null)
        //        return false;

        //    return true;
        //}

        //private static async Task<bool> UserExists(BankSystemContext _context, string username)
        //{
        //    var account = await _context.Account.FirstOrDefaultAsync(m => m.Username == username);

        //    if (account == null)
        //        return false;

        //    return true;
        //}

        //private static async Task<string> GetUserPassword(BankSystemContext _context, string username)
        //{
        //    var pass = await _context.Account.FirstOrDefaultAsync(c => c.Username == username);

        //    if (pass == null)
        //        return "";

        //    return pass.Password;
        //}

        public static async Task<Account> GetUserData(BankSystemContext _context, int key)
        {
            return await _context.Account.FindAsync(key);
        }

        public static async Task<Account> GetUserDataByCardNumber(BankSystemContext _context, string cardNumber)
        {
           return await _context.Account.FirstAsync(a => a.CardNumber.Equals(cardNumber));
        }

        //private static async Task<bool> NameExists(BankSystemContext _context, string name)
        //{
        //    var account = await _context.Account.FirstOrDefaultAsync(m => m.Name == name);


        //    if (account == null)
        //        return false;

        //    return true;
        //}

        

        // END DATABASE QUERY FUNCTIONS



        // USEFUL FUNCTIONS WITH INDIRECT ACTION TO DATABASE



        public static Account Login(BankSystemContext _context, string username, string password)
        {
            if (username == null || password == null)
                return null;

            if (!UserExists(_context, username))
                return null;

            if(GetPasswordByUsername(_context, username).Equals(Encryptor.MD5Hash(password)))
            {
                return GetAccountByUsername(_context, username);
            }
            
            return null;

        }

        public static void UpdateAccounts(BankSystemContext _context, List<Account> accounts)
        {
            foreach (var a in accounts)
            {
                _context.Account.Update(a);
            }
        }

        // CARD NUMBER GENERATOR METHOD
        public static string GenerateCardNumber(BankSystemContext _context)
        {
            int max_digits = 16;
            string cardNumber = "";

            while (FindCardNumber(_context, cardNumber) || cardNumber.Length == 0)
            {
                cardNumber = "";
                for (int i = 0; i < max_digits; i++)
                {
                    int randomDigit = new Random().Next(0, 10);
                    cardNumber += randomDigit.ToString();
                }
            }

            return cardNumber;
        }

        // PIN GENERATOR METHOD
        public static string GeneratePin()
        {
            int max_digits = 4;
            string pin = "";

            while (pin.Length < max_digits)
            {
                pin += new Random().Next(0, 10).ToString();
            }

            return pin;
        }

        public static async void UpdateLoggedInAccount(BankSystemContext _context)
        {
            Account.LoggedInAccount = await _context.Account.FindAsync(Account.LoggedInAccount.Id);
        }


        public static int GetAccountKeyByCardNumber(BankSystemContext _context, string cardNumber)
        {
            return _context.Account.Where(a => a.CardNumber.Equals(cardNumber)).First().Id;
        }
        public static Account GetAccountByUsername(BankSystemContext _context, string userName)
        {
            return _context.Account.Where(a => a.Username.Equals(userName)).First();
            //return GetUserData(_context, userName).Result;
        }

        public static Account GetAccountByName(BankSystemContext _context, string name)
        {
            return _context.Account.Where(a => a.Name.Equals(name)).First();
            //return GetUserData(_context, userName).Result;
        }

        public static string GetPasswordByUsername(BankSystemContext _context, string userName)
        {
            return _context.Account.Where(a => a.Username.Equals(userName)).First().Password;
        }


        public static bool UserExists(BankSystemContext _context, string userName)
        {
            return _context.Account.Where(a => a.Username == userName).Any();
        }

        public static bool EmailExists(BankSystemContext _context, string email)
        {
            return _context.Account.Where(m => m.Email.Equals(email)).Any();
        }

        public static bool SearchAccountDb(BankSystemContext _context, string userName)
        {
            return _context.Account.Where(a => a.CardNumber.Equals(userName)).Any();
            //return UserExists(_context, userName).Result;
        }

        public static bool FindCardNumber(BankSystemContext _context, string cardNumber)
        {
            return _context.Account.Where(a => a.CardNumber.Equals(cardNumber)).Any();
            //return CardNumberExists(_context, cardNumber).Result;
        }

        public static bool FindName(BankSystemContext _context, string name)
        {
            return _context.Account.Where(a => a.Name == name).Any();
            //return NameExists(_context, name).Result;
        }


        public static List<Transaction> GetTransactionsByCardNumber(BankSystemContext _context, string cardNumber)
        {
            return _context.Transaction.Where(a => a.FromUserCardNumber == cardNumber).ToList();
        }
    }
}
