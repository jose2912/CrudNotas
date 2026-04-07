using Microsoft.AspNetCore.Mvc;
using CrudNotas.BusinessLayer;
using CrudNotas.EntityLayer;

namespace CrudNotas.Presentation.Controllers
{
    public class CursosController : Controller
    {
        private readonly CursoBL _cursoBL;

        // Constructor: inyecta la capa de negocio de cursos
        public CursosController(CursoBL cursoBL, NotaBL notaBL)
        {
            _cursoBL = cursoBL;
        }
        // GET: Cursos/Index
        // Lista todos los cursos disponibles
        public IActionResult Index()
        {
            var cursos = _cursoBL.ListarCursos();
            return View(cursos);
        }
        // GET: Cursos/Details/5
        // Muestra los detalles de un curso específico
        public IActionResult Details(int id)
        {
            var curso = _cursoBL.ListarCursos().FirstOrDefault(c => c.Id == id);
            if (curso == null) return NotFound();
            return View(curso);
        }
        // GET: Cursos/Create
        // Muestra el formulario para crear un nuevo curso
        public IActionResult Create()
        {
            return View(new Curso());
        }

        // POST: Cursos/Create
        // Inserta un curso en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Curso curso)
        {
            if (ModelState.IsValid)
            {
                _cursoBL.InsertarCurso(curso);
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }
        // POST: Cursos/DeleteConfirmed/5
        // Elimina un curso y sus notas asociadas
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var curso = _cursoBL.ListarCursos().FirstOrDefault(c => c.Id == id);
            if (curso == null) return NotFound();

            if (curso.Notas != null && curso.Notas.Any())
            {
                foreach (var nota in curso.Notas.ToList())
                {
                    _cursoBL.EliminarCurso(nota.Id);
                }
            }

            _cursoBL.EliminarCurso(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Cursos/Edit/5
        // Muestra el formulario para editar un curso existente
        public IActionResult Edit(int id)
        {
            var nota = _cursoBL.ObtenerCurso(id);
            if (nota == null) return NotFound();

            ViewBag.Cursos = _cursoBL.ListarCursos();
            return View(nota);
        }


        // POST: Cursos/Edit/5
        // Actualiza los datos de un curso en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Curso curso)
        {
            if (ModelState.IsValid)
            {
                _cursoBL.ActualizarCurso(curso);
                return RedirectToAction(nameof(Index), new { cursoId = curso.Id });
            }
            ViewBag.Cursos = _cursoBL.ListarCursos();
            return View(curso);
        }
    }
}
