namespace ImagineClub.Tests
{
    public class SqLiteInMemoryTestingConnectionProvider : NHibernate.Connection.DriverConnectionProvider
    {
        public static System.Data.IDbConnection Connection = null;

        public override System.Data.IDbConnection GetConnection()
        {
            if (Connection == null)
                Connection = base.GetConnection();

            return Connection;
        }

        public override void CloseConnection(System.Data.IDbConnection conn)
        {
        }
    }
}