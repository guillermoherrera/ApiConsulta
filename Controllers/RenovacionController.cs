using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiConsulta.Services;

namespace ApiConsulta.Controllers
{
    [Route(Constants.WS_GROUP_RENOVACION)]
    [ApiController]
    public class RenovacionController : ControllerBase
    {
        private IRenovacionService _renovacionService;
        private readonly ILogger<RenovacionController> _logger;

        public RenovacionController(IRenovacionService RenovacionService, ILogger<RenovacionController> logger)
        {
            _renovacionService = RenovacionService;
            _logger = logger;
        }

        [HttpGet(Constants.WS_RENOVACION)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartera([FromHeader] String userID, [FromHeader] String fechaInicio, [FromHeader] String fechaFin)
        {
            try
            {
                //[FromRoute] int productoId, 
                //var usuarioId = Request.Headers[Constants.FirebaseUserId];

                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _logger.LogInformation("Usuario: {0} IP: {1} Device: {2}", "", ip, "");//, ((uniqueDevice is null) ? "" : uniqueDevice));

                var respuesta = await _renovacionService.GetRenovaciones(userID, fechaInicio, fechaFin);

                if (respuesta == null)
                    throw new Exception("NO SE OBTUVIERON RESULTADOS.");

                object response = new { resultCode = 0, resultDesc = "OK.", contratos = respuesta.Count, data = respuesta };

                return Ok(response);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                return Helpers.ExceptionErrors.Exception(e.Class, string.Format("{0}.", e.Message), e);
            }
            catch (Exception e)
            {
                return Helpers.ExceptionErrors.Exception(Helpers.ExceptionErrors.RESPONSE_INTERNAL_SERVER_ERROR, string.Format("ERROR {0}.", e.Message), e);
            }
        }
    }
}