namespace ImagineClub.Tests.Model
{
    using System;
    using Rhino.Mocks;
    using Web.Models;
    using Web.Models.Services;
    using Xunit;

    public class MemberFixture
    {
        [Fact]
        public void AccountActive_IfAccountExpirationIsSmallerThanNow_ReturnsFalse()
        {
            DateTime date = SetupDateScenario();
            var member = new Member();
            member.AccountExpiration = date.AddDays(-1);

            var isActive = member.IsAccountActive;

            Assert.False(isActive);
        }

        private DateTime SetupDateScenario()
        {
            var provider = MockRepository.GenerateStub<IDateProvider>();
            var date = new DateTime(2009, 1, 15);
            provider.Stub(p => p.GetNow()).Return(date).Repeat.Any();
            DateProviderFactory.SetProvider(provider);
            return date;
        }

        [Fact]
        public void AccountActive_IfAccountExpirationIsBiggerThanNow_ReturnsTrue()
        {
            DateTime date = SetupDateScenario();
            var member = new Member();
            member.AccountExpiration = date.AddDays(1);

            var isActive = member.IsAccountActive;

            Assert.True(isActive);
        }
    }
}