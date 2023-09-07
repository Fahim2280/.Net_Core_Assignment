using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations
{
    /// <inheritdoc />
    public partial class A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbsentCount",
                table: "EmployeeAttendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OffdayCount",
                table: "EmployeeAttendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PresentCount",
                table: "EmployeeAttendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsentCount",
                table: "EmployeeAttendances");

            migrationBuilder.DropColumn(
                name: "OffdayCount",
                table: "EmployeeAttendances");

            migrationBuilder.DropColumn(
                name: "PresentCount",
                table: "EmployeeAttendances");
        }
    }
}
