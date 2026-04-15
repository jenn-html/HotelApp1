using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp1.Migrations
{
    /// <inheritdoc />
    public partial class Updatesinrooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "RoomNumbers",
                schema: "dbo",
                startValue: 200L);

            migrationBuilder.AlterColumn<int>(
                name: "RoomNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR dbo.RoomNumbers",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "RoomNumbers",
                schema: "dbo");

            migrationBuilder.AlterColumn<int>(
                name: "RoomNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR dbo.RoomNumbers");
        }
    }
}
