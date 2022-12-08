using Microsoft.AspNetCore.Mvc;
using SchoolWebApp1.Models;
using SchoolWebApp1.Services;
using SchoolWebApp1.ViewModels;

namespace SchoolWebApp1.Controllers
{
    public class StudentsController : Controller {
        public StudentService _service;
        public StudentsController(StudentService service) {
            this._service = service;
        }
        public async Task<IActionResult> Index() {
            var allStudents = await _service.GetAllAsync();
            return View(allStudents);
        }
        public async Task<IActionResult> Details(int id) {
            var student = await _service.GetByIdAsync(id);
            return View("Details", student);
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student newStudent) {
            await _service.CreateAsync(newStudent);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edit(int id) {
            var studentToEdit = await _service.GetByIdAsync(id);
            if (studentToEdit== null) {
                return View("NotFound");
            }
            return View(studentToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id, FirstName, LastName, DateOfBirth")]Student updatedStudent) {
            await _service.UpdateAsync(id, updatedStudent);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id) {
            var studentToDelete = await _service.GetByIdAsync(id);
            if (studentToDelete == null) {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
