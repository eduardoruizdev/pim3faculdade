<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="PIM_3SEMESTRE.Pages.Login.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

<link rel="stylesheet" href="../../css/login/login.css"/>

<link rel="preconnect" href="https://fonts.googleapis.com"/>
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="anonymous"/>
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700;800&display=swap" rel="stylesheet"/>

<link rel="stylesheet"
href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css"/>

<title>AutoTech - Login</title>
</head>

<body>

<form id="form1" runat="server">

    <section class="container">

        <!-- lado esquerdo -->
        <div class="left-side">

            <div class="overlay"></div>

            <div class="logo-box">
                <h1>AUTO <span>TECH</span></h1>
                <p>TECNOLOGIA • CONFIANÇA • PERFORMANCE</p>
            </div>

            <img src="../../img/carro-login.png" class="car-image"/>

            <div class="features">
                <div class="feature">
                    <i class="fa-solid fa-shield-halved"></i>
                    <h4>SEGURANÇA</h4>
                    <p>Proteção total dos seus dados</p>
                </div>

                <div class="feature">
                    <i class="fa-solid fa-chart-line"></i>
                    <h4>GESTÃO</h4>
                    <p>Controle completo da oficina</p>
                </div>

                <div class="feature">
                    <i class="fa-solid fa-wrench"></i>
                    <h4>PERFORMANCE</h4>
                    <p>Mais eficiência no negócio</p>
                </div>
            </div>

        </div>


        <!-- lado direito -->
        <div class="right-side">

            <div class="login-card">

                <div class="logo-mini">
                    <h2>AUTO <span>TECH</span></h2>
                    <p>Sistema de Gestão para Oficinas</p>
                </div>

                <h1>Bem-vindo de <span>volta!</span></h1>
                <p class="subtitle">
                    Entre com suas credenciais para acessar o sistema.
                </p>

                <!-- email -->
                <div class="input-group">
                    <label>E-mail ou usuário</label>
                    <div class="input-box">
                        <i class="fa-regular fa-user"></i>
                        <asp:TextBox ID="txtUsuario"
                            runat="server"
                            placeholder="Digite seu e-mail ou usuário">
                        </asp:TextBox>
                    </div>
                </div>

                <!-- senha -->
                <div class="input-group">
                    <label>Senha</label>
                    <div class="input-box">
                        <i class="fa-solid fa-lock"></i>

                        <asp:TextBox
                            ID="txtSenha"
                            runat="server"
                            TextMode="Password"
                            placeholder="Digite sua senha">
                        </asp:TextBox>

                        <i class="fa-regular fa-eye eye"></i>
                    </div>
                </div>

                <div class="remember">
                    <asp:CheckBox ID="chkLembrar" runat="server"/>
                    <span>Lembrar de mim neste dispositivo</span>
                </div>

             <div class="captcha-box">

 <div class="captcha-title">
        <i class="fa-solid fa-shield"></i>
        Autenticação de dois fatores (2FA)
    </div>

    <p>
        Para sua segurança, confirme que você não é um robô.
    </p>

    <div class="captcha-real">
        <div class="g-recaptcha"
             data-sitekey="6LdQROMsAAAAAFB3zCPaoBOkHXjReFnrYBNnDplJ">
        </div>
    </div>

</div><asp:Button
    ID="btnEntrar"
    runat="server"
    Text="Entrar"
    CssClass="btn-login"
    OnClick="btnEntrar_Click"/>

                <a href="#" class="forgot">
                    <i class="fa-solid fa-lock"></i>
                    Esqueceu sua senha?
                </a>

            </div>

        </div>

    </section>

</form>
    <!-- VLibras -->
<div vw class="enabled">
    <div vw-access-button class="active"></div>

    <div vw-plugin-wrapper>
        <div class="vw-plugin-top-wrapper"></div>
    </div>
</div>

<script src="https://vlibras.gov.br/app/vlibras-plugin.js"></script>

<script>
    new window.VLibras.Widget('https://vlibras.gov.br/app');
</script>
</body>
</html>
</html>