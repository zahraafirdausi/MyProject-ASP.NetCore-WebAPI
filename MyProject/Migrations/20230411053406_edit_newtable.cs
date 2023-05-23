using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class editnewtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Employee_EmployeeNIK",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_EmployeeNIK",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "EmployeeNIK",
                table: "Account");

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

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNIK",
                table: "Account",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Account_EmployeeNIK",
                table: "Account",
                column: "EmployeeNIK");

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
