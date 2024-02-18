using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lanchonete.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Lanches",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2");

            //Popula a tabela com dados iniciais
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, Descricao) " +
                "VALUES ('Normal', 'Lanche feito com ingredientes normais')");
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome, Descricao) " +
              "VALUES ('Natural', 'Lanche feito com ingredientes integrais e naturais')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Lanches",
                type: "decimal(10,2",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
