using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.WebApp.Controllers
{
    public class SessionController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpPost]
        public IActionResult PullSession()
        {
            var userId = _httpContextAccessor.HttpContext.Session.GetString("USER_ID");
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");
            if (userId is not null && token is not null)
            {
                return Ok(new { userID = userId, token = token });
            }
            else
            {
                return NotFound(); // Veya uygun bir hata durumu döndürebilirsiniz
            }
        }

    }
}
