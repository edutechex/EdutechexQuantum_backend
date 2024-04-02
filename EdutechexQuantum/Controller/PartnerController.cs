using EdutechexQuantum.DTO;
using EdutechexQuantum.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EdutechexQuantum.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PartnerController(AppDbContext dbContext,
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
        public async Task<ActionResult<IEnumerable<Partner>>> GetPartner()
        {
            if (_dbContext.Partner == null)
            {
                return NotFound();
            }
            return await _dbContext.Partner.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddPartner([FromForm] AddPartner p)
        {
            try
            {
                Partner T = new Partner();
                T.name = p.name;
                T.type = p.type;
                
                if (p.imageFile != null)
                {
                    T.image = await UploadImage(p.imageFile);
                }
                else
                {
                    T.image = null;
                }

                await _dbContext.Partner.AddAsync(T);
                await _dbContext.SaveChangesAsync();
                return Ok(T);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePartner(int Id)
        {
            if (_dbContext.Partner == null)
            {
                return NotFound();
            }

            var T = _dbContext.Partner.SingleOrDefault(p => p.Id == Id);
            if (T == null)
            {
                return NotFound();
            }

            _dbContext.Partner.Remove(T);
            await _dbContext.SaveChangesAsync();

            return Ok(T);
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> EditPartner([FromForm] EditPartner p)
        {
            try
            {
                var T = _dbContext.Partner.SingleOrDefault(opt => opt.Id == p.Id);
                if (T != null)
                {
                    T.name = p.name;
                    T.type = p.type;
                    if (p.imageFile != null)
                    {
                        T.image = await UploadImage(p.imageFile);
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

