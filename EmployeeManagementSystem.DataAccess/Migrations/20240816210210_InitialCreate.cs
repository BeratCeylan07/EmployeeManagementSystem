using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ISCREATEDUSERID = table.Column<int>(type: "INTEGER", nullable: false),
                    ISMODIFIEDUSERID = table.Column<int>(type: "INTEGER", nullable: true),
                    ISCREATEDDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISMODIFIEDDATE = table.Column<DateTime>(type: "TEXT", nullable: true),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ISCREATEDUSERID = table.Column<int>(type: "INTEGER", nullable: false),
                    ISMODIFIEDUSERID = table.Column<int>(type: "INTEGER", nullable: true),
                    ISCREATEDDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISMODIFIEDDATE = table.Column<DateTime>(type: "TEXT", nullable: true),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<double>(type: "REAL", maxLength: 100, nullable: false),
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: false),
                    ISCREATEDUSERID = table.Column<int>(type: "INTEGER", nullable: false),
                    ISMODIFIEDUSERID = table.Column<int>(type: "INTEGER", nullable: true),
                    ISCREATEDDATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ISMODIFIEDDATE = table.Column<DateTime>(type: "TEXT", nullable: true),
                    isActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeePayments_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayments_EmployeeID",
                table: "EmployeePayments",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
