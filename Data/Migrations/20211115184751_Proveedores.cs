using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalLaboratorio4.Data.Migrations
{
    public partial class Proveedores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Domicilio = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Localidad = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Provincia = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductoProveedor",
                columns: table => new
                {
                    ProductosId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProveedoresId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoProveedor", x => new { x.ProductosId, x.ProveedoresId });
                    table.ForeignKey(
                        name: "FK_ProductoProveedor_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoProveedor_Proveedores_ProveedoresId",
                        column: x => x.ProveedoresId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoProveedor_ProveedoresId",
                table: "ProductoProveedor",
                column: "ProveedoresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoProveedor");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
