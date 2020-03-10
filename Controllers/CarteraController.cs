using ApiConsulta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ApiConsulta.Controllers
{
    [ApiController]
    [Route(Constants.WS_GROUP_CARTERA)]
    public class CarteraController : ControllerBase
    {
        private ICarteraService _carteraService;
        private readonly ILogger<CarteraController> _logger;

        private bool headerUser = false;

        //[FromHeader(Name = "apiKey")]
        //[Required]
        //public string apiKey { get; set; }

        public CarteraController(ICarteraService carteraService, ILogger<CarteraController> logger)
        {
            _carteraService = carteraService;
            _logger = logger;
        }

        [HttpGet(Constants.WS_CARTERA)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartera([FromHeader] String userID)
        {
            try
            {
                //[FromRoute] int productoId, 
                //var usuarioId = Request.Headers[Constants.FirebaseUserId];
                
               var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _logger.LogInformation("Usuario: {0} IP: {1} Device: {2}", "", ip, "");//, ((uniqueDevice is null) ? "" : uniqueDevice));

                var respuesta = await _carteraService.GetCartera(userID);

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

        [HttpGet(Constants.WS_CARTERA_CONTRATO_DETALLE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCarteraContratoDetalle([FromHeader] string userID, [FromHeader] long contrato)
        {
            try
            {
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _logger.LogInformation("Usuario: {0} IP: {1} Device: {2}", "", ip, "");

                var respuesta = await _carteraService.GetCarteraContratoDetalle(userID, contrato);

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

        [HttpGet(Constants.WS_CARTERA_CREDITO_DETALLE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCarteraCreditoDetalle([FromHeader] int contrato, [FromHeader] string cveCliente, [FromHeader] string userID)
        {
            try
            {
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _logger.LogInformation("Usuario: {0} IP: {1} Device: {2}", "", ip, "");

                var respuesta = await _carteraService.GetCarteraCreditoDetalle(contrato, cveCliente, userID);

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


        /*
        [HttpGet(Constants.WS_CARTERA_GRUPOS)]
        public async Task<IActionResult> GetGrupos([FromRoute]string usuarioId)
        {
            try
            {
                var cartera = await _carteraService.GetGrupos(usuarioId);
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();

                _logger.LogInformation("Usuario: {0}  IP: {1} .", usuarioId, ip);

                if (cartera == null)
                    return BadRequest(new { message = "NO SE OBTUVIERON REGISTROS." });

                return Ok(cartera);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        */

        /*
        [HttpGet(Constants.WS_CARTERA_GRUPOS_DETALLE)]
        public async Task<IActionResult> GetGruposDetalle([FromRoute] string usuarioId, string grupoId)
        {

        }
        */

        //private void ContainUser()
        //{
        //    if (!Request.Headers.ContainsKey(Constants.FirebaseUserId))
        //        throw new Exception
            
        //}
    }
}