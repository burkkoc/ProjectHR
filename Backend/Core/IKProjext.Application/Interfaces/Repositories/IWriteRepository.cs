﻿using IK.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKProject.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);
        bool Remove(T model);
        Task<bool> RemoveAsync(string id);

        bool RemoveRange(List<T> models);
        bool Update(T model);

        Task<int> SaveAsync();


        Task<bool> RemoveRelatedRecordsAsync(Guid id);//yeni eklendi
    }
}
