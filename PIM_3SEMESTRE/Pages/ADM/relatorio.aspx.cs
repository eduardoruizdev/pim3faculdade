using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using System;

namespace PIM_3SEMESTRE.Pages.ADM
{
    /// <summary>
    /// Página responsável pela exibição dos relatórios
    /// e indicadores gerais do sistema.
    /// Apresenta informações sobre clientes, mecânicos,
    /// serviços cadastrados e faturamento total.
    /// </summary>
    public partial class relatorio
        : System.Web.UI.Page
    {
        // Instância do controller responsável pelos relatórios
        RelatorioController controller =
        new RelatorioController();

        /// <summary>
        /// Evento executado ao carregar a página.
        /// Carrega os dados do dashboard apenas na primeira abertura.
        /// </summary>
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carrega os indicadores do dashboard
                CarregarDashboard();
            }
        }

        /// <summary>
        /// Carrega os indicadores principais do sistema.
        /// Exibe totais de clientes, mecânicos, serviços
        /// e valor total arrecadado.
        /// </summary>
        private void CarregarDashboard()
        {
            // Exibe a quantidade total de clientes cadastrados
            lblClientes.Text =
            controller.TotalClientes().ToString();

            // Exibe a quantidade total de mecânicos cadastrados
            lblMecanicos.Text =
            controller.TotalMecanicos().ToString();

            // Exibe a quantidade total de serviços cadastrados
            lblServicos.Text =
            controller.TotalServicos().ToString();

            // Exibe o valor total dos serviços formatado em moeda
            lblValor.Text =
            "R$ " +
            controller.ValorTotalServicos()
            .ToString("N2");

            // Carrega a tabela com os últimos serviços cadastrados
            CarregarTabela();
        }

        /// <summary>
        /// Carrega a tabela contendo os serviços mais recentes.
        /// </summary>
        private void CarregarTabela()
        {
            // Busca os dados dos serviços no banco de dados
            MySqlDataReader dados =
            controller.ListarUltimosServicos();

            // Define a fonte de dados da GridView
            gvServicos.DataSource = dados;

            // Atualiza a exibição da GridView
            gvServicos.DataBind();
        }
    }
}