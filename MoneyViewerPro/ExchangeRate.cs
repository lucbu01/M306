using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MoneyViewerPro
{
    public class ExchangeRate
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public double Factor { get; private set; }
        public DateTime Date { get; private set; }

        private ExchangeRate(string from, string to, double factor, DateTime date)
        {
            From = from;
            To = to;
            Factor = factor;
            Date = date;
        }

        public double calculate(double value)
        {
            return value * Factor;
        }

        public static ExchangeRate getExchangeRate(string from, string to, DateTime date)
        {
            if (date.Date > DateTime.Now.Date)
            {
                date = DateTime.Now;
            }
            WebRequest request = WebRequest.Create("https://api.exchangeratesapi.io/" + date.ToString("yyyy-MM-dd") + "?base=" + from + "&symbols=" + to);
            IWebProxy proxy = WebRequest.DefaultWebProxy;

            proxy.Credentials = CredentialCache.DefaultCredentials;
            request.Proxy = proxy;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            try
            {
                string readToEnd = new StreamReader(responseStream).ReadToEnd();
                ExchangeRateBase exchangeRateBase = JsonConvert.DeserializeObject<ExchangeRateBase>(readToEnd);
                if (exchangeRateBase != null && exchangeRateBase.Rates.ContainsKey(to))
                {
                    return new ExchangeRate(exchangeRateBase.Base, to, exchangeRateBase.Rates[to], exchangeRateBase.Date);
                }
                throw new Exception();
            }
            catch (Exception e)
            {
                throw new Exception("invalid values for exchange rate");
            }

        }

        public static ExchangeRate getExchangeRate(string from, string to)
        {
            return getExchangeRate(from, to, DateTime.Now);
        }

        public class ExchangeRateBase
        {
            [JsonProperty("rates")]
            public Dictionary<string, double> Rates { get; set; } = new Dictionary<string, double>();
            [JsonProperty("base")]
            public string Base { get; set; }
            [JsonProperty("date")]
            public DateTime Date { get; set; }
        }
    }
}
