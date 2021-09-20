using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedeSocialWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocialWeb.Controllers
{
    public class AmigosController : Controller
    {
        //buscar todos as pessoas
        public IActionResult View()
        {
            return View();
        }


        //buscar todos as pessoa por id
        [HttpPost("id:Guid")]
        public IActionResult View(Guid id)
        {
            return View("Details");
        }

        [HttpPost]
        public IActionResult Adicionar()
        {
            return View();
        }

    }
}
