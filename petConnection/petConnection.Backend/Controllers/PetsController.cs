using System;
using Microsoft.AspNetCore.Mvc;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : GenericController<Pet>
    {
        public PetsController(IGenericUnitOfWork<Pet> unitOfWork) : base(unitOfWork)
        {
        }
    }
}

