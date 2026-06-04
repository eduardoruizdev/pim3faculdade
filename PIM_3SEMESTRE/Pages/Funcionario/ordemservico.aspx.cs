using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using System;
using System.Web.UI.WebControls;

namespace PIM_3SEMESTRE.Pages.Funcionario
{
    /// <summary>
    /// Página responsável pelo gerenciamento das ordens de serviço.
    /// Permite listar, buscar por CPF, visualizar detalhes,
    /// atualizar status e excluir serviços.
    /// </summary>
    public partial class ordemservico : System.Web.UI.Page
    {
        // Instância do controller de serviços
        ControllerServico controller =
            new ControllerServico();

        /// <summary>
        /// Evento executado ao carregar a página.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carrega todas as ordens de serviço na GridView
                CarregarServicos();
            }
        }

        // =========================================
        // CARREGAR GRID DE SERVIÇOS
        // =========================================

        /// <summary>
        /// Carrega todos os serviços cadastrados na GridView.
        /// </summary>
        private void CarregarServicos()
        {
            gvServicos.DataSource =
                controller.ListarServicos();

            gvServicos.DataBind();
        }

        // =========================================
        // BUSCA DE SERVIÇOS POR CPF
        // =========================================

        /// <summary>
        /// Realiza busca de serviços pelo CPF do cliente.
        /// </summary>
        protected void btnBuscar_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                // Captura e normaliza o CPF informado
                string cpf =
                    txtBuscar.Text.Trim();

                cpf = cpf.Replace(".", "")
                         .Replace("-", "")
                         .Replace("/", "");

                // Se não houver CPF, recarrega todos os registros
                if (string.IsNullOrEmpty(cpf))
                {
                    CarregarServicos();
                    return;
                }

                // Atualiza GridView com o resultado da busca
                gvServicos.DataSource =
                    controller.BuscarServicoPorCpf(cpf);

                gvServicos.DataBind();
            }
            catch (Exception ex)
            {
                // Exibe erro caso ocorra falha na busca
                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }

        // =========================================
        // SELEÇÃO DE SERVIÇO NA GRID
        // =========================================

        /// <summary>
        /// Evento executado ao selecionar uma ordem de serviço na GridView.
        /// Carrega os detalhes completos do serviço.
        /// </summary>
        protected void gvServicos_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            try
            {
                // Obtém o ID do serviço selecionado
                int idServico =
                    Convert.ToInt32(
                        gvServicos.SelectedDataKey.Value
                    );

                // Busca detalhes completos do serviço
                MySqlDataReader dados =
                    controller.BuscarServico(idServico);

                if (dados.Read())
                {
                    // Armazena o ID em campo oculto
                    hfIdServico.Value =
                        dados["id_servico"].ToString();

                    // =====================================
                    // DADOS DO CABEÇALHO
                    // =====================================

                    lblOS.Text =
                        dados["id_servico"].ToString();

                    lblTituloServico.Text =
                        dados["nm_titulo_servico"].ToString();

                    lblStatus.Text =
                        dados["st_servico"].ToString();

                    // =====================================
                    // DADOS DO VEÍCULO
                    // =====================================

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
                    // DADOS DO SERVIÇO
                    // =====================================

                    lblTipoServico.Text =
                        dados["nm_tipo_servico"].ToString();

                    lblDescricao.Text =
                        dados["ds_servico"].ToString();

                    // =====================================
                    // DADOS DO MECÂNICO
                    // =====================================

                    lblMecanico.Text =
                        dados["nm_mecanico"].ToString();

                    // =====================================
                    // DADOS DO CLIENTE
                    // =====================================

                    lblCliente.Text =
                        dados["nm_usuario"].ToString();

                    lblTelefone.Text =
                        dados["cd_telefone_cliente"].ToString();

                    // =====================================
                    // VALOR DO SERVIÇO
                    // =====================================

                    lblValor.Text =
                        Convert.ToDecimal(
                            dados["vl_servico"]
                        ).ToString("C");

                    // Atualiza dropdown de status
                    ddlStatus.SelectedValue =
                        dados["st_servico"].ToString();
                }

                dados.Close();
            }
            catch (Exception ex)
            {
                // Exibe erro caso ocorra falha na seleção
                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }

        // =========================================
        // EXCLUSÃO DE SERVIÇO
        // =========================================

        /// <summary>
        /// Remove uma ordem de serviço da base de dados.
        /// </summary>
        protected void gvServicos_RowDeleting(
            object sender,
            GridViewDeleteEventArgs e)
        {
            try
            {
                // Obtém o ID do serviço selecionado
                int idServico =
                    Convert.ToInt32(
                        gvServicos.DataKeys[e.RowIndex].Value
                    );

                // Executa exclusão
                controller.ExcluirServico(idServico);

                // Mensagem de sucesso
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Serviço excluído com sucesso!');",
                    true
                );

                // Recarrega a grid
                CarregarServicos();
            }
            catch (Exception ex)
            {
                // Exibe erro caso falha na exclusão
                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }

        // =========================================
        // ATUALIZAÇÃO DE STATUS
        // =========================================

        /// <summary>
        /// Atualiza o status da ordem de serviço selecionada.
        /// </summary>
        protected void btnAtualizar_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                // Verifica se um serviço foi selecionado
                if (string.IsNullOrEmpty(hfIdServico.Value))
                {
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "msg",
                        "alert('Selecione uma ordem de serviço primeiro!');",
                        true
                    );

                    return;
                }

                // Atualiza status no banco de dados
                controller.AtualizarStatus(
                    Convert.ToInt32(hfIdServico.Value),
                    ddlStatus.SelectedValue
                );

                // Mensagem de sucesso
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('Status atualizado com sucesso!');",
                    true
                );

                // Atualiza grid
                CarregarServicos();
            }
            catch (Exception ex)
            {
                // Exibe erro caso falha na atualização
                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }
    }
}