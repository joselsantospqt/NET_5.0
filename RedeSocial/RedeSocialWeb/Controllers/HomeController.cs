using Domain.Entidade.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedeSocialWeb.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entidade;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace RedeSocialWeb.Controllers
{


    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        public IConfiguration Configuration { get; }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient _client = new HttpClient();
            var b = await _userManager.FindByNameAsync(User.Identity.Name);

            string urlApi = $"{Configuration.GetSection("Logging").GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Pessoas/{User.Identity.Name}";
            var resultado = await _client.GetAsync(urlApi);
            var Json = await resultado.Content.ReadAsStringAsync();
            Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(Json);
            if (pessoa == null)
            {
                var createPessoa = new CreatePessoa
                {
                    Email = User.Identity.Name
                };
                var jsonTodo = JsonConvert.SerializeObject(createPessoa);
                var conteudo = new StringContent(jsonTodo, System.Text.Encoding.UTF8, "application/json");
                urlApi = $"{Configuration.GetSection("Logging").GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Pessoas";
                resultado = await _client.PostAsync(urlApi, conteudo);
                Json = await resultado.Content.ReadAsStringAsync();
                Pessoa novaPessoa = JsonConvert.DeserializeObject<Pessoa>(Json);

                return RedirectToAction("Index", "Feed", novaPessoa);
            }

            return RedirectToAction("Index", "Feed", pessoa);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
