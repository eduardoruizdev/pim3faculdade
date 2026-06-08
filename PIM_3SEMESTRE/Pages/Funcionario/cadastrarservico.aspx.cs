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
                try
                {
                    Logger.Log(
                        "ACESSO_CADASTRO_SERVICO",
                        $"Usuário acessou a página de cadastro de serviço | Usuário: {Session["nome_usuario"]}"
                    );

                    CarregarClientes();
                    CarregarTiposServico();
                    CarregarMecanicos();
                }
                catch (Exception ex)
                {
                    Logger.Log(
                        "ERRO_CARREGAR_PAGINA_SERVICO",
                        $"Erro ao carregar página de serviços | Erro: {ex.Message}"
                    );
                }
            }
        }

        /// <summary>
        /// Carrega os clientes cadastrados no DropDownList.
        /// </summary>
        private void CarregarClientes()
        {
            try
            {
                ClienteController controller =
                    new ClienteController();

                MySqlDataReader dados =
                    controller.ListarClientes();

                ddlCliente.Items.Clear();

                ddlCliente.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        "Selecione o cliente",
                        "0"
                    )
                );

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
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_CARREGAR_CLIENTES",
                    $"Erro ao carregar clientes | Erro: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Carrega os mecânicos cadastrados no sistema.
        /// </summary>
        private void CarregarMecanicos()
        {
            try
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
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_CARREGAR_MECANICOS",
                    $"Erro ao carregar mecânicos | Erro: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Carrega os tipos de serviços cadastrados.
        /// </summary>
        private void CarregarTiposServico()
        {
            try
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
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_CARREGAR_TIPOS_SERVICO",
                    $"Erro ao carregar tipos de serviço | Erro: {ex.Message}"
                );
            }
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

                Logger.Log(
                    "CLIENTE_SELECIONADO",
                    $"Cliente selecionado no cadastro de serviço | ID Cliente: {ddlCliente.SelectedValue}"
                );
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_SELECIONAR_CLIENTE",
                    $"Erro ao selecionar cliente | Erro: {ex.Message}"
                );

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
/// </summary>
protected void btnCadastrar_Click(
    object sender,
    EventArgs e
)
        {
            try
            {
                // =====================================
                // VALIDAÇÕES
                // =====================================

                if (ddlTipoServico.SelectedValue == "0")
                {
                    throw new Exception(
                        "Selecione um tipo de serviço."
                    );
                }

                if (ddlCliente.SelectedValue == "0")
                {
                    throw new Exception(
                        "Selecione um cliente."
                    );
                }

                if (ddlMecanico.SelectedValue == "0")
                {
                    throw new Exception(
                        "Selecione um mecânico."
                    );
                }

                // =====================================
                // VALIDAÇÕES EXTRAS
                // =====================================

                // Título obrigatório
                if (string.IsNullOrWhiteSpace(txtTitulo.Text))
                {
                    throw new Exception(
                        "Informe o título do serviço."
                    );
                }

                // Descrição obrigatória
                if (string.IsNullOrWhiteSpace(txtDescricao.Text))
                {
                    throw new Exception(
                        "Informe a descrição do serviço."
                    );
                }

                // Data entrada obrigatória
                if (string.IsNullOrWhiteSpace(txtDataEntrada.Text))
                {
                    throw new Exception(
                        "Informe a data de entrada."
                    );
                }

                // Data entrega obrigatória
                if (string.IsNullOrWhiteSpace(txtDataEntrega.Text))
                {
                    throw new Exception(
                        "Informe a data prevista de entrega."
                    );
                }

                // Validar datas
                DateTime dataEntrada;
                DateTime dataEntrega;

                if (!DateTime.TryParse(
                    txtDataEntrada.Text,
                    out dataEntrada
                ))
                {
                    throw new Exception(
                        "Data de entrada inválida."
                    );
                }

                if (!DateTime.TryParse(
                    txtDataEntrega.Text,
                    out dataEntrega
                ))
                {
                    throw new Exception(
                        "Data de entrega inválida."
                    );
                }

                // Data entrega menor
                if (dataEntrega < dataEntrada)
                {
                    throw new Exception(
                        "A data de entrega não pode ser menor que a data de entrada."
                    );
                }

                // Placa obrigatória
                if (string.IsNullOrWhiteSpace(txtPlaca.Text))
                {
                    throw new Exception(
                        "Informe a placa do veículo."
                    );
                }

                // Modelo obrigatório
                if (string.IsNullOrWhiteSpace(txtModelo.Text))
                {
                    throw new Exception(
                        "Informe o modelo do veículo."
                    );
                }

                // Validar ano
                int anoVeiculo = 0;

                if (!string.IsNullOrWhiteSpace(txtAno.Text))
                {
                    if (!int.TryParse(
                        txtAno.Text,
                        out anoVeiculo
                    ))
                    {
                        throw new Exception(
                            "Ano do veículo inválido."
                        );
                    }

                    if (
                        anoVeiculo < 1950 ||
                        anoVeiculo > DateTime.Now.Year + 1
                    )
                    {
                        throw new Exception(
                            "Informe um ano válido."
                        );
                    }
                }

                // Validar KM
                decimal km = 0;

                if (!string.IsNullOrWhiteSpace(txtKm.Text))
                {
                    if (
                        !decimal.TryParse(
                            txtKm.Text.Replace(",", "."),
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out km
                        )
                    )
                    {
                        throw new Exception(
                            "Quilometragem inválida."
                        );
                    }

                    if (km < 0)
                    {
                        throw new Exception(
                            "A quilometragem não pode ser negativa."
                        );
                    }
                }

                // Validar mão de obra
                decimal maoObra = 0;

                if (!string.IsNullOrWhiteSpace(txtMaoObra.Text))
                {
                    if (
                        !decimal.TryParse(
                            txtMaoObra.Text.Replace(",", "."),
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out maoObra
                        )
                    )
                    {
                        throw new Exception(
                            "Valor da mão de obra inválido."
                        );
                    }

                    if (maoObra < 0)
                    {
                        throw new Exception(
                            "O valor da mão de obra não pode ser negativo."
                        );
                    }
                }

                // Validar peças
                decimal pecas = 0;

                if (!string.IsNullOrWhiteSpace(txtPecas.Text))
                {
                    if (
                        !decimal.TryParse(
                            txtPecas.Text.Replace(",", "."),
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out pecas
                        )
                    )
                    {
                        throw new Exception(
                            "Valor das peças inválido."
                        );
                    }

                    if (pecas < 0)
                    {
                        throw new Exception(
                            "O valor das peças não pode ser negativo."
                        );
                    }
                }

                // Validar desconto
                decimal desconto = 0;

                if (!string.IsNullOrWhiteSpace(txtDesconto.Text))
                {
                    if (
                        !decimal.TryParse(
                            txtDesconto.Text.Replace(",", "."),
                            System.Globalization.NumberStyles.Any,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out desconto
                        )
                    )
                    {
                        throw new Exception(
                            "Valor do desconto inválido."
                        );
                    }

                    if (desconto < 0)
                    {
                        throw new Exception(
                            "O desconto não pode ser negativo."
                        );
                    }

                    if (desconto > (maoObra + pecas))
                    {
                        throw new Exception(
                            "O desconto não pode ser maior que o valor total."
                        );
                    }
                }

                // =====================================
                // CALCULAR TOTAL
                // =====================================

                decimal total =
                    (maoObra + pecas) - desconto;

                if (total <= 0)
                {
                    throw new Exception(
                        "O valor do serviço deve ser maior que zero."
                    );
                }

                // Não permitir valor absurdamente alto
                if (total > 1000000)
                {
                    throw new Exception(
                        "O valor do serviço está acima do limite permitido."
                    );
                }

                // Garantir que o campo não fique vazio
                if (string.IsNullOrWhiteSpace(txtValorTotal.Text))
                {
                    throw new Exception(
                        "O valor total do serviço não pode ficar vazio."
                    );
                }

                // =====================================
                // OBJETO SERVIÇO
                // =====================================

                ModelServico servico =
                    new ModelServico();

                servico.IdTipoServico =
                    Convert.ToInt32(
                        ddlTipoServico.SelectedValue
                    );

                servico.NmTituloServico =
                    txtTitulo.Text.Trim();

                servico.DsServicoResumido =
                    txtDescricao.Text.Trim();

                servico.DtCadastroServico =
                    dataEntrada;

                servico.DtPrevistaEntregaServico =
                    dataEntrega;

                servico.DsPrioridadeServico =
                    ddlPrioridade.SelectedValue;

                servico.CdPlacaVeiculoServico =
                    txtPlaca.Text
                        .Trim()
                        .ToUpper();

                servico.NmModeloVeiculoServico =
                    txtModelo.Text.Trim();

                servico.CdAnoVeiculoServico =
                    anoVeiculo;

                servico.NmCorVeiculoServico =
                    ddlCor.SelectedValue;

                servico.QtQuilometragemVeiculoServico =
                    km;

                servico.IdCliente =
                    Convert.ToInt32(
                        ddlCliente.SelectedValue
                    );

                servico.VlServico =
                    total;

                servico.DsServico =
                    txtObservacao.Text.Trim();

                servico.IdMecanico =
                    Convert.ToInt32(
                        ddlMecanico.SelectedValue
                    );

                // =====================================
                // SALVAR SERVIÇO
                // =====================================

                ControllerServico controller =
                    new ControllerServico();

                controller.CadastrarServico(
                    servico
                );

                Logger.Log(
                    "CADASTRO_SERVICO_OFICINA",
                    $"Serviço cadastrado | Cliente: {ddlCliente.SelectedItem.Text} | Mecânico: {ddlMecanico.SelectedItem.Text} | Veículo: {txtModelo.Text} | Placa: {txtPlaca.Text} | Valor: R$ {total}"
                );

                // =====================================
                // SUCESSO
                // =====================================

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
                Logger.Log(
                    "ERRO_CADASTRAR_SERVICO",
                    $"Erro ao cadastrar serviço | Erro: {ex.Message}"
                );

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
