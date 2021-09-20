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
using Microsoft.AspNetCore.Authorization;

namespace RedeSocialWeb.Controllers
{
    [Authorize]
    public class FeedController : ControllerBase
    {
        public FeedController(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<IActionResult> Index(Pessoa pessoa)
        {
            //COMANDO PARA SALVAR NA SESSION
            //List<PessoaFeed> obj = new();

            //HttpClient httpClient = new HttpClient();
            //var resultado = await httpClient.GetAsync($"{Configuration.GetSection("Logging").GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Posts/getAll");
            //var conteudo = await resultado.Content.ReadAsStringAsync();
            //var posts = JsonConvert.DeserializeObject<List<Post>>(conteudo);

            //resultado = await httpClient.GetAsync($"{Configuration.GetSection("Logging").GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Comments/getAll");
            //conteudo = await resultado.Content.ReadAsStringAsync();
            //var comments = JsonConvert.DeserializeObject<List<Comment>>(conteudo);

            //foreach (var item in posts) {

            //    string urlApi = $"{Configuration.GetSection("Logging").GetSection("ConnectionStrings")["ConnectionStringsApi"]}/api/Pessoas/{item.Id}";
            //    resultado = await httpClient.GetAsync(urlApi);
            //    conteudo = await resultado.Content.ReadAsStringAsync();

            //    PessoaFeed newObj = new();
            //    newObj.Posts = item;
            //    newObj.AutorPost = JsonConvert.DeserializeObject<Pessoa>(conteudo);
            //    newObj.Comments = new List<Comment>();
            //    newObj.Comments.AddRange(comments.FindAll(x => x.Post.PostId.Equals(item.Id)));
            //    obj.Add(newObj);
            //}

            var ListaPost = await ApiFind<Post>(this.HttpContext.Session.GetString("token"), "Posts/getAll");
            var ListaComments = await ApiFind<Comment>(this.HttpContext.Session.GetString("token"), "Comments/getAll");


            var query = from posts in ListaPost
                        join comments in ListaComments on posts.Id equals comments.Post.PostId
                        select new { idPost = posts.Id, postMensagem = posts.Message, IdComment = comments.Id, commentsMessage = comments.Text , IdPessoaComment = comments.Pessoa.Id};


            foreach (var item in query)
            {
                Console.WriteLine($"\"{item.idPost}\" is owned by {item.IdComment}");
            }


            ViewData["ID"] = pessoa.Id;
            ViewData["Email"] = pessoa.Email;
            //var blobstorage = new ImagemRepositorio(Configuration.GetConnectionString("KeyBlobStorage"), Configuration.GetConnectionString("UrlBlobStorageImagem"));
            ViewData["url"] = "https://img.ibxk.com.br/2017/06/22/22100428046161.jpg?w=1120&h=420&mode=crop&scale=both";

            return View(query);
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
            var urlApi = $"https://localhost:44383/api/post/{id}";
            await httpClient.PostAsync(urlApi, conteudo);

            return RedirectToAction("Index");
        }


    }
}
