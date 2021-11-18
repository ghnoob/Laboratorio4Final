using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalLaboratorio4.Data;
using FinalLaboratorio4.Models;
using FinalLaboratorio4.Models.ProductoViewModels;
using FinalLaboratorio4.Helpers;

namespace FinalLaboratorio4.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Productos.Include(p => p.Categoria).Include(p => p.Marca);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Descripcion");

            PopulateProveedoresData();

            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,Descripcion,Imagen,CategoriaId,MarcaId")] Producto producto, string[] selectedProveedores)
        {
            if (ModelState.IsValid)
            {
                producto.UrlImagen = Path.GetRelativePath(
                    _env.WebRootPath,
                    FileManager.Create(
                        producto.Imagen,
                        Path.Combine(_env.WebRootPath, "img", "productos")
                    )
                );

                producto.Proveedores = new List<Proveedor>();

                UpdateProductoProveedores(selectedProveedores, producto);

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", producto.CategoriaId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Descripcion", producto.MarcaId);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto producto = await _context.Productos
                .Include(p => p.Proveedores)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", producto.CategoriaId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Descripcion", producto.MarcaId);

            PopulateProveedoresData(producto);

            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedProveedores)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto producto = await _context.Productos
                .Include(p => p.Proveedores)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (
                await TryUpdateModelAsync<Producto>(
                    producto,
                    "",
                    p => p.Nombre,
                    p => p.Precio,
                    p => p.Descripcion,
                    p => p.CategoriaId,
                    p => p.MarcaId,
                    p => p.Imagen
                )
            )
            {
                if (producto.Imagen != null)
                {
                    FileManager.Delete(Path.Combine(_env.WebRootPath, producto.UrlImagen));

                    producto.UrlImagen = Path.GetRelativePath(
                        _env.WebRootPath,
                        FileManager.Create(
                            producto.Imagen,
                            Path.Combine(_env.WebRootPath, "img", "productos")
                        )
                    );
                }

                UpdateProductoProveedores(selectedProveedores, producto);
                PopulateProveedoresData(producto);

                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion", producto.CategoriaId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Descripcion", producto.MarcaId);
            return View(producto);
        }

        private void PopulateProveedoresData()
        {
            DbSet<Proveedor> proveedores = _context.Proveedores;
            List<AssignedProveedorData> viewModel = proveedores.Select(
                p => new AssignedProveedorData()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Asignado = false
                }
            ).ToList();

            ViewData["Proveedores"] = viewModel;
        }

        private void PopulateProveedoresData(Producto producto)
        {
            DbSet<Proveedor> proveedores = _context.Proveedores;
            HashSet<int> productoProveedores = new(producto.Proveedores.Select(p => p.Id));

            List<AssignedProveedorData> viewModel = proveedores.Select(
                p => new AssignedProveedorData()
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Asignado = productoProveedores.Contains(p.Id)
                }
            ).ToList();

            ViewData["Proveedores"] = viewModel;
        }

        private void UpdateProductoProveedores(string[] proveedores, Producto producto)
        {
            if (proveedores == null)
            {
                producto.Proveedores = new List<Proveedor>();
                return;
            }

            HashSet<string> proveedoresHS = new(proveedores);
            HashSet<int> productoProveedores = new(producto.Proveedores.Select(p => p.Id));

            foreach (Proveedor proveedor in _context.Proveedores)
            {
                if (
                    proveedoresHS.Contains(proveedor.Id.ToString())
                    && !productoProveedores.Contains(proveedor.Id)
                )
                {
                    producto.Proveedores.Add(proveedor);
                }
                else if (productoProveedores.Contains(proveedor.Id))
                {
                    producto.Proveedores.Remove(proveedor);
                }
            }
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            FileManager.Delete(Path.Combine(_env.WebRootPath, producto.UrlImagen));

            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
