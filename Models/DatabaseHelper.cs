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
        private static async Task<bool> CardNumberExists(BankSystemContext _context, string cardNumber)
        {
            var account = await _context.Account.FirstOrDefaultAsync(m => m.CardNumber == cardNumber);

            if (account == null)
                return false;

            return true;
        }

        private static async Task<bool> UserExists(BankSystemContext _context, string username)
        {
            var account = await _context.Account.FirstOrDefaultAsync(m => m.Username == username);

            if (account == null)
                return false;

            return true;
        }

        private static async Task<string> GetUserPassword(BankSystemContext _context, string username)
        {
            var pass = await _context.Account.FirstOrDefaultAsync(c => c.Username == username);

            if (pass == null)
                return "";
            
            return pass.Password;
        }

        private static async Task<Account> GetUserData(BankSystemContext _context, string username)
        {
            return await _context.Account.FirstOrDefaultAsync(m => m.Username == username);
        }

        private static async Task<bool> NameExists(BankSystemContext _context, string name)
        {
            var account = await _context.Account.FirstOrDefaultAsync(m => m.Name == name);

            if (account == null)
                return false;

            return true;
        }


        // END DATABASE QUERY FUNCTIONS



        // USEFUL FUNCTIONS WITH INDIRECT ACTION TO DATABASE


        public static Account Login(BankSystemContext _context, string username, string password)
        {
            if (username == null || password == null)
                return null;

            if (!UserExists(_context, username).Result)
                return null;

            if(GetUserPassword(_context, username).Result.Equals(Encryptor.MD5Hash(password)))
            {
                return GetUserData(_context, username).Result;
            }
            
            return null;

        }


        // CARD NUMBER GENERATOR METHOD
        public static string GenerateCardNumber(BankSystemContext _context)
        {
            int max_digits = 16;
            string cardNumber = "";

            while (CardNumberExists(_context, cardNumber).Result || cardNumber.Length == 0)
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

        public static Account AccountInfo(BankSystemContext _context, string userName)
        {
            return GetUserData(_context, userName).Result;
        }

        public static bool SearchAccountDb(BankSystemContext _context, string userName)
        {
            return UserExists(_context, userName).Result;
        }

        public static bool FindCardNumber(BankSystemContext _context, string cardNumber)
        {
            return CardNumberExists(_context, cardNumber).Result;
        }

        public static bool FindName(BankSystemContext _context, string name)
        {
            return NameExists(_context, name).Result;
        }
    }
}
