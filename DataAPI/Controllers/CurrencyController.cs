using DataAPI.DAL;
using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyDBContext _dbcontext;


        public CurrencyController(CurrencyDBContext currencydbContext)
        {
            _dbcontext = currencydbContext;
        }

        //GET
        [HttpGet]
        public ActionResult<IEnumerable<DailyRecords>> GetDailyRecords()
        {

            return _dbcontext.DailyRecordss;
        }


        [HttpGet("{Id:int}")]
        public async Task<ActionResult<DailyRecords>> GetById(int id)
        {

            var records = await _dbcontext.DailyRecordss.FindAsync(id);
            return records;
        }

        //POST
        [HttpPost]
        public async Task<ActionResult> Create(DailyRecords dailyRecords)
        {
            await _dbcontext.DailyRecordss.AddAsync(dailyRecords);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }


        //PUT
        [HttpPut]
        public async Task<ActionResult> Update(DailyRecords dailyRecords)
        {

            _dbcontext.DailyRecordss.Update(dailyRecords);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }


        //DELETE
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var records = await _dbcontext.DailyRecordss.FindAsync(id);
            _dbcontext.DailyRecordss.Remove(records);
            await _dbcontext.SaveChangesAsync();
            return Ok();

        }
    }
}
