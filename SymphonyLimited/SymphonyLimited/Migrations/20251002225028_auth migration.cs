using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SymphonyLimited.Migrations
{
    /// <inheritdoc />
    public partial class authmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auths",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellNo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auths", x => x.AdminID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auths_CellNo",
                table: "Auths",
                column: "CellNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auths_Username",
                table: "Auths",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auths");
        }
    }
}
