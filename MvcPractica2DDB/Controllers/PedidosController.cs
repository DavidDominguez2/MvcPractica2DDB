using Microsoft.AspNetCore.Mvc;
using MvcPractica2DDB.Extensions;
using MvcPractica2DDB.Filters;
using MvcPractica2DDB.Models;
using MvcPractica2DDB.Repositories;
using System.Security.Claims;

namespace MvcPractica2DDB.Controllers {
    public class PedidosController : Controller {

        private RepositoryLibros repo;

        public PedidosController(RepositoryLibros repo) {
            this.repo = repo;
        }

        [AuthorizeUsers]
        public async Task<IActionResult> RealizarPedido() {
            List<int> listidlibros = HttpContext.Session.GetObject<List<int>>("CARRITO");
            int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            foreach (int id in listidlibros) {
                await this.repo.InsertPedidoAsync(id, idusuario);
            }


            return RedirectToAction("DetallesPedidos", "Pedidos");
        }

        public async Task<IActionResult> DetallesPedidos() {
            int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<VistaPedidos> pedidos = await this.repo.GetAllPedidosUserAsync(idusuario);
            return View(pedidos);
        }
    }
}
