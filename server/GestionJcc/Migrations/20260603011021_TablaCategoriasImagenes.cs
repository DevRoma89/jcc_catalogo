using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionJcc.Migrations
{
    /// <inheritdoc />
    public partial class TablaCategoriasImagenes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoImagen_Productos_ProductoId",
                table: "ProductoImagen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoImagen",
                table: "ProductoImagen");

            migrationBuilder.RenameTable(
                name: "ProductoImagen",
                newName: "ProductoImagenes");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoImagen_ProductoId",
                table: "ProductoImagenes",
                newName: "IX_ProductoImagenes_ProductoId");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoImagenes",
                table: "ProductoImagenes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoImagenes_Productos_ProductoId",
                table: "ProductoImagenes",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Categorias_CategoriaId",
                table: "Productos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoImagenes_Productos_ProductoId",
                table: "ProductoImagenes");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Categorias_CategoriaId",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoImagenes",
                table: "ProductoImagenes");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Productos");

            migrationBuilder.RenameTable(
                name: "ProductoImagenes",
                newName: "ProductoImagen");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoImagenes_ProductoId",
                table: "ProductoImagen",
                newName: "IX_ProductoImagen_ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoImagen",
                table: "ProductoImagen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoImagen_Productos_ProductoId",
                table: "ProductoImagen",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
