using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolWebApp1.Models;
using SchoolWebApp1.Services;
using SchoolWebApp1.ViewModels;

namespace SchoolWebApp1.Controllers
{
    public class GradesController : Controller {
        GradeService _service;
        public GradesController(GradeService service) {
            _service = service;
        }
        public async Task<IActionResult> Index() {
            var allGrades = await _service.GetAllAsync();
            return View(allGrades);
        }
        public async Task<IActionResult> CreateAsync() {
            var gradesSelectsData = await _service.GetGradesSelectsValues();
            ViewBag.Students = new SelectList(gradesSelectsData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesSelectsData.Subjects, "Id", "Name");
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Create(GradesViewModel newGrade) {
            await _service.CreateAsync(newGrade);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id) {
            var gradeToEdit = await _service.GetByIdAsync(id);
            if (gradeToEdit == null) {
                return View("NotFound");
            }
            GradesViewModel grade = new GradesViewModel() {
                Id = gradeToEdit.Id,
                StudentId = gradeToEdit.Student.Id,
                SubjectId = gradeToEdit.Subject.Id,
                What = gradeToEdit.What,
                Mark = gradeToEdit.Mark,
                Date = gradeToEdit.Date
            };
            var gradesSelectsData = await _service.GetGradesSelectsValues();
            ViewBag.Students = new SelectList(gradesSelectsData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesSelectsData.Subjects, "Id", "Name");          
            return View(grade);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GradesViewModel updatedGrade) {
            await _service.UpdateAsync(id, updatedGrade);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteAsync(int id) {
            var gradeToDelete = await _service.GetByIdAsync(id);
            if (gradeToDelete == null) {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
