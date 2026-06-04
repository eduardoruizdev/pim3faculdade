using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;
using System;
using System.Web.UI;

namespace PIM_3SEMESTRE.Pages.ADM
{
    public partial class cadastrarfuncionario : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {

                // VALIDAÇÕES

                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    ExibirMensagem("Digite o nome.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    ExibirMensagem("Digite o email.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSenha.Text))
                {
                    ExibirMensagem("Digite a senha.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    ddlTipoUsuario.SelectedValue))
                {
                    ExibirMensagem("Selecione o tipo de usuário.");
                    return;
                }

                // CONTROLLER

                UsuarioController usuarioController =
                new UsuarioController();

                // VERIFICA EMAIL

                bool emailExiste =
                usuarioController.VerificarEmailExiste(
                    txtEmail.Text
                );

                if (emailExiste)
                {
                    ExibirMensagem("Este email já está cadastrado.");
                    return;
                }

                // MODEL USUARIO

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

                // CADASTRA USUARIO

                int idUsuario =
                usuarioController.CadastrarUsuario(
                    usuario
                );

                // SE FOR MECANICO

                if (ddlTipoUsuario.SelectedValue == "3")
                {

                    MecanicoModel mecanico =
                    new MecanicoModel();

                    mecanico.IdUsuario =
                    idUsuario;

                    mecanico.EspecialidadeMecanico =
                    txtEspecialidade.Text;

                    mecanico.ObservacaoMecanico =
                    txtObservacao.Text;

                    MecanicoController mecanicoController =
                    new MecanicoController();

                    mecanicoController.CadastrarMecanico(
                        mecanico
                    );
                }

                // LIMPAR CAMPOS

                txtNome.Text = "";
                txtEmail.Text = "";
                txtSenha.Text = "";
                txtEspecialidade.Text = "";
                txtObservacao.Text = "";
                ddlTipoUsuario.SelectedIndex = 0;

                // SUCESSO

                ExibirMensagem(
                    "Usuário cadastrado com sucesso!"
                );

            }
            catch (Exception ex)
            {
                ExibirMensagem(
                    "Erro: " + ex.Message
                );
            }
        }

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
