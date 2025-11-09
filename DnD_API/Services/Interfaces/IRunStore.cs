using DnD_API.Data;
using DnD_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DnD_API.Services.Interfaces
{
    public interface IRunStore
    {
        void Create(Run r);
        Run Get(string id);
        void Update(Run r);
        void Delete(string id);
    }

    public class RunStore : IRunStore
    {
        private readonly DnDDbContext _dbContext;
        public RunStore(DnDDbContext dbContext)  => _dbContext = dbContext;
       
        public void Create(Run r) { _dbContext.Runs.Add(r); _dbContext.SaveChanges(); }
        public Run? Get(string? id) => _dbContext?.Runs?.FirstOrDefault(r => r.Id == id);
        public void Update(Run r) { _dbContext?.Runs.Update(r); _dbContext.SaveChanges(); }
        public void Delete(string id) { _dbContext?.Runs.Remove(Get(id)); _dbContext?.SaveChanges(); }
    }
}
