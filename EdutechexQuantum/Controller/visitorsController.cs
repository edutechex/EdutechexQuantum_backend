using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdutechexQuantum.DTO;
using EdutechexQuantum.Model;

namespace EdutechexQuantum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public VisitorsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visitors>>> GetVisitors()
        {
            if (_dbContext.Visitors == null)
            {
                return NotFound();
                return BadRequest();
            }
            return await _dbContext.Visitors.ToListAsync();
        }

        [HttpPost] /*Add Counselor*/
        public async Task<ActionResult> AddVisitors(AddVisitors visitors)
        {
            try
            {
                Visitors f = new Visitors();
                f.IPaddress = visitors.IPaddress;
                f.region = visitors.region;
                f.city = visitors.city;
                f.country = visitors.country;
                f.post_code = visitors.post_code;
                f.date = DateTime.Now;
                await _dbContext.Visitors.AddAsync(f);
                await _dbContext.SaveChangesAsync();
                return Ok(f);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
