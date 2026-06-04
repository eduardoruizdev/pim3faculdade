using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using PIM_3SEMESTRE.Models;
using System;

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
                // Verifica se o nome do serviço foi informado
                if (string.IsNullOrWhiteSpace(
                    txtTipoServico.Text))
                {
                    ExibirMensagem(
                    "Digite o nome do serviço.");

                    return;
                }

                // Verifica se o serviço já existe no banco
                bool existe =
                controller.VerificarTipoServicoExiste(
                    txtTipoServico.Text
                );

                if (existe)
                {
                    ExibirMensagem(
                    "Esse tipo de serviço já existe.");

                    return;
                }

                // Cria o objeto do tipo de serviço
                TipoServicoModel servico =
                new TipoServicoModel();

                // Atribui o nome informado pelo usuário
                servico.NomeTipoServico =
                txtTipoServico.Text;

                // Realiza o cadastro no banco de dados
                controller.CadastrarTipoServico(
                    servico
                );

                // Limpa o campo após o cadastro
                txtTipoServico.Text = "";

                // Atualiza a GridView
                CarregarServicos();

                // Exibe mensagem de sucesso
                ExibirMensagem(
                "Tipo de serviço cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                // Exibe mensagem caso ocorra erro
                ExibirMensagem(
                "Erro: " + ex.Message);
            }
        }

        /// <summary>
        /// Carrega todos os tipos de serviços cadastrados
        /// e os exibe na GridView.
        /// </summary>
        private void CarregarServicos()
        {
            // Busca os registros no banco
            MySqlDataReader dados =
            controller.ListarTiposServico();

            // Define a fonte de dados da GridView
            gvServicos.DataSource = dados;

            // Atualiza a exibição dos dados
            gvServicos.DataBind();
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
                // Obtém o ID do serviço selecionado
                int id =
                Convert.ToInt32(
                    gvServicos.DataKeys[e.RowIndex].Value
                );

                // Executa a exclusão
                controller.ExcluirTipoServico(id);

                // Atualiza a GridView
                CarregarServicos();

                // Exibe mensagem de sucesso
                ExibirMensagem(
                "Serviço excluído com sucesso!");
            }
            catch (Exception ex)
            {
                // Exibe mensagem caso ocorra erro
                ExibirMensagem(
                "Erro: " + ex.Message);
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