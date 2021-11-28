using Microsoft.AspNetCore.Mvc;
using FinalLaboratorio4.Models;

namespace FinalLaboratorio4.ViewComponents
{
    public class CardProductoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Producto producto)
        {
            return View(producto);
        }
    }
}
