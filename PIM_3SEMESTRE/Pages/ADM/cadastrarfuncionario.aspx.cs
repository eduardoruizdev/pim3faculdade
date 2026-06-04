using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;
using System;
using System.Web.UI;

namespace PIM_3SEMESTRE.Pages.ADM
{
    /// <summary>
    /// Página responsável pelo cadastro de funcionários do sistema.
    /// Permite cadastrar administradores, atendentes e mecânicos.
    /// </summary>
    public partial class cadastrarfuncionario : System.Web.UI.Page
    {
        /// <summary>
        /// Evento executado ao carregar a página.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Nenhuma ação necessária no carregamento da página
        }

        /// <summary>
        /// Evento disparado ao clicar no botão Cadastrar.
        /// Realiza validações e efetua o cadastro do funcionário.
        /// </summary>
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                #region Validações

                // Verifica se o campo nome foi preenchido
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    ExibirMensagem("Digite o nome.");
                    return;
                }

                // Verifica se o campo e-mail foi preenchido
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    ExibirMensagem("Digite o email.");
                    return;
                }

                // Verifica se o campo senha foi preenchido
                if (string.IsNullOrWhiteSpace(txtSenha.Text))
                {
                    ExibirMensagem("Digite a senha.");
                    return;
                }

                // Verifica se o tipo de usuário foi selecionado
                if (string.IsNullOrWhiteSpace(
                    ddlTipoUsuario.SelectedValue))
                {
                    ExibirMensagem(
                        "Selecione o tipo de usuário.");
                    return;
                }

                #endregion

                #region Controller de Usuário

                // Instancia o controller responsável pelos usuários
                UsuarioController usuarioController =
                new UsuarioController();

                #endregion

                #region Verificar E-mail

                // Verifica se já existe um usuário com o mesmo e-mail
                bool emailExiste =
                usuarioController.VerificarEmailExiste(
                    txtEmail.Text
                );

                if (emailExiste)
                {
                    ExibirMensagem(
                        "Este email já está cadastrado.");
                    return;
                }

                #endregion

                #region Criar Objeto Usuário

                // Cria objeto usuário para armazenar os dados informados
                UsuarioModel usuario =
                new UsuarioModel();

                usuario.NomeUsuario =
                txtNome.Text;

                usuario.EmailUsuario =
                txtEmail.Text;

                usuario.SenhaUsuario =
                txtSenha.Text;

                usuario.IdTipoUsuario =
                Convert.ToInt32(
                    ddlTipoUsuario.SelectedValue
                );

                #endregion

                #region Cadastrar Usuário

                // Salva o usuário no banco de dados
                int idUsuario =
                usuarioController.CadastrarUsuario(
                    usuario
                );

                #endregion

                #region Cadastro de Mecânico

                // Caso o tipo selecionado seja Mecânico (ID = 3)
                if (ddlTipoUsuario.SelectedValue == "3")
                {
                    // Cria objeto mecânico
                    MecanicoModel mecanico =
                    new MecanicoModel();

                    // Vincula o mecânico ao usuário recém-cadastrado
                    mecanico.IdUsuario =
                    idUsuario;

                    // Define a especialidade do mecânico
                    mecanico.EspecialidadeMecanico =
                    txtEspecialidade.Text;

                    // Define observações adicionais
                    mecanico.ObservacaoMecanico =
                    txtObservacao.Text;

                    // Instancia o controller de mecânicos
                    MecanicoController mecanicoController =
                    new MecanicoController();

                    // Realiza o cadastro do mecânico
                    mecanicoController.CadastrarMecanico(
                        mecanico
                    );
                }

                #endregion

                #region Limpar Campos

                // Limpa os campos do formulário após o cadastro
                txtNome.Text = "";
                txtEmail.Text = "";
                txtSenha.Text = "";
                txtEspecialidade.Text = "";
                txtObservacao.Text = "";
                ddlTipoUsuario.SelectedIndex = 0;

                #endregion

                #region Mensagem de Sucesso

                // Exibe mensagem de confirmação
                ExibirMensagem(
                    "Usuário cadastrado com sucesso!"
                );

                #endregion
            }
            catch (Exception ex)
            {
                // Exibe mensagem de erro caso ocorra alguma exceção
                ExibirMensagem(
                    "Erro: " + ex.Message
                );
            }
        }

        /// <summary>
        /// Exibe uma mensagem na tela utilizando JavaScript.
        /// </summary>
        /// <param name="mensagem">
        /// Texto da mensagem a ser exibida.
        /// </param>
        private void ExibirMensagem(string mensagem)
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "msg",
                $"alert('{mensagem}');",
                true
            );
        }
    }
}