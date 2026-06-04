using System;

namespace PIM_3SEMESTRE.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }

        public string NomeUsuario { get; set; }

        public string EmailUsuario { get; set; }

        public string SenhaUsuario { get; set; }

        public int IdTipoUsuario { get; set; }
    }
}
