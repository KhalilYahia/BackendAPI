using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Model;

namespace YaznGhanem.Data.Repositories
{
    // AppDbContextExtensions.cs
    public static class UnitOfWorkExtensions
    {
        public static async Task<ReportResult> GetReportDataAsync(this IUnitOfWork IunitOfWork)
        {
            var result = new ReportResult();
            var context = IunitOfWork.GetDbContext(); 

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "EXEC GetReportData";
                command.CommandType = CommandType.Text;

                await context.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    // Read Total Funds
                    if (await reader.ReadAsync())
                    {
                        result.TotalFunds = new TotalFundsResult
                        {
                            TotalIn = reader.GetDecimal(0),
                            TotalOut = reader.GetDecimal(1),
                            Profits = reader.GetDecimal(2),
                            CurrentFund = reader.GetDecimal(3)
                        };
                    }

                    // Read TheSafe Totals
                    await reader.NextResultAsync();
                    if (await reader.ReadAsync())
                    {
                        result.TheSafe = new TheSafeResult
                        {
                            TotalIn = reader.GetDecimal(0),
                            TotalOut = reader.GetDecimal(1)
                        };
                    }

                    // Read Operations
                    await reader.NextResultAsync();
                    result.Operations = new List<OperationResult>();
                    while (await reader.ReadAsync())
                    {
                        result.Operations.Add(new OperationResult
                        {
                            Date = reader.GetDateTime(0),
                            Amount = reader.GetDecimal(1),
                            IsProfit = reader.GetBoolean(2)
                        });
                    }
                }
                await context.Database.CloseConnectionAsync();
            }
            return result;
        }
    }

}
