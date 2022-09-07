using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEWS.Extensions;
using NEWS.Models;

namespace NEWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly SocialfireContext _context;

        public SitesController(SocialfireContext context)
        {
            _context = context;
        }

        // GET: api/Sites
        [HttpGet]
        public async Task<IActionResult> GetSites([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Sites
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToListAsync();
            if (_context.Sites == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!",
                    totalItem = 0
                });
            }
            var totalRecords = await _context.Sites.CountAsync();
            return Ok(new PagedResponse<List<Site>>(pagedData, validFilter.PageNumber, validFilter.PageSize)
            { totalItem = totalRecords });
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSite(int id)
        {
            if (_context.Sites == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!"
                });
            }
            var site = await _context.Sites.FindAsync(id);

            return Ok(new ResultMessageResponse()
            {
                data = site,
                success = true,
                message = "Sucess!",
                totalItem = 1
            });
        }

        // PUT: api/Sites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSite(int id, Site site)
        {
            if (id != site.SiteId)
            {
                return BadRequest();
            }

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Site>> PostSite(Site site)
        {
            if (_context.Sites == null)
            {
                return Problem("Entity set 'SocialfireContext.Sites'  is null.");
            }
            _context.Sites.Add(site);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SiteExists(site.SiteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSite", new { id = site.SiteId }, site);
        }

        // DELETE: api/Sites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            if (_context.Sites == null)
            {
                return NotFound();
            }
            var site = await _context.Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteExists(int id)
        {
            return (_context.Sites?.Any(e => e.SiteId == id)).GetValueOrDefault();
        }
    }
}