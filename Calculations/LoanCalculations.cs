using Infrastructure.Interfaces;
using Models;
using System;
using System.Collections.Generic;

namespace Calculations
{
    public class LoanCalculations : ILoanCalculations
    {
        #region Public Methods
        /// <summary>
        /// Executing of Loan calculations for getting summary info
        /// </summary>
        /// <param name="amount">Loan amount</param>
        /// <param name="apr">Annual Percentage Rate</param>
        /// <param name="paymentsCount">Count of payments</param>
        /// <returns>Filled LoanSummaryItem</returns>
        public LoanSummaryItem GetSummaryInfo(decimal amount, int paymentsCount, decimal apr)
        {
            var payment = Math.Round(GetInstallmentAmount(amount, paymentsCount, apr));
            var totalRepaid = payment * paymentsCount;
            var totalInterest = totalRepaid - amount;
            var result = new LoanSummaryItem
            {
                WeeklyRepayment = payment,
                TotalRepaid = totalRepaid,
                TotalInterest = totalInterest
            };

            return result;
        }

        /// <summary>
        /// Executing of Loan calculations for getting list of RepaymentScheduleItem
        /// </summary>
        /// <param name="amount">Loan amount</param>
        /// <param name="apr">Annual Percentage Rate</param>
        /// <param name="paymentsCount">Count of payments</param>
        /// <returns>List of RepaymentScheduleItem</returns>
        public IEnumerable<RepaymentScheduleItem> GetRepaymentScheduleInfo(decimal amount, int paymentsCount, decimal apr)
        {
            var result = new List<RepaymentScheduleItem>(paymentsCount);

            var installment = GetInstallmentAmount(amount, paymentsCount, apr);
            var interestRate = GetInterestRate(paymentsCount, apr);
            var amountDue = amount;
            decimal interest;
            decimal principal;
            for(var i = 0; i < paymentsCount; i++)
            {
                interest = amountDue * interestRate;
                principal = installment - interest;

                result.Add(new RepaymentScheduleItem
                {
                    InstallmentNumber = i + 1,
                    AmountDue = Math.Round(amountDue, 2),
                    Interest = Math.Round(interest),
                    Principal = Math.Round(principal)
                });
                amountDue = amountDue - principal;
            }

            return result;
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Calculating of interest rate
        /// </summary>
        /// <param name="apr">Annual Percentage Rate</param>
        /// <param name="paymentsCount">Count of payments</param>
        /// <returns>Interest rate value</returns>
        private decimal GetInterestRate(int paymentsCount, decimal apr)
        {
            return apr / (100 * paymentsCount);
        }
        /// <summary>
        /// Calculating of intallment amount
        /// </summary>
        /// <param name="amount">Loan amount</param>
        /// <param name="apr">Annual Percentage Rate</param>
        /// <param name="paymentsCount">Count of payments</param>
        /// <returns>Interest rate value</returns>
        private decimal GetInstallmentAmount(decimal amount, int paymentsAmount, decimal apr)
        {
            var interestRate = GetInterestRate(paymentsAmount, apr);

            return amount *
                   (interestRate /
                    (decimal)(1.0 - Math.Pow(decimal.ToDouble(1 + interestRate), -paymentsAmount)));
        }
        #endregion

    }
}
