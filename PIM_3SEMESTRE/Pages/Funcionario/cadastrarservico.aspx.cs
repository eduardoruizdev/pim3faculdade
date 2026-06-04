using System;
using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;

namespace PIM_3SEMESTRE.Pages.Funcionario
{
    /// <summary>
    /// Página responsável pelo cadastro de serviços da oficina.
    /// Permite selecionar cliente, mecânico, tipo de serviço,
    /// informar dados do veículo e calcular o valor total.
    /// </summary>
    public partial class cadastrarservico : System.Web.UI.Page
    {
        /// <summary>
        /// Evento executado ao carregar a página.
        /// Carrega clientes, tipos de serviços e mecânicos.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarClientes();
                CarregarTiposServico();
                CarregarMecanicos();
            }
        }

        /// <summary>
        /// Carrega os clientes cadastrados no DropDownList.
        /// </summary>
        private void CarregarClientes()
        {
            ClienteController controller =
                new ClienteController();

            MySqlDataReader dados =
                controller.ListarClientes();

            ddlCliente.Items.Clear();

            // Item padrão da lista
            ddlCliente.Items.Add(
                new System.Web.UI.WebControls.ListItem(
                    "Selecione o cliente",
                    "0"
                )
            );

            // Adiciona os clientes encontrados
            while (dados.Read())
            {
                ddlCliente.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        dados["nm_usuario"].ToString(),
                        dados["id_cliente"].ToString()
                    )
                );
            }

            dados.Close();
        }

        /// <summary>
        /// Carrega os mecânicos cadastrados no sistema.
        /// </summary>
        private void CarregarMecanicos()
        {
            ControllerServico controller =
                new ControllerServico();

            MySqlDataReader dados =
                controller.ListarMecanicos();

            ddlMecanico.Items.Clear();

            ddlMecanico.Items.Add(
                new System.Web.UI.WebControls.ListItem(
                    "Selecione o mecânico",
                    "0"
                )
            );

            while (dados.Read())
            {
                ddlMecanico.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        dados["nm_usuario"].ToString(),
                        dados["id_mecanico"].ToString()
                    )
                );
            }

            dados.Close();
        }

        /// <summary>
        /// Carrega os tipos de serviços cadastrados.
        /// </summary>
        private void CarregarTiposServico()
        {
            ControllerServico controller =
                new ControllerServico();

            MySqlDataReader dados =
                controller.ListarTiposServico();

            ddlTipoServico.Items.Clear();

            ddlTipoServico.Items.Add(
                new System.Web.UI.WebControls.ListItem(
                    "Selecione o tipo de serviço",
                    "0"
                )
            );

            while (dados.Read())
            {
                ddlTipoServico.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        dados["nm_tipo_servico"].ToString(),
                        dados["id_tipo_servico"].ToString()
                    )
                );
            }

            dados.Close();
        }

        /// <summary>
        /// Evento executado ao selecionar um cliente.
        /// Carrega automaticamente telefone, e-mail e CPF.
        /// </summary>
        protected void ddlCliente_SelectedIndexChanged(
            object sender,
            EventArgs e
        )
        {
            try
            {
                if (ddlCliente.SelectedValue == "0")
                    return;

                ClienteController controller =
                    new ClienteController();

                MySqlDataReader dados =
                    controller.BuscarClientePorId(
                        Convert.ToInt32(
                            ddlCliente.SelectedValue
                        )
                    );

                if (dados.Read())
                {
                    txtTelefone.Text =
                        dados["cd_telefone_cliente"].ToString();

                    txtEmail.Text =
                        dados["nm_email_usuario"].ToString();

                    txtCpf.Text =
                        dados["cd_cpf_cliente"].ToString();
                }

                dados.Close();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "alert",
                    "alert('" +
                    ex.Message.Replace("'", "") +
                    "');",
                    true
                );
            }
        }

        /// <summary>
        /// Calcula automaticamente o valor total do serviço.
        /// Soma mão de obra e peças, subtraindo o desconto.
        /// </summary>
        protected void CalcularValorTotal(
            object sender,
            EventArgs e
        )
        {
            decimal maoObra = 0;
            decimal pecas = 0;
            decimal desconto = 0;

            decimal.TryParse(
                txtMaoObra.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out maoObra
            );

            decimal.TryParse(
                txtPecas.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out pecas
            );

            decimal.TryParse(
                txtDesconto.Text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out desconto
            );

            decimal total =
                (maoObra + pecas) - desconto;

            txtValorTotal.Text =
                total.ToString("N2");
        }

        /// <summary>
        /// Evento executado ao clicar no botão Cadastrar.
        /// Realiza validações e salva o serviço no banco.
        /// </summary>
        protected void btnCadastrar_Click(
            object sender,
            EventArgs e
        )
        {
            try
            {
                // Validação do tipo de serviço
                if (ddlTipoServico.SelectedValue == "0")
                {
                    throw new Exception(
                        "Selecione um tipo de serviço."
                    );
                }

                // Validação do cliente
                if (ddlCliente.SelectedValue == "0")
                {
                    throw new Exception(
                        "Selecione um cliente."
                    );
                }

                // Criação do objeto serviço
                ModelServico servico =
                    new ModelServico();

                // Preenchimento dos dados do serviço
                servico.IdTipoServico =
                    Convert.ToInt32(
                        ddlTipoServico.SelectedValue
                    );

                servico.NmTituloServico =
                    txtTitulo.Text;

                servico.DsServicoResumido =
                    txtDescricao.Text;

                servico.DtCadastroServico =
                    Convert.ToDateTime(
                        txtDataEntrada.Text
                    );

                servico.DtPrevistaEntregaServico =
                    Convert.ToDateTime(
                        txtDataEntrega.Text
                    );

                servico.DsPrioridadeServico =
                    ddlPrioridade.SelectedValue;

                servico.CdPlacaVeiculoServico =
                    txtPlaca.Text.ToUpper();

                servico.NmModeloVeiculoServico =
                    txtModelo.Text;

                servico.CdAnoVeiculoServico =
                    string.IsNullOrWhiteSpace(txtAno.Text)
                    ? 0
                    : Convert.ToInt32(txtAno.Text);

                servico.NmCorVeiculoServico =
                    ddlCor.SelectedValue;

                servico.QtQuilometragemVeiculoServico =
                    string.IsNullOrWhiteSpace(txtKm.Text)
                    ? 0
                    : Convert.ToDecimal(txtKm.Text);

                servico.IdCliente =
                    Convert.ToInt32(
                        ddlCliente.SelectedValue
                    );

                servico.VlServico =
                    string.IsNullOrWhiteSpace(
                        txtValorTotal.Text
                    )
                    ? 0
                    : decimal.Parse(
                        txtValorTotal.Text,
                        new System.Globalization.CultureInfo("pt-BR")
                    );

                servico.DsServico =
                    txtObservacao.Text;

                // Define o mecânico responsável
                servico.IdMecanico =
                    Convert.ToInt32(
                        ddlMecanico.SelectedValue
                    );

                // Validação do mecânico
                if (ddlMecanico.SelectedValue == "0")
                {
                    throw new Exception(
                        "Selecione um mecânico."
                    );
                }

                // Salva o serviço no banco de dados
                ControllerServico controller =
                    new ControllerServico();

                controller.CadastrarServico(
                    servico
                );

                // Exibe mensagem de sucesso
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "alert",
                    "alert('Serviço cadastrado com sucesso!');",
                    true
                );

                LimparCampos();
            }
            catch (Exception ex)
            {
                // Exibe mensagem de erro
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "alert",
                    "alert('Erro ao cadastrar: " +
                    ex.Message.Replace("'", "") +
                    "');",
                    true
                );
            }
        }

        /// <summary>
        /// Limpa todos os campos do formulário após o cadastro.
        /// </summary>
        private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtDescricao.Text = "";
            txtDataEntrada.Text = "";
            txtDataEntrega.Text = "";
            txtPlaca.Text = "";
            txtModelo.Text = "";
            txtAno.Text = "";
            txtKm.Text = "";
            txtMaoObra.Text = "";
            txtPecas.Text = "";
            txtDesconto.Text = "";
            txtValorTotal.Text = "";
            txtObservacao.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            txtCpf.Text = "";

            ddlTipoServico.SelectedIndex = 0;
            ddlPrioridade.SelectedIndex = 0;
            ddlCor.SelectedIndex = 0;
            ddlCliente.SelectedIndex = 0;
            ddlMecanico.SelectedIndex = 0;
        }
    }
}