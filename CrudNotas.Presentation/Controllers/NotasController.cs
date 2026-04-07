using Microsoft.AspNetCore.Mvc;
using CrudNotas.BusinessLayer;
using CrudNotas.EntityLayer;

namespace CrudNotas.Presentation.Controllers
{
    public class NotasController : Controller
    {
        private readonly NotaBL _notaBL;
        private readonly CursoBL _cursoBL;

        public NotasController(NotaBL notaBL, CursoBL cursoBL)
        {
            _notaBL = notaBL;
            _cursoBL = cursoBL;
        }

        // Listar todas las notas o filtradas por curso
        public IActionResult Index(int? cursoId)
        {
            var notas = _notaBL.ListarNotas();

            if (cursoId.HasValue)
            {
                notas = notas.Where(n => n.CursoId == cursoId.Value).ToList();
                ViewBag.Curso = _cursoBL.ListarCursos().FirstOrDefault(c => c.Id == cursoId.Value);
            }

            return View(notas);
        }

        // Detalles de una nota
        public IActionResult Details(int id)
        {
            var nota = _notaBL.ObtenerNota(id);
            if (nota == null) return NotFound();
            return View(nota);
        }

        // GET: Crear nota
        public IActionResult Create(int? cursoId)
        {
            ViewBag.Cursos = _cursoBL.ListarCursos();
            var nota = new Nota();
            if (cursoId.HasValue)
            {
                nota.CursoId = cursoId.Value; // precarga el curso
            }
            return View(nota);
        }

        // POST: Crear nota
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Nota nota)
        {
            if (ModelState.IsValid)
            {
                _notaBL.InsertarNota(nota);
                return RedirectToAction(nameof(Index), new { cursoId = nota.CursoId });
            }
            ViewBag.Cursos = _cursoBL.ListarCursos();
            return View(nota);
        }

        // GET: Editar nota
        public IActionResult Edit(int id)
        {
            var nota = _notaBL.ObtenerNota(id);
            if (nota == null) return NotFound();

            ViewBag.Cursos = _cursoBL.ListarCursos();
            return View(nota);
        }

        // POST: Editar nota
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Nota nota)
        {
            if (ModelState.IsValid)
            {
                _notaBL.ActualizarNota(nota);
                return RedirectToAction(nameof(Index), new { cursoId = nota.CursoId });
            }
            ViewBag.Cursos = _cursoBL.ListarCursos();
            return View(nota);
        }

        // GET: Eliminar nota
        public IActionResult Delete(int id)
        {
            var nota = _notaBL.ObtenerNota(id);
            if (nota == null) return NotFound();
            return View(nota);
        }

        // POST: Eliminar nota
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var nota = _notaBL.ObtenerNota(id);
            if (nota == null) return NotFound();

            _notaBL.EliminarNota(id);
            return RedirectToAction(nameof(Index), new { cursoId = nota.CursoId });
        }
    }
}
