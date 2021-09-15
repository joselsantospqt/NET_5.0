using Domain.Entidade;
using Domain.Entidade.Request;
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
    public class AutenticacaoController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> efetuaLogin(IFormCollection collection)
        {
            var login = collection["Email"];
            var senha = collection["Senha"];

            //Subistituir aqui para autenticação
            var pessoaLogin = new CreatePessoa();

            //HttpClient httpClient = new HttpClient();

            //aqui get id pessoa com email e senha passados para o token ficar salvo com o ID

            //var jsonLoginPessoa = JsonConvert.SerializeObject(pessoaLogin);
            //var conteudo = new StringContent(jsonLoginPessoa, System.Text.Encoding.UTF8, "application/json");

            //await httpClient.PostAsync("https://localhost:44383/api/Pessoas/login", conteudo);

            return RedirectToAction("Index", "Feed");
        }

        public IActionResult RecuperarSenha()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(IFormCollection collection)
        {
            ViewData["validar"] = "Validado com sucesso";

            if (collection["Senha"].ToString().Length < 1)
            {
                ViewData["validar"] = "Senha não pode ser vazia";
                return View();

            }else if (collection["Nome"].ToString().Length < 1)
            {
                ViewData["validar"] = "Nome não pode ser vazio";
                return View();

            }else if (collection["Email"].ToString().Length < 1)
            {
                ViewData["validar"] = "Email não pode ser vazio";
                return View();

            }else if (collection["DataNascimento"].ToString() == "dd/mm/aaaa")
            {
                ViewData["validar"] = "Data de nascimento não pode ser vazio";
                return View();
            }

            var createPessoa = new CreatePessoa();
            createPessoa.Nome = collection["Nome"];
            createPessoa.Email = collection["Email"];
            createPessoa.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
            createPessoa.Senha = collection["Senha"];


            HttpClient httpClient = new HttpClient();

            var jsonCreatePessoa = JsonConvert.SerializeObject(createPessoa);
            var conteudo = new StringContent(jsonCreatePessoa, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("https://localhost:44383/api/Pessoas", conteudo);

            return RedirectToAction("Login");
        }
    }
}
