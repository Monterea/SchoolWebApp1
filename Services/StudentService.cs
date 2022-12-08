using Microsoft.EntityFrameworkCore;
using SchoolWebApp1.Controllers;
using SchoolWebApp1.Data;
using SchoolWebApp1.Models;
using SchoolWebApp1.ViewModels;

namespace SchoolWebApp1.Services {
    public class StudentService {
        private ApplicationDbContext _dbContext;
        public StudentService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
        internal async Task<IEnumerable<Student>> GetAllAsync() {
            return await _dbContext.Students.ToListAsync();
        }
        internal async Task <Student> GetByIdAsync(int id) {
            return await _dbContext.Students.FirstOrDefaultAsync(st => st.Id == id);
        }
        internal async Task CreateAsync(Student newStudent) {
            await _dbContext.Students.AddAsync(newStudent);
            await _dbContext.SaveChangesAsync();
        }
        internal async Task UpdateAsync(int id, Student updatedStudent) {
            _dbContext.Students.Update(updatedStudent);
            await _dbContext.SaveChangesAsync();
        }
        internal async Task DeleteAsync(int id) {
            var studentToDelete = await _dbContext.Students.FirstOrDefaultAsync(st => st.Id == id); 
            _dbContext.Students.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}