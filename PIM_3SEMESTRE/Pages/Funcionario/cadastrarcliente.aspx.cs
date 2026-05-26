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

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteModel cliente = new ClienteModel();

                cliente.NomeUsuario = txtNome.Text.Trim();

                cliente.EmailUsuario = txtEmail.Text.Trim();

                cliente.SenhaUsuario = "123456";

                cliente.CpfCliente = txtCpf.Text.Trim();

                if (!string.IsNullOrEmpty(txtNascimento.Text))
                {
                    cliente.DataNascimentoCliente =
                        Convert.ToDateTime(txtNascimento.Text);
                }

                cliente.TelefoneCliente = txtTelefone.Text.Trim();

                cliente.CepCliente = txtCep.Text.Trim();

                cliente.RuaCliente = txtRua.Text.Trim();

                if (!string.IsNullOrEmpty(txtNumero.Text))
                {
                    cliente.NumeroResidenciaCliente =
                        Convert.ToInt32(txtNumero.Text);
                }

                cliente.ComplementoResidenciaCliente =
                    txtComplemento.Text.Trim();

                cliente.BairroCliente = txtBairro.Text.Trim();

                cliente.CidadeCliente = txtCidade.Text.Trim();

                cliente.EstadoResidenciaCliente =
                    ddlEstado.SelectedItem.Text;

                cliente.ObservacaoCliente =
                    txtObservacao.Text.Trim();

                ClienteController controller =
                    new ClienteController();

                controller.CadastrarCliente(cliente);

                Response.Write(
                    "<script>alert('Cliente cadastrado com sucesso!');</script>"
                );

                LimparCampos();
            }
            catch (Exception ex)
            {
                Response.Write(
                    "<script>alert('Erro: " + ex.Message.Replace("'", "") + "');</script>"
                );
            }
        }

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
    }
}