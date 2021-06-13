using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        private readonly ICalculationService _calculationService;

        public CalculationController(IUnitOfWork unitOfWork,
            ICalculationService calculationService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _calculationService = calculationService;
        }

        [Route("DoTaxCalculation/{annualIncome}/{code}")]
        public async Task<CalculationResultDto> DoTaxCalculation(decimal annualIncome, string code)
        {
            return await _calculationService.ReturnDtoAndSave(annualIncome, code);
        }

        //generic call
        [HttpGet]
        public async Task<List<CalculationListOutputDto>> GetAllCalculationResults()
        {
            var list = await _unitOfWork.CalculationResults.GetAll();            
            var mapped = _mapper.Map<List<CalculationListOutputDto>>(list);
            return mapped;
        }

        //specific for datatables
        [Route("GetAllForDatables")]
        public async Task<DataTablesResponseDto> GetAllForDatables()
        {
            var list = await _unitOfWork.CalculationResults.GetAll();
            var mapped = _mapper.Map<List<CalculationListOutputDto>>(list.ToList());
            return new DataTablesResponseDto()
            {
                Data = mapped.ToArray(),
                RecordsTotal = mapped.Count,
            };
        }
    }
}