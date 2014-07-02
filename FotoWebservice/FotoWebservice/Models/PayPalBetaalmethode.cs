using Newtonsoft.Json;
using PayPal;
using PayPal.Api.Payments;
using PayPal.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace FotoWebservice.Models
{
    // https://github.com/paypal/sdk-core-dotnet/blob/master/Source/SDK/OAuthTokenCredential.cs
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

        public PayPalBetaalMethode() 
        {
            if (File.Exists(CREDENTIALSPATH))
            {
                credentials = JsonConvert.DeserializeObject<PayPalCredentials>(File.ReadAllText(CREDENTIALSPATH));
            }
            else
            {
                throw new FileNotFoundException("/PayPalConfig/credentials.json bestaat niet");
            }
        }

        public PayPalBetaalMethode(PayPalCredentials cred)
        {
            credentials = cred;
            WriteCredentials();
        }


        public string ApproveUrl { get; set; } // url to redirect customer to
        public string RedirectUrl { get; set; } // url to give paypal where paypal redirects when payment approved
        public string CancelUrl { get; set; } // // url to give paypal where paypal redirects when payment cancelled
        public string ExecuteUrl { get; set; } // url to finalize payment

      
        public void GetAccessToken()
        {
            if (credentials.AccessToken == null || credentials.AccessToken == string.Empty || 
               (credentials.AccessTokenExpires == null || credentials.AccessTokenExpires < DateTime.Now))
            {
                // Check https://github.com/paypal/sdk-core-dotnet/blob/master/Source/SDK/OAuthTokenCredential.cs voor de opties
                
                Dictionary<string, string> config = new Dictionary<string, string>();
                config.Add(BaseConstants.ApplicationModeConfig, BaseConstants.SandboxMode);
                config.Add(BaseConstants.RESTSandboxEndpoint, credentials.Endpoint); // Use Sandbox

                credentials.AccessToken = new OAuthTokenCredential(credentials.ClientID, credentials.ClientSecret, config).GetAccessToken();
                Debug.WriteLine("PayPal AccessToken: " + credentials.AccessToken);
                WriteCredentials();
            }
        }

        public Payment GetPayment(string paymentId)
        {
            GetAccessToken();
            return Payment.Get(credentials.AccessToken, paymentId);
        }

        public void PostPayment(string redirectUrl, string cancelUrl, decimal total) 
        {
            GetAccessToken();

            Payment payment = new Payment();
            payment.intent = "sale";

            Payer payer = new Payer();
            payer.payment_method = "paypal";

            payment.redirect_urls = new RedirectUrls();
            payment.redirect_urls.return_url = redirectUrl;
            payment.redirect_urls.cancel_url = cancelUrl;

            Transaction trans = new Transaction();
            trans.amount = new Amount();
            trans.amount.currency = "EUR";
            trans.amount.total = total.ToString();
            trans.amount.details = new Details();
            trans.amount.details.subtotal = (total - 0.03m - 0.03m).ToString();
            trans.amount.details.shipping = "0.03";
            trans.amount.details.tax = "0.03";
            trans.description = "Fotos kopen bij Fotolab Inc.";

            payment.transactions = new List<Transaction>();
            payment.transactions.Add(trans);

            Payment responsePayment = payment.Create(credentials.AccessToken);
            Debug.WriteLine(responsePayment.create_time);
            WriteTestData("TestPostPayment", responsePayment);
        }

        public void ExecutePayment(string payerId)
        {
            PaymentExecution exec = new PaymentExecution();
            exec.payer_id = payerId;

            Payment payment = new Payment();
            Payment responsePayment = payment.Execute(credentials.AccessToken, new PaymentExecution());

            WriteTestData("TestExecutePayment", responsePayment);
        }

        private void WriteCredentials()
        {
            // string clientId, string clientSecret, string endpoint, string accessToken, DateTime expires
            /*PayPalCredentials cred = new PayPalCredentials { 
                ClientID = clientId, 
                ClientSecret = clientSecret, 
                Endpoint = endpoint,
                AccessToken = accessToken,
                AccessTokenExpires = expires
            };*/
            if (File.Exists(CREDENTIALSPATH))
            {
                File.Delete(CREDENTIALSPATH);
            }

            string json = JsonConvert.SerializeObject(credentials);
            File.WriteAllText(CREDENTIALSPATH, json);
        }

        private static void WriteTestData(string filename, Object obj)
        {
            string testPath = HttpContext.Current.Server.MapPath("~/PayPalConfig/" + filename + ".json");
            if (File.Exists(testPath))
            {
                File.Delete(testPath);
            }
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(testPath, json);
        }

    }
}