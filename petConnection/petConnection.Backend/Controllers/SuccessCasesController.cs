using System;
using Microsoft.AspNetCore.Mvc;
using petConnection.Backend.UnitOfWork.Implementations;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Controllers
{
    [ApiController]
    [Route("api/successCases")]
    public class SuccessCasesController : GenericController<SuccessCase>
    {
        private readonly ISuccessCasesUnitOfWork _successCasesUnitOfWork;

        public SuccessCasesController(IGenericUnitOfWork<SuccessCase> unitOfWork, ISuccessCasesUnitOfWork successCasesUnitOfWork) : base(unitOfWork)
        {
            _successCasesUnitOfWork = successCasesUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var response = await _successCasesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _successCasesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}