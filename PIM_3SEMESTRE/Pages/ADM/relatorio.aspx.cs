
using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using System;

namespace PIM_3SEMESTRE.Pages.ADM
{
    public partial class relatorio : System.Web.UI.Page
    {
        RelatorioController controller =
            new RelatorioController();

        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDashboard();
            }
        }

        private void CarregarDashboard()
        {
            lblClientes.Text =
                controller.TotalClientes().ToString();

            lblMecanicos.Text =
                controller.TotalMecanicos().ToString();

            lblServicos.Text =
                controller.TotalServicos().ToString();

            lblValor.Text =
                "R$ " +
                controller.ValorTotalServicos()
                .ToString("N2");

            CarregarUltimosServicos();
            CarregarProdutividade();
            CarregarRankingServicos();
            CarregarClientes();
            CarregarMecanicos();
        }

        private void CarregarUltimosServicos()
        {
            gvServicos.DataSource =
                controller.ListarUltimosServicos();

            gvServicos.DataBind();
        }

        private void CarregarProdutividade()
        {
            gvProdutividade.DataSource =
                controller.RelatorioProdutividadeMensal();

            gvProdutividade.DataBind();
        }

        private void CarregarRankingServicos()
        {
            gvRankingServicos.DataSource =
                controller.RankingServicos();

            gvRankingServicos.DataBind();
        }

        private void CarregarClientes()
        {
            gvClientes.DataSource =
                controller.ClientesRecorrentes();

            gvClientes.DataBind();
        }

        private void CarregarMecanicos()
        {
            gvMecanicos.DataSource =
                controller.RankingMecanicos();

            gvMecanicos.DataBind();
        }
    }
}
