using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraDeVeiculos.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Tabela_Plano : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBPlano",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoPlano = table.Column<int>(type: "int", nullable: false),
                    PrecoDiario = table.Column<double>(type: "float", nullable: true),
                    PrecoPorKm = table.Column<double>(type: "float", nullable: true),
                    KmLivres = table.Column<double>(type: "float", nullable: true),
                    PrecoPorKmExplorado = table.Column<double>(type: "float", nullable: true),
                    PrecoLivre = table.Column<double>(type: "float", nullable: true),
                    GrupoAutomovelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBPlano", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBPlano_AspNetUsers_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TBPlano_TBGrupoAutomovel_GrupoAutomovelId",
                        column: x => x.GrupoAutomovelId,
                        principalTable: "TBGrupoAutomovel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBPlano_EmpresaId",
                table: "TBPlano",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_TBPlano_GrupoAutomovelId",
                table: "TBPlano",
                column: "GrupoAutomovelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBPlano");
        }
    }
}
