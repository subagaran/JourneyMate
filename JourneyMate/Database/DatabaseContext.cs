using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.Database
{

    public class DatabaseContext
    {
        private const string DbName = "JourneyMate.db3";
        private static readonly Lazy<string> DbPath = new Lazy<string>(() => Path.Combine(FileSystem.AppDataDirectory, DbName));

        private readonly SQLiteAsyncConnection _connection;
        public DatabaseContext()
        {
            _connection = new SQLiteAsyncConnection(DbPath.Value, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        }

        private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
        {
            await _connection.CreateTableAsync<TTable>();
        }

        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return _connection.Table<TTable>();
        }

        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }

        public async Task<IEnumerable<TTable>> GetFilteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();
        }

        private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>> action) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await action();
        }

        public async Task<TTable> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            return await Execute<TTable, TTable>(async () => await _connection.GetAsync<TTable>(primaryKey));
        }

        public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            return await Execute<TTable, bool>(async () => await _connection.InsertAsync(item) > 0);
        }

        public async Task<bool> AddRangeAsync<TTable>(IEnumerable<TTable> items) where TTable : class, new()
        {
            return await Execute<TTable, bool>(async () => await _connection.InsertAllAsync(items) > 0);
        }

        public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            return await _connection.UpdateAsync(item) > 0;
        }

        public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            return await _connection.DeleteAsync(item) > 0;
        }

        public async Task<bool> DeleteItemRangeAsync<TTable>(IEnumerable<TTable> items) where TTable : class, new()
        {
            return await _connection.DeleteAsync(items) > 0;
        }

        public async Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            return await _connection.DeleteAsync<TTable>(primaryKey) > 0;
        }

        public async Task<bool> DeleteAllAsync<TTable>() where TTable : class, new()
        {
            return await _connection.DeleteAllAsync<TTable>() > 0;
        }

        public async ValueTask DisposeAsync() => await _connection?.CloseAsync();

    }
}
