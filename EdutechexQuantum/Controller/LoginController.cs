using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdutechexQuantum.DTO;
using EdutechexQuantum.Model;

namespace EdutechexQuantum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private AppDbContext context;
        public LoginController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpPost]

        public IActionResult RegisterAdmin(addAdmin admin)
        {
            var objCheck = context.Admin.SingleOrDefault(opt => opt.UserName == admin.UserName);
            try
            {
                if (objCheck == null)
                {
                    Admin a = new Admin();
                    a.FullName = admin.FullName;
                    a.UserName = admin.UserName;
                    a.Password = admin.Password;
                    a.Admintype = "Admin";
                    context.Admin.Add(a);
                    context.SaveChanges();
                    return Ok(admin);
                }
                else if (objCheck != null)
                {
                    return BadRequest("This UserName is Exist!");
                }
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]

        public ActionResult GetAdminList()
        {
            try
            {
                var list = (from u in context.Admin
                            select new
                            {
                                u.Id,
                                u.FullName,
                                u.UserName,
                                u.Password,
                                u.Admintype
                            });

                int totalRecords = list.Count();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Admin Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login AA)
        {
            if (AA == null)
                return BadRequest();

            var sa = await context.Admin
            .FirstOrDefaultAsync(opt => opt.UserName == AA.UserName && opt.Password == AA.Password );
            if (sa == null)
                return NotFound(new { Message = "User Not Found" });

            return Ok(/*new { Message = "Login Success!" }*/sa);

        }
        [HttpPut]
        [AllowAnonymous]
        public ActionResult EditAdmin(Admin admin)
        {
            try
            {
                var aa = context.Admin.SingleOrDefault(opt => opt.Id == admin.Id);
                if (aa != null)
                {
                    aa.FullName = admin.FullName;
                    aa.UserName = admin.UserName;
                    aa.Password = admin.Password;
                    context.SaveChanges();
                }
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                var admin = await context.Admin.FindAsync(id);
                if (admin != null)
                {
                    context.Admin.Remove(admin);
                    context.SaveChanges();
                    return Ok(admin);
                }
                return NotFound(new { Message = "User Not Found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}