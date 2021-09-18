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
using Infrastructure.BlobStorage;

namespace RedeSocialWeb.Controllers
{
    public class FeedController : Controller
    {
        public FeedController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public ActionResult View()
        {
            return View();
        }
        public async Task<IActionResult> Index(Pessoa pessoa)
        {
            //COMANDO PARA SALVAR NA SESSION
            //this.HttpContext.Session.SetString("IdPessoa", result.Token);
            PessoaPosts obj = new();
            obj.pessoa = pessoa;
            HttpClient httpClient = new HttpClient();
            var resultado = await httpClient.GetAsync("https://localhost:44383/api/Posts/getAll");
            var conteudo = await resultado.Content.ReadAsStringAsync();

            obj.Posts = JsonConvert.DeserializeObject<List<Post>>(conteudo);
            ViewData["ID"] = pessoa.Id;
            ViewData["Nome"] = pessoa.Nome;
            //var blobstorage = new ImagemRepositorio(Configuration.GetConnectionString("KeyBlobStorage"), Configuration.GetConnectionString("UrlBlobStorageImagem"));
            ViewData["url"] = "blobstorage.GetById(pessoa.ImagemUrlPessoa)";

            return View(obj.Posts);
        }

        public async Task<IActionResult> trazerDadosComent(String urlApi)
        {
            HttpClient httpClient = new HttpClient();

            var resultado = await httpClient.GetAsync($"https://localhost:44383/api/{urlApi}");
            var conteudo = await resultado.Content.ReadAsStringAsync();

            List<Post> listaPosts = JsonConvert.DeserializeObject<List<Post>>(conteudo);
            return View();
        }


        public async Task<ActionResult> EnviaDadosPost(string textoPost, string filePost)
        {
             var createPost = new CreatePost();
            createPost.Message = textoPost;
            createPost.ImagemUrl = filePost;
            //pegar o id do usuario para passar na url
            int id = 1; // por ser GUID assim da ruim, não sei como simular
            

            HttpClient httpClient = new HttpClient();

            var jsonTodo = JsonConvert.SerializeObject(createPost);
            var conteudo = new StringContent(jsonTodo, System.Text.Encoding.UTF8, "application/json");
            var urlApi = $"https://localhost:44383/api/ost/{id}";

            await httpClient.PostAsync(urlApi, conteudo);

            return RedirectToAction("Index");
        }


    }
}
