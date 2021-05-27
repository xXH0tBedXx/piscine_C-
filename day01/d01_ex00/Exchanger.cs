using System;
namespace d01_ex00
{
    public class Exchanger
    {
        internal Models.ExchangeRate convert(string sum, double value, double value1, double value2, Models.ExchangeRate rate)
        {
            switch (sum)
            {
                case "RUB":
                    Models.ExchangeRate.RUB = value;
                    Models.ExchangeRate.USD = value1 * value;
                    Models.ExchangeRate.EUR = value2 * value;
                    break;
                case "EUR":
                    Models.ExchangeRate.EUR = value;
                    Models.ExchangeRate.USD = value1 * value;
                    Models.ExchangeRate.RUB = value2 * value;
                    break;
                case "USD":
                    Models.ExchangeRate.USD = value;
                    Models.ExchangeRate.RUB = value1 * value;
                    Models.ExchangeRate.EUR = value2 * value;
                    break;
            }
            return rate;
        }
    }
}


