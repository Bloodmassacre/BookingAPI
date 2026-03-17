using Microsoft.EntityFrameworkCore;
using Api.Models;
namespace Api.events_repo
{
    public class EventsRepository
    {
        private readonly AppDbContext _db;

        public EventsRepository(AppDbContext db)
        {
            _db = db;
        }

        // Получить все события
        public async Task<List<Events>> GetAll()
        {
            return await _db.Events.ToListAsync();
        }
    }
}