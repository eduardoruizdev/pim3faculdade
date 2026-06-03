using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;
using System;

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

                if (txtNome.Text.Trim().Length < 3)
                {
                    Alerta(
                        "O nome deve ter no mínimo 3 caracteres."
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

                if (!email.Contains("@") ||
                    !email.Contains("."))
                {
                    Alerta("Digite um e-mail válido.");
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

                if (!DateTime.TryParse(
                    txtNascimento.Text,
                    out nascimento))
                {
                    Alerta(
                        "Data de nascimento inválida."
                    );

                    return;
                }

                int idade =
                    DateTime.Now.Year -
                    nascimento.Year;

                if (nascimento >
                    DateTime.Now.AddYears(-idade))
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

                if (telefone.Length < 10)
                {
                    Alerta("Telefone inválido.");
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

                if (!int.TryParse(
                    txtNumero.Text,
                    out numero))
                {
                    Alerta(
                        "Número da residência inválido."
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
                    txtNome.Text.Trim();

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

                if (erro.Contains("email") ||
                    erro.Contains("nm_email_usuario"))
                {
                    Alerta(
                        "Este e-mail já está cadastrado."
                    );

                    return;
                }

                // =====================================
                // CPF DUPLICADO
                // =====================================

                if (erro.Contains("cpf") ||
                    erro.Contains("cd_cpf_cliente"))
                {
                    Alerta(
                        "Este CPF já está cadastrado."
                    );

                    return;
                }

                // =====================================
                // ERRO PADRÃO
                // =====================================

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
