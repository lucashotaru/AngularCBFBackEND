using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularCBFBackEND.Migrations
{
    /// <inheritdoc />
    public partial class AddJogosModelAnoTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnoTabela",
                table: "jogos",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoTabela",
                table: "jogos");
        }
    }
}
