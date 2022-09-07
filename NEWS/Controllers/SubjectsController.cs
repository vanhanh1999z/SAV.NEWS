using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEWS.Extensions;
using NEWS.Models;

namespace NEWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly SocialfireContext _context;

        public SubjectsController(SocialfireContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            if (_context.Subjects == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!"
                });
            }
            var res = await _context.Subjects.ToListAsync();
            return Ok(new ResultMessageResponse()
            {
                data = res,
                success = true,
                message = "Sucess!",
                totalItem = res.Count()
            });
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            if (_context.Subjects == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!"
                });
            }
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!"
                });
            }

            return Ok(new ResultMessageResponse()
            {
                data = subject,
                success = true,
                message = "Sucess!",
                totalItem = 1
            });
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
            if (_context.Subjects == null)
            {
                return Problem("Entity set 'SocialfireContext.Subjects'  is null.");
            }
            _context.Subjects.Add(subject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubjectExists(subject.SubjectId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubject", new { id = subject.SubjectId }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            if (_context.Subjects == null)
            {
                return NotFound();
            }
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(int id)
        {
            return (_context.Subjects?.Any(e => e.SubjectId == id)).GetValueOrDefault();
        }
    }
}