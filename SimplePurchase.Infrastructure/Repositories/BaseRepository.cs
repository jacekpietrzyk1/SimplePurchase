using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SimplePurchase.Infrastructure.Repositories
{
    public class BaseRepository
    {
        private static string _connectionString;
        private readonly string _tableName;

        protected BaseRepository(string tableName, string connectionString)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        protected string GetTableName()
        {
            return _tableName;
        }

        protected T GetById<T>(int id)
        {
            return QueryFirstOrDefault<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { id });
        }

        protected IEnumerable<T> GetAll<T>()
        {
            return Query<T>(string.Format($"SELECT * FROM {_tableName}"));
        }

        protected T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        protected IEnumerable<T> Query<T>(string sql, object param = null, bool isStoredProcedure = false)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                if (isStoredProcedure)
                    return conn.Query<T>(sql, param, commandType: CommandType.StoredProcedure);
                return conn.Query<T>(sql, param);
            }
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, bool isStoredProcedure = false)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                if (isStoredProcedure)
                    return conn.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure).Result;
                var result = await conn.QueryAsync<T>(sql, param);
                return result;
            }
        }

        protected static int Execute(string sql, object param = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Execute(sql, param);
            }
        }

        protected static async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                return await conn.ExecuteAsync(sql, param);
            }
        }
    }
}
