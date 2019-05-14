using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanSummaryController : ControllerBase
    {
        private readonly ICalculationService _calculationService;

        public LoanSummaryController(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        /// <summary>
        /// Performs the calculations of the Loan: how much a weekly installment will be, the total amount of interest paid and the overall cost of the loan.
        /// </summary>
        /// <param name="amount">Loan amount</param>
        /// <param name="apr">Annual Percentage Rate</param>
        /// <returns>Loan info data item</returns>
        [HttpGet()]
        public JsonResult Get(int amount, int apr)
        {
            if(amount < 0 || apr < 0)
            {
                return new JsonResult("Check input data. All amounts must be more than zero.");
            }
            var result = _calculationService.GetSummaryInfo(amount, apr);
            return new JsonResult(result);
        }

    }
}