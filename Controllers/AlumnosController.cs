using Microsoft.AspNetCore.Mvc;
using SistemaExpedientesAcademicos.Data;
using SistemaExpedientesAcademicos.Models;
using System.Linq;

namespace SistemaExpedientesAcademicos.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly AppDbContext _context;

        public AlumnosController(AppDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            var alumnos = _context.Alumnos.ToList();
            return View(alumnos);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Alumno alumno)
        {
            _context.Alumnos.Add(alumno);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var alumno = _context.Alumnos.Find(id);
            if (alumno == null) return NotFound();
            return View(alumno);
        }

        // POST: Alumnos/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Alumno alumno)
        {
            if (id != alumno.AlumnoId) return NotFound();
            _context.Update(alumno);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var alumno = _context.Alumnos.Find(id);
            if (alumno == null) return NotFound();
            return View(alumno);
        }

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var alumno = _context.Alumnos.Find(id);
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}