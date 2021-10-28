using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_login.Models;

namespace Mvc_login.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario u)
        {
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                using (CadastroEntities dc = new CadastroEntities())
                {
                    var v = dc.Usuarios.Where(a => a.NomeUsuario.Equals(u.NomeUsuario) && a.Senha.Equals(u.Senha)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["usuarioLogadoID"] = v.Id.ToString();
                        Session["nomeUsuarioLogado"] = v.NomeUsuario.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(u);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Macoratti .net";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "http://www.macoratti.net";

            return View();
        }
    }
}