using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YaznGhanem.Data.Migrations
{
    /// <inheritdoc />
    public partial class PROCEDURE_GetReportData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb82e5e2-b60e-4fce-9c91-f92fbd329fef", "AQAAAAIAAYagAAAAEAQieihZVK6BoFp53VfjSaXFowhBOb4DCAm3ilLiuf0s0+qVw/hGl1Gwsjc0LEN2cw==", "5ba74554-626c-4cd9-919d-516d3e8a8d3f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843yy",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d84e70ae-9765-4070-ba3a-d16427eb6232", "AQAAAAIAAYagAAAAENbqZP3t0AF6M1xwb/DDlARUnmE20/FJJd3MI4K1Q333Zlb0e/23vZYjLkqzj0WIFQ==", "4c7e7579-7fc0-41d7-8988-53a74e41fcbd" });

            // add procedure 
            migrationBuilder.Sql(@"CREATE PROCEDURE GetReportData
                                AS
                                BEGIN
                                    -- 1. Total Funds
                                    SELECT TOP 1 
                                        TotalIn, 
                                        TotalOut, 
                                        Profits,
                                        CurrentFund
                                    FROM TotalFunds;
    
                                    -- 2. TheSafe Totals
                                    SELECT 
                                        (SELECT ISNULL(SUM(TotalSalesPriceOfAll), 0) FROM Refrigerator) +
                                        (SELECT ISNULL(SUM(SalesPriceOfAll), 0) FROM ExternalEnvoices) +
                                        (SELECT ISNULL(SUM(CostOfAll), 0) FROM CoolingRooms) +
                                        (SELECT ISNULL(SUM(SalesPriceOfAll), 0) FROM OtherSales) AS TotalIn,
        
                                        (SELECT ISNULL(SUM(Payments), 0) FROM Employees) +
                                        (SELECT ISNULL(SUM(AmountPayment), 0) FROM FinancialPayment) AS TotalOut;
    
                                    -- 3. Operations
                                    SELECT 
                                        Date,
                                        Amount,
                                        CAST(IsProfit AS BIT) AS IsProfit
                                    FROM (
                                        -- Revenue
                                        SELECT Date, TotalSalesPriceOfAll AS Amount, 1 AS IsProfit FROM Refrigerator
                                        UNION ALL
                                        SELECT Date, SalesPriceOfAll AS Amount, 1 AS IsProfit FROM ExternalEnvoices
                                        UNION ALL
                                        SELECT Date, CostOfAll AS Amount, 1 AS IsProfit FROM CoolingRooms
                                        UNION ALL
                                        SELECT Date, SalesPriceOfAll AS Amount, 1 AS IsProfit FROM OtherSales
        
                                        UNION ALL
        
                                        -- Expenses
                                        SELECT Date, BuyPriceOfAll AS Amount, 0 AS IsProfit FROM Dailies
                                        UNION ALL
                                        SELECT Date, TotalPrice AS Amount, 0 AS IsProfit FROM Cars
                                        UNION ALL
                                        SELECT Date, TotalPrice AS Amount, 0 AS IsProfit FROM Fuel
                                        UNION ALL
                                        SELECT Date, PaidWage AS Amount, 0 AS IsProfit FROM DailyChekEmployees
                                        UNION ALL
                                        SELECT Date, BuyPriceOfAll AS Amount, 0 AS IsProfit FROM Repository_InOut
                                    ) AS Operations;
                                END
                            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetReportData");
        }
    }
}
