using RedeSocialWeb.Models;
using Domain.Entidade;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Domain.Entidade.View;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace RedeSocialWeb.Controllers
{
    [Authorize]
    public class FeedController : BaseController
    {
        public FeedController(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {

        }

        public async Task<IActionResult> Index()
        {
            var ListaDePost = await ApiFind<ItemPost>(this.HttpContext.Session.GetString("token"), "Posts/getAllFeed");

            await CarregaDadosPessoa();

            ////var blobstorage = new ImagemRepositorio(Configuration.GetConnectionString("KeyBlobStorage"), Configuration.GetConnectionString("UrlBlobStorageImagem"));

            return View(ListaDePost);
        }

        private async Task CarregaDadosPessoa()
        {
            var CargaPessoa = await ApiFindById<Pessoa>(this.HttpContext.Session.GetString("token"), this.HttpContext.Session.GetString("UserId"), "Pessoas");
            ViewData["ID"] = CargaPessoa.Id;
            ViewData["Email"] = CargaPessoa.Email;
            ViewData["url"] = CargaPessoa.ImagemUrlPessoa;
        }

        [HttpPost]
        [Route("Feed/Comentar/{Id:guid}")]
        public async Task<IActionResult> Comentar(Guid id, IFormCollection collection)
        {
            CreateComment comentario = new() { Text = collection["comentario"] };
            var retorno = await ApiSaveAutorize<Comment>(this.HttpContext.Session.GetString("token"), comentario, $"Comments/{id}/{collection["idPessoa"]}");
            if (retorno != null)
                ViewData["MensagemRetorno"] = "Comentado com Sucesso !";
            else
                ViewData["MensagemRetorno"] = "Houve Um erro durante o comentário !";

            return View("Index");
        }

        [HttpPost]
        [Route("Feed/Postar/{Id:guid}")]
        public async Task<IActionResult> Postar(Guid id, IFormCollection collection)
        {

            var existeImagem = false;

            foreach (var item in this.Request.Form.Files)
            {
                existeImagem = true;
                MemoryStream ms = new MemoryStream();

                item.CopyTo(ms);

                ms.Position = 0;

                var fileName = $"Imagem_Post_{id}_.png";

                //blobstorage.SaveUpdate(fileName, ms);
            }

            CreatePost post = new();

            if (existeImagem)
            {
                post = new() { Message = collection["message"], ImagemUrl = $"Imagem_Post_{id}_.png" };
            }
            else
                post = new() { Message = collection["message"], ImagemUrl = "" };

            if (post.Message != null)
            {
                var retorno = await ApiSaveAutorize<Post>(this.HttpContext.Session.GetString("token"), post, $"Posts/{id}");

                if (retorno == null)
                {
                    ViewData["MensagemRetorno"] = "Houve Um erro durante o post !";
                }
            }



            return RedirectToAction("Index");
        }

    }
}
