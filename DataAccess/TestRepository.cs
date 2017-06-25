using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TestRepository
    {
        private readonly SqlTransaction transaction;

        public TestRepository(SqlTransaction transaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public async Task AddDataWithADO(int id, string name)
        {
            SqlConnection connection = this.transaction.Connection;
            SqlCommand command = new SqlCommand("INSERT INTO TestTable ( Id, Name ) VALUES ( @Id, @name )", connection, this.transaction);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            await command.ExecuteNonQueryAsync();
        }
    }
}
