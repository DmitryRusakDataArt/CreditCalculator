using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces
{
    public interface ILoanCalculations
    {
        LoanSummaryItem GetSummaryInfo(decimal amount, int paymentsCount, decimal apr);

        IEnumerable<RepaymentScheduleItem> GetRepaymentScheduleInfo(decimal amount, int paymentsCount, decimal apr);
    }
}
