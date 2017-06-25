using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AppService
    {
        private const string ConnectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=SqlPerf-TestDb;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly Action<string> progressUpdates;

        public AppService(Action<string> progressUpates) => this.progressUpdates = progressUpates;

        public async Task SaveData()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                var profileTime = new List<double>();
                var repository = new TestRepository(transaction);

                var watch = new Stopwatch();
                for (int index = 0; index < 1000; index++)
                {
                    watch.Start();
                    await repository.AddDataWithADO(index, "Foo Bar");
                    watch.Stop();
                    profileTime.Add(watch.Elapsed.TotalMilliseconds);
                    this.progressUpdates($"Execution time: {watch.Elapsed.TotalMilliseconds}");
                    watch.Reset();
                }

                transaction.Rollback();
                transaction.Dispose();
                connection.Dispose();
                this.progressUpdates($"Total average: {profileTime.Average()}");
            }

           await this.Cleanup();
        }

        private async Task Cleanup()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM TestTable", connection);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
