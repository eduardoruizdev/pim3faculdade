using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using System;

namespace PIM_3SEMESTRE.Pages.ADM
{
    public partial class relatorio
        : System.Web.UI.Page
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

            CarregarTabela();
        }

        private void CarregarTabela()
        {
            MySqlDataReader dados =
            controller.ListarUltimosServicos();

            gvServicos.DataSource = dados;

            gvServicos.DataBind();
        }

    }
}
