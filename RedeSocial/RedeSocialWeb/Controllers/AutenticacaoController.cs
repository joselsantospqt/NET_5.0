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
            return Redirect("/Identity/Account/Login");
        }

        public IActionResult Delete()
        {
            return Redirect("/Identity/Account/Manage/DeletePersonalData");
        }

        public IActionResult PainelAdm()
        {
            return Redirect("/Identity/Account/Manage");
        }
    }
}
