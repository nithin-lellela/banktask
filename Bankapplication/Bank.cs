using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankapplication
{
    internal class Bank
    {
        public string BankName { get; set; }
        private string _bankId;

        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; } 
        }
        public Bank(string name)
        {
            this.BankName = name;
            this.BankId = name.Substring(0,3) + DateTime.Now.ToString().Substring(0,9);
        }
        public List<Account> Accounts = new List<Account>();
        public List<BankStaff> BankStaffs = new List<BankStaff>();
        public List<Currency> Currencies = new List<Currency>()
        {
            new Currency()
        };
        
        
        private int _sameBankRTGS = 0;
        private int _sameBankIMPS = 5;
        private int _otherBankRTGS = 2;
        private int _otherBankIMPS = 6;

        public int SameBankRTGS
        {
            get { return _sameBankRTGS; }
            set { _sameBankRTGS = value; }
        }
        public int SameBankIMPS
        {
            get { return _sameBankIMPS; }
            set { _sameBankIMPS = value; }
        }

        public int OtherBankRTGS
        {
            get { return _otherBankRTGS; }
            set { _otherBankRTGS = value; }
        }

        public int OtherBankIMPS
        {
            get { return _otherBankIMPS; }
            set { _otherBankIMPS = value; }
        }

    }
}
