﻿using ApiConsulta.Entities;
using ApiConsulta.SQL;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace ApiConsulta.Services
{
    public interface IConsultaService
    {
        Task<CreditoDetalle> getCurp(string curp);
    }
    public class ConsultaService : IConsultaService
    {
        private readonly AppSettings _appSettings;

        public ConsultaService(IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
        }

        public async Task<CreditoDetalle> getCurp(string curp)
        {
            ConsultaSQL sql = new ConsultaSQL(_appSettings);
            CreditoDetalle datos = await sql.getCurp(curp);
            return datos;
        }
    }
}
