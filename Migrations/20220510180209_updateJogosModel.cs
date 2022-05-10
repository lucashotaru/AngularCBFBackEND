using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularCBFBackEND.Migrations
{
    /// <inheritdoc />
    public partial class updateJogosModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SiglaTimeCasa",
                table: "jogos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SiglaTimeVisitante",
                table: "jogos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiglaTimeCasa",
                table: "jogos");

            migrationBuilder.DropColumn(
                name: "SiglaTimeVisitante",
                table: "jogos");
        }
    }
}
