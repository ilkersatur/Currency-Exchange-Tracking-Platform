using BussinessAPI.Interfaces;
using BussinessAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BussinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachingCurrencyController : ControllerBase
    {
        private readonly ICurrencyServices _currencyServices;

        public CachingCurrencyController(ICurrencyServices currencyServices)
        {
            _currencyServices = currencyServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Currency> currencies = await _currencyServices.GetAllAsync();
            return Ok(currencies);
        }
    }
}
