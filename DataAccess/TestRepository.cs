using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace DataAccess
{
    public class TestRepository
    {
        private readonly IDbTransaction transaction;

        public TestRepository(IDbTransaction transaction)
        {
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public async Task AddData(int id, string name)
        {
            IDbConnection connection = this.transaction.Connection;
            var parameters = new { Id = id, Name = name };
            await connection.ExecuteAsync("INSERT INTO TestTable ( Id, Name ) VALUES ( @Id, @name )", parameters, this.transaction);
        }
    }
}
