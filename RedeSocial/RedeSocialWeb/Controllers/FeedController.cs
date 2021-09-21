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
using Infrastructure.BlobStorage;
using Microsoft.Extensions.Hosting;

namespace RedeSocialWeb.Controllers
{
    [Authorize]
    public class FeedController : BaseController
    {
        public FeedController(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
            _blobstorage = new ImagemRepositorio(configuration.GetSection("Logging").GetSection("ConnectionStrings")["KeyBlobStorage"], configuration.GetSection("Logging").GetSection("ConnectionStrings")["UrlBlobStorageImagem"]);
        }


        private ImagemRepositorio _blobstorage;

        public async Task<IActionResult> Index()
        {
            var ListaDePost = await ApiFind<ItemPost>(this.HttpContext.Session.GetString("token"), "Posts/getAllFeed");

            await CarregaDadosPessoa();

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
            MemoryStream ms = new MemoryStream();
            var fileName = $"Post_{id}_.png";

            foreach (var item in this.Request.Form.Files)
            {
                existeImagem = true;

                item.CopyTo(ms);

                ms.Position = 0;
            }

            CreatePost post = new();

            if (existeImagem)
            {
                post = new() { Message = collection["message"], ImagemUrl = $"{fileName}"};
            }
            else
                post = new() { Message = collection["message"], ImagemUrl = "Post_default.png" };

            if (post.Message != null)
            {
                var retorno = await ApiSaveAutorize<Post>(this.HttpContext.Session.GetString("token"), post, $"Posts/{id}");

                if (retorno == null)
                {
                    ViewData["MensagemRetorno"] = "Houve Um erro durante o post !";
                }
                else
                    await _blobstorage.SaveUpdate(fileName, ms);
            }



            return RedirectToAction("Index");
        }



        [HttpGet]
        [Route("Feed/GetTodosPosts/{Id:guid}")]
        public async Task<IActionResult> TodosAmigos(Guid id)
        {
            var ListaDePost = await ApiFindAllById<Post>(this.HttpContext.Session.GetString("token"), id, "Posts/getTodosPosts");

            await CarregaDadosPessoa();

            return View(ListaDePost);
        }

    }
}
