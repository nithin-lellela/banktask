using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankapplication
{
    internal class Account
    {
        public string Username { get; set; }
        private string _accountId;
        private decimal _accountBalance;
        public string BankId { get; set; }
        public List<Transaction> TransactionHistory = new List<Transaction>();
        public string AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
        public decimal AccountBalance
        {
            get { return _accountBalance; }
            set { _accountBalance = value; }
        }
        public string Password { get; set; }

        /*public Account(string username)
        {
            this.Username = username;
            this.Password = username+"123";
            this.AccountId = username.Substring(0, 3) + DateTime.Now.ToString().Substring(0, 9);
            this.AccountBalance = 0;
        }*/
    }
}
