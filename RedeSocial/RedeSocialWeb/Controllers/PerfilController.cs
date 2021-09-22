using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entidade;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using RedeSocialWeb.Models;

namespace RedeSocialWeb.Controllers
{
    [Authorize]
    public class PerfilController : BaseController
    {
        private ImagemRepositorio _blobstorage;

        public PerfilController(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
            _blobstorage = new ImagemRepositorio(configuration.GetSection("Logging").GetSection("ConnectionStrings")["KeyBlobStorage"], configuration.GetSection("Logging").GetSection("ConnectionStrings")["UrlBlobStorageImagem"]);
        }

        public IConfiguration Configuration { get; }

        public async Task<IActionResult> Editar(Pessoa pessoa)
        {
            await CarregaDadosPessoa();

            return View(pessoa);
        }
        private async Task CarregaDadosPessoa()
        {
            var CargaPessoa = await ApiFindById<Pessoa>(this.HttpContext.Session.GetString("token"), this.HttpContext.Session.GetString("UserId"), "Pessoas");
            ViewData["ID"] = CargaPessoa.Id;
            ViewData["Email"] = CargaPessoa.Email;
            ViewData["url"] = CargaPessoa.ImagemUrlPessoa;
        }


        [HttpGet]
        [Route("Perfil/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var pessoa = await ApiFindById<Pessoa>(this.HttpContext.Session.GetString("token"), id, "Pessoas");
            await CarregaDadosPessoa();

            return View(pessoa);
        }

        [HttpPost]
        [Route("Perfil/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id, IFormCollection collection)
        {
            var existeImagem = false;

            MemoryStream ms = new MemoryStream();
            var fileName = $"Perfil_{id}{RandomNumber()}_.png";


            foreach (var item in this.Request.Form.Files)
            {
                existeImagem = true;

                item.CopyTo(ms);

                ms.Position = 0;
            }

            Pessoa pessoa = new();

            if (existeImagem)
            {
                pessoa = new() { Nome = collection["Nome"], Sobrenome = collection["Sobrenome"], ImagemUrlPessoa = $"{fileName}" };
            }
            else
                pessoa = new() { Nome = collection["Nome"], Sobrenome = collection["Sobrenome"], ImagemUrlPessoa = "Perfil_default.png" };


            var retorno = await ApiUpdate<Pessoa>(this.HttpContext.Session.GetString("token"), id, pessoa, "Pessoas");

            if (retorno == null)
            {
                ViewData["MensagemRetorno"] = "Houve Um erro durante o post !";
            }
            else
                await _blobstorage.SaveUpdate(fileName, ms);

            return Redirect(retorno.Id.ToString());

        }
        private string RandomNumber()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }

        [HttpGet]
        [Route("Perfil/Excluir/{Id:guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var retorno = await ApiRemove(this.HttpContext.Session.GetString("token"), id, "Pessoas");
            if (retorno.IsSuccessStatusCode)
                return RedirectToAction("Delete", "Autenticacao");

            return View();
        }

        [HttpGet]
        [Route("Perfil/PainelAdm")]
        public IActionResult PainelAdm ()
        {
                return RedirectToAction("PainelAdm", "Autenticacao");
        }


        [HttpGet]
        [Route("Perfil/ExcluirImagem/{Id:guid}")]
        public async Task<IActionResult> ExcluirImagem(Guid id)
        {
            Pessoa pessoa = await ApiFindById<Pessoa>(this.HttpContext.Session.GetString("token"), id, "Pessoas");
            await _blobstorage.Remove(pessoa.ImagemUrlPessoa);
            pessoa.ImagemUrlPessoa = "Perfil_default.png";
            var retorno = await ApiUpdate<Pessoa>(this.HttpContext.Session.GetString("token"), id, pessoa, "Pessoas");

            return RedirectToAction("Index", "Feed");
        }

        [HttpGet]
        [Route("Perfil/Detalhes/{Id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var pessoa = await ApiFindById<Pessoa>(this.HttpContext.Session.GetString("token"), id, "Pessoas");
            await CarregaDadosPessoa();

            return View(pessoa);
        }




    }
}
