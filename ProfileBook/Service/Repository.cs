﻿using ProfileBook.Model;
using ProfileBook.Model.Interface;
using ProfileBook.Services.Interface;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProfileBook.Services
{
    class Repository : IRepository
    {
        readonly Lazy<SQLiteAsyncConnection> database;
        public Repository()
        {
            database = new Lazy<SQLiteAsyncConnection>(() =>
              {
                  //Get the path to local folder 
                  var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profilebook.db3");
                  //Create a connection
                  var database_ = new SQLiteAsyncConnection(path);
                  //Create the table ProfileModel
                  database_.CreateTableAsync<ProfileModel>();
                  //Create the table UserModel
                  database_.CreateTableAsync<UserModel>();
                  return database_;
              });
        }
        public async Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            return await database.Value.DeleteAsync(entity);
        }
        public async Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new()
        {
            return await database.Value.Table<T>().ToListAsync();
        }

        public async Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            return await database.Value.InsertAsync(entity);
        }
        public async Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            return await database.Value.UpdateAsync(entity);
        }
    }
}
