using ApiConsulta;
using ApiConsulta.Helpers;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> Autentificar(string username, string password);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly AppSettings _appSettings;

        public UsuarioService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<Usuario> Autentificar(string username, string password)
        {
            UsuarioSQL sql = new UsuarioSQL(_appSettings);
            var usuario = await sql.Login(username, password);

            if (usuario == null)
                return null;

            usuario.token = Utilerias.GetToken(username, _appSettings.secret, _appSettings.segundosVidaToken);
            return usuario;
        }
    }
}