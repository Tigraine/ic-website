namespace ImagineClub.Tests.Infrastructure
{
    using Web.Validators;
    using Xunit;

    public class UsernameUniqueValidator_Fixture : ActiveRecordInMemoryTestBase
    {
        [Fact]
        public void ReturnsFalseIfUsernameAlreadyExists()
        {
            ObjectMother.GetAdminAndSaveToDatabase();
            var validator = new UsernameUniqueValidator();
            
            bool valid = validator.IsValid(null, "Tigraine");

            Assert.Equal(false, valid);
        }

        [Fact]
        public void ReturnsTrueIfUsernameDoesNotExist()
        {
            var validator = new UsernameUniqueValidator();

            bool valid = validator.IsValid(null, "Tigraine");

            Assert.Equal(true, valid);
        }
    }
}