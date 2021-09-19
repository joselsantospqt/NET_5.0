using RedeSocialWeb.Models;
using Domain.Entidade;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RedeSocialWeb.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        public ActionResult feed()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();

            var resultado = await httpClient.GetAsync("https://localhost:44383/api/Posts/getAll");
            var conteudo = await resultado.Content.ReadAsStringAsync();

            List<Post> listaPosts = JsonConvert.DeserializeObject<List<Post>>(conteudo);

            return View(listaPosts);
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
