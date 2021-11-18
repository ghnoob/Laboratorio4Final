using System;
using System.Linq;
using Bogus;
using FinalLaboratorio4.Models;

namespace FinalLaboratorio4.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Productos.Any())
            {
                return;
            }

            int categoriaId = 1;

            Faker<Categoria> testCategoria = new Faker<Categoria>("es")
                .RuleFor(c => c.Id, f => categoriaId++)
                // se le concatena el id para asegurarse que el valor generado sea unico y
                // no provoque errores en la base de datos
                .RuleFor(c => c.Descripcion, f => $"{categoriaId} {f.Commerce.Department()}");

            Categoria[] categorias = testCategoria.GenerateLazy(20).ToArray();

            context.Categorias.AddRange(categorias);

            context.SaveChanges();

            int marcaId = 1;

            Faker<Marca> testMarca = new Faker<Marca>("es")
                .RuleFor(m => m.Id, f => marcaId++)
                .RuleFor(m => m.Descripcion, f => f.Lorem.Word());

            Marca[] marcas = testMarca.GenerateLazy(45).ToArray();

            context.Marcas.AddRange(marcas);

            context.SaveChanges();

            int proveedorId = 1;

            Faker<Proveedor> testProveedor = new Faker<Proveedor>("es")
                .RuleFor(p => p.Id, f => proveedorId++)
                .RuleFor(p => p.Nombre, f => f.Company.CompanyName())
                .RuleFor(p => p.Telefono, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Domicilio, f => f.Address.StreetAddress())
                .RuleFor(p => p.Localidad, f => f.Address.City())
                .RuleFor(p => p.Provincia, f => f.Address.State());

            Proveedor[] proveedores = testProveedor.GenerateLazy(35).ToArray();

            context.AddRange(proveedores);

            context.SaveChanges();

            int productoId = 1;

            Faker<Producto> testProducto = new Faker<Producto>("es")
                .RuleFor(p => p.Id, f => productoId++)
                .RuleFor(p => p.Nombre, f => f.Commerce.ProductName())
                .RuleFor(p => p.Precio, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(p => p.Categoria, f => f.PickRandom(categorias))
                .RuleFor(p => p.Marca, f => f.PickRandom(marcas))
                .RuleFor(p => p.Descripcion, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.UrlImagen, f => f.Image.PicsumUrl())
                .RuleFor(p => p.Favorito, f => f.Random.Bool(0.2f))
                .RuleFor(p => p.Proveedores, f => f.PickRandom(proveedores, f.Random.Int(0, 4)).ToArray());

            Producto[] productos = testProducto.GenerateLazy(120).ToArray();

            context.AddRange(productos);

            context.SaveChanges();
        }
    }
}
