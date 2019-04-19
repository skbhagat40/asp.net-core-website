using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovies.Migrations
{
    public partial class new1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectedActors",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Actors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedActors",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Actors");
        }
    }
}
