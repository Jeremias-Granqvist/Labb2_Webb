using Labb2_Shared.Interfaces;
using Labb2_Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Infrastructure.Services
{
    public class RepositoryService<Tentity> : IRepositoryService<Tentity> where Tentity : class
    {
        IRepository<Tentity> _repository;
        public RepositoryService(IRepository<Tentity> repository)
        {
            _repository = repository;
        }

        public async Task<Tentity> AddAsync(Tentity entity)
        {
            return await _repository.AddAsync(entity);


        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await _repository.GetByIdAsync(id);
            if (entityToDelete == null)
            {
                return false;
            }

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Tentity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(Tentity entity)
        {
            var result = _repository.UpdateAsync(entity);

            if (result != null)
            {
                return result;
            }
            return result;
        }
    }
}
