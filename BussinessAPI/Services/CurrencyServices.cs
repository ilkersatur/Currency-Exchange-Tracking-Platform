using BussinessAPI.DAL;
using BussinessAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BussinessAPI.Interfaces
{
    public interface ICurrencyServices
    {
         Task<List<Currency>> GetAllAsync();
    }
    public class CurrencyServices : ICurrencyServices
    {
        private readonly CachingDataDBContext _dbContext;

        public CurrencyServices(CachingDataDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Currency>> GetAllAsync()
        {
            var currencies = await _dbContext.Dailyrecords.ToListAsync();
            return currencies;
        }
    }
}
