using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;
using System;

namespace PIM_3SEMESTRE.Pages.ADM
{
    public partial class CadastrarServico
        : System.Web.UI.Page
    {

        TipoServicoController controller =
        new TipoServicoController();

        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarServicos();
            }
        }

        protected void btnCadastrar_Click(
            object sender,
            EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(
                    txtTipoServico.Text))
                {
                    ExibirMensagem(
                    "Digite o nome do serviço.");

                    return;
                }

                bool existe =
                controller.VerificarTipoServicoExiste(
                    txtTipoServico.Text
                );

                if (existe)
                {
                    ExibirMensagem(
                    "Esse tipo de serviço já existe.");

                    return;
                }

                TipoServicoModel servico =
                new TipoServicoModel();

                servico.NomeTipoServico =
                txtTipoServico.Text;

                controller.CadastrarTipoServico(
                    servico
                );

                txtTipoServico.Text = "";

                CarregarServicos();

                ExibirMensagem(
                "Tipo de serviço cadastrado com sucesso!");

            }
            catch (Exception ex)
            {
                ExibirMensagem(
                "Erro: " + ex.Message);
            }
        }

        private void CarregarServicos()
        {
            MySqlDataReader dados =
            controller.ListarTiposServico();

            gvServicos.DataSource = dados;

            gvServicos.DataBind();
        }

        protected void gvServicos_RowDeleting(
            object sender,
            System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {

                int id =
                Convert.ToInt32(
                    gvServicos.DataKeys[e.RowIndex].Value
                );

                controller.ExcluirTipoServico(id);

                CarregarServicos();

                ExibirMensagem(
                "Serviço excluído com sucesso!");

            }
            catch (Exception ex)
            {
                ExibirMensagem(
                "Erro: " + ex.Message);
            }
        }

        private void ExibirMensagem(
            string mensagem)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "msg",
                $"alert('{mensagem}');",
                true
            );
        }

    }
}
