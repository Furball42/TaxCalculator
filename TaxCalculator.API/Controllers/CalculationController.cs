using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaxCalculator.Core.Dtos;
using TaxCalculator.Core.Models;
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
    }
}