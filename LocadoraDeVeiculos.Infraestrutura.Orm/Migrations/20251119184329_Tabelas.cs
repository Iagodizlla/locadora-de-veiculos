using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraDeVeiculos.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Tabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "TBFuncionario",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TBCondutor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Cnh = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    ValidadeCnh = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCondutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBCondutor_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBFuncionario_EmpresaId",
                table: "TBFuncionario",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_TBCondutor_UsuarioId",
                table: "TBCondutor",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBFuncionario_AspNetUsers_EmpresaId",
                table: "TBFuncionario",
                column: "EmpresaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBFuncionario_AspNetUsers_EmpresaId",
                table: "TBFuncionario");

            migrationBuilder.DropTable(
                name: "TBCondutor");

            migrationBuilder.DropIndex(
                name: "IX_TBFuncionario_EmpresaId",
                table: "TBFuncionario");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "TBFuncionario");
        }
    }
}
