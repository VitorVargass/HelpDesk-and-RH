using Microsoft.AspNetCore.Mvc;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using HD_SUPPORT.Models;
using Microsoft.EntityFrameworkCore;

namespace HD_SUPPORT.Controllers
{
    public class AutenticacaoFatores : Controller
    {
        
        private readonly Contexto _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IActionResult Index()
        {
            var nomeUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNome");
            if (!string.IsNullOrEmpty(nomeUsuario))
            {
                ViewBag.UsuarioNome = nomeUsuario;
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        
       

        public AutenticacaoFatores(Contexto context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            
        }

        public string GerarCodigoDeConfirmacao()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void EnviarCodigoPorEmail(string email, string codigo)
        {
            var mensagemDeEmail = new MimeMessage();
            mensagemDeEmail.From.Add(new MailboxAddress("HD_Support", "h8263157@gmail.com"));
            mensagemDeEmail.To.Add(MailboxAddress.Parse(email));
            mensagemDeEmail.Subject = "Código de Confirmação";
            mensagemDeEmail.Body = new TextPart("html") { Text = $"Seu código de confirmação é: {codigo}" };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("h8263157@gmail.com", "rzll hbxf kohn ppno");
                client.Send(mensagemDeEmail);
                client.Disconnect(true);
            }
        }
        [HttpPost]
        public IActionResult VerificarCodigo(string codigoInserido)
        {
            var codigoSalvo = _httpContextAccessor.HttpContext.Session.GetString("CodigoDeConfirmacao");
            if (codigoInserido == codigoSalvo)
            {
                var categoriaUsuario = _httpContextAccessor.HttpContext.Session.GetString("CategoriaUsuario");
                if (categoriaUsuario == "RH")
                {
                    return RedirectToAction("Index", "Rh");
                }
                    
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Erro = "Código incorreto. Tente novamente.";
                return RedirectToAction("Index", "AutenticacaoFatores");
            }
        }
    }
}
