using AspNetCoreMVCEgitim.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace AspNetCoreMVCEgitim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("deger", "deneme"); // AspNetCore.Http; usingi ekle
            return View();
        }

        public IActionResult Privacy()
        {
            TempData["data"] = HttpContext.Session.GetString("deger");
            return View();
        }

        public IActionResult SessionSil()
        {
            HttpContext.Session.Remove("deger");
            // kullanıcıya ait, tüm session’ları siler.
            //HttpContext.Session.Clear();
            return RedirectToAction(nameof(Privacy));
        }

        public IActionResult CookieOlustur()
        {
            CookieOptions cookie = new()
            {
                Expires = DateTime.Now.AddMinutes(1)
            };
            Response.Cookies.Append("username", "admin", cookie);
            Response.Cookies.Append("password", "123456", cookie);
            return RedirectToAction(nameof(CookieOku));
        }

        public IActionResult CookieOku()
        {
            TempData["kullanici"] = Request.Cookies["username"];
            TempData["parola"] = Request.Cookies["password"];
            return View();
        }

        public IActionResult CookieSil()
        {
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("password");
            return RedirectToAction(nameof(CookieOku));
        }

        public IActionResult AppSetting()
        {
            TempData["Email"] = _configuration["Email"];
            TempData["MailSunucu"] = _configuration["MailSunucu"];
            TempData["UserName"] = _configuration["MailKullanici:UserName"];
            TempData["Password"] = _configuration.GetSection("MailKullanici:UserName").Value;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
