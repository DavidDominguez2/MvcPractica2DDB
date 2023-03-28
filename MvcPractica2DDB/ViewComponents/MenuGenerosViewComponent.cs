using Microsoft.AspNetCore.Mvc;
using MvcPractica2DDB.Models;
using MvcPractica2DDB.Repositories;

namespace MvcPractica2DDB.ViewComponents {
    public class MenuGenerosViewComponent : ViewComponent {

        private RepositoryLibros repo;

        public MenuGenerosViewComponent(RepositoryLibros repo) {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            List<Genero> generos = await this.repo.GetGenerosAsync();
            return View(generos);
        }

    }
}
