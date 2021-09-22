using Domain.Entidade;
using Domain.Entidade.Request;
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
        public AmigosController(IHttpClientFactory httpClientFactory,
            IConfiguration configuration, ILogger<HomeController> logger) : base(httpClientFactory, configuration)
        {
            _logger = logger;
        }

        public async Task<IActionResult> View()
        {
            var retorno = await ApiFind<Pessoa>(this.HttpContext.Session.GetString("token"), "Pessoas/getAll");
            Pessoa usuario = new();

            foreach (var pessoa in retorno)
            {
                if (pessoa.Id.ToString() == this.HttpContext.Session.GetString("UserId"))
                {
                    usuario = pessoa;
                }
            }
            retorno.Remove(usuario);

            await CarregaDadosPessoa();
            return View(retorno);
        }
        public async Task<IActionResult> List()
        {
            var retorno = await ApiFindAllById<Pessoa>(this.HttpContext.Session.GetString("token"),
                this.HttpContext.Session.GetString("UserId"), "Pessoas/getTodosAmigos");
            await CarregaDadosPessoa();
            return View(retorno);
        }

        //buscar a pessoa por id
        [HttpGet]
        [Route("Amigos/Details/{Id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var pessoa = await ApiFindById<Pessoa>(this.HttpContext.Session.GetString("token"), id, "Pessoas");
            await CarregaDadosPessoa();
            return View(pessoa);
        }

        [HttpGet]
        public async Task<IActionResult> Adicionar(Guid id)
        {
            CreateAmigo novoAmigo = new() {Id = id };
            var retorno = await ApiSaveAutorize<Pessoa>(this.HttpContext.Session.GetString("token"), novoAmigo, $"Pessoas/cadastrarAmigo/{this.HttpContext.Session.GetString("UserId")}");

            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("Amigos/Deletar/{Id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            CreateAmigo novoAmigo = new() { Id = id };
            var retorno = await ApiUpdate<Pessoa>(this.HttpContext.Session.GetString("token"),
                this.HttpContext.Session.GetString("UserId"), novoAmigo, "Pessoas/removerAmigo");

            return RedirectToAction("List");
        }

        private async Task CarregaDadosPessoa()
        {
            var CargaPessoa = await ApiFindById<Pessoa>(this.HttpContext.Session.GetString("token"), this.HttpContext.Session.GetString("UserId"), "Pessoas");
            ViewData["ID"] = CargaPessoa.Id;
            ViewData["Email"] = CargaPessoa.Email;
            ViewData["url"] = CargaPessoa.ImagemUrlPessoa;
        }

    }
}
