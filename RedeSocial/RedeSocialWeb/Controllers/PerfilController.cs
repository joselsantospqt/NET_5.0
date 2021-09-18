using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Entidade;
using Microsoft.AspNetCore.Http;
using Infrastructure.BlobStorage;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RedeSocialWeb.Controllers
{
    public class PerfilController : Controller
    {
        public PerfilController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IActionResult Editar()
        {
            return View();
        }



        [HttpGet]
        [Route("Perfil/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            HttpClient httpClient = new HttpClient();
            var resultado = await httpClient.GetAsync($"https://localhost:44383/api/Pessoas/{id}");
            var conteudo = await resultado.Content.ReadAsStringAsync();
            Pessoa Pessoa = JsonConvert.DeserializeObject<Pessoa>(conteudo);

            return View(Pessoa);
        }

        [HttpPost]
        [Route("Perfil/Editar/{Id:guid}")]
        public async Task<IActionResult> Editar(Guid id, IFormCollection collection)
        {
            var a = Configuration.GetConnectionString("KeyBlobStorage");
            var b = Configuration.GetConnectionString("UrlBlobStorageImagem");
            //CHAMAR AQUI O REPOSITORIO DE BLOBSTORAGE
            var blobstorage = new ImagemRepositorio("DefaultEndpointsProtocol=https;AccountName=paquetahomologstorage;AccountKey=lPmNaptgPdpehmmtA58M7mYcYOCwikpg06HZ9Cvw3VG/F8U9EqGWF6GQII4jpc9x6mjriXyzuVgkfnb5h6vcQg==;EndpointSuffix=core.windows.net", "https://paquetahomologstorage.blob.core.windows.net/imagens/");

            foreach (var item in this.Request.Form.Files)
            {

                MemoryStream ms = new MemoryStream();

                item.CopyTo(ms);

                ms.Position = 0;

                var fileName = "Imagem_Perfil" + id + ".png";

                 blobstorage.SaveUpdate(fileName, ms);

            }



            //string urlApi = $"http://localhost:7071/api/Post";
            //var putAsJson = JsonConvert.SerializeObject(Pessoa);
            //conteudo = new StringContent(putAsJson, System.Text.Encoding.UTF8, "application/json");
            //resultado = await httpClient.PutAsync(urlApi, conteudo);
            //var Json = await resultado.Content.ReadAsStringAsync();
            //Pessoa reponseJson = JsonConvert.DeserializeObject<Pessoa>(Json);

            return View();
        }

        public IActionResult Excluir()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }

        public IActionResult PerfilUsuario()
        {
            return View();
        }


    }
}
