// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using System;
using System.Collections.Generic;

namespace Bankapplication
{
    internal class Program
    {
        public static void DisplayTransactionHistory(List<Transaction> transactionHistory)
        {
            foreach (Transaction transaction in transactionHistory)
            {
                Console.WriteLine($"{transaction.TransactionId}  {transaction.TransactionType}  INR {transaction.Amount}  {transaction.BankId}  {transaction.AccountId}");
            }
        }
        public static void DisplayAcceptedCurrencies(List<Currency> currencies)
        {
            Console.WriteLine("\nACCEPTED CURRENCIES: ");
            foreach(Currency currency in currencies)
            {
                Console.WriteLine($"{currency.AcceptedCurrency}");
            }
        }
        public static void DisplayUserAccounts(List<Account> accounts)
        {
            Console.WriteLine("Account ID:      Account Name: ");
            foreach(Account account in accounts)
            {
                Console.WriteLine($"{account.AccountId}     {account.Username}");
            }
        }
        public static void CreateTransaction(Account userAccount, Transaction transaction, string transactionType, string bankId, string userAccountId)
        {
            transaction.TransactionType = transactionType;
            transaction.TransactionId = "TXN" + bankId + userAccountId + DateTime.Now.ToString();
            transaction.BankId = bankId;
            transaction.AccountId = userAccountId;
            userAccount.TransactionHistory.Add(transaction);
        }
        public static void AccountHolderOperations(Bank bank, List<Account> accounts, List<Currency> currencies, Services listOfBanks)
        {
            bool condition = true;
            Console.WriteLine("This is Account holder section\n");
            Console.Write("Enter your Account ID: ");
            string accountId = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            Account userAccount = accounts.FirstOrDefault(x => x.AccountId == accountId);
            Console.WriteLine(userAccount.Username);
            if(userAccount != null && userAccount.Password == password)
            {
                Console.WriteLine("\nHello " + userAccount.Username);
                while (condition)
                {
                    try
                    {
                        Console.WriteLine("\nSelect the following options for operations \n1. Deposit Amount\n2. Withdraw Amount\n3. Transfer funds\n4. View Transaction history\n5. Exit\n");
                        int option = Convert.ToInt32(Console.ReadLine());
                        Transaction transaction = new Transaction();
                        switch (option)
                        {
                            case 1:

                                Console.WriteLine("\nDEPOSIT AMOUNT");
                                DisplayAcceptedCurrencies(currencies);
                                Console.Write("\nSELECT CURRENCY:");
                                string enteredCurrency = Console.ReadLine();
                                Currency currency = currencies.FirstOrDefault(x => x.AcceptedCurrency == enteredCurrency);
                                if(currency == null)
                                {
                                    throw new Exception("\nSELECT A VALID CURRENCY");
                                }
                                Console.Write($"\nSelected currency is {currency.AcceptedCurrency}\nEnter the amount to be deposited: ");
                                decimal amount = Convert.ToDecimal(Console.ReadLine());
                                transaction.DepositAmount(amount, currency, userAccount);
                                CreateTransaction(userAccount, transaction, "Deposit", bank.BankId, userAccount.AccountId);
                                Console.WriteLine($"Amount has been succesfully deposited\n{userAccount.Username} has balance : {userAccount.AccountBalance}");
                                break;

                            case 2:

                                Console.WriteLine("\nWITHDRAW AMOUNT");
                                Console.Write("\nEnter the Amount to be withdrawn: ");
                                decimal withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                                bool isSuccessfull = transaction.WithdrawAmount(withdrawAmount, userAccount);
                                if (isSuccessfull)
                                {
                                    CreateTransaction(userAccount, transaction, "Withdraw", bank.BankId, userAccount.AccountId);
                                    Console.WriteLine($"Amount has succesfully withdrawn\n{userAccount.Username} has balance : {userAccount.AccountBalance}");
                                }
                                else
                                {
                                    throw new Exception("\nINSUFFICIENT BALANCE");
                                }
                                break;

                            case 3:

                                Console.WriteLine("\nTRANSFER FUNDS");
                                Console.Write("\n Enter the account ID : ");
                                string accId = Console.ReadLine();
                                Console.Write("\nEnter the Bank ID: ");
                                string bankId = Console.ReadLine();
                                Console.Write("\nEnter the amount: ");
                                decimal transferAmount = Convert.ToDecimal(Console.ReadLine());
                                bool isSuccess = transaction.TransferFunds(userAccount, accId, bankId, transferAmount, accounts, bank, listOfBanks);
                                if (isSuccess)
                                {
                                    Console.WriteLine($"\nTransaction successfull\nAccount Balance: {userAccount.AccountBalance}");
                                    CreateTransaction(userAccount, transaction, "Transfer", bankId, accId);
                                }
                                break;

                            case 4:

                                Console.WriteLine("\nVIEW TRANSACTION HISTORY");
                                DisplayTransactionHistory(userAccount.TransactionHistory);
                                break;

                            case 5:
                                condition = false;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter valid login credentials");
            }

        }
        public static void BankStaffOperations(Bank bank, List<BankStaff> bankStaffs, List<Account> accounts, List<Currency> currencies, Services listOfBanks)
        {
            bool condition = true;
            Console.WriteLine("\n---------Welcome to " + bank.BankName + " Bank-----------\n");
            Console.Write("Enter Username : ");
            string staffId = Console.ReadLine();
            Console.Write("Enter Password: ");
            string staffPass = Console.ReadLine();
            BankStaff staff = bankStaffs.FirstOrDefault(x => x.StaffId == staffId);
            if (staff != null && staff.StaffPass == staffPass)
            {
                Console.WriteLine("\nSuccessfully logged in to " + staff.StaffName);
                while (condition)
                {
                    try
                    {
                        Console.WriteLine("\n Select the following options for the operations \n1. Create New Account\n2. Update Account\n3. Delete Account\n" +
                        "4. Add new Accepted currency\n5. View Transaction history\n6. Revert Transaction\n7. Add service charges for same bank account\n8. Add service charges for other bank account\n9. Exit\n");
                        int option = Convert.ToInt32(Console.ReadLine());
                        switch (option)
                        {
                            case 1:

                                Console.Write("Enter Account Holder name: ");
                                string username = Console.ReadLine();
                                Account account = new Account();
                                staff.CreateNewAccount(username, account, accounts, bank.BankId);
                                Console.WriteLine($"\nSuccesfully created account \nAccount ID: {account.AccountId}\nUsername: {account.Username}\nPassword: {account.Password}\nAccount Balance: {account.AccountBalance}\n");
                                break;

                            case 2:

                                DisplayUserAccounts(accounts);
                                Console.WriteLine("Enter Account ID: ");
                                string accountId = Console.ReadLine();
                                Account userAccount = accounts.FirstOrDefault(x => x.AccountId == accountId);
                                if(userAccount == null)
                                {
                                    throw new Exception("\nPLEASE ENTER VALID ACCOUNT ID");
                                }
                                Console.Write("Enter the username to update: ");
                                string name = Console.ReadLine();
                                Console.Write("Enter the password to update:");
                                string password = Console.ReadLine();
                                staff.UpdateAccount(userAccount, name, password);
                                Console.WriteLine($"\nSuccesfully updated account \nAccount ID: {userAccount.AccountId}\nUsername: {userAccount.Username}\nPassword: {userAccount.Password}\nAccount Balance: {userAccount.AccountBalance}\n");
                                break;

                            case 3:

                                DisplayUserAccounts(accounts);
                                Console.Write("\nEnter Account ID: ");
                                string id = Console.ReadLine();
                                Account account1 = accounts.FirstOrDefault(x => x.AccountId == id);
                                if(account1 == null)
                                {
                                    throw new Exception("\nPLEASE ENTER VALID ACCOUNT ID");
                                }
                                staff.DeleteAccount(id, accounts);
                                Console.WriteLine("\nSuccessfully deleted a user Account");
                                break;

                            case 4:

                                Console.Write("\nAdd new accepted currency: ");
                                string acceptedCurrency = Console.ReadLine().ToUpper();
                                Console.Write("\nAdd exchange rate of currency: ");
                                decimal exchangeRate = Convert.ToDecimal(Console.ReadLine());
                                staff.AddNewAcceptedCurrency(acceptedCurrency, exchangeRate, currencies);
                                Console.WriteLine($"Successfully added currency {acceptedCurrency} with an exchange rate of {exchangeRate}");
                                break;

                            case 5:

                                Console.Write("\nEnter the account Id: ");
                                string accId = Console.ReadLine();
                                Account acc = accounts.FirstOrDefault(x => x.AccountId == accId);
                                staff.ViewTransactionHistory(accId, accounts);
                                break;

                            case 6:
                                
                                Console.WriteLine("\nEnter accound ID: ");
                                string accoId = Console.ReadLine();
                                staff.ViewTransactionHistory(accoId, accounts);
                                Account acco = accounts.FirstOrDefault(x => x.AccountId == accoId);
                                Console.WriteLine("\nEnter the transaction ID: ");
                                string txnId = Console.ReadLine();
                                staff.RevertTransaction(acco, txnId, listOfBanks);
                                break;

                            case 7:
                                Console.Write("\nEnter the RTGS charges: ");
                                int sameBankRTGS = Convert.ToInt32(Console.ReadLine());
                                Console.Write("\nEnter the IMPS charges: ");
                                int sameBankIMPS = Convert.ToInt32(Console.ReadLine());
                                staff.AddNewChargesSameBank(sameBankRTGS, sameBankIMPS, bank);
                                break;
                            case 8:
                                Console.Write("\nEnter the RTGS charges: ");
                                int otherBankRTGS = Convert.ToInt32(Console.ReadLine());
                                Console.Write("\nEnter the IMPS charges: ");
                                int otherBankIMPS = Convert.ToInt32(Console.ReadLine());
                                staff.AddNewChargesOtherBank(otherBankRTGS, otherBankIMPS, bank);
                                break;
                            case 9:
                                condition = false;
                                break;
                            default:
                                Console.WriteLine("Invalid");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {

                    }                  
                }
            }
            else
            {
                Console.WriteLine("Invalid username or password\n");
            }

        }
        public static void BankOperations(Bank selectedBank, Services services)
        {
            Console.WriteLine($"\n--------- WELCOME TO {selectedBank.BankName} BANK -----------");
            bool condition = true;
            while (condition)
            {
                try
                {
                    Console.WriteLine("\nSelect the below options to Login as\n1. Account Holder\n2. Bank Staff\n3. EXIT\n");
                    int option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            AccountHolderOperations(selectedBank, selectedBank.Accounts, selectedBank.Currencies, services);
                            break;
                        case 2:
                            BankStaffOperations(selectedBank, selectedBank.BankStaffs, selectedBank.Accounts, selectedBank.Currencies, services);
                            break;
                        case 3:
                            condition = false;
                            break;
                        default:
                            throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nINVALID, PLEASE SELECT A VALID OPTION\n");
                }

            }
        }
        public static void Main(String[] args)
        {
            Services services = new Services();
            services.InitializeBank("HDFC");
            services.InitializeBank("SBI");
            while (true)
            {
                try
                {
                    Console.WriteLine("LIST OF BANKS : ");
                    foreach (Bank bank1 in services.Banks)
                    {
                        Console.WriteLine($"{bank1.BankName}");
                    }
                    Console.WriteLine("\nENTER THE BANK NAME: ");
                    string select = Console.ReadLine().ToUpper();
                    Bank selectedBank = services.Banks.FirstOrDefault(x => x.BankName == select);
                    if(selectedBank == null)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        BankOperations(selectedBank, services);
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine("\nPLEASE ENTER A VALID BANK\n");
                }
            }
            
        }
    }
}