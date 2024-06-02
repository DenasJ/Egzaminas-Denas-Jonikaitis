using System;
using System.Security.Cryptography;
using System.Text;

namespace SlaptazodzioAtkurimas
{
    public class slaptazodziomanageris
    {
        private static readonly string salt = "statine_salt_reiksme";

        public string sukurtiSlaptazodi(string slaptazodis)
        {
            return koduotiSlaptazodi(slaptazodis);
        }

        private string koduotiSlaptazodi(string slaptazodis)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] saltSlaptazodis = Encoding.UTF8.GetBytes(slaptazodis + salt);
                byte[] hash = sha256.ComputeHash(saltSlaptazodis);
                return Convert.ToBase64String(hash);
            }
        }

        public bool PatvirtintiSlaptazodi(string slaptazodis, string uzkoduotasSlaptazodis)
        {
            return koduotiSlaptazodi(slaptazodis) == uzkoduotasSlaptazodis;
        }
    }
}