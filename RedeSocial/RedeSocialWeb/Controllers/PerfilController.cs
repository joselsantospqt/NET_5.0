using Domain.Entidade.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedeSocialWeb.Controllers
{
    public class PerfilController : Controller
    {
        public IActionResult EditarPerfil()
        {
            return View();
        } 

        public IActionResult ExcluirConta()
        {
            return View();
        } 
        
        public IActionResult PerfilExterno()
        {
            return View();
        } 

        public IActionResult PerfilUsuario()
        {
            return View();
        }
        
        public async Task<IActionResult> CadastrarUsuario()
        {
            var pessoaEmail = new CreatePessoa();
            pessoaEmail.Email = User.Identity.Name;

            HttpClient httpClient = new HttpClient();
            var retorno = await httpClient.GetAsync($"https://localhost:44383/api/Pessoas/Email{pessoaEmail}");

            if (retorno.Content != null)
            {
                return RedirectToAction("Index", "Feed");
            }

            return View(pessoaEmail);
        }
        
        [HttpPost]
        public async Task<IActionResult> Cadastrar(IFormCollection collection)
        {
            var createPessoa = new CreatePessoa();
            createPessoa.Email = User.Identity.Name;
            createPessoa.Nome = collection["Nome"];
            createPessoa.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);

            HttpClient httpClient = new HttpClient();

            var jsonCreatePessoa = JsonConvert.SerializeObject(createPessoa);
            var conteudo = new StringContent(jsonCreatePessoa, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("https://localhost:44383/api/Pessoas", conteudo);

            return RedirectToAction("Index", "Feed");
        }
    }
}
