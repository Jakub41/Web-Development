using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using ComputerWebShop.CurrencyExchangeRates;

namespace ComputerWebShop.Helper
{
    public static class MyExtensionMethods
    {
        public static string ToCurrency(this int amount)
        {

            if (CultureHelper.GetCurrentCulture() == "da-DK")
            {
                  

                NumberFormatInfo dk = new CultureInfo("da-DK",true).NumberFormat;
                 
                return CurrencyConversion.ConvertCurrency(amount, "USD", "DKK").ToString("C2", dk);
            }
            else
            {
                return String.Format("{0:C2}", amount);
            }

        }
        public static string ToCurrency(this double amount)

        {
            if (CultureHelper.GetCurrentCulture() == "da-DK")
            {
                NumberFormatInfo dk = new CultureInfo("da-DK", true).NumberFormat;
                return  CurrencyConversion.ConvertCurrency(Convert.ToDecimal( amount), "USD", "DKK").ToString("C2", dk);
            }
            else
            {

                return    String.Format("{0:C2}", amount);
            }

        }

    }


}