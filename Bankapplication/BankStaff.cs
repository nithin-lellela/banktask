using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankapplication
{
    internal class BankStaff
    {
        public string StaffName { get; set; }
        public string StaffId { get; set; }
        public string StaffPass { get; set; }

        private string _bankId;
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }

        public void CreateNewAccount(string name, Account account, List<Account> accounts, string bankId)
        {
            account.Username = name;
            account.Password = name+"123";
            account.AccountId = name.Substring(0, 3) + DateTime.Now.ToString().Substring(0,9);
            account.AccountBalance = 0;
            account.BankId = bankId;
            accounts.Add(account);
        }

        public void DeleteAccount(string id, List<Account> accounts)
        {
            Console.WriteLine("This is delete section");
            int index = accounts.FindIndex(x => x.AccountId == id);
            accounts.RemoveAt(index);
        }

        public void UpdateAccount(Account account, string name, string password)
        {
            account.Username = name;
            account.Password = password;
            account.AccountId = name.Substring(0,3) + DateTime.Now.ToString().Substring(0,9);
        }

        public void ViewTransactionHistory(string id, List<Account> accounts)
        {
            Account account = accounts.FirstOrDefault(x => x.AccountId == id);
            if (account == null)
            {
                Console.WriteLine("Account Not Found");
            }
            else
            {
                int i = 1;
                Console.WriteLine("S.no: Transaction ID: Transaction Type: Amount: ");
                foreach(Transaction transaction in account.TransactionHistory)
                {
                    Console.WriteLine($"{i}     {transaction.TransactionId}    {transaction.TransactionType}     {transaction.Amount}");
                    i++;
                }
            }
        }
        public void RevertTransaction(Account account, string transactionId, Services listOfBanks)
        {
            Transaction transaction = account.TransactionHistory.FirstOrDefault(x => x.TransactionId == transactionId);
            if (transaction != null)
            {

                if(transaction.TransactionType == "Deposit" && transaction.Amount < account.AccountBalance)
                {
                    account.AccountBalance -= transaction.Amount;
                    Console.WriteLine($"\nSuccessfully reverted transaction\n{account.Username} has Account balance: {account.AccountBalance}");
                }
                else if(transaction.TransactionType == "Withdraw")
                {
                    account.AccountBalance += transaction.Amount;
                    Console.WriteLine($"\nSuccessfully reverted transaction\n{account.Username} has Account balance: {account.AccountBalance}");
                }
                else if(transaction.TransactionType == "Transfer funds")
                {
                    Bank bank = listOfBanks.Banks.FirstOrDefault(x => x.BankId == transaction.BankId);
                    if(bank != null)
                    {
                        Account reciptentAccount = bank.Accounts.FirstOrDefault(x => x.AccountId == transaction.AccountId);
                        if(reciptentAccount != null)
                        {
                            reciptentAccount.AccountBalance -= transaction.Amount;
                            account.AccountBalance += transaction.Amount;
                            Console.WriteLine($"\nSuccessfully reverted transaction\n{account.Username} has Account balance: {account.AccountBalance}");
                        }
      
                    }
                }
                int index = account.TransactionHistory.FindIndex(x => x.TransactionId.Equals(transactionId));
                account.TransactionHistory.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("\nCannot Revert transaction");
            }
        }
        public void AddNewAcceptedCurrency(string acceptedCurrency, decimal exchangeRate, List<Currency> currencies)
        {
            Currency currency = new Currency();
            currency.AcceptedCurrency = acceptedCurrency.ToUpper();
            currency.CurrencyExchangeRate = exchangeRate;
            currencies.Add(currency);
        }
        public void AddNewChargesSameBank(int rtgs, int imps, Bank bank)
        {
            bank.SameBankRTGS = rtgs;
            bank.SameBankIMPS = imps;
        }
        public void AddNewChargesOtherBank(int rtgs, int imps, Bank bank)
        {
            bank.OtherBankRTGS = rtgs;
            bank.OtherBankIMPS = imps;
        }

    }
}
