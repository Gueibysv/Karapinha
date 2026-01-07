using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarapinhaAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IDCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IDCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    IDUtilizador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BilheteIdentidade = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    NomeUtilizador = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.IDUtilizador);
                });

            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    IDProfissional = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IDCategoria = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BilheteIdentidade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.IDProfissional);
                    table.ForeignKey(
                        name: "FK_Profissionais_Categorias_IDCategoria",
                        column: x => x.IDCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IDCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    IDServico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IDCategoria = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.IDServico);
                    table.ForeignKey(
                        name: "FK_Servicos_Categorias_IDCategoria",
                        column: x => x.IDCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IDCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marcacoes",
                columns: table => new
                {
                    IDMarcacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUtilizador = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcacoes", x => x.IDMarcacao);
                    table.ForeignKey(
                        name: "FK_Marcacoes_Utilizadores_IDUtilizador",
                        column: x => x.IDUtilizador,
                        principalTable: "Utilizadores",
                        principalColumn: "IDUtilizador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicoProfissionais",
                columns: table => new
                {
                    IDProfissional = table.Column<int>(type: "int", nullable: false),
                    IDServico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicoProfissionais", x => new { x.IDProfissional, x.IDServico });
                    table.ForeignKey(
                        name: "FK_ServicoProfissionais_Profissionais_IDProfissional",
                        column: x => x.IDProfissional,
                        principalTable: "Profissionais",
                        principalColumn: "IDProfissional");
                    table.ForeignKey(
                        name: "FK_ServicoProfissionais_Servicos_IDServico",
                        column: x => x.IDServico,
                        principalTable: "Servicos",
                        principalColumn: "IDServico");
                });

            migrationBuilder.CreateTable(
                name: "ServicoMarcacoes",
                columns: table => new
                {
                    IDMarcacao = table.Column<int>(type: "int", nullable: false),
                    IDServico = table.Column<int>(type: "int", nullable: false),
                    IDProfissional = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicoMarcacoes", x => new { x.IDMarcacao, x.IDServico, x.IDProfissional });
                    table.ForeignKey(
                        name: "FK_ServicoMarcacoes_Marcacoes_IDMarcacao",
                        column: x => x.IDMarcacao,
                        principalTable: "Marcacoes",
                        principalColumn: "IDMarcacao");
                    table.ForeignKey(
                        name: "FK_ServicoMarcacoes_Profissionais_IDProfissional",
                        column: x => x.IDProfissional,
                        principalTable: "Profissionais",
                        principalColumn: "IDProfissional",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicoMarcacoes_Servicos_IDServico",
                        column: x => x.IDServico,
                        principalTable: "Servicos",
                        principalColumn: "IDServico");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_IDUtilizador",
                table: "Marcacoes",
                column: "IDUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_IDCategoria",
                table: "Profissionais",
                column: "IDCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoMarcacoes_IDProfissional",
                table: "ServicoMarcacoes",
                column: "IDProfissional");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoMarcacoes_IDServico",
                table: "ServicoMarcacoes",
                column: "IDServico");

            migrationBuilder.CreateIndex(
                name: "IX_ServicoProfissionais_IDServico",
                table: "ServicoProfissionais",
                column: "IDServico");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_IDCategoria",
                table: "Servicos",
                column: "IDCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicoMarcacoes");

            migrationBuilder.DropTable(
                name: "ServicoProfissionais");

            migrationBuilder.DropTable(
                name: "Marcacoes");

            migrationBuilder.DropTable(
                name: "Profissionais");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
