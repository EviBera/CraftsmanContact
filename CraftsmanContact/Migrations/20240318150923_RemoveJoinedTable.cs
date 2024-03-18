using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftsmanContact.Migrations
{
    /// <inheritdoc />
    public partial class RemoveJoinedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersAndServicesJoinedTable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersAndServicesJoinedTable",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OfferedServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAndServicesJoinedTable", x => new { x.AppUserId, x.OfferedServiceId });
                    table.ForeignKey(
                        name: "FK_UsersAndServicesJoinedTable_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersAndServicesJoinedTable_OfferedServices_OfferedServiceId",
                        column: x => x.OfferedServiceId,
                        principalTable: "OfferedServices",
                        principalColumn: "OfferedServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersAndServicesJoinedTable_OfferedServiceId",
                table: "UsersAndServicesJoinedTable",
                column: "OfferedServiceId");
        }
    }
}
