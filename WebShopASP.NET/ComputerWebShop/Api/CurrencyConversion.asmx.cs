using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Globalization;
namespace ComputerWebShop
{
    /// <summary>
    /// Summary description for CurrencyConversion
    /// </summary>
    [WebService(Namespace = "http://www.google.com/ig/calculator?hl=en&q={2}{0}%3D%3F{1}")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class CurrencyConversion : System.Web.Services.WebService
    {

        [WebMethod]
        public static decimal ConvertCurrency(decimal Amount, string Current, string Target)
        {
            WebClient webclient = new WebClient();

            string url = "https://currency-api.appspot.com/api/" + Current.ToUpper() + "/" + Target.ToUpper() + ".json?amount=" + Convert.ToInt32( Amount )+"";
            string response = webclient.DownloadString(url);
            dynamic JSONResult =    JsonConvert.DeserializeObject(response);
            decimal rate = Convert.ToDecimal( JSONResult.amount);
               
            return rate;

        }
    }
}
