using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp1.Migrations
{
    /// <inheritdoc />
    public partial class Editedtablesagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Invoices");
        }
    }
}
