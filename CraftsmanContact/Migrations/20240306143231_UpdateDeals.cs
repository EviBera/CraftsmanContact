using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftsmanContact.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfferedServiceId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferedServiceId",
                table: "Deals");
        }
    }
}
