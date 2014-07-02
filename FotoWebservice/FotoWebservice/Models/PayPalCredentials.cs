using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotoWebservice.Models
{
    public class PayPalCredentials
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string Endpoint { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpires { get; set; }
    }
}
