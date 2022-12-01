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
        public static async Task<bool> CardNumberExists(BankSystemContext _context, string cardNumber)
        {
            var account = await _context.Account.FirstOrDefaultAsync(m => m.CardNumber == cardNumber);

            if (account == null)
                return false;

            return true;
        }

        public static async Task<bool> UserExists(BankSystemContext _context, string username)
        {
            var account = await _context.Account.FirstOrDefaultAsync(m => m.Username == username);

            if (account == null)
                return false;

            return true;
        }

        public static async Task<string> GetUserPassword(BankSystemContext _context, string username)
        {
            var pass = await _context.Account.FirstOrDefaultAsync(c => c.Username == username);

            if (pass == null)
                return "";
            
            return pass.Password;
        }

        public static async Task<Account> GetUserData(BankSystemContext _context, string username)
        {
            return await _context.Account.FirstOrDefaultAsync(m => m.Username == username);

        }


        // END DATABASE QUERY FUNCTIONS



        // USEFUL FUNCTIONS WITH INDIRECT ACTION TO DATABASE

        // LOGIN FUNCTION
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

    }
}
