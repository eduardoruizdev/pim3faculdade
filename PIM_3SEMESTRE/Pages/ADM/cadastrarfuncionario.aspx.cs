using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PIM_3SEMESTRE.Pages.ADM
{
    public partial class cadastrarfuncionario :
        System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarFuncionarios();
            }
        }

        // =====================================
        // CADASTRAR
        // =====================================

        protected void btnCadastrar_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                // VALIDAÇÕES

                if (string.IsNullOrWhiteSpace(
                    txtNome.Text))
                {
                    ExibirMensagem(
                        "Digite o nome."
                    );

                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    txtEmail.Text))
                {
                    ExibirMensagem(
                        "Digite o email."
                    );

                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    txtSenha.Text))
                {
                    ExibirMensagem(
                        "Digite a senha."
                    );

                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    ddlTipoUsuario.SelectedValue))
                {
                    ExibirMensagem(
                        "Selecione o tipo de usuário."
                    );

                    return;
                }

                UsuarioController usuarioController =
                    new UsuarioController();

                // VERIFICA EMAIL

                bool emailExiste =
                    usuarioController.VerificarEmailExiste(
                        txtEmail.Text
                    );

                if (emailExiste)
                {
                    ExibirMensagem(
                        "Este email já está cadastrado."
                    );

                    return;
                }

                // CRIA USUÁRIO

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

                int idUsuario =
                    usuarioController.CadastrarUsuario(
                        usuario
                    );

                // CADASTRO MECÂNICO

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

                // RECARREGA GRID

                CarregarFuncionarios();

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

        // =====================================
        // LISTAR FUNCIONÁRIOS
        // =====================================

        private void CarregarFuncionarios()
        {
            try
            {
                UsuarioController controller =
                    new UsuarioController();

                gvFuncionarios.DataSource =
                    controller.ListarFuncionarios();

                gvFuncionarios.DataBind();
            }
            catch (Exception ex)
            {
                ExibirMensagem(
                    "Erro ao carregar funcionários: "
                    + ex.Message
                );
            }
        }

        // =====================================
        // EXCLUIR
        // =====================================

        protected void gvFuncionarios_RowDeleting(
            object sender,
            GridViewDeleteEventArgs e)
        {
            try
            {
                int idUsuario =
                    Convert.ToInt32(
                        gvFuncionarios.DataKeys[
                            e.RowIndex
                        ].Value
                    );

                UsuarioController controller =
                    new UsuarioController();

                controller.ExcluirUsuario(
                    idUsuario
                );

                ExibirMensagem(
                    "Usuário excluído com sucesso!"
                );

                CarregarFuncionarios();
            }
            catch (Exception ex)
            {
                ExibirMensagem(
                    "Erro ao excluir usuário: "
                    + ex.Message
                );
            }
        }

        // =====================================
        // CONFIRMAÇÃO EXCLUIR
        // =====================================

        protected void gvFuncionarios_RowDataBound(
            object sender,
            GridViewRowEventArgs e)
        {
            if (e.Row.RowType ==
                DataControlRowType.DataRow)
            {
                LinkButton btnExcluir =
                    (LinkButton)e.Row.Cells[4]
                    .Controls[0];

                btnExcluir.OnClientClick =
                    "return confirm('Deseja realmente excluir este usuário?');";
            }
        }

        // =====================================
        // ALERTA
        // =====================================

        private void ExibirMensagem(
            string mensagem)
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
