using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petConnection.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.DropTable(
           name: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                // other columns...
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });
        }
    }
}
