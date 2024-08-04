using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMS01.Migrations
{
    /// <inheritdoc />
    public partial class roleIdAddedInLecturerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Lecturers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Lecturers");
        }
    }
}
