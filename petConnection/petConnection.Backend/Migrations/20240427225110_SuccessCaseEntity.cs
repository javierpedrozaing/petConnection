using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petConnection.Backend.Migrations
{
    /// <inheritdoc />
    public partial class SuccessCaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraint
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Users_UserId",
                table: "Cities");

            // Drop primary key and indexes
            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");            

            // Drop column with identity
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Profiles");


            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuccessCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuccessCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuccessCases_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_Id",
                table: "Pets",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Id",
                table: "Articles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuccessCases_Id",
                table: "SuccessCases",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuccessCases_PetId",
                table: "SuccessCases",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "SuccessCases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Pets_Id",
                table: "Pets");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Profiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_UserId",
                table: "Cities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Users_UserId",
                table: "Cities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
