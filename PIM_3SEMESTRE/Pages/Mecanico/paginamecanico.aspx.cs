using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using System;
using System.Text;

namespace PIM_3SEMESTRE.Pages.Mecanico
{
    public partial class paginamecanico : System.Web.UI.Page
    {
        ControllerServico controller =
            new ControllerServico();

        protected string cardsServicos = "";

        protected string nomeMecanico = "";

        protected string detalhesServico = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarLogin();

                CarregarServicos();
            }
        }

        private void ValidarLogin()
        {
            if (Session["id_usuario"] == null)
            {
                Response.Redirect(
                    "~/Pages/Login/login.aspx");
            }
        }

        private void CarregarServicos()
        {
            try
            {
                int idUsuario =
                    Convert.ToInt32(
                        Session["id_usuario"]);

                StringBuilder html =
                    new StringBuilder();

                MySqlDataReader dados =
                    controller.ListarServicosMecanico(
                        idUsuario);

                int idSelecionado = 0;

                // PEGA ID DA URL
                if (!string.IsNullOrEmpty(
                    Request.QueryString["id"]))
                {
                    idSelecionado =
                        Convert.ToInt32(
                            Request.QueryString["id"]);
                }

                bool primeiro = true;

                while (dados.Read())
                {
                    nomeMecanico =
                        dados["nm_mecanico"].ToString();

                    int idServico =
                        Convert.ToInt32(
                            dados["id_servico"]);

                    // SE NÃO TIVER ID NA URL,
                    // PEGA O PRIMEIRO
                    if (idSelecionado == 0)
                    {
                        idSelecionado = idServico;
                    }

                    string status =
                        dados["st_servico"].ToString();

                    string classeStatus = "";

                    if (status == "Em andamento")
                    {
                        classeStatus = "andamento";
                    }
                    else if (status == "Concluído")
                    {
                        classeStatus = "concluido";
                    }
                    else
                    {
                        classeStatus = "aguardando";
                    }

                    bool ativo =
                        idSelecionado == idServico;

                    html.Append($@"

<a href='paginamecanico.aspx?id={idServico}'
   class='order-card {(ativo ? "active-card" : "")}'>

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
            {Convert.ToDateTime(
                        dados["dt_cadastro_servico"])
                        .ToString("dd/MM/yyyy")}

        </span>

        <span>

            <i class='fa-regular fa-clock'></i>

            Previsão:
            {Convert.ToDateTime(
                        dados["dt_prevista_entrega_servico"])
                        .ToString("dd/MM/yyyy")}

        </span>

    </div>

    <div class='arrow'>
        <i class='fa-solid fa-chevron-right'></i>
    </div>

</a>

");

                    // DETALHES DO SERVIÇO SELECIONADO
                    if (idSelecionado == idServico)
                    {
                        detalhesServico = $@"

<div class='details-top'>

    <h2>
        OS #{dados["id_servico"]}
    </h2>

    <span class='status {classeStatus}'>
        {status}
    </span>

</div>

<div class='section'>

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

</div>

<div class='section'>

    <h3>
        Informações do serviço
    </h3>

    <div class='service-info'>

        <p>
            <strong>Serviço solicitado</strong>
        </p>

        <span>
            {dados["nm_titulo_servico"]}
        </span>

        <p>
            <strong>Descrição</strong>
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
            {Convert.ToDateTime(
                        dados["dt_cadastro_servico"])
                        .ToString("dd/MM/yyyy")}
        </span>

    </div>

    <div class='dates'>

        <div>
            <i class='fa-regular fa-clock'></i>
            Previsão
        </div>

        <span>
            {Convert.ToDateTime(
                        dados["dt_prevista_entrega_servico"])
                        .ToString("dd/MM/yyyy")}
        </span>

    </div>

</div>

<div class='section'>

    <h3>
        Cliente
    </h3>

    <p class='obs'>
        {dados["nm_cliente"]}
    </p>

</div>

<div class='section'>

    <h3>
        Valor do Serviço
    </h3>

    <p class='obs'>

        R$ {Convert.ToDecimal(
                    dados["vl_servico"])
                    .ToString("N2")}

    </p>

</div>

<button class='details-btn' type='button'>
    Ver detalhes completos
</button>

";
                    }

                    primeiro = false;
                }

                cardsServicos = html.ToString();

                dados.Close();
            }
            catch (Exception ex)
            {
                Response.Write(
                    "<script>alert('Erro: " +
                    ex.Message.Replace("'", "") +
                    "');</script>");
            }
        }
    }
}