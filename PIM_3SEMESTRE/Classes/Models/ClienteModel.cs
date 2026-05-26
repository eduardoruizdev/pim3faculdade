using System;

namespace PIM_3SEMESTRE.Models
{
    public class ClienteModel
    {

        public int IdUsuario { get; set; }

        public string NomeUsuario { get; set; }

        public string EmailUsuario { get; set; }

        public string SenhaUsuario { get; set; }

        public int IdTipoUsuario { get; set; }

        public int IdCliente { get; set; }

        public string CpfCliente { get; set; }

        public DateTime DataNascimentoCliente { get; set; }

        public string TelefoneCliente { get; set; }

        public string CepCliente { get; set; }

        public string RuaCliente { get; set; }

        public int NumeroResidenciaCliente { get; set; }

        public string ComplementoResidenciaCliente { get; set; }

        public string BairroCliente { get; set; }

        public string CidadeCliente { get; set; }

        public string EstadoResidenciaCliente { get; set; }

        public string ObservacaoCliente { get; set; }
    }
}