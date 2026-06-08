using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;
using System;
using System.Linq;
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
                // =========================
                // VALIDAÇÕES
                // =========================

                if (string.IsNullOrWhiteSpace(
                    txtNome.Text))
                {
                    ExibirMensagem(
                        "Digite o nome."
                    );

                    return;
                }

                string nome =
                    txtNome.Text.Trim();

                // Nome mínimo
                if (nome.Length < 3)
                {
                    ExibirMensagem(
                        "O nome deve possuir no mínimo 3 caracteres."
                    );

                    return;
                }

                // Nome com número
                if (nome.Any(char.IsDigit))
                {
                    ExibirMensagem(
                        "O nome não pode conter números."
                    );

                    return;
                }

                // =========================
                // EMAIL
                // =========================

                if (string.IsNullOrWhiteSpace(
                    txtEmail.Text))
                {
                    ExibirMensagem(
                        "Digite o email."
                    );

                    return;
                }

                string email =
                    txtEmail.Text.Trim();

                if (
                    !email.Contains("@") ||
                    !email.Contains(".")
                )
                {
                    ExibirMensagem(
                        "Digite um email válido."
                    );

                    return;
                }

                // =========================
                // SENHA
                // =========================

                if (string.IsNullOrWhiteSpace(
                    txtSenha.Text))
                {
                    ExibirMensagem(
                        "Digite a senha."
                    );

                    return;
                }

                string senha =
                    txtSenha.Text.Trim();

                // Senha mínima
                if (senha.Length < 6)
                {
                    ExibirMensagem(
                        "A senha deve ter no mínimo 6 caracteres."
                    );

                    return;
                }

                // =========================
                // TIPO USUÁRIO
                // =========================

                if (
                    ddlTipoUsuario.SelectedIndex == 0
                )
                {
                    ExibirMensagem(
                        "Selecione o tipo de usuário."
                    );

                    return;
                }

                // =========================
                // MECÂNICO
                // =========================

                // Tipo 3 = Mecânico
                if (ddlTipoUsuario.SelectedValue == "3")
                {
                    if (
                        string.IsNullOrWhiteSpace(
                            txtEspecialidade.Text
                        )
                    )
                    {
                        ExibirMensagem(
                            "Digite a especialidade do mecânico."
                        );

                        return;
                    }

                    if (
                        txtEspecialidade.Text
                            .Trim()
                            .Length < 3
                    )
                    {
                        ExibirMensagem(
                            "A especialidade deve possuir no mínimo 3 caracteres."
                        );

                        return;
                    }
                }

                UsuarioController usuarioController =
                    new UsuarioController();

                // =========================
                // VERIFICA EMAIL
                // =========================

                bool emailExiste =
                    usuarioController.VerificarEmailExiste(
                        email
                    );

                if (emailExiste)
                {
                    Logger.Log(
                        "EMAIL_DUPLICADO",
                        $"Tentativa de cadastro com email já existente | Email: {email}"
                    );

                    ExibirMensagem(
                        "Este email já está cadastrado."
                    );

                    return;
                }

                // =========================
                // CRIA USUÁRIO
                // =========================

                UsuarioModel usuario =
                    new UsuarioModel();

                usuario.NomeUsuario =
                    nome;

                usuario.EmailUsuario =
                    email;

                usuario.SenhaUsuario =
                    senha;

                usuario.IdTipoUsuario =
                    Convert.ToInt32(
                        ddlTipoUsuario.SelectedValue
                    );

                int idUsuario =
                    usuarioController.CadastrarUsuario(
                        usuario
                    );

                Logger.Log(
                    "CADASTRO_USUARIO",
                    $"Usuário cadastrado | ID: {idUsuario} | Nome: {nome} | Email: {email} | Tipo: {ddlTipoUsuario.SelectedItem.Text}"
                );

                // =========================
                // CADASTRO MECÂNICO
                // =========================

                if (ddlTipoUsuario.SelectedValue == "3")
                {
                    MecanicoModel mecanico =
                        new MecanicoModel();

                    mecanico.IdUsuario =
                        idUsuario;

                    mecanico.EspecialidadeMecanico =
                        txtEspecialidade.Text.Trim();

                    mecanico.ObservacaoMecanico =
                        txtObservacao.Text.Trim();

                    MecanicoController mecanicoController =
                        new MecanicoController();

                    mecanicoController.CadastrarMecanico(
                        mecanico
                    );

                    Logger.Log(
                        "CADASTRO_MECANICO",
                        $"Mecânico cadastrado | ID Usuário: {idUsuario} | Especialidade: {txtEspecialidade.Text}"
                    );
                }

                // =========================
                // LIMPAR CAMPOS
                // =========================

                txtNome.Text = "";

                txtEmail.Text = "";

                txtSenha.Text = "";

                txtEspecialidade.Text = "";

                txtObservacao.Text = "";

                ddlTipoUsuario.SelectedIndex = 0;

                // =========================
                // RECARREGA GRID
                // =========================

                CarregarFuncionarios();

                ExibirMensagem(
                    "Usuário cadastrado com sucesso!"
                );
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_CADASTRO_USUARIO",
                    $"Erro ao cadastrar usuário | Erro: {ex.Message}"
                );

                ExibirMensagem(
                    "Erro: " +
                    ex.Message.Replace("'", "")
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
                Logger.Log(
                    "ERRO_LISTAR_FUNCIONARIOS",
                    $"Erro ao carregar funcionários | Erro: {ex.Message}"
                );

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

                Logger.Log(
                    "EXCLUIR_USUARIO",
                    $"Tentativa de exclusão | ID Usuário: {idUsuario}"
                );

                UsuarioController controller =
                    new UsuarioController();

                controller.ExcluirUsuario(
                    idUsuario
                );

                Logger.Log(
                    "USUARIO_EXCLUIDO",
                    $"Usuário excluído com sucesso | ID Usuário: {idUsuario}"
                );

                ExibirMensagem(
                    "Usuário excluído com sucesso!"
                );

                CarregarFuncionarios();
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_EXCLUIR_USUARIO",
                    $"Erro ao excluir usuário | Erro: {ex.Message}"
                );

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
