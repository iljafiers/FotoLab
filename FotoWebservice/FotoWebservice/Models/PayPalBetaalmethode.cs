using Newtonsoft.Json;
using PayPal;
using PayPal.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    public class PayPalBetaalMethode
    {
        /* 1. Met ClientID en ClientSecret een Token verkrijgen (OAuth)
         * 2. Met dit token een SALE bij paypal inschieten  --> In de response krijg je een saleID, een ApproveURL en een ExecuteURL
         * 3. Stuur de klant door naar de ApproveUrl (PayPal) met daarbij de redirectUrl
         * 4. Klant logt in bij PayPal en bevestigt de Betaling
         * 5. PayPal stuurt klant naar de redirectUrl (eigen website)
         */
        private static string CREDENTIALSPATH = HttpContext.Current.Server.MapPath("~/PayPalConfig/credentials.json"); // Let op! Niet in GIT
        private PayPalCredentials credentials;
        private string accessToken;

        public PayPalBetaalMethode() 
        {
            credentials = JsonConvert.DeserializeObject<PayPalCredentials>(File.ReadAllText(CREDENTIALSPATH));
        }


        public string ApproveUrl { get; set; }
        public string RedirectUrl { get; set; }
        public string ExecuteUrl { get; set; }

      
        public void GetAccessToken()
        {
            accessToken = new OAuthTokenCredential(credentials.ClientID, credentials.ClientSecret).GetAccessToken();
        }

        public void PostPayment() 
        {
            Debug.WriteLine(credentials.ClientID);
        }

        public static void WriteCredentials(string clientId, string clientSecret, string endpoint)
        {
            PayPalCredentials cred = new PayPalCredentials { ClientID = clientId, ClientSecret = clientSecret, Endpoint = endpoint };
            string json = JsonConvert.SerializeObject(cred);
            File.WriteAllText(CREDENTIALSPATH, json);
        }

    }
}