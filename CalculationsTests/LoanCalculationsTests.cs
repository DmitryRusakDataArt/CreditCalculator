using Calculations;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace CalculationsTests
{
    [TestClass]
    public class LoanCalculationsTests
    {
        private ICalculationService _calculationService;

        private static readonly LoanSummaryItem _expectedResult = new LoanSummaryItem
        {
            WeeklyRepayment = 1058,
            TotalRepaid = 55016,
            TotalInterest = 5016
        };

        [TestInitialize]
        public void Intialize()
        {
            _calculationService = new CalculationService(new LoanCalculations());
        }

        [DataTestMethod]
        [DataRow(50000, 19)]
        public void GetSummaryInfo_ReturnsCorrectValues(int amount, int weekCount)
        {
            var actualResult = _calculationService.GetSummaryInfo(amount, weekCount);
            Assert.AreEqual(_expectedResult, actualResult);
        }

        [DataTestMethod]
        [DataRow(-50000, 19)]
        public void GetSummaryInfoReturn_CorrectButNegativeValues_IfAmountIsNegative(int amount, int weekCount)
        {
            var actualResult = _calculationService.GetSummaryInfo(amount, weekCount);

            Assert.AreEqual(_expectedResult.WeeklyRepayment, -actualResult.WeeklyRepayment);
            Assert.AreEqual(_expectedResult.TotalRepaid, -actualResult.TotalRepaid);
            Assert.AreEqual(_expectedResult.TotalInterest, -actualResult.TotalInterest);
        }

        [DataTestMethod]
        [DataRow(5000, -19)]
        public void GetSummaryInfo_ReturnIncorrectValues_IfWeekCountIsNegative(int amount, int weekCount)
        {
            var actualResult = _calculationService.GetSummaryInfo(amount, weekCount);
            Assert.AreNotEqual(_expectedResult, actualResult);
        }
    }
}
