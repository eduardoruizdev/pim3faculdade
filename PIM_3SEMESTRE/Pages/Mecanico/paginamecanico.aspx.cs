using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using System;
using System.Text;

namespace PIM_3SEMESTRE.Pages.Mecanico
{
    /// <summary>
    /// Página inicial do mecânico.
    /// Responsável por listar ordens de serviço atribuídas ao mecânico
    /// e exibir os detalhes da OS selecionada.
    /// </summary>
    public partial class paginamecanico : System.Web.UI.Page
    {
        // Controller responsável pelas operações de serviço
        ControllerServico controller =
            new ControllerServico();

        // HTML dinâmico dos cards de serviços
        protected string cardsServicos = "";

        // Nome do mecânico logado
        protected string nomeMecanico = "";

        // HTML dos detalhes do serviço selecionado
        protected string detalhesServico = "";

        /// <summary>
        /// Evento de carregamento da página
        /// </summary>
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            ValidarLogin();

            CarregarServicos();
        }

        /// <summary>
        /// Verifica se existe sessão ativa do usuário
        /// </summary>
        private void ValidarLogin()
        {
            if (Session["id_usuario"] == null)
            {
                Logger.Log(
                    "ACESSO_NEGADO_MECANICO",
                    "Tentativa de acesso sem login na página do mecânico."
                );

                Response.Redirect(
                    "~/Pages/Login/login.aspx"
                );
            }
        }

        /// <summary>
        /// Carrega os serviços do mecânico
        /// </summary>
        private void CarregarServicos()
        {
            try
            {
                int idUsuario =
                    Convert.ToInt32(
                        Session["id_usuario"]
                    );

                Logger.Log(
                    "ACESSO_PAGINA_MECANICO",
                    $"Página do mecânico carregada | ID Usuário: {idUsuario}"
                );

                StringBuilder html =
                    new StringBuilder();

                MySqlDataReader dados =
                    controller.ListarServicosMecanico(
                        idUsuario
                    );

                int idSelecionado = 0;

                // =========================================
                // PEGA ID DA URL COM SEGURANÇA
                // =========================================

                if (!string.IsNullOrEmpty(
                    Request.QueryString["id"]))
                {
                    int.TryParse(
                        Request.QueryString["id"],
                        out idSelecionado
                    );
                }

                // =========================================
                // PERCORRE SERVIÇOS
                // =========================================

                while (dados.Read())
                {
                    nomeMecanico =
                        dados["nm_mecanico"]
                        .ToString();

                    int idServico =
                        Convert.ToInt32(
                            dados["id_servico"]
                        );

                    // Se nenhum serviço estiver selecionado
                    if (idSelecionado == 0)
                    {
                        idSelecionado =
                            idServico;
                    }

                    string status =
                        dados["st_servico"]
                        .ToString();

                    string classeStatus =
                        "aguardando";

                    if (status == "Em andamento")
                    {
                        classeStatus =
                            "andamento";
                    }
                    else if (
                        status == "Concluído")
                    {
                        classeStatus =
                            "concluido";
                    }

                    bool ativo =
                        idSelecionado ==
                        idServico;

                    // =========================================
                    // DATAS SEGURAS
                    // =========================================

                    string dataEntrada =
                        dados["dt_cadastro_servico"]
                        != DBNull.Value
                        ?
                        Convert.ToDateTime(
                            dados[
                                "dt_cadastro_servico"])
                            .ToString("dd/MM/yyyy")
                        :
                        "Sem data";

                    string dataPrevisao =
                        dados[
                            "dt_prevista_entrega_servico"]
                        != DBNull.Value
                        ?
                        Convert.ToDateTime(
                            dados[
                                "dt_prevista_entrega_servico"])
                            .ToString("dd/MM/yyyy")
                        :
                        "Sem previsão";

                    // =========================================
                    // VALOR SEGURO
                    // =========================================

                    string valorServico =
                        dados["vl_servico"]
                        != DBNull.Value
                        ?
                        Convert.ToDecimal(
                            dados["vl_servico"])
                            .ToString("N2")
                        :
                        "0,00";

                    // =========================================
                    // CARD DO SERVIÇO
                    // =========================================

                    html.Append($@"

<a href='paginamecanico.aspx?id={idServico}' 
class='card-service {(ativo ? "ativo" : "")}'>

<div class='top-order'>

    <div class='order-number'>
        OS #{dados["id_servico"]}
    </div>

    <span class='status {classeStatus}'>
        {status}
    </span>

</div>

<h3>
    {dados["nm_titulo_servico"]}
</h3>

<div class='info-line'>

    <span>
        <i class='fa-solid fa-car'></i>
        {dados["nm_modelo_veiculo_servico"]}
    </span>

    <span>
        Placa:
        {dados["cd_placa_veiculo_servico"]}
    </span>

</div>

<div class='info-line'>

    <span>
        <i class='fa-regular fa-calendar'></i>
        Entrada:
        {dataEntrada}
    </span>

    <span>
        <i class='fa-regular fa-clock'></i>
        Previsão:
        {dataPrevisao}
    </span>

</div>

<div class='arrow'>
    <i class='fa-solid fa-chevron-right'></i>
</div>

</a>

");

                    // =========================================
                    // DETALHES DO SERVIÇO
                    // =========================================

                    if (idSelecionado == idServico)
                    {
                        Logger.Log(
                            "VISUALIZAR_OS_MECANICO",
                            $"Mecânico visualizou OS | ID Serviço: {idServico} | Status: {status}"
                        );

                        detalhesServico = $@"

<h2>
    OS #{dados["id_servico"]}
</h2>

<span class='status {classeStatus}'>
    {status}
</span>

<h3>
    <i class='fa-solid fa-screwdriver-wrench'></i>
    Informações do veículo
</h3>

<div class='vehicle-box'>

    <img src='https://images.unsplash.com/photo-1492144534655-ae79c964c9d7?q=80&w=1200&auto=format&fit=crop' />

    <div>

        <h4>
            {dados["nm_modelo_veiculo_servico"]}
        </h4>

        <p>
            Placa:
            {dados["cd_placa_veiculo_servico"]}
        </p>

        <p>
            Cor:
            {dados["nm_cor_veiculo_servico"]}
        </p>

    </div>

</div>

<h3>
    Informações do serviço
</h3>

<div class='service-info'>

    <p>
        <strong>
            Serviço solicitado
        </strong>
    </p>

    <span>
        {dados["nm_titulo_servico"]}
    </span>

    <p>
        <strong>
            Descrição
        </strong>
    </p>

    <span>
        {dados["ds_servico"]}
    </span>

</div>

<div class='dates'>

    <div>
        <i class='fa-regular fa-calendar'></i>
        Entrada
    </div>

    <span>
        {dataEntrada}
    </span>

</div>

<div class='dates'>

    <div>
        <i class='fa-regular fa-clock'></i>
        Previsão
    </div>

    <span>
        {dataPrevisao}
    </span>

</div>

<h3>
    Cliente
</h3>

<p class='obs'>
    {dados["nm_cliente"]}
</p>

<h3>
    Valor do Serviço
</h3>

<p class='obs'>
    R$ {valorServico}
</p>

<a href='detalheservico.aspx?id={idServico}' class='btn-details'>
    Ver detalhes completos
</a>

";
                    }
                }

                cardsServicos =
                    html.ToString();

                dados.Close();

                Logger.Log(
                    "SERVICOS_MECANICO_CARREGADOS",
                    $"Serviços do mecânico carregados com sucesso | Mecânico: {nomeMecanico}"
                );
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_PAGINA_MECANICO",
                    $"Erro na página do mecânico | Erro: {ex.Message}"
                );

                Response.Write(
                    "<script>alert('Erro: " +
                    ex.Message
                    .Replace("'", "") +
                    "');</script>"
                );
            }
        }
    }
}
