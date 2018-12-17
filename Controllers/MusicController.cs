using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dot_net_api.Models;

namespace dot_net_api.Controllers
{
    [Route("api/music")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly MusicContext _context;

        public MusicController(MusicContext context)
        {
            _context = context;

            if (_context.SongItems.Count() == 0)
            {
                // Create a new SongItem if collection is empty,
                // which means you can't delete all SongItems.
                _context.SongItems.Add(new SongItem { name = "Song 1", artist_name = "Hover brew" });
                _context.SaveChanges();
            }
        }
        // GET: api/Song
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongItem>>> GetSongItems()
        {
            return await _context.SongItems.ToListAsync();
        }

        // GET: api/Song/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SongItem>> GetSongItem(long id)
        {
            var songItem = await _context.SongItems.FindAsync(id);

            if (songItem == null)
            {
                return NotFound();
            }

            return songItem;
        }

        // POST: api/Song
        [HttpPost]
        public async Task<ActionResult<SongItem>> PostSongItem(SongItem songItem)
        {
            _context.SongItems.Add(songItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSongItem", new { id = songItem.id }, songItem);
        }

        // PUT: api/Song/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongItem(long id, SongItem song)
        {
            if (id != song.id)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SongItem>> DeleteSongItem(long id)
        {
            var songItem = await _context.SongItems.FindAsync(id);
            if (songItem == null)
            {
                return NotFound();
            }

            _context.SongItems.Remove(songItem);
            await _context.SaveChangesAsync();

            return songItem;
        }

        
    }

}