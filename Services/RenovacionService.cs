using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiConsulta.Entities;
using ApiConsulta.SQL;
using Microsoft.Extensions.Options;
using WebApi.Helpers;

namespace ApiConsulta.Services
{
    public interface IRenovacionService
    {
        Task<List<Contrato>> GetRenovaciones(String userID, String fechaInicio, String fechaFin);
    }

    public class RenovacionService : IRenovacionService
    {
        private readonly AppSettings _appSettings;

        public RenovacionService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<List<Contrato>> GetRenovaciones(String userID, String fechaInicio, String fechaFin)
        {
            RenovacionSQL sql = new RenovacionSQL(_appSettings);
            List<Contrato> datos = await sql.RenovacionGrupos(userID, fechaInicio, fechaFin);
            return datos;
        }


    }
}
