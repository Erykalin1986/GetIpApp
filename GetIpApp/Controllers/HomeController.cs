using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetIpApp.Controllers
{

    [AllowAnonymous]
    [Route("home")]
    public class HomeController : Controller
    {
        [HttpGet]
        [HttpGet("get-ip")]
        public IActionResult Index()
        {
            string clientIp = "Не удалось определить";

            // 1. Проверяем заголовок X-Forwarded-For (для Nginx/Cloudflare)
            if (HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                clientIp = forwardedFor.ToString().Split(',')[0].Trim();
            }
            // 2. Если заголовка нет, берем прямое подключение
            else if (HttpContext.Connection.RemoteIpAddress != null)
            {
                clientIp = HttpContext.Connection.RemoteIpAddress.ToString();
            }

            // Передаем IP-адрес в представление (View)
            ViewBag.ClientIp = clientIp;

            return View();
        }
    }
}
