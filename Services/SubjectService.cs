using Microsoft.EntityFrameworkCore;
using SchoolWebApp1.Controllers;
using SchoolWebApp1.Data;
using SchoolWebApp1.Models;

namespace SchoolWebApp1.Services {
    public class SubjectService {
        private ApplicationDbContext _dbContext;
        public SubjectService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
        internal async Task<IEnumerable<Subject>> GetAllAsync() {
            return await _dbContext.Subjects.ToListAsync();
        }
        internal async Task <Subject> GetByIdAsync(int id) {
            return await _dbContext.Subjects.FirstOrDefaultAsync(st => st.Id == id);
        }
        internal async Task CreateAsync(Subject newSubject) {
            await _dbContext.Subjects.AddAsync(newSubject);
            await _dbContext.SaveChangesAsync();
        }
        internal async Task UpdateAsync(int id, Subject updatedSubject) {
            _dbContext.Subjects.Update(updatedSubject);
            await _dbContext.SaveChangesAsync();
        }
        internal async Task DeleteAsync(int id) {
            var subjectToDelete = await _dbContext.Subjects.FirstOrDefaultAsync(st => st.Id == id); 
            _dbContext.Subjects.Remove(subjectToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}