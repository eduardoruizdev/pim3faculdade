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
                string email = txtUsuario.Text.Trim();
                string senha = txtSenha.Text.Trim();

                // =========================
                // CAPTCHA
                // =========================
                string captcha = Request.Form["g-recaptcha-response"];

                if (string.IsNullOrEmpty(captcha))
                {
                    Logger.Log("LOGIN_CAPTCHA_EMPTY", $"Tentativa sem captcha | Email: {email}");

                    Response.Write("<script>alert('Confirme o captcha!');</script>");
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

                        JObject dadosCaptcha = JObject.Parse(json);

                        bool sucesso = (bool)dadosCaptcha["success"];

                        if (!sucesso)
                        {
                            Logger.Log("LOGIN_CAPTCHA_FAIL", $"Captcha inválido | Email: {email}");

                            Response.Write("<script>alert('Captcha inválido!');</script>");
                            return;
                        }
                    }
                }

                // =========================
                // LOGIN BANCO
                // =========================
                Banco bd = new Banco();

                List<MySqlParameter> parametros = new List<MySqlParameter>
                {
                    new MySqlParameter("p_email", email),
                    new MySqlParameter("p_senha", senha)
                };

                using (MySqlDataReader dados =
                    bd.Consultar("sp_validar_login", parametros))
                {
                    if (dados.Read())
                    {
                        Session["id_usuario"] = dados["id_usuario"].ToString();
                        Session["nome_usuario"] = dados["nm_usuario"].ToString();
                        Session["email_usuario"] = dados["nm_email_usuario"].ToString();
                        Session["tipo_usuario"] = dados["nm_tipo_usuario"].ToString();

                        string tipoUsuario = Session["tipo_usuario"].ToString();

                        Logger.Log("LOGIN_SUCCESS",
                            $"Login OK | ID: {Session["id_usuario"]} | Email: {email} | Tipo: {tipoUsuario}");

                        // =========================
                        // REDIRECIONAMENTO (CORRIGIDO)
                        // =========================

                        string redirectUrl = "";

                        if (tipoUsuario == "Administrador")
                            redirectUrl = "~/Pages/ADM/cadastrarfuncionario.aspx";
                        else if (tipoUsuario == "Funcionario")
                            redirectUrl = "~/Pages/Funcionario/cadastrarcliente.aspx";
                        else if (tipoUsuario == "Mecanico")
                            redirectUrl = "~/Pages/Mecanico/paginamecanico.aspx";
                        else if (tipoUsuario == "Cliente")
                            redirectUrl = "~/Pages/Cliente/historicocliente.aspx";

                        Response.Redirect(redirectUrl, false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                    else
                    {
                        Logger.Log("LOGIN_FAIL", $"Email ou senha inválidos | Email: {email}");

                        Response.Write("<script>alert('E-mail ou senha inválidos!');</script>");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                // evita log falso de ThreadAbortException
                if (ex is System.Threading.ThreadAbortException)
                    return;

                Logger.Log("LOGIN_ERROR",
                    $"Erro no login: {ex.Message} | Email: {txtUsuario.Text}");

                Response.Write(
                    "<script>alert('Erro: " + ex.Message.Replace("'", "") + "');</script>"
                );
            }
        }
    }
}