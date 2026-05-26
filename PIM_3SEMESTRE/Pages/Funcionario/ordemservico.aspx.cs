using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using System;
using System.Web.UI.WebControls;


namespace PIM_3SEMESTRE.Pages.Funcionario
{
    public partial class ordemservico : System.Web.UI.Page
    {
        ControllerServico controller =
            new ControllerServico();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarServicos();
            }
        }

        // =========================================
        // CARREGAR GRID
        // =========================================

        private void CarregarServicos()
        {
            gvServicos.DataSource =
                controller.ListarServicos();

            gvServicos.DataBind();
        }

        // =========================================
        // BUSCAR
        // =========================================

        protected void btnBuscar_Click(
            object sender,
            EventArgs e)
        {
            CarregarServicos();
        }

        // =========================================
        // SELECIONAR GRID
        // =========================================

protected void gvServicos_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            try
            {
                int idServico =
                    Convert.ToInt32(
                        gvServicos.SelectedDataKey.Value
                    );

                MySqlDataReader dados =
                    controller.BuscarServico(idServico);

                if (dados.Read())
                {
                    hfIdServico.Value =
                        dados["id_servico"].ToString();

                    lblOS.Text =
                        dados["id_servico"].ToString();

                    lblStatus.Text =
                        dados["st_servico"].ToString();

                    lblModelo.Text =
                        dados["nm_modelo_veiculo_servico"].ToString();

                    lblPlaca.Text =
                        dados["cd_placa_veiculo_servico"].ToString();

                    lblCor.Text =
                        dados["nm_cor_veiculo_servico"].ToString();

                    lblAno.Text =
                        dados["cd_ano_veiculo_servico"].ToString();

                    lblKm.Text =
                        dados["qt_quilometragem_veiculo_servico"].ToString();

                    // =====================================
                    // TIPO DE SERVIÇO
                    // =====================================

                    lblTipoServico.Text =
                        dados["nm_tipo_servico"].ToString();

                    // =====================================
                    // MECÂNICO RESPONSÁVEL
                    // =====================================

                    lblMecanico.Text =
                        dados["nm_mecanico"].ToString();

                    // =====================================
                    // CLIENTE
                    // =====================================

                    lblCliente.Text =
                        dados["nm_usuario"].ToString();

                    lblTelefone.Text =
                        dados["cd_telefone_cliente"].ToString();

                    // =====================================
                    // VALOR
                    // =====================================

                    lblValor.Text =
                        Convert.ToDecimal(
                            dados["vl_servico"]
                        ).ToString("C");

                    // =====================================
                    // DESCRIÇÃO
                    // =====================================

                    lblDescricao.Text =
                        dados["ds_servico"].ToString();

                    // =====================================
                    // STATUS
                    // =====================================

                    ddlStatus.SelectedValue =
                        dados["st_servico"].ToString();
                }

                dados.Close();
            }
            catch (Exception ex)
            {
                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }

        // =========================================
        // EXCLUIR
        // =========================================

        protected void gvServicos_RowDeleting(
            object sender,
            GridViewDeleteEventArgs e)
        {
            try
            {
                int idServico =
                    Convert.ToInt32(
                        gvServicos.DataKeys[e.RowIndex].Value
                    );

                controller.ExcluirServico(idServico);

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Serviço excluído com sucesso!');",
                    true
                );

                CarregarServicos();
            }
            catch (Exception ex)
            {
                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }

        // =========================================
        // ATUALIZAR STATUS
        // =========================================

        protected void btnAtualizar_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                controller.AtualizarStatus(
                    Convert.ToInt32(hfIdServico.Value),
                    ddlStatus.SelectedValue
                );

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Status atualizado com sucesso!');",
                    true
                );

                CarregarServicos();
            }
            catch (Exception ex)
            {
                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }
    }
}
