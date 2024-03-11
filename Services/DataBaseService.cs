using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseJournal.Model;

namespace WarehouseJournal.Services
{
    public class DataBaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DataBaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            _database.CreateTableAsync<Item>();
        }

        public Task<List<Item>> GetItemsAsync()
        {
            return _database.Table<Item>().ToListAsync();
        }

        public Task<int> SaveItemAsync(Item item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(Item item)
        {
            return _database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(Item item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
