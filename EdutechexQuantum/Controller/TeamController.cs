using EdutechexQuantum.DTO;
using EdutechexQuantum.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EdutechexQuantum.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TeamController(AppDbContext dbContext,
               IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = environment;
        }

        [NonAction]
        public async Task<string> UploadImage(IFormFile imageFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var image = Path.Combine(_hostingEnvironment.WebRootPath, "images", imageName);
            using (var fileStream = new FileStream(image, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            var path = GetImageUrl(HttpContext, imageName);
            return path;
        }
        [NonAction]
        public string RemoveImage(string code)
        {
            string Imagepath = code;
            try
            {
                if (System.IO.File.Exists(Imagepath))
                {
                    System.IO.File.Delete(Imagepath);
                }
                return Imagepath;
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }
        [NonAction]
        public string GetImageUrl(HttpContext context, string imageName)
        {
            return Path.Combine(GetBaseUrl(context), "images", imageName);
        }
        [NonAction]
        public string GetBaseUrl(HttpContext context)
        {
            var request = context.Request;
            var host = request.Host.ToUriComponent();
            var pathBase = request.PathBase.ToUriComponent();
            return $"{request.Scheme}://{host}{pathBase}";
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeam()
        {
            if (_dbContext.Team == null)
            {
                return NotFound();
            }
            return await _dbContext.Team.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddTeam([FromForm] AddTeam team)
        {
            try
            {
                Team T = new Team();
                T.name = team.name;
                T.about = team.about;
                if (team.imageFile != null)
                {
                    T.image = await UploadImage(team.imageFile);
                }
                else
                {
                    T.image = null;
                }

                await _dbContext.Team.AddAsync(T);
                await _dbContext.SaveChangesAsync();
                return Ok(T);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTeam(int Id)
        {
            if (_dbContext.Team == null)
            {
                return NotFound();
            }

            var T = _dbContext.Team.SingleOrDefault(team => team.Id == Id);
            if (T == null)
            {
                return NotFound();
            }

            _dbContext.Team.Remove(T);
            await _dbContext.SaveChangesAsync();

            return Ok(T);
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> EditTeam([FromForm] EditTeam team)
        {
            try
            {
                var T = _dbContext.Team.SingleOrDefault(opt => opt.Id == team.Id);
                if (T != null)
                {
                    T.name = team.name;
                    T.about = team.about;
                    if (team.imageFile != null)
                    {
                        T.image = await UploadImage(team.imageFile);
                    }
                    _dbContext.SaveChanges();
                }
                return Ok(T);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

