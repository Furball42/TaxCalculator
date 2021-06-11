using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxCalculator.Repo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculationResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculatedTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlatRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlatValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValueThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Progressives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtendedData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progressives", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FlatRates",
                columns: new[] { "Id", "Description", "Rate" },
                values: new object[] { 1, "Flat Rate", 17.5m });

            migrationBuilder.InsertData(
                table: "FlatValues",
                columns: new[] { "Id", "Description", "Percentage", "Value", "ValueThreshold" },
                values: new object[] { 1, "Flat Rate", 5.0m, 10000m, 200000m });

            migrationBuilder.InsertData(
                table: "PostalCodes",
                columns: new[] { "Id", "CalculationType", "Description", "ReferenceId" },
                values: new object[,]
                {
                    { 1, 2, "7441", 1 },
                    { 2, 0, "A100", 1 },
                    { 3, 1, "7000", 1 },
                    { 4, 2, "1000", 1 }
                });

            migrationBuilder.InsertData(
                table: "Progressives",
                columns: new[] { "Id", "Description", "ExtendedData" },
                values: new object[] { 1, "Progressive", "[{\"Rate\":10.0,\"Min\":0.0,\"Max\":8350.0,\"SortOrder\":1},{\"Rate\":15.0,\"Min\":8351.0,\"Max\":33950.0,\"SortOrder\":2},{\"Rate\":25.0,\"Min\":33951.0,\"Max\":82250.0,\"SortOrder\":3},{\"Rate\":28.0,\"Min\":82251.0,\"Max\":171550.0,\"SortOrder\":4},{\"Rate\":33.0,\"Min\":171551.0,\"Max\":372950.0,\"SortOrder\":5},{\"Rate\":35.0,\"Min\":372951.0,\"Max\":-1.0,\"SortOrder\":6}]" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculationResult");

            migrationBuilder.DropTable(
                name: "FlatRates");

            migrationBuilder.DropTable(
                name: "FlatValues");

            migrationBuilder.DropTable(
                name: "PostalCodes");

            migrationBuilder.DropTable(
                name: "Progressives");
        }
    }
}
