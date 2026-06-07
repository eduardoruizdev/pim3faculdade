using MySql.Data.MySqlClient;
using PIM_3SEMESTRE.Controllers;
using System;

namespace PIM_3SEMESTRE.Pages.Cliente
{
    public partial class historicocliente :
        System.Web.UI.Page
    {
        ControllerServico controller =
            new ControllerServico();

        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarHistorico();
            }
        }

        // =====================================
        // CARREGAR HISTÓRICO
        // =====================================

        private void CarregarHistorico()
        {
            try
            {
                if (Session["id_usuario"] == null)
                {
                    Response.Redirect(
                        "~/Pages/Login/login.aspx"
                    );

                    return;
                }

                int idUsuario =
                    Convert.ToInt32(
                        Session["id_usuario"]
                    );

                MySqlDataReader dados =
                    controller.ListarServicosCliente(
                        idUsuario
                    );

                rptHistorico.DataSource = dados;
                rptHistorico.DataBind();

                dados.Close();
            }
            catch (Exception ex)
            {
                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }

        // =====================================
        // FILTRAR HISTÓRICO
        // =====================================

        private void FiltrarHistorico(
            string status)
        {
            try
            {
                if (Session["id_usuario"] == null)
                {
                    Response.Redirect(
                        "~/Pages/Login/login.aspx"
                    );

                    return;
                }

                int idUsuario =
                    Convert.ToInt32(
                        Session["id_usuario"]
                    );

                MySqlDataReader dados =
                    controller.ListarServicosClientePorStatus(
                        idUsuario,
                        status
                    );

                rptHistorico.DataSource = dados;
                rptHistorico.DataBind();

                dados.Close();
            }
            catch (Exception ex)
            {
                Response.Write(
                    "<script>alert('" +
                    ex.Message.Replace("'", "") +
                    "')</script>"
                );
            }
        }

        // =====================================
        // BOTÃO TODOS
        // =====================================

        protected void btnTodos_Click(
            object sender,
            EventArgs e)
        {
            CarregarHistorico();
        }

        // =====================================
        // BOTÃO EM ANDAMENTO
        // =====================================

        protected void btnAndamento_Click(
            object sender,
            EventArgs e)
        {
            FiltrarHistorico(
                "Em andamento"
            );
        }

        // =====================================
        // BOTÃO CONCLUÍDOS
        // =====================================

        protected void btnConcluidos_Click(
            object sender,
            EventArgs e)
        {
            FiltrarHistorico(
                "Concluído"
            );
        }

        // =====================================
        // BOTÃO CANCELADOS
        // =====================================

        protected void btnCancelados_Click(
            object sender,
            EventArgs e)
        {
            FiltrarHistorico(
                "Cancelado"
            );
        }
    }
}
