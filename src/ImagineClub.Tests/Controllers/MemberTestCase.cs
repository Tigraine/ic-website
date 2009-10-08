namespace ImagineClub.Tests.Controllers
{
    using System;
    using Web.Models;
    using Xunit;

    public class MemberTestCase : ActiveRecordInMemoryTestBase
    {
        [Fact]
        public void Member_FindMemberByLogin()
        {
            Administrator administrator = CreateAdministrator();

            Member member = Member.FindMemberByLogin("Tigraine", "random-password");
            Assert.Equal(administrator.Id, member.Id);
        }

        [Fact]
        public void Member_FindMemberByLogin_UsernameIsCaseInsensitive()
        {
            Administrator administrator = ObjectMother.GetAdminAndSaveToDatabase();
            Member member = Member.FindMemberByLogin("tigraine", "random-password");
            Assert.NotNull(member);
        }

        private Administrator CreateAdministrator()
        {
            return ObjectMother.GetAdminAndSaveToDatabase();
        }

        [Fact]
        public void Member_FindMemberByLogin_NullIfPasswordWrong()
        {
            Administrator administrator = CreateAdministrator();

            var member = Member.FindMemberByLogin("tigraine", "wrongpass");
            Assert.Null(member);
        }

        [Fact]
        public void Member_CategoryGetsReadCorrectly()
        {
            var admin = CreateAdministrator();

            var member = Member.FindMemberByLogin(admin.Username, ObjectMother.AdminPlaintextPassword);
            Assert.Equal(admin.PersonalInformation.Category, member.PersonalInformation.Category);
        }
    }
}