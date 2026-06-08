using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;
using System;
using System.Linq;

namespace PIM_3SEMESTRE.Pages.ADM
{
    /// <summary>
    /// Página responsável pelo cadastro, listagem e exclusão
    /// dos tipos de serviços do sistema.
    /// </summary>
    public partial class CadastrarServico
        : System.Web.UI.Page
    {
        // Instância do controller responsável pelos tipos de serviço
        TipoServicoController controller =
            new TipoServicoController();

        /// <summary>
        /// Evento executado ao carregar a página.
        /// Carrega a lista de serviços apenas na primeira abertura.
        /// </summary>
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carrega os tipos de serviços cadastrados
                CarregarServicos();
            }
        }

        /// <summary>
        /// Evento acionado ao clicar no botão de cadastro.
        /// Realiza validações e salva um novo tipo de serviço.
        /// </summary>
        protected void btnCadastrar_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                // =========================
                // VALIDAÇÃO
                // =========================

                if (string.IsNullOrWhiteSpace(
                    txtTipoServico.Text))
                {
                    ExibirMensagem(
                        "Digite o nome do serviço."
                    );

                    return;
                }

                string nomeServico =
                    txtTipoServico.Text.Trim();

                // =========================
                // TAMANHO MÍNIMO
                // =========================

                if (nomeServico.Length < 3)
                {
                    ExibirMensagem(
                        "O nome do serviço deve possuir no mínimo 3 caracteres."
                    );

                    return;
                }

                // =========================
                // TAMANHO MÁXIMO
                // =========================

                if (nomeServico.Length > 100)
                {
                    ExibirMensagem(
                        "O nome do serviço é muito grande."
                    );

                    return;
                }

                // =========================
                // NÃO PERMITIR APENAS NÚMEROS
                // =========================

                bool apenasNumeros =
                    true;

                foreach (char c in nomeServico)
                {
                    if (!char.IsDigit(c) &&
                        c != ' ')
                    {
                        apenasNumeros = false;
                        break;
                    }
                }

                if (apenasNumeros)
                {
                    ExibirMensagem(
                        "O nome do serviço não pode conter apenas números."
                    );

                    return;
                }

                // =========================
                // NÃO PERMITIR CARACTERES ESTRANHOS
                // =========================

                string caracteresInvalidos =
                    "@#$%¨&*+=[]{}<>|\\/";

                foreach (char c in nomeServico)
                {
                    if (caracteresInvalidos.Contains(c))
                    {
                        ExibirMensagem(
                            "O nome do serviço contém caracteres inválidos."
                        );

                        return;
                    }
                }

                // =========================
                // VERIFICA SE JÁ EXISTE
                // =========================

                bool existe =
                    controller.VerificarTipoServicoExiste(
                        nomeServico
                    );

                if (existe)
                {
                    Logger.Log(
                        "SERVICO_DUPLICADO",
                        $"Tentativa de cadastrar serviço já existente | Serviço: {nomeServico}"
                    );

                    ExibirMensagem(
                        "Esse tipo de serviço já existe."
                    );

                    return;
                }

                // =========================
                // CRIA OBJETO
                // =========================

                TipoServicoModel servico =
                    new TipoServicoModel();

                servico.NomeTipoServico =
                    nomeServico;

                // =========================
                // CADASTRO
                // =========================

                controller.CadastrarTipoServico(
                    servico
                );

                Logger.Log(
                    "CADASTRO_SERVICO",
                    $"Tipo de serviço cadastrado | Serviço: {nomeServico}"
                );

                // =========================
                // LIMPA CAMPO
                // =========================

                txtTipoServico.Text = "";

                // =========================
                // ATUALIZA GRID
                // =========================

                CarregarServicos();

                // =========================
                // MENSAGEM SUCESSO
                // =========================

                ExibirMensagem(
                    "Tipo de serviço cadastrado com sucesso!"
                );
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_CADASTRO_SERVICO",
                    $"Erro ao cadastrar tipo de serviço | Erro: {ex.Message}"
                );

                ExibirMensagem(
                    "Erro: " +
                    ex.Message.Replace("'", "")
                );
            }
        }

        /// <summary>
        /// Carrega todos os tipos de serviços cadastrados
        /// e os exibe na GridView.
        /// </summary>
        private void CarregarServicos()
        {
            try
            {
                // Busca os registros no banco
                MySqlDataReader dados =
                    controller.ListarTiposServico();

                // Define a fonte de dados da GridView
                gvServicos.DataSource = dados;

                // Atualiza a exibição dos dados
                gvServicos.DataBind();
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_LISTAR_SERVICOS",
                    $"Erro ao carregar serviços | Erro: {ex.Message}"
                );

                ExibirMensagem(
                    "Erro ao carregar serviços: "
                    + ex.Message
                );
            }
        }

        /// <summary>
        /// Evento acionado ao excluir um registro da GridView.
        /// Remove o tipo de serviço selecionado.
        /// </summary>
        protected void gvServicos_RowDeleting(
            object sender,
            System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {
                // =========================
                // OBTÉM ID
                // =========================

                int id =
                    Convert.ToInt32(
                        gvServicos.DataKeys[e.RowIndex].Value
                    );

                Logger.Log(
                    "EXCLUIR_SERVICO",
                    $"Tentativa de exclusão de serviço | ID Serviço: {id}"
                );

                // =========================
                // EXCLUI
                // =========================

                controller.ExcluirTipoServico(id);

                Logger.Log(
                    "SERVICO_EXCLUIDO",
                    $"Serviço excluído com sucesso | ID Serviço: {id}"
                );

                // =========================
                // ATUALIZA GRID
                // =========================

                CarregarServicos();

                // =========================
                // SUCESSO
                // =========================

                ExibirMensagem(
                    "Serviço excluído com sucesso!"
                );
            }
            catch (Exception ex)
            {
                Logger.Log(
                    "ERRO_EXCLUIR_SERVICO",
                    $"Erro ao excluir serviço | Erro: {ex.Message}"
                );

                ExibirMensagem(
                    "Erro: " + ex.Message
                );
            }
        }

        /// <summary>
        /// Exibe mensagens para o usuário utilizando JavaScript.
        /// </summary>
        /// <param name="mensagem">
        /// Texto que será exibido na tela.
        /// </param>
        private void ExibirMensagem(
            string mensagem)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "msg",
                $"alert('{mensagem}');",
                true
            );
        }
    }
}
