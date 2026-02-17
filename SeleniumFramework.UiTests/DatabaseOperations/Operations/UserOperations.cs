using SeleniumFramework.DatabaseOperations.Queries;
using System.Data;

namespace SeleniumFramework.DatabaseOperations.Operations
{
    // Use this as an example. Delete before submitting the exam.
    public class UserOperations : IDisposable
    {
        private readonly IDbConnection _connection;

        public UserOperations(IDbConnection connection)
        {
            this._connection = connection;
            this._connection.Open();
        }

        public void DeleteUserWithEmail(string email)
        {
            var command = this._connection.CreateCommand();
            command.CommandText = UserQueries.DeleteUserByEmail(email);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            this._connection?.Close();
            this._connection?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
