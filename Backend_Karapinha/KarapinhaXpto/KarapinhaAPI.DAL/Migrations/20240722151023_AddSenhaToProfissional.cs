using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KarapinhaAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSenhaToProfissional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Profissionais",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Profissionais");
        }
    }
}
