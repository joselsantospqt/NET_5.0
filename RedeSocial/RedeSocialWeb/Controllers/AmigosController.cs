using Domain.Entidade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedeSocialWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedeSocialWeb.Controllers
{
    public class AmigosController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        public AmigosController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<HomeController> logger) : base(httpClientFactory, configuration)
        {
            _logger = logger;
        }

        //public IActionResult List(List<Pessoa> retorno)
        //{

        //    return View(retorno);
        //}

        public async Task<IActionResult> List()
        {
            var retorno = await ApiFind<Pessoa>(this.HttpContext.Session.GetString("token"), "Pessoas/getAll");

            return View(retorno);
        }


        //buscar a pessoa por id
        public async Task<IActionResult> Details(Guid id)
        {
            var pessoa = await ApiFindById<Pessoa>(this.HttpContext.Session.GetString("token"), id ,"Pessoas");
            return View(pessoa);
        }

        public async Task<IActionResult> Adicionar(Guid id)
        {
            await ApiSaveAutorize(this.HttpContext.Session.GetString("token"), 
                new { idPessoa = this.HttpContext.Session.GetString("UserId"), idAmigo = id }, "Pessoas/cadastrarAmigo/");

            return RedirectToAction("List");
        }

        public async Task<IActionResult> ListaAmigos()
        {
            var retorno = await ApiFindById<Pessoa>(this.HttpContext.Session.GetString("token"), this.HttpContext.Session.GetString("UserId"), "Pessoas/getTodosAmigos/");

            //var listId = 

            return View(retorno);
        }

    }
}
