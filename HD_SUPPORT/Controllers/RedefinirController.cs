using HD_SUPPORT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Security.Cryptography;
using MailKit.Net.Smtp;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NuGet.Common;

namespace HD_SUPPORT.Controllers
{
    public class RedefinirController : Controller
    {
        private readonly Contexto _context;
     

        public RedefinirController(Contexto context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public string GerarTokenRedefinicaoSenha(string email)
        {
            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1yAz1msGEWM4fz13kgoyHuuhNd1SsblVPSlELuWu8tiIu9uZ8E")); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "HD_Support", 
                audience: "Usuario", 
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), 
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost]
        public IActionResult SolicitacaoRedefinicaoSenha(string email)
        {
            var Email = _context.Administradores.FirstOrDefault(u => u.Email == email);
            if(Email != null)
            {
                string token = GerarTokenRedefinicaoSenha(email);

                
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("HD_Support", "seu-email@dominio.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Redefinição de Senha";

                string urlRedefinicao = Url.Action("ValidacaoToken", "Redefinir", new { token = token }, Request.Scheme);
                message.Body = new TextPart("html")
                {
                    Text = $"<p>Para redefinir sua senha, clique no link abaixo:</p><a href='{urlRedefinicao}'>Redefinir Senha</a>"
                };

                
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("h8263157@gmail.com", "rzll hbxf kohn ppno");
                    client.Send(message);
                    client.Disconnect(true);
                }


                TempData["MensagemSucesso"] = $"Email Enviado com sucesso, caso não tenha recebido tente novamente!!";
                return RedirectToAction("Index", "Redefinir");
            } else
            {
                TempData["MensagemErro"] = $"Email não encontrado no banco de dados! Insira um válido por favor!";
                return RedirectToAction("Index", "Redefinir");
            }
        }
        public IActionResult ValidacaoToken(string token)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1yAz1msGEWM4fz13kgoyHuuhNd1SsblVPSlELuWu8tiIu9uZ8E"));
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "HD_Support",
                    ValidAudience = "Usuario",
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero 
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var emailClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

                return View();
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Token inválido!";
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AtualizarSenha(AtualizarSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1yAz1msGEWM4fz13kgoyHuuhNd1SsblVPSlELuWu8tiIu9uZ8E"));
                    tokenHandler.ValidateToken(model.Token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "HD_Support",
                        ValidAudience = "Usuario",
                        IssuerSigningKey = key,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var emailClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;


                    var usuario = _context.Administradores.FirstOrDefault(u => u.Email == emailClaim);
                    if (usuario != null)
                    {
                        
                        usuario.Senha = model.NovaSenha; 
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        
                        TempData["MensagemErro"] = "Usuário não encontrado.";
                        return RedirectToAction("ValidacaoToken", "Redefinir");
                    }
                }
                catch (SecurityTokenException ex)
                {
                    
                    TempData["MensagemErro"] = "Token inválido ou expirado.";
                    return RedirectToAction("ValidacaoToken", "Redefinir");
                }
                catch (Exception ex)
                {
                    
                    TempData["MensagemErro"] = $"Erro ao redefinir a senha: {ex.Message}";
                    return RedirectToAction("ValidacaoToken", "Redefinir");
                }
            }
            TempData["MensagemErro"] = "Model inválida.";
            return RedirectToAction("ValidacaoToken", "Redefinir");
        }
    }
}
