using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    //public class Usuario
    //{
    //    public int usuarioId { get; set; }
    //    public string nombre { get; set; }
    //    public string correo { get; set; }
    //    public List<string> roles { get; set; }
    //    public string usuario { get; set; }
    //    public string password { get; set; }
    //    public string token { get; set; }
    //}

    public class Usuario
    {
        public Int64 usuarioId { get; set; }
        public int perfilId { get; set; }
        public string perfilDesc { get; set; }
        public string token { get; set; }
    }
}