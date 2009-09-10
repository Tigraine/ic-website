namespace ImagineClub.Tests
{
    using Castle.MonoRail.Framework;
    using Castle.MonoRail.TestSupport;

    public class DatabaseDependantControllerTest : BaseControllerTest
    {
        public DatabaseDependantControllerTest()
        {
            new ActiveRecordInMemoryTestBase();
        }
    }

    public class GenericDatabaseDependantControllerTest<T> : GenericBaseControllerTest<T>
        where T : Controller
    {
        public GenericDatabaseDependantControllerTest()
        {
            new ActiveRecordInMemoryTestBase();
        }
    }
}