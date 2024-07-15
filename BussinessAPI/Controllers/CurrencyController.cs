using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BussinessAPI.Models;
using BussinessAPI.DAL;

namespace BussinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly currencydbContext _context;

        public CurrencyController(currencydbContext context)
        {
            _context = context;
        }

        // GET: api/Dailyrecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetDailyrecords()
        {
            return await _context.Dailyrecords.ToListAsync();
        }

        // GET: api/Dailyrecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetDailyrecord(int id)
        {
            var dailyrecord = await _context.Dailyrecords.FindAsync(id);

            if (dailyrecord == null)
            {
                return NotFound();
            }

            return dailyrecord;
        }

        //// PUT: api/Dailyrecords/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDailyrecord(int id, Currency dailyrecord)
        //{
        //    if (id != dailyrecord.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(dailyrecord).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DailyrecordExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Dailyrecords
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Currency>> PostDailyrecord(Currency dailyrecord)
        //{
        //    _context.Dailyrecords.Add(dailyrecord);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDailyrecord", new { id = dailyrecord.Id }, dailyrecord);
        //}

        //// DELETE: api/Dailyrecords/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDailyrecord(int id)
        //{
        //    var dailyrecord = await _context.Dailyrecords.FindAsync(id);
        //    if (dailyrecord == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Dailyrecords.Remove(dailyrecord);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool DailyrecordExists(int id)
        //{
        //    return _context.Dailyrecords.Any(e => e.Id == id);
        //}
    }
}
