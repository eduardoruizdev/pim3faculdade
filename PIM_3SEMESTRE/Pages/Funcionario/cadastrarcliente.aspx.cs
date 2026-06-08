using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;
using System;
using System.Linq;

namespace PIM_3SEMESTRE.Pages.Funcionario
{
    public partial class cadastrarcliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCadastrar_Click(
     object sender,
     EventArgs e)
        {
            try
            {
                // =========================================
                // VALIDAÇÕES
                // =========================================

                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    Alerta("Digite o nome do cliente.");
                    return;
                }

                string nome =
                    txtNome.Text.Trim();

                if (nome.Length < 3)
                {
                    Alerta(
                        "O nome deve ter no mínimo 3 caracteres."
                    );

                    return;
                }

                // Não permitir números no nome
                if (nome.Any(char.IsDigit))
                {
                    Alerta(
                        "O nome não pode conter números."
                    );

                    return;
                }

                // =========================================
                // EMAIL
                // =========================================

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    Alerta("Digite o e-mail.");
                    return;
                }

                string email =
                    txtEmail.Text.Trim();

                if (
                    !email.Contains("@") ||
                    !email.Contains(".")
                )
                {
                    Alerta(
                        "Digite um e-mail válido."
                    );

                    return;
                }

                // =========================================
                // CPF
                // =========================================

                if (string.IsNullOrWhiteSpace(txtCpf.Text))
                {
                    Alerta("Digite o CPF.");
                    return;
                }

                string cpf =
                    txtCpf.Text.Replace(".", "")
                               .Replace("-", "")
                               .Replace("/", "")
                               .Trim();

                if (cpf.Length != 11)
                {
                    Alerta("CPF inválido.");
                    return;
                }

                if (!long.TryParse(cpf, out _))
                {
                    Alerta(
                        "O CPF deve conter apenas números."
                    );

                    return;
                }

                // CPF repetido
                if (
                    cpf == "00000000000" ||
                    cpf == "11111111111" ||
                    cpf == "22222222222" ||
                    cpf == "33333333333" ||
                    cpf == "44444444444" ||
                    cpf == "55555555555" ||
                    cpf == "66666666666" ||
                    cpf == "77777777777" ||
                    cpf == "88888888888" ||
                    cpf == "99999999999"
                )
                {
                    Alerta("CPF inválido.");
                    return;
                }

                // =========================================
                // NASCIMENTO
                // =========================================

                if (string.IsNullOrWhiteSpace(txtNascimento.Text))
                {
                    Alerta(
                        "Digite a data de nascimento."
                    );

                    return;
                }

                DateTime nascimento;

                if (
                    !DateTime.TryParse(
                        txtNascimento.Text,
                        out nascimento
                    )
                )
                {
                    Alerta(
                        "Data de nascimento inválida."
                    );

                    return;
                }

                // Data futura
                if (nascimento > DateTime.Now)
                {
                    Alerta(
                        "A data de nascimento não pode ser futura."
                    );

                    return;
                }

                int idade =
                    DateTime.Now.Year -
                    nascimento.Year;

                if (
                    nascimento >
                    DateTime.Now.AddYears(-idade)
                )
                {
                    idade--;
                }

                if (idade < 18)
                {
                    Alerta(
                        "O cliente deve ser maior de idade."
                    );

                    return;
                }

                // =========================================
                // TELEFONE
                // =========================================

                if (string.IsNullOrWhiteSpace(txtTelefone.Text))
                {
                    Alerta("Digite o telefone.");
                    return;
                }

                string telefone =
                    txtTelefone.Text.Replace("(", "")
                                    .Replace(")", "")
                                    .Replace("-", "")
                                    .Replace(" ", "")
                                    .Trim();

                if (
                    telefone.Length < 10 ||
                    telefone.Length > 11
                )
                {
                    Alerta("Telefone inválido.");
                    return;
                }

                if (!long.TryParse(telefone, out _))
                {
                    Alerta(
                        "O telefone deve conter apenas números."
                    );

                    return;
                }

                // =========================================
                // CEP
                // =========================================

                if (string.IsNullOrWhiteSpace(txtCep.Text))
                {
                    Alerta("Digite o CEP.");
                    return;
                }

                string cep =
                    txtCep.Text.Replace("-", "")
                               .Trim();

                if (cep.Length != 8)
                {
                    Alerta("CEP inválido.");
                    return;
                }

                if (!int.TryParse(cep, out _))
                {
                    Alerta(
                        "O CEP deve conter apenas números."
                    );

                    return;
                }

                // =========================================
                // RUA
                // =========================================

                if (string.IsNullOrWhiteSpace(txtRua.Text))
                {
                    Alerta("Digite a rua.");
                    return;
                }

                // =========================================
                // NÚMERO
                // =========================================

                if (string.IsNullOrWhiteSpace(txtNumero.Text))
                {
                    Alerta(
                        "Digite o número da residência."
                    );

                    return;
                }

                int numero;

                if (
                    !int.TryParse(
                        txtNumero.Text,
                        out numero
                    )
                )
                {
                    Alerta(
                        "Número da residência inválido."
                    );

                    return;
                }

                // Número negativo
                if (numero <= 0)
                {
                    Alerta(
                        "O número da residência deve ser maior que zero."
                    );

                    return;
                }

                // =========================================
                // BAIRRO
                // =========================================

                if (string.IsNullOrWhiteSpace(txtBairro.Text))
                {
                    Alerta("Digite o bairro.");
                    return;
                }

                // =========================================
                // CIDADE
                // =========================================

                if (string.IsNullOrWhiteSpace(txtCidade.Text))
                {
                    Alerta("Digite a cidade.");
                    return;
                }

                // =========================================
                // ESTADO
                // =========================================

                if (ddlEstado.SelectedIndex == 0)
                {
                    Alerta("Selecione o estado.");
                    return;
                }

                // =========================================
                // OBJETO
                // =========================================

                ClienteModel cliente =
                    new ClienteModel();

                cliente.NomeUsuario =
                    nome;

                cliente.EmailUsuario =
                    email;

                cliente.SenhaUsuario =
                    "123456";

                cliente.CpfCliente =
                    cpf;

                cliente.DataNascimentoCliente =
                    nascimento;

                cliente.TelefoneCliente =
                    telefone;

                cliente.CepCliente =
                    cep;

                cliente.RuaCliente =
                    txtRua.Text.Trim();

                cliente.NumeroResidenciaCliente =
                    numero;

                cliente.ComplementoResidenciaCliente =
                    txtComplemento.Text.Trim();

                cliente.BairroCliente =
                    txtBairro.Text.Trim();

                cliente.CidadeCliente =
                    txtCidade.Text.Trim();

                cliente.EstadoResidenciaCliente =
                    ddlEstado.SelectedItem.Text;

                cliente.ObservacaoCliente =
                    txtObservacao.Text.Trim();

                // =========================================
                // CADASTRAR
                // =========================================

                ClienteController controller =
                    new ClienteController();

                controller.CadastrarCliente(cliente);

                Logger.Log(
                    "CADASTRO_CLIENTE",
                    $"Cliente cadastrado | Nome: {txtNome.Text} | CPF: {cpf} | Cidade: {txtCidade.Text}"
                );

                Alerta(
                    "Cliente cadastrado com sucesso!"
                );

                LimparCampos();
            }
            catch (Exception ex)
            {
                string erro =
                    ex.Message.ToLower();

                // =====================================
                // EMAIL DUPLICADO
                // =====================================

                if (
                    erro.Contains("email") ||
                    erro.Contains("nm_email_usuario")
                )
                {
                    Logger.Log(
                        "EMAIL_CLIENTE_DUPLICADO",
                        $"Tentativa de cadastro com email já existente | Email: {txtEmail.Text}"
                    );

                    Alerta(
                        "Este e-mail já está cadastrado."
                    );

                    return;
                }

                // =====================================
                // CPF DUPLICADO
                // =====================================

                if (
                    erro.Contains("cpf") ||
                    erro.Contains("cd_cpf_cliente")
                )
                {
                    Logger.Log(
                        "CPF_CLIENTE_DUPLICADO",
                        $"Tentativa de cadastro com CPF já existente | CPF: {txtCpf.Text}"
                    );

                    Alerta(
                        "Este CPF já está cadastrado."
                    );

                    return;
                }

                // =====================================
                // ERRO PADRÃO
                // =====================================

                Logger.Log(
                    "ERRO_CADASTRO_CLIENTE",
                    $"Erro ao cadastrar cliente | Erro: {ex.Message}"
                );

                Alerta(
                    "Erro: " +
                    ex.Message.Replace("'", "")
                );
            }
        }
        // =========================================
        // LIMPAR CAMPOS
        // =========================================

        private void LimparCampos()
        {
            txtNome.Text = "";

            txtEmail.Text = "";

            txtCpf.Text = "";

            txtNascimento.Text = "";

            txtTelefone.Text = "";

            txtCep.Text = "";

            txtRua.Text = "";

            txtNumero.Text = "";

            txtComplemento.Text = "";

            txtBairro.Text = "";

            txtCidade.Text = "";

            ddlEstado.SelectedIndex = 0;

            txtObservacao.Text = "";
        }

        // =========================================
        // ALERTA
        // =========================================

        private void Alerta(string mensagem)
        {
            Response.Write(
                "<script>alert('" +
                mensagem.Replace("'", "") +
                "');</script>"
            );
        }
    }
}
