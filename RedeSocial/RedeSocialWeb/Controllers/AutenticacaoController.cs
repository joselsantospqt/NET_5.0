using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedeSocialWeb.Controllers
{
    public class AutenticacaoController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult RecuperarSenha()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }
    }
}
