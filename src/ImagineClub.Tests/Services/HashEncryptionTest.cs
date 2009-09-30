namespace ImagineClub.Tests.Services
{
    using System;
    using Web.Models.Services;
    using Xunit;

    public class HashEncryptionTest
    {
        [Fact]
        public void HashEncryption()
        {
            var encryption = new SHA1HashAlgorithm();
            string hash = encryption.Hash("test");
            Console.WriteLine(hash);
            Assert.Equal("N+W7VitQp7gwLamW/6kQwX0lyzo=", hash);
        }
    }
}