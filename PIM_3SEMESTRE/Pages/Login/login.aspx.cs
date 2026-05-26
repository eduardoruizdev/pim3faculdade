using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.UI;

namespace PIM_3SEMESTRE.Pages.Login
{
    public partial class login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {

                string captcha = Request.Form["g-recaptcha-response"];

                if (string.IsNullOrEmpty(captcha))
                {
                    Response.Write(
                        "<script>alert('Confirme o captcha!');</script>"
                    );
                    return;
                }

                string secretKey = "6LdQROMsAAAAACpff4o5FYiDZxtcmwoVxUOzknXV";

                string url =
                    "https://www.google.com/recaptcha/api/siteverify?secret="
                    + secretKey +
                    "&response=" + captcha;

                HttpWebRequest request =
     (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentLength = 0;

                using (HttpWebResponse response =
                    (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader =
                        new StreamReader(response.GetResponseStream()))
                    {
                        string json = reader.ReadToEnd();

                        JObject dadosCaptcha =
                            JObject.Parse(json);

                        bool sucesso =
                            (bool)dadosCaptcha["success"];

                        if (!sucesso)
                        {
                            Response.Write(
                                "<script>alert('Captcha inválido!');</script>"
                            );
                            return;
                        }
                    }
                }


                string email = txtUsuario.Text.Trim();
                string senha = txtSenha.Text.Trim();

                Banco bd = new Banco();

                List<MySqlParameter> parametros =
                    new List<MySqlParameter>();

                parametros.Add(
                    new MySqlParameter("p_email", email)
                );

                parametros.Add(
                    new MySqlParameter("p_senha", senha)
                );

                using (MySqlDataReader dados =
                    bd.Consultar("sp_validar_login", parametros))
                {
                    if (dados.Read())
                    {
                        Session["id_usuario"] =
                            dados["id_usuario"].ToString();

                        Session["nome_usuario"] =
                            dados["nm_usuario"].ToString();

                        Session["email_usuario"] =
                            dados["nm_email_usuario"].ToString();

                        Session["tipo_usuario"] =
                            dados["nm_tipo_usuario"].ToString();

                        string tipoUsuario =
                            dados["nm_tipo_usuario"].ToString();

                        if (tipoUsuario == "Administrador")
                        {
                            Response.Redirect(
                                "~/Pages/Admin/dashboard.aspx"
                            );
                        }
                        else if (tipoUsuario == "Funcionario")
                        {
                            Response.Redirect(
                                "~/Pages/Funcionario/dashboard.aspx"
                            );
                        }
                        else if (tipoUsuario == "Mecanico")
                        {
                            Response.Redirect(
                                "~/Pages/Mecanico/dashboard.aspx"
                            );
                        }
                        else if (tipoUsuario == "Cliente")
                        {
                            Response.Redirect(
                                "~/Pages/Cliente/historicocliente.aspx"
                            );
                        }
                    }
                    else
                    {
                        Response.Write(
                            "<script>alert('E-mail ou senha inválidos!');</script>"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(
                    "<script>alert('Erro: " + ex.Message.Replace("'", "") + "');</script>"
                );
            }
        }
    }
}