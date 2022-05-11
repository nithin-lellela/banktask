using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankapplication
{
    internal class Transaction
    {
        public string TransactionType { get; set; }
        private string _transactionId;
        private decimal _amount;
        private string _accountId;
        private string _bankId;
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }
        public string AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        public string TransactionId 
        { 
            get { return _transactionId; } 
            set { _transactionId = value; } 
        }
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public void DepositAmount(decimal amount, Currency currency, Account userAccount)
        {
            amount = amount * currency.CurrencyExchangeRate;
            userAccount.AccountBalance += amount;
            this.Amount = amount;
        }

        public bool WithdrawAmount(decimal amount, Account userAccount)
        {
            if(amount > userAccount.AccountBalance)
            {
                return false;
            }
            this.Amount = amount;
            userAccount.AccountBalance -= amount;
            return true;
        }

        public bool TransferFunds(Account user, string accountId, string bankId, decimal amount, List<Account> accounts, Bank bank, Services listOfBanks)
        {
            try
            {
                if (user.BankId == bankId)
                {
                    Account account = accounts.FirstOrDefault(x => x.AccountId == accountId);
                    if (account != null)
                    {
                        decimal totalAmount = amount + (amount * bank.SameBankRTGS) / 100 + (amount * bank.SameBankIMPS) / 100;
                        if(totalAmount > user.AccountBalance)
                        {
                            throw new Exception("\nINSUFFICIENT ACCOUNT BALANCE");
                        }
                        user.AccountBalance -= totalAmount;
                        account.AccountBalance += amount;
                        this.Amount = amount;
                        return true;
                    } 
                }
                else
                {
                    Bank receiverBank = listOfBanks.Banks.FirstOrDefault(x => x.BankId == bankId);
                    if(receiverBank != null)
                    {
                        Account acc = receiverBank.Accounts.FirstOrDefault(x => x.AccountId == accountId);
                        if(acc != null)
                        {
                            decimal totalAmount = amount + (amount * bank.OtherBankRTGS) / 100 + (amount * bank.OtherBankIMPS) / 100;
                            if (totalAmount > user.AccountBalance)
                            {
                                throw new Exception("\nINSUFFICIENT ACCOUNT BALANCE");
                            }                           
                            user.AccountBalance -= totalAmount;
                            acc.AccountBalance += amount;
                            this.Amount = amount;
                            return true;
                        }
                    }
                }
                throw new Exception("PLEASE ENTER VALID DETAILS");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
    }
}
