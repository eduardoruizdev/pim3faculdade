using System;

namespace PIM_3SEMESTRE.Models
{
    public class ModelServico
    {
        // ID DO SERVIÇO
        public int IdServico { get; set; }

        // TIPO SERVIÇO
        public int IdTipoServico { get; set; }

        // SERVIÇO
        public string NmTituloServico { get; set; }

        public string DsServicoResumido { get; set; }

        public DateTime DtCadastroServico { get; set; }

        public DateTime DtPrevistaEntregaServico { get; set; }

        public string DsPrioridadeServico { get; set; }

        // VEÍCULO
        public string CdPlacaVeiculoServico { get; set; }

        public string NmModeloVeiculoServico { get; set; }

        public int CdAnoVeiculoServico { get; set; }

        public string NmCorVeiculoServico { get; set; }

        public decimal QtQuilometragemVeiculoServico { get; set; }

        // CLIENTE
        public int IdCliente { get; set; }

        // VALORES
        public decimal VlServico { get; set; }

        // OBSERVAÇÃO
        public string DsServico { get; set; }

        // MECÂNICO
        public int IdMecanico { get; set; }

        // STATUS
        public string StServico { get; set; }
    }
}