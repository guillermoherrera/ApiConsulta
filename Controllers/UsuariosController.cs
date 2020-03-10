using ApiConsulta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route(Constants.WS_GROUP_USUARIOS)]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioService _userService;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuarioService userService, ILogger<UsuariosController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        //[HttpPost(Constants.WS_USUARIOS_AUTENTIFICAR)]
        //public async Task<IActionResult> Autentificar([FromBody]AutentificacionModelo model)
        //{
        //    try
        //    {
        //        var user = await _userService.Autentificar(model.usuario, model.password);
        //        var ip = HttpContext.Connection.RemoteIpAddress.ToString();

        //        _logger.LogInformation("Usuario: {0} intentando loguearse desde : {1} .", model.usuario, ip);

        //        if (user == null)
        //            return BadRequest(new { message = "El usuario o la contraseña son incorrectos..." });

        //        return Ok(user);
        //    }
        //    catch (System.Data.SqlClient.SqlException e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}


        //[Authorize]
        //[HttpGet(Constants.WS_USUARIOS_USUARIOS)]
        //public async Task<IActionResult> GetAll()
        //{
        //    var users = await _userService.GetAll();
        //    return Ok(users);
        //}



    }
}