using Calculations;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Linq;

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

        private static readonly RepaymentScheduleItem _week1Info = new RepaymentScheduleItem
        {
            InstallmentNumber = 1,
            AmountDue = 50000,
            Principal = 875,
            Interest = 183
        };
        private static readonly RepaymentScheduleItem _week2Info = new RepaymentScheduleItem
        {
            InstallmentNumber = 2,
            AmountDue = 49125.17m,
            Principal = 878,
            Interest = 179
        };
        private static readonly RepaymentScheduleItem _week10Info = new RepaymentScheduleItem
        {
            InstallmentNumber = 10,
            AmountDue = 42010.44m,
            Principal = 904,
            Interest = 153
        };
        private static readonly RepaymentScheduleItem _week26Info = new RepaymentScheduleItem
        {
            InstallmentNumber = 26,
            AmountDue = 27142.8m,
            Principal = 958,
            Interest = 99
        };
        private static readonly RepaymentScheduleItem _week52Info = new RepaymentScheduleItem
        {
            InstallmentNumber = 52,
            AmountDue = 1053.68m,
            Principal = 1054,
            Interest = 4
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

        [DataTestMethod]
        [DataRow(50000, 19)]
        public void GetRepaymentScheduleInfo_ReturnsCorrectValues(int amount, int weekCount)
        {
            var actualResult = _calculationService.GetRepaymentScheduleInfo(amount, weekCount).ToArray();

            Assert.AreEqual(_week1Info, actualResult[0]);
            Assert.AreEqual(_week2Info, actualResult[1]);
            Assert.AreEqual(_week10Info, actualResult[9]);
            Assert.AreEqual(_week26Info, actualResult[25]);
            Assert.AreEqual(_week52Info, actualResult[51]);
        }

        [DataTestMethod]
        [DataRow(-50000, 19)]
        public void GetRepaymentScheduleInfo_ReturnsCorrectButNegativeValues_IfAmountIsNegative(int amount, int weekCount)
        {
            var actualResult = _calculationService.GetRepaymentScheduleInfo(amount, weekCount).ToArray();

            Assert.AreEqual(_week1Info.InstallmentNumber, actualResult[0].InstallmentNumber);
            Assert.AreEqual(_week1Info.AmountDue, -actualResult[0].AmountDue);
            Assert.AreEqual(_week1Info.Interest, -actualResult[0].Interest);
            Assert.AreEqual(_week1Info.Principal, -actualResult[0].Principal);

            Assert.AreEqual(_week2Info.InstallmentNumber, actualResult[1].InstallmentNumber);
            Assert.AreEqual(_week2Info.AmountDue, -actualResult[1].AmountDue);
            Assert.AreEqual(_week2Info.Interest, -actualResult[1].Interest);
            Assert.AreEqual(_week2Info.Principal, -actualResult[1].Principal);

            Assert.AreEqual(_week10Info.InstallmentNumber, actualResult[9].InstallmentNumber);
            Assert.AreEqual(_week10Info.AmountDue, -actualResult[9].AmountDue);
            Assert.AreEqual(_week10Info.Interest, -actualResult[9].Interest);
            Assert.AreEqual(_week10Info.Principal, -actualResult[9].Principal);

            Assert.AreEqual(_week26Info.InstallmentNumber, actualResult[25].InstallmentNumber);
            Assert.AreEqual(_week26Info.AmountDue, -actualResult[25].AmountDue);
            Assert.AreEqual(_week26Info.Interest, -actualResult[25].Interest);
            Assert.AreEqual(_week26Info.Principal, -actualResult[25].Principal);

            Assert.AreEqual(_week52Info.InstallmentNumber, actualResult[51].InstallmentNumber);
            Assert.AreEqual(_week52Info.AmountDue, -actualResult[51].AmountDue);
            Assert.AreEqual(_week52Info.Interest, -actualResult[51].Interest);
            Assert.AreEqual(_week52Info.Principal, -actualResult[51].Principal);
        }

        [DataTestMethod]
        [DataRow(50000, -19)]
        public void GetRepaymentScheduleInfo_ReturnsIncorrectValues_IfWeekCountIsNegative(int amount, int weekCount)
        {
            var actualResult = _calculationService.GetRepaymentScheduleInfo(amount, weekCount).ToArray();

            Assert.AreNotEqual(_week1Info, actualResult[0]);
            Assert.AreNotEqual(_week2Info, actualResult[1]);
            Assert.AreNotEqual(_week10Info, actualResult[9]);
            Assert.AreNotEqual(_week26Info, actualResult[25]);
            Assert.AreNotEqual(_week52Info, actualResult[51]);
        }
    }
}
