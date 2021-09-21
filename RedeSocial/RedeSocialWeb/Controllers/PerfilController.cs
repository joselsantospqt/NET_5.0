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

namespace RedeSocialWeb.Controllers
{
    [Authorize]
    public class PerfilController : BaseController
    {
        public PerfilController(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
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

            foreach (var item in this.Request.Form.Files)
            {
                existeImagem = true;
                MemoryStream ms = new MemoryStream();

                item.CopyTo(ms);

                ms.Position = 0;

                var fileName = $"Perfil_{id}_.png";

                //blobstorage.SaveUpdate(fileName, ms);
            }

            Pessoa pessoa = new();

            if (existeImagem)
            {
                pessoa = new() { Nome = collection["Nome"], Sobrenome = collection["Sobrenome"], ImagemUrlPessoa = $"Perfil_{id}_.png" };
            }
            else
                pessoa = new() { Nome = collection["Nome"], Sobrenome = collection["Sobrenome"], ImagemUrlPessoa = "Perfil_default.png" };


            var retorno = await ApiUpdate<Pessoa>(this.HttpContext.Session.GetString("token"), id, pessoa, "Pessoas");

            if (retorno == null)
            {
                ViewData["MensagemRetorno"] = "Houve Um erro durante o post !";
            }

            return View(retorno);
        }

        [HttpPost]
        [Route("Perfil/Excluir/{Id:guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var retorno = await ApiRemove(this.HttpContext.Session.GetString("token"), id, "Pessoas");
            if (retorno.IsSuccessStatusCode)
                Redirect("/Identity/Account/Login");

            return View();
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
