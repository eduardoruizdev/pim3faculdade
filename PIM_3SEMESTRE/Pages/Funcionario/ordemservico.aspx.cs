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
                try
                {
                    Logger.Log(
                        "ACESSO_ORDEM_SERVICO",
                        $"Usuário acessou a página de ordens de serviço | Usuário: {Session["nome_usuario"]}"
                    );

                    // Carrega todas as ordens de serviço
                    CarregarServicos();
                }
                catch (Exception ex)
                {
                    Logger.Log(
                        "ERRO_CARREGAR_ORDEM_SERVICO",
                        $"Erro ao carregar página de ordens de serviço | Erro: {ex.Message}"
                    );
                }
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
            try
            {
                gvServicos.DataSource =
                    controller.ListarServicos();

                gvServicos.DataBind();

                Logger.Log(
                    "LISTAGEM_SERVICOS",
                    "Lista de ordens de serviço carregada com sucesso."
                );
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_LISTAGEM_SERVICOS",
                    $"Erro ao carregar ordens de serviço | Erro: {ex.Message}"
                );
            }
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
                string cpf =
                    txtBuscar.Text.Trim();

                cpf = cpf.Replace(".", "")
                         .Replace("-", "")
                         .Replace("/", "");

                // Se vazio, recarrega tudo
                if (string.IsNullOrEmpty(cpf))
                {
                    Logger.Log(
                        "BUSCA_SERVICO_VAZIA",
                        "Busca vazia realizada. Lista completa carregada."
                    );

                    CarregarServicos();
                    return;
                }

                gvServicos.DataSource =
                    controller.BuscarServicoPorCpf(cpf);

                gvServicos.DataBind();

                Logger.Log(
                    "BUSCA_SERVICO_CPF",
                    $"Busca de serviço realizada | CPF: {cpf}"
                );
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_BUSCAR_SERVICO",
                    $"Erro ao buscar serviço por CPF | Erro: {ex.Message}"
                );

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
        /// Evento executado ao selecionar uma ordem de serviço.
        /// </summary>
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

                    ddlStatus.SelectedValue =
                        dados["st_servico"].ToString();

                    Logger.Log(
                        "VISUALIZAR_ORDEM_SERVICO",
                        $"Ordem de serviço visualizada | OS: {idServico} | Cliente: {lblCliente.Text}"
                    );
                }

                dados.Close();
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_VISUALIZAR_SERVICO",
                    $"Erro ao visualizar ordem de serviço | Erro: {ex.Message}"
                );

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
                int idServico =
                    Convert.ToInt32(
                        gvServicos.DataKeys[e.RowIndex].Value
                    );

                controller.ExcluirServico(idServico);

                Logger.Log(
                    "EXCLUIR_ORDEM_SERVICO",
                    $"Serviço excluído | ID Serviço: {idServico}"
                );

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
                Logger.Log(
                    "ERRO_EXCLUIR_SERVICO",
                    $"Erro ao excluir serviço | Erro: {ex.Message}"
                );

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

                controller.AtualizarStatus(
                    Convert.ToInt32(hfIdServico.Value),
                    ddlStatus.SelectedValue
                );

                Logger.Log(
                    "ATUALIZAR_STATUS_SERVICO",
                    $"Status atualizado | OS: {hfIdServico.Value} | Novo Status: {ddlStatus.SelectedValue}"
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
                Logger.Log(
                    "ERRO_ATUALIZAR_STATUS",
                    $"Erro ao atualizar status do serviço | Erro: {ex.Message}"
                );

                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }
    }
}
