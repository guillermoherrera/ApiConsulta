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
    [Route(Constants.WS_CONSULTA_CONSULTA)]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private IConsultaService _consultaService;
        private readonly ILogger<ConsultaController> _logger;

        public ConsultaController(IConsultaService ConsultaService, ILogger<ConsultaController> logger)
        {
            _consultaService = ConsultaService;
            _logger = logger;
        }

        [HttpGet(Constants.WS_CURP)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getCurp([FromHeader] string curp)
        {
            try
            {
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _logger.LogInformation("Usuario: {0} IP: {1} Device: {2}", "", ip, "");

                var respuesta = await _consultaService.getCurp(curp);

                if (respuesta == null)
                    throw new Exception("NO SE OBTUVIERON RESULTADOS.");

                object response = new { resultCode = 0, resultDesc = "OK.", data = respuesta };

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

        [HttpGet(Constants.WS_DISP_NOMBRE_GRUPO)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getDispNomGrupo([FromHeader] string nombreGrupo, [FromHeader] int sistema)
        {
            try
            {
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _logger.LogInformation("Usuario: {0} IP: {1} Device: {2}", "", ip, "");

                var respuesta = await _consultaService.getDispNomGrupo(nombreGrupo, sistema);

                if (respuesta == null)
                    throw new Exception("NO SE OBTUVIERON RESULTADOS.");

                object response = new { resultCode = 0, resultDesc = "OK.", data = respuesta };

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