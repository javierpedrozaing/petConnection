using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using petConnection.Backend.UnitOfWork.Implementations;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]

    public class AdoptionsController : GenericController<Adoption>
    {        
        private readonly IAdoptionsUnitOfWork _adoptionsUnitOfWork;

        public AdoptionsController(IGenericUnitOfWork<Adoption> unitOfWork, IAdoptionsUnitOfWork adoptionsUnitOfWork) : base(unitOfWork)
        {            
            _adoptionsUnitOfWork = adoptionsUnitOfWork;
        }


        [HttpPost("adoption")]
        public async Task<IActionResult> PostAsync(AdoptionDTO adoptionDTO)
        {
            var action = await _adoptionsUnitOfWork.AddAdoptionAsync(User.Identity!.Name, adoptionDTO);

            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpGet("myAdoption")]
        public async Task<IActionResult> GetAsync()
        {
            var action = await _adoptionsUnitOfWork.GetAdoptionAsync(User.Identity!.Name);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpGet("countAdoptions")]
        public async Task<IActionResult> GetCountAdoptions()
        {
            // usuario logueado User.Identity!.Name
            var action = await _adoptionsUnitOfWork.GetCountAsync(User.Identity!.Name);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }
    }
}

