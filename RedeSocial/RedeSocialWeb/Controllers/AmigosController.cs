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
        public IActionResult TodosAmigos()
        {
            return View();
        }
    }
}
