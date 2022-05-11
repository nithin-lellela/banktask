using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankapplication
{
    internal class Currency
    {
        string _acceptedCurrency = "INR";
        decimal _currencyExchangeRate = 1;
        public decimal CurrencyExchangeRate
        {
            get { return _currencyExchangeRate; }
            set { _currencyExchangeRate = value; }
        }
        public string AcceptedCurrency
        {
            get { return _acceptedCurrency; }
            set { _acceptedCurrency = value; }
        }
        public static void AddNewCurrency(string acceptedCurrency, decimal exchangeRate, List<Currency> currencies)
        {
            Currency currency = new Currency();
            currency.AcceptedCurrency = acceptedCurrency.ToUpper();
            currency.CurrencyExchangeRate = exchangeRate;
            currencies.Add(currency);
        }
    }
}
