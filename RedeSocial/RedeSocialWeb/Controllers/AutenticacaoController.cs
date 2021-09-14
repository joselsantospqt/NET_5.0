using Domain.Entidade;
using Domain.Entidade.Request;
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

        public async Task<ActionResult> efetuaLogin(string email, string senha)
        {
            //Subistituir aqui para autenticação
            var pessoaLogin = new CreatePessoa();

            HttpClient httpClient = new HttpClient();

            //aqui get id pessoa com email e senha passados para o token ficar salvo com o ID

            var jsonLoginPessoa = JsonConvert.SerializeObject(pessoaLogin);
            var conteudo = new StringContent(jsonLoginPessoa, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("https://localhost:44383/api/Pessoas/login", conteudo);

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

        public async Task<ActionResult> RegistroPessoa(string nome, string email, DateTime dataNascimento,string senha, string senhaRepetida)
        {
            if (senha != senhaRepetida)
            {
                return RedirectToAction("Registro");
            }else if (senha == null)
            {
                return RedirectToAction("Registro");
            }

            if (nome == null)
            {
                return RedirectToAction("Registro");
            }

            if (email == null)
            {
                return RedirectToAction("Registro");
            }

            var createPessoa = new CreatePessoa();
            createPessoa.Nome = nome;
            createPessoa.Email = email;
            createPessoa.DataNascimento = dataNascimento;
            createPessoa.Senha = senha;


            HttpClient httpClient = new HttpClient();

            var jsonCreatePessoa = JsonConvert.SerializeObject(createPessoa);
            var conteudo = new StringContent(jsonCreatePessoa, System.Text.Encoding.UTF8, "application/json");

            await httpClient.PostAsync("https://localhost:44383/api/Pessoas", conteudo);

            return RedirectToAction("Login");
        }
    }
}
