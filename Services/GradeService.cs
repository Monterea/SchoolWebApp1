using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SchoolWebApp1.Controllers;
using SchoolWebApp1.Data;
using SchoolWebApp1.Models;
using SchoolWebApp1.ViewModels;
using System.Diagnostics;

namespace SchoolWebApp1.Services {
    public class GradeService {
        private ApplicationDbContext _dbContext;
        public GradeService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
        internal async Task<GradesSelectsViewModel> GetGradesSelectsValues() {
            var gradesSelectsViewModel = new GradesSelectsViewModel() {
                Students = await _dbContext.Students.OrderBy(st => st.LastName).ToListAsync(),
                Subjects = await _dbContext.Subjects.OrderBy(sb => sb.Name).ToListAsync(),
            };
            return gradesSelectsViewModel;
        }
        internal async Task CreateAsync(GradesViewModel newGrade) {
            //namapování vlastností z GradesViewModel a převést na Grade
            Grade gradeToInsert = new Grade() {
                Student = _dbContext.Students.FirstOrDefault(s => s.Id == newGrade.StudentId),
                Subject = _dbContext.Subjects.FirstOrDefault(s => s.Id == newGrade.SubjectId),
                What = newGrade.What,
                Mark = newGrade.Mark,
                Date = DateTime.Today,
            };
            if(gradeToInsert.Student!=null && gradeToInsert.Subject!=null) {
                await _dbContext.Grades.AddAsync(gradeToInsert);
                await _dbContext.SaveChangesAsync();
            }   
        }
        internal async Task<IEnumerable<Grade>> GetAllAsync() {
            return await _dbContext.Grades.Include(s =>s.Student).Include(s => s.Subject).ToListAsync();
        }
        internal async Task<Grade> GetByIdAsync(int id) {
            return await _dbContext.Grades.Include(s => s.Student).Include(s => s.Subject).FirstOrDefaultAsync(g => g.Id == id);
        }

        internal async Task UpdateAsync(int id, GradesViewModel updatedGrade) {
            var gradeToUpdate = await _dbContext.Grades.FirstOrDefaultAsync(g => g.Id == id);
            if (gradeToUpdate != null) {
                gradeToUpdate.Student = _dbContext.Students.FirstOrDefault(s => s.Id == updatedGrade.StudentId);
                gradeToUpdate.Subject = _dbContext.Subjects.FirstOrDefault(s => s.Id == updatedGrade.SubjectId);
                gradeToUpdate.What = updatedGrade.What;
                gradeToUpdate.Mark = updatedGrade.Mark;
                gradeToUpdate.Date = updatedGrade.Date;
            }
            _dbContext.Update(gradeToUpdate);
            await _dbContext.SaveChangesAsync();
        }
        internal async Task DeleteAsync(int id) {
            var gradeToDelete = await _dbContext.Grades.FirstOrDefaultAsync(g => g.Id == id);
            _dbContext.Grades.Remove(gradeToDelete);
            await _dbContext.SaveChangesAsync();
        }


    }
}