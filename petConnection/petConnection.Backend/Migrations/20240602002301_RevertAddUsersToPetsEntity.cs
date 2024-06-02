using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petConnection.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RevertAddUsersToPetsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Pets_PetId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PetId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PetId",
                table: "AspNetUsers",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pets_PetId",
                table: "AspNetUsers",
                column: "PetId",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
