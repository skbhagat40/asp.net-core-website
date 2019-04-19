using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovies.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Producer_ProducerId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "Movies",
                newName: "ProducerID");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_ProducerId",
                table: "Movies",
                newName: "IX_Movies_ProducerID");

            migrationBuilder.AlterColumn<int>(
                name: "ProducerID",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ProducerName",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Producer_ProducerID",
                table: "Movies",
                column: "ProducerID",
                principalTable: "Producer",
                principalColumn: "ProducerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Producer_ProducerID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ProducerName",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "ProducerID",
                table: "Movies",
                newName: "ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_ProducerID",
                table: "Movies",
                newName: "IX_Movies_ProducerId");

            migrationBuilder.AlterColumn<int>(
                name: "ProducerId",
                table: "Movies",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Producer_ProducerId",
                table: "Movies",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "ProducerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
