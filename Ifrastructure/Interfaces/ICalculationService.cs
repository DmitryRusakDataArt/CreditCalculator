using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface ICalculationService
    {
        LoanSummaryItem GetSummaryInfo(decimal amount, decimal apr);

        IEnumerable<RepaymentScheduleItem> GetRepaymentScheduleInfo(decimal amount, decimal apr);
    }
}
