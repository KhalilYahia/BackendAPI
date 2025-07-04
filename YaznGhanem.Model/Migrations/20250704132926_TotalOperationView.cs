using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YaznGhanem.Data.Migrations
{
    /// <inheritdoc />
    public partial class TotalOperationView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "754981be-f748-45dd-aec7-48954f4f8fe2", "AQAAAAIAAYagAAAAEJl/6UGuK7cscG6tiUtvK9jVhBZFmXLSSCJK3HMb8OAvF1XYsRIgbuYrsEVCo1NzlQ==", "899f8d86-c232-46d2-9863-facae19ed759" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843yy",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a83664d2-d5d1-4ba0-abb4-2687c0a2b7f8", "AQAAAAIAAYagAAAAEFNQ0VS7UJlJJQxCqbgKUXzJLUoPkClXF6uo+ZjswbOf4LMWUZnlM8lspO0CiFAQXA==", "083560a3-102b-446c-b2a2-5a827daefbd3" });
            #region SQL View

            migrationBuilder.Sql(@"
                    CREATE VIEW SQLView_TotalOperations AS
                    SELECT BuyPriceOfAll AS Cost, [Date], 0 AS IsProfit FROM Dailies
                    UNION ALL
                    SELECT TotalPrice AS Cost, [Date], 0 AS IsProfit FROM Cars
                    UNION ALL
                    SELECT TotalPrice AS Cost, [Date], 0 AS IsProfit FROM Fuel
                    UNION ALL
                    SELECT PaidWage AS Cost, [Date], 0 AS IsProfit FROM DailyChekEmployees
                    UNION ALL
                    SELECT BuyPriceOfAll AS Cost, [Date], 0 AS IsProfit FROM Repository_InOut
                    UNION ALL
                    SELECT TotalSalesPriceOfAll AS Cost, [Date], 1 AS IsProfit FROM Refrigerator
                    UNION ALL
                    SELECT SalesPriceOfAll AS Cost, [Date], 1 AS IsProfit FROM ExternalEnvoices
                    UNION ALL
                    SELECT CostOfAll AS Cost, [Date], 1 AS IsProfit FROM CoolingRooms
                    UNION ALL
                    SELECT SalesPriceOfAll AS Cost, [Date], 1 AS IsProfit FROM OtherSales;
                ");

            migrationBuilder.Sql(@"
        CREATE VIEW SQLView_TheSafe AS
        SELECT
            (SELECT ISNULL(SUM(TotalSalesPriceOfAll), 0) FROM Refrigerator) +
            (SELECT ISNULL(SUM(SalesPriceOfAll), 0) FROM ExternalEnvoices) +
            (SELECT ISNULL(SUM(CostOfAll), 0) FROM CoolingRooms) +
            (SELECT ISNULL(SUM(SalesPriceOfAll), 0) FROM OtherSales) AS TotalIn,

            (SELECT ISNULL(SUM(Payments), 0) FROM Employees) +
            (SELECT ISNULL(SUM(AmountPayment), 0) FROM FinancialPayment) AS TotalOut;");

         

            #endregion SQL View

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9778337-e8dd-42f9-bea2-50a66b09be7f", "AQAAAAIAAYagAAAAEL48HIYVOvJpZs1FpRnm+h/+L0sN0iqHL31XoOZ5cN2tYWZMv85EMC9zDqUph1pnMg==", "bee71fd8-8c0d-4663-a58e-056ff17cd601" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843yy",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89368f41-65d4-449d-b0ff-cc323a446d64", "AQAAAAIAAYagAAAAEKzEftvEdJGsUfJm/q+EqFPgEsCjtzweDLjNhIJBTZSCy1nnUQToOhsSm6SHDXVUeg==", "386393b5-2411-4c74-86b2-7d225ac94b20" });
           
         
            migrationBuilder.Sql("DROP VIEW IF EXISTS SQLView_TheSafe;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS SQLView_TotalOperations;");

        }
    }
}
