using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankapplication
{
    internal class Services
    {
        public List<Bank> Banks = new List<Bank>();
        public void InitializeBank(string bankName)
        {
            Bank bank = new Bank(bankName);
            BankStaff bankStaff = new BankStaff();
            bankStaff.StaffName = "admin";
            bankStaff.StaffId = "admin123";
            bankStaff.StaffPass = "admin";
            bankStaff.BankId = bank.BankId;
            bank.BankStaffs.Add(bankStaff);
            Banks.Add(bank);
        }
    }
}
