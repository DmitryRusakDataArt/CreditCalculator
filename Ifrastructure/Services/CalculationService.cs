using Common;
using Infrastructure.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly ILoanCalculations _loanCalculations;

        public CalculationService(ILoanCalculations loanCalculations)
        {
            _loanCalculations = loanCalculations;
        }

        /// <summary>
        /// Service methood for calculations of the Loan
        /// </summary>
        /// <param name="amount">Loan amount</param>
        /// <param name="apr">Annual Percentage Rate</param>
        /// <returns>LoanSummaryItem</returns>
        public LoanSummaryItem GetSummaryInfo(decimal amount, decimal apr)
        {
            return _loanCalculations.GetSummaryInfo(amount, Constants.PAYMENT_PERIOD, apr);
        }
    }
}
