using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Core.Dtos;
using TaxCalculator.Core.Models;
using TaxCalculator.Core.Models.CalculationResults;
using TaxCalculator.Core.Services;

namespace TaxCalculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICalculationService _calculationService;

        public CalculationController(IUnitOfWork unitOfWork,
            ICalculationService calculationService)
        {
            _unitOfWork = unitOfWork;
            _calculationService = calculationService;
        }

        [Route("DoTaxCalculation/{annualIncome}/{code}")]
        [HttpGet]
        public async Task<CalculationResultDto> DoTaxCalculation(decimal annualIncome, string code)
        {
            return await _calculationService.ReturnDtoAndSave(annualIncome, code);
        }

        //generic call
        [Route("GetCalculations")]
        [HttpGet]
        public async Task<List<CalculationResult>> GetCalculations()
        {
            var list = await _unitOfWork.CalculationResults.GetAll();
            return list.ToList();
        }

        //specific for datatables
        [Route("GetCalculationsForDatables")]
        [HttpGet]
        public async Task<DataTablesResponseDto> GetCalculationsForDatables()
        {
            var result = await _unitOfWork.CalculationResults.GetAll();
            var list = result.ToList();

            return new DataTablesResponseDto()
            {
                Data = list.ToArray(),
                RecordsTotal = list.Count,
            };
        }
    }
}