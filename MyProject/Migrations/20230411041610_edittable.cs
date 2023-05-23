using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class edittable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Employee_EmployeeNIK",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "EmployeeNIK",
                table: "Account",
                newName: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Employee_NIK",
                table: "Account",
                column: "NIK",
                principalTable: "Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Employee_NIK",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "Account",
                newName: "EmployeeNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Employee_EmployeeNIK",
                table: "Account",
                column: "EmployeeNIK",
                principalTable: "Employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
