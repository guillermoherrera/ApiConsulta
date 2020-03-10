using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConsulta.Helpers
{
    public class ExceptionErrors
    {
        public const int RESPONSE_BAD_REQUEST = 20;
        public const int RESPONSE_UNAUTHORIZED = 15;
        public const int RESPONSE_INTERNAL_SERVER_ERROR = 0;

        public static IActionResult Exception(int errorCode, string msj, object e)
        {
            Serilog.Log.Error((Exception)e, "Error ");
            switch (errorCode)
            {
                case 15:
                    return new ObjectResult(new { resultId = 1, resultDesc = msj }) { StatusCode = 401 };
                case 20:
                    return new ObjectResult(new { resultId = 1, resultDesc = msj }) { StatusCode = 400 };
                default:
                    return new ObjectResult(new { resultId = 1, resultDesc = msj }) { StatusCode = 500 };
            }
        }
    }
}