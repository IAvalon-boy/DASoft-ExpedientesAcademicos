using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaExpedientesAcademicos.Data;
using SistemaExpedientesAcademicos.Models;
using System.Linq;

namespace SistemaExpedientesAcademicos.Controllers
{
    public class ExpedientesController : Controller
    {
        private readonly AppDbContext _context;

        public ExpedientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Expedientes
        public IActionResult Index()
        {
            var expedientes = _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .ToList();
            return View(expedientes);
        }

        // GET: Expedientes/Create
        public IActionResult Create()
        {
            ViewBag.Alumnos = _context.Alumnos.ToList();
            ViewBag.Materias = _context.Materias.ToList();
            return View();
        }

        // POST: Expedientes/Create
        [HttpPost]
        public IActionResult Create(int AlumnoId, int MateriaId, decimal NotaFinal, string Observaciones)
        {
            var expediente = new Expedientes
            {
                AlumnoId = AlumnoId,
                MateriaId = MateriaId,
                NotaFinal = NotaFinal,
                Observaciones = Observaciones
            };

            _context.Expedientes.Add(expediente);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var expediente = _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefault(e => e.ExpedienteId == id);

            if (expediente == null) return NotFound();

            ViewBag.Alumnos = _context.Alumnos.ToList();
            ViewBag.Materias = _context.Materias.ToList();
            return View(expediente);
        }

        
        [HttpPost]
        public IActionResult Edit(int ExpedienteId, int AlumnoId, int MateriaId, decimal NotaFinal, string Observaciones)
        {
            var expediente = new Expedientes
            {
                ExpedienteId = ExpedienteId,
                AlumnoId = AlumnoId,
                MateriaId = MateriaId,
                NotaFinal = NotaFinal,
                Observaciones = Observaciones
            };

            _context.Update(expediente);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var expediente = _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefault(e => e.ExpedienteId == id);

            if (expediente == null) return NotFound();
            return View(expediente);
        }

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var expediente = _context.Expedientes.Find(id);
            if (expediente != null)
            {
                _context.Expedientes.Remove(expediente);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}