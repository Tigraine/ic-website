namespace ImagineClub.Web.Models.Services
{
    using System;

    public class SHA1HashAlgorithm : IHashAlgorithm
    {
        public string Hash(string input)
        {
            const string salt = "imagineClub";
            var managed = new System.Security.Cryptography.SHA1Managed();
            managed.Initialize();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            byte[] hash = managed.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}