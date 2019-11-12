using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai_2s2019_CodeXP_Gufos.Domains;
using senai_2s2019_CodeXP_Gufos.Repositories;
using senai_2s2019_CodeXP_Gufos.ViewModels;

namespace senai_2s2019_CodeXP_Gufos.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UsuarioRepository _repositorio = new UsuarioRepository();

        // Faz a chamada do método para validar o usuário na aplicação
        private Usuario ValidaUsuario(LoginViewModel login)
        {

            Usuario usuarioLogado = _repositorio.RealizarLogin(login.Email, login.Senha);

            return usuarioLogado;
        }

        // Gera o token
        private string GerarToken(Usuario userInfo)
        {

            // Define a criptografia do token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyGufosSecretKey"));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                       
            // Define as Claims (dados da sessão)
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Nome),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userInfo.TipoUsuario.Titulo.ToString()),
                new Claim("Role", userInfo.TipoUsuario.Titulo.ToString())
            };

            // Configura o token e seu tempo de vida
            var token = new JwtSecurityToken(
                issuer: "gufos.com",
                audience: "gufos.com",
                claims : claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Usa a anotação abaixo para ignorar a autenticação neste método
        /// <summary>
        /// Realiza login no sistema
        /// </summary>
        /// <param name="login">Email e Senha para realizar login</param>
        /// <returns>Retorna um token com os dados do usuário logado</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]LoginViewModel login)
        {
            //IActionResult response = Unauthorized();
            var user = ValidaUsuario(login);

            if (user == null)
            {
                return NotFound(new { mensagem = "Usuário ou senha inválidos!" });
            }

            var tokenString = GerarToken(user);

            return Ok(new { token = tokenString });
        }
    }
}