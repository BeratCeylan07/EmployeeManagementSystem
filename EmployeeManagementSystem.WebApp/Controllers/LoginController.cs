using EmployeeManagementSystem.WebApp.Models;
using EmployeeManagementSystem.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.WebApp.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly TokenValidator _tokenValidator;

        public LoginController(IHttpClientFactory clientFactory, TokenValidator tokenValidator)
        {
            _clientFactory = clientFactory;
            _tokenValidator = tokenValidator;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var client = _clientFactory.CreateClient();
        
            var response = await client.PostAsJsonAsync("https://localhost:7161/api/auth/Login/login", model);
        
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        
                if (jsonResponse != null && jsonResponse.success)
                {
                    var token = jsonResponse.token;
                    var userid = jsonResponse.employeeId;
                    // Token'ı HttpContext.Items'da saklayalım
                    HttpContext.Items["AuthToken"] = token;
                    
                    // Alternatif olarak, session'da da saklayabilirsiniz
                    HttpContext.Session.SetString("AuthToken", token);
                    HttpContext.Session.SetString("USER_ID", userid.ToString());

                    return Redirect("~/App/Index");
                }
            }
        
            ModelState.AddModelError(string.Empty, "Login failed");

            return RedirectToAction("Index");
        }

    }
}
