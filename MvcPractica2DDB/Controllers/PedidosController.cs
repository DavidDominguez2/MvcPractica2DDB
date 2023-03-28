using Microsoft.AspNetCore.Mvc;
using MvcPractica2DDB.Filters;

namespace MvcPractica2DDB.Controllers {
    public class PedidosController : Controller {
        [AuthorizeUsers]
        public async Task<IActionResult> RealizarPedido() {
            return RedirectToAction("Index", "Libros");
        }
    }
}
