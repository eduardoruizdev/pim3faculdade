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
    }
}