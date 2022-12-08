using Microsoft.AspNetCore.Mvc;
using SchoolWebApp1.Models;
using SchoolWebApp1.Services;

namespace SchoolWebApp1.Controllers
{
    public class SubjectsController : Controller {
        public SubjectService _service;
        public SubjectsController(SubjectService service) {
            this._service = service;
        }
        public async Task<IActionResult> Index() {
            var allSubjects = await _service.GetAllAsync();
            return View(allSubjects);
        }
        public async Task<IActionResult> Details(int id) {
            var subject = await _service.GetByIdAsync(id);
            return View("Details", subject);
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Subject newSubject) {
            await _service.CreateAsync(newSubject);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edit(int id) {
            var subjectToEdit = await _service.GetByIdAsync(id);
            if (subjectToEdit== null) {
                return View("NotFound");
            }
            return View(subjectToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id, Name")] Subject updatedSubject) {
            await _service.UpdateAsync(id, updatedSubject);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id) {
            var subjectToDelete = await _service.GetByIdAsync(id);
            if (subjectToDelete == null) {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
