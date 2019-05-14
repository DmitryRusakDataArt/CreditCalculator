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
    public class RepaymentScheduleController : ControllerBase
    {
        private readonly ICalculationService _calculationService;

        public RepaymentScheduleController(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        /// <summary>
        /// Performs calculating of repayment scheduler
        /// </summary>
        /// <param name="amount">Loan amount</param>
        /// <param name="apr">Annual Percentage Rate</param>
        /// <returns>List of repayments in terms of interest paid and principal repaid</returns>
        [HttpGet()]
        public JsonResult Get(int amount, int apr)
        {
            if(amount < 0 || apr < 0)
            {
                return new JsonResult("Check input data. All amounts must be more than zero.");
            }

            var result = _calculationService.GetRepaymentScheduleInfo(amount, apr);
            return new JsonResult(new { installments = result });
        }

    }
}