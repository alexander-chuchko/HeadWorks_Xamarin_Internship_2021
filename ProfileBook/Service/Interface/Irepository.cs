﻿using ProfileBook.Model.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Interface
{
    public interface IRepository
    {
        Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new();
        Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new();
        Task<int> DeleteAsync<T>(T entitty) where T : IEntityBase, new();
        Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new();
    }
}
