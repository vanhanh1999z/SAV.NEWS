using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NEWS.Extensions;
using NEWS.Models;

namespace NEWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("authenticatied")]
    public class ArticlesController : ControllerBase
    {
        private readonly SocialfireContext _context;

        public ArticlesController(SocialfireContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpPost]
        [Route("listbypaing")]
        public async Task<IActionResult> GetArticles([FromBody] PaginationFilter? filter)
        {
            if (filter == null)
            {
                filter = new PaginationFilter();
            }
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Articles
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .Include(x => x.subjectIdNavigition)
                    .ToListAsync();
            var res = from x in pagedData
                      select new Article()
                      {
                          No = x.No,
                          Url = x.Url,
                          Emotion = x.Emotion,
                          Title = x.Title,
                          SiteName = x.SiteName,
                          Time = x.Time,
                          Date = x.Date,
                          SubjectName = x.SubjectName,
                          Image = x.Image,
                          Process = x.Process,
                          SiteId = x.SiteId,
                          SubjectId = x.SubjectId,
                          ResourceType = x.ResourceType,
                          subjectIdNavigition = x.subjectIdNavigition
                      };
            if (_context.Articles == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!",
                    totalItem = 0
                });
            }

            var totalRecords = await _context.Articles.CountAsync();
            return Ok(new PagedResponse<List<Article>>(res.ToList(), validFilter.PageNumber, validFilter.PageSize)
            { totalItem = totalRecords });
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(long id)
        {
            if (_context.Articles == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!"
                });
            }
            var article = await _context.Articles
                .Include(x => x.subjectIdNavigition)
                .FirstOrDefaultAsync(x => x.No.Equals(id));
            if (article == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!"
                });
            }

            return Ok(new ResultMessageResponse()
            {
                data = article,
                success = true,
                message = "Sucess!",
                totalItem = 1
            });
        }

        [HttpPost("getbysubject")]
        public async Task<IActionResult> GetListBySubject([FromBody] SubjectByID subject)
        {
            if (subject.Pagination == null)
            {
                subject.Pagination = new PaginationFilter();
            }
            if (_context.Articles == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!"
                });
            }
            var validFilter = new PaginationFilter(subject.Pagination.PageNumber, subject.Pagination.PageSize);
            var pagedData = await _context.Articles
                    .Where(el => el.SubjectId.Equals(subject.SubjectId))
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .Include(subject => subject.subjectIdNavigition)
                    .ToListAsync();

            if (pagedData == null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Not Found!"
                });
            }
            var res = from x in pagedData
                      select new Article()
                      {
                          No = x.No,
                          Url = x.Url,
                          Emotion = x.Emotion,
                          Title = x.Title,
                          SiteName = x.SiteName,
                          Time = x.Time,
                          Date = x.Date,
                          SubjectName = x.SubjectName,
                          Image = x.Image,
                          Process = x.Process,
                          SiteId = x.SiteId,
                          SubjectId = x.SubjectId,
                          ResourceType = x.ResourceType,
                          subjectIdNavigition = x.subjectIdNavigition
                      };
            var totalRecords = await _context.Articles
                    .Where(el => el.SubjectId.Equals(subject.SubjectId))
                    .ToListAsync();
            return Ok(new PagedResponse<List<Article>>(res.ToList(), validFilter.PageNumber, validFilter.PageSize)
            { totalItem = totalRecords.Count() });
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(long id, Article article)
        {
            if (id != article.No)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            if (_context.Articles == null)
            {
                return Problem("Entity set 'SocialfireContext.Articles'  is null.");
            }
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.No }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(long id)
        {
            if (_context.Articles == null)
            {
                return NotFound();
            }
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(long id)
        {
            return (_context.Articles?.Any(e => e.No == id)).GetValueOrDefault();
        }
    }
}