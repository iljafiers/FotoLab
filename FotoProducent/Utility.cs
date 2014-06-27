using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FotoProducent
{
    public class Utility
    {
        public static string GenerateRandomString(int length) 
        {
            StringBuilder str = new StringBuilder();

            Random rnd = new Random();
            char[] availableCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

            for (int i = 0; i < length; i++)
            {
                str.Append(availableCharacters[rnd.Next(0, availableCharacters.Length - 1)]);
            }

            return str.ToString();
        }
    }
}
