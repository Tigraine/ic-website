namespace ImagineClub.Tests.Infrastructure
{
    using System;
    using Web.Models;
    using Xunit;

    public class ModelTests : ActiveRecordInMemoryTestBase
    {
        [Fact]
        public void CanInsertMember()
        {
            Assert.DoesNotThrow(() => ObjectMother.GetAdminAndSaveToDatabase());
        }

        [Fact]
        public void AssertDatabaseIsEmpty()
        {
            Assert.Equal(0, Member.FindAll().Length);
        }
    }
}