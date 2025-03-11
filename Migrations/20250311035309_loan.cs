using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPLoanMSC103.Migrations
{
    /// <inheritdoc />
    public partial class loan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionTypes = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, defaultValueSql: "GETDATE()"),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, defaultValueSql: "GETDATE()"),
                    Updated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoanId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LoanCategoryId = table.Column<int>(type: "int", nullable: false),
                    RegsiterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Installment = table.Column<int>(type: "int", nullable: false),
                    Principle = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Loans_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_LoanCategories_LoanCategoryId",
                        column: x => x.LoanCategoryId,
                        principalTable: "LoanCategories",
                        principalColumn: "LoanCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loanSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<long>(type: "bigint", nullable: false),
                    LineSeq = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidPrinciple = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutStanding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    LoanTransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loanSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_loanSchedules_Loans_LoanTransactionId",
                        column: x => x.LoanTransactionId,
                        principalTable: "Loans",
                        principalColumn: "TransactionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CustomerId",
                table: "Loans",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_EmployeeId",
                table: "Loans",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanCategoryId",
                table: "Loans",
                column: "LoanCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_loanSchedules_LoanTransactionId",
                table: "loanSchedules",
                column: "LoanTransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loanSchedules");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "transactions");
        }
    }
}
