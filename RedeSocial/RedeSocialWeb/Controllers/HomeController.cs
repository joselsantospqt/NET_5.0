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
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<HomeController> logger) : base(httpClientFactory, configuration)
        {
            _logger = logger;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            CredenciaisUsuario Autorizicao = new() { idUsuario = Guid.Parse(this.HttpContext.Session.GetString("UserId")) };
            var value = await ApiToken(Autorizicao);
            if (value.Token == null)
            {
                var createPessoa = new CreatePessoa
                {
                    Id = Guid.Parse(this.HttpContext.Session.GetString("UserId")),
                    Email = this.HttpContext.Session.GetString("UserName")
                };

                var retorno = await ApiSave(createPessoa, "Pessoas");
                CredenciaisUsuario credenciais = new(){ idUsuario = createPessoa.Id};
                value = await ApiToken(credenciais);
                if (value.Token != null)
                {
                    this.HttpContext.Session.SetString("token", value.Token.ToString());
                    return RedirectToAction("Index", "Feed");
                }

                return RedirectToAction("Autenticacao", "Login");
            }

            this.HttpContext.Session.SetString("token", value.Token.ToString());
            var pessoa = await ApiFindById<Pessoa>(value.Token.ToString(), this.HttpContext.Session.GetString("UserId"), "Pessoas");
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
