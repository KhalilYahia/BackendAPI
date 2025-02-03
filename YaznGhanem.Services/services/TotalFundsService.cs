using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YaznGhanem.Common;
using YaznGhanem.Domain;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;
using YaznGhanem.Services.Iservices;

namespace YaznGhanem.Services.services
{
    public class TotalFundsService : ITotalFundsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TotalFundsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

     
        public async Task<TotalFundsDto> GetAllAsync()
        {
            var TotalFundss = await _unitOfWork.repository<TotalFunds>().GetAllAsync();
            return _mapper.Map<TotalFundsDto>(TotalFundss.ToList().FirstOrDefault());
        }

        public async Task<ReportDto> GetAllOpt()
        {
            var result = new ReportDto();
            var TotalFund =  _unitOfWork.repository<TotalFunds>().GetAllAsync().Result.FirstOrDefault();
            result.earns = new earnsdto()
            {
                TotalIn = TotalFund.TotalIn,
                TotalOut = TotalFund.TotalOut,
                Profits = TotalFund.Profits
            };
            // Output from funds
            var daily_ = (await _unitOfWork.repository<Daily>().GetAllAsync()).ToList();
            var cars_ = (await _unitOfWork.repository<Cars>().GetAllAsync()).ToList();
            var employees_ = (await _unitOfWork.repository<Employees>().GetAllAsync()).ToList();
            var checkemployees_ = (await _unitOfWork.repository<DailyChekEmployees>().GetAllAsync()).ToList();
            var fuel_ = (await _unitOfWork.repository<Fuel>().GetAllAsync()).ToList();
            var repo_ = (await _unitOfWork.repository<Repository_InOut>().GetAllAsync()).ToList();
            var financialPayments_ = (await _unitOfWork.repository<FinancialPayment>().GetAllAsync()).ToList();

            // Input to funds
            var refrigerator_ = (await _unitOfWork.repository<Refrigerator>().GetAllAsync()).ToList();
            var externalEnvoices_ = (await _unitOfWork.repository<ExternalEnvoices>().GetAllAsync()).ToList();
            var coolingrooms_ = (await _unitOfWork.repository<CoolingRooms>().GetAllAsync()).ToList();
            var othersales_ = (await _unitOfWork.repository<OtherSales>().GetAllAsync()).ToList();

            // sum of all input
            decimal refrigerator_sum = refrigerator_.Sum(m => m.TotalSalesPriceOfAll);
            decimal externalEnvoices_sum = externalEnvoices_.Sum(m => m.SalesPriceOfAll);
            decimal coolingrooms_sum = coolingrooms_.Sum(m => m.CostOfAll);
            decimal othersales_sum = othersales_.Sum(m => m.SalesPriceOfAll);

            decimal employees_Payments_sum = employees_.Sum(m => m.Payments);
            decimal financialPayments_sum = financialPayments_.Sum(m => m.AmountPayment);

            result.TheSafe = new TheSafeDto()
            {
                TotalIn = refrigerator_sum + externalEnvoices_sum + coolingrooms_sum + othersales_sum,
                TotalOut = employees_Payments_sum + financialPayments_sum,
                Current = TotalFund.CurrentFund
            };

            var totalOPT = new List<TotalOperations>();

            #region All expenses operations
            daily_.ForEach((m) =>
            {
                totalOPT.Add(new TotalOperations
                {
                    Cost = m.BuyPriceOfAll,
                    Date = m.Date,
                    IsProfit = false
                });
            });
            cars_.ForEach((m) =>
            {
                totalOPT.Add(new TotalOperations
                {
                    Cost = m.TotalPrice,
                    Date = m.Date,
                    IsProfit = false
                });
            });
            fuel_.ForEach((m) =>
            {
                totalOPT.Add(new TotalOperations
                {
                    Cost = m.TotalPrice,
                    Date = m.Date,
                    IsProfit = false
                });
            });
            checkemployees_.ForEach((m) =>
            {
                totalOPT.Add(new TotalOperations
                {
                    Cost = m.PaidWage,
                    Date = m.Date,
                    IsProfit = false
                });
            });
            repo_.ForEach((m) =>
            {
                totalOPT.Add(new TotalOperations
                {
                    Cost = m.BuyPriceOfAll,
                    Date = m.Date,
                    IsProfit = false
                });
            });
            #endregion

            #region All revenue operations

            refrigerator_.ForEach((m) =>
            {
                totalOPT.Add(new TotalOperations
                {
                    Cost = m.TotalSalesPriceOfAll,
                    Date = m.Date,
                    IsProfit = true
                });
            });
            externalEnvoices_.ForEach((m) =>
            {
                totalOPT.Add(new TotalOperations
                {
                    Cost = m.SalesPriceOfAll,
                    Date = m.Date,
                    IsProfit = true
                });
            });
            coolingrooms_.ForEach((m) =>
            {
                totalOPT.Add(new TotalOperations
                {
                    Cost = m.CostOfAll,
                    Date = m.Date,
                    IsProfit = true
                });
            });
            othersales_.ForEach((m) =>
            {
                totalOPT.Add(new TotalOperations
                {
                    Cost = m.SalesPriceOfAll,
                    Date = m.Date,
                    IsProfit = true
                });
            });

            #endregion

            totalOPT= totalOPT.OrderByDescending(o => o.Date).ToList();

            var operationOneWeek = totalOPT.Where(m => m.Date > Utils.ServerNow.AddDays(-7)).ToList();

            result.Last7Days_revenue_Opt = operationOneWeek.Where(p => p.IsProfit).GroupBy(m => m.Date.Date)
                .Select(g => new TotalOperations
                {
                    Date = g.Key,
                    Cost = g.Sum(x => x.Cost), // Sum the Cost
                    IsProfit = g.All(x => x.IsProfit) // Check if all are profit or not
                }).OrderBy(o => o.Date)
                .ToList();

            result.Last7Days_expenses_Opt = operationOneWeek.Where(p => !p.IsProfit).GroupBy(m => m.Date.Date)
                .Select(g => new TotalOperations
                {
                    Date = g.Key,
                    Cost = g.Sum(x => x.Cost), // Sum the Cost
                    IsProfit = g.All(x => x.IsProfit) // Check if all are profit or not
                }).OrderBy(o => o.Date)
                .ToList();

            var last7Days = Enumerable.Range(0, 7)
                          .Select(i => DateTime.Now.Date.AddDays(-i))
                          .ToList();
            // Check for missing days and add them with Cost = 0
            foreach (var date in last7Days)
            {
                if (!result.Last7Days_revenue_Opt.Any(o => o.Date == date))
                {
                    result.Last7Days_revenue_Opt.Add(new TotalOperations
                    {
                        Date = date,
                        Cost = 0,
                        IsProfit = true // Since it's an revenue
                    });
                }
                if (!result.Last7Days_expenses_Opt.Any(o => o.Date == date))
                {
                    result.Last7Days_expenses_Opt.Add(new TotalOperations
                    {
                        Date = date,
                        Cost = 0,
                        IsProfit = false // Since it's an expense
                    });
                }
            }

            result.Last7Days_revenue_Opt = result.Last7Days_revenue_Opt.OrderBy(m => m.Date).ToList();
            result.Last7Days_expenses_Opt = result.Last7Days_expenses_Opt.OrderBy(m => m.Date).ToList();

            totalOPT.ForEach((item) =>
            {
                item.Cost = item.IsProfit ? item.Cost : -item.Cost;
            });

            decimal cumulativeCost = 0;

            result.AllOpt = totalOPT.GroupBy(m => m.Date.Date)
                .OrderBy(g => g.Key) // Order by Date first to ensure proper cumulative sum
                .Select(g =>
                {
                    cumulativeCost += g.Sum(x => x.Cost); // Add the current group's cost to the cumulative total
                    return new TotalOperations
                    {
                        Date = g.Key,
                        Cost = cumulativeCost, // Use the cumulative cost
                        IsProfit = g.All(x => x.IsProfit)
                    };
                })
                .ToList();
            return result;


        }

        public async Task<bool> UpdateFunds(TotalFundsDto dto)
        {
            var model = (await _unitOfWork.repository<TotalFunds>().Get(m => m.Id == dto.Id)).FirstOrDefault();
            model.TotalIn = dto.TotalIn;

            model.Profits = dto.Profits;
            model.CurrentFund = dto.CurrentFund;

             _unitOfWork.repository<TotalFunds>().Update(model);
           await _unitOfWork.Complete();
            return true;

        }

        public async Task<Report_DesktopDto> GetAllOpt_Fordesktop()
        {
            var result = new Report_DesktopDto();
            var TotalFund = _unitOfWork.repository<TotalFunds>().GetAllAsync().Result.FirstOrDefault();
            result.earns = new earnsdto()
            {
                TotalIn = TotalFund.TotalIn,
                TotalOut = TotalFund.TotalOut,
                Profits = TotalFund.Profits
            };
            // Output from funds
            var daily_ = (await _unitOfWork.repository<Daily>().GetAllAsync()).ToList();
            var cars_ = (await _unitOfWork.repository<Cars>().GetAllAsync()).ToList();
            var employees_ = (await _unitOfWork.repository<Employees>().GetAllAsync()).ToList();
            var checkemployees_ = (await _unitOfWork.repository<DailyChekEmployees>().Get(m => m.PaidWage > 0)).ToList();
            var fuel_ = (await _unitOfWork.repository<Fuel>().GetAllAsync()).ToList();
            var repo_ = (await _unitOfWork.repository<Repository_InOut>().GetAllAsync()).ToList();
            var financialPayments_ = (await _unitOfWork.repository<FinancialPayment>().GetAllAsync()).ToList();

            // Input to funds
            var refrigerator_ = (await _unitOfWork.repository<Refrigerator>().GetAllAsync()).ToList();
            var externalEnvoices_ = (await _unitOfWork.repository<ExternalEnvoices>().GetAllAsync()).ToList();
            var coolingrooms_ = (await _unitOfWork.repository<CoolingRooms>().GetAllAsync()).ToList();
            var othersales_ = (await _unitOfWork.repository<OtherSales>().GetAllAsync()).ToList();

            // sum of all input
            decimal refrigerator_sum = refrigerator_.Sum(m => m.TotalSalesPriceOfAll);
            decimal externalEnvoices_sum = externalEnvoices_.Sum(m => m.SalesPriceOfAll);
            decimal coolingrooms_sum = coolingrooms_.Sum(m => m.CostOfAll);
            decimal othersales_sum = othersales_.Sum(m => m.SalesPriceOfAll);

            decimal employees_Payments_sum = employees_.Sum(m => m.Payments);
            decimal financialPayments_sum = financialPayments_.Sum(m => m.AmountPayment);

            result.TheSafe = new TheSafeDto()
            {
                TotalIn = refrigerator_sum + externalEnvoices_sum + coolingrooms_sum + othersales_sum,
                TotalOut = employees_Payments_sum + financialPayments_sum,
                Current = TotalFund.CurrentFund
            };

            result.Operations = new List<Operations_Desktop>();

            result.Operations.AddRange(_mapper.Map<List<List<Operations_Desktop>>>(daily_).SelectMany(x=>x).ToList());
            result.Operations.AddRange(_mapper.Map<List<Operations_Desktop>>(cars_));
            result.Operations.AddRange(_mapper.Map<List<Operations_Desktop>>(checkemployees_));
            result.Operations.AddRange(_mapper.Map<List<Operations_Desktop>>(fuel_));
            result.Operations.AddRange(_mapper.Map<List<Operations_Desktop>>(repo_));

            result.Operations.AddRange(_mapper.Map<List<Operations_Desktop>>(refrigerator_));
            result.Operations.AddRange(_mapper.Map<List<Operations_Desktop>>(externalEnvoices_));
            result.Operations.AddRange(_mapper.Map<List<Operations_Desktop>>(coolingrooms_));
            result.Operations.AddRange(_mapper.Map<List<Operations_Desktop>>(othersales_));

            result.Operations = result.Operations.OrderByDescending(m => m.Date).ToList();

         
            return result;


        }

    }

}
