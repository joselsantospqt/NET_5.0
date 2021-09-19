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
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager) : base(httpClientFactory, configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var userIdentity = await _userManager.FindByNameAsync(User.Identity.Name);
            var pessoa = await ApiFindById<Pessoa>(userIdentity.Id, "Pessoas");
            if (pessoa == null)
            {
                var createPessoa = new CreatePessoa
                {
                    Id = Guid.Parse(userIdentity.Id),
                    Email = User.Identity.Name
                };

                var retorno = await ApiSave(createPessoa, "Pessoas");

                return RedirectToAction("Index", "Feed", createPessoa);
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
