using Microsoft.AspNetCore.Mvc;
using MvcPractica2DDB.Extensions;
using MvcPractica2DDB.Filters;
using MvcPractica2DDB.Models;
using MvcPractica2DDB.Repositories;

namespace MvcPractica2DDB.Controllers {
    public class LibrosController : Controller {

        private RepositoryLibros repo;

        public LibrosController(RepositoryLibros repo) {
            this.repo = repo;
        }

        public async Task<IActionResult> Index(int? posicion) {
            posicion = (posicion == null) ? 0 : posicion.Value;

            LibrosPaginados paginados = await this.repo.GetLibrosPaginadosAsync(posicion.Value);

            ViewData["REGISTROS"] = paginados.NumRegistros;
            return View(paginados.Libros);
        }

        public async Task<IActionResult> LibrosGenero(int idgenero) {
            List<Libro> librosGenero = await this.repo.GetLibrosGeneroAsync(idgenero);
            return View(librosGenero);
        }

        public async Task<IActionResult> Details(int idlibro) {
            Libro libro = await this.repo.FindLibroAsync(idlibro);
            return View(libro);
        }

        public IActionResult AddToCart(int idlibro) {
            List<int> listidslibros = HttpContext.Session.GetObject<List<int>>("CARRITO");

            if (listidslibros != null) {
                listidslibros.Add(idlibro);
                HttpContext.Session.SetObject("CARRITO", listidslibros);
            } else {
                List<int> newlist = new List<int> { idlibro };
                HttpContext.Session.SetObject("CARRITO", newlist);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Carrito() {
            List<int> listidlibros = HttpContext.Session.GetObject<List<int>>("CARRITO");
            List<Libro> libros = new List<Libro>();
            if (listidlibros != null) {
                foreach (int id in listidlibros) {
                    Libro libro = await this.repo.FindLibroAsync(id);
                    libros.Add(libro);
                }
            }

            return View(libros);
        }


    }
}
