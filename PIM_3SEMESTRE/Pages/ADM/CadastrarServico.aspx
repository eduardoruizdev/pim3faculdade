<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="CadastrarServico.aspx.cs"
Inherits="PIM_3SEMESTRE.Pages.ADM.CadastrarServico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta http-equiv="Content-Type"
        content="text/html; charset=utf-8"/>

    <title>Cadastrar Tipo de Serviço</title>

    <link href="../../css/Adm/cadastrarServico.css"
        rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="preconnect"
        href="https://fonts.googleapis.com"/>

    <link rel="preconnect"
        href="https://fonts.gstatic.com"
        crossorigin="anonymous"/>

    <link href=
    "https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap"
    rel="stylesheet"/>

    <link rel="stylesheet"
    href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css"/>

</head>

<body>

<form id="form1" runat="server">

  <div class="container">

      <!-- SIDEBAR -->
      <aside class="sidebar">

          <div class="logo">
              <img src="../../img/logoempresa.png"/>
          </div>

          <div class="menu-title">
              ADMINISTRADOR
          </div>

          <div class="menu">

              <a href="cadastrarfuncionario.aspx" >
                  <i class="fa-solid fa-user-plus"></i>
                  Cadastrar Funcionário
              </a>

              <a href="cadastrarServico.aspx" class="active">
                  <i class="fa-solid fa-users"></i>
                  Cadastrar Tipo Serviço
              </a>

              <a href="relatorio.aspx">
                  <i class="fa-solid fa-screwdriver-wrench"></i>
                  Relatorio
              </a>

          </div>

                  <div class="help-box">

            <h3>Precisa de ajuda?</h3>

            <p>
                Nossa equipe está pronta
                para te atender.
            </p>

          
<a href="mailto:autotechoficina@gmail.com"
   class="help-btn">

    <i class="fa-solid fa-headset"></i>
    Falar com a oficina

</a>


        </div>

          <a href="../Login/login.aspx" class="logout">
              <i class="fa-solid fa-arrow-right-from-bracket"></i>
              Sair da conta
          </a>

      </aside>

    <!-- CONTEUDO -->
    <main class="content">

        <div class="topbar">

            <div class="profile">

                <span>Administrador</span>

                <img src="../../img/boneco.png"/>

            </div>

        </div>

        <!-- CARD -->
        <div class="form-container">

            <div class="form-header">

                <h1>
                    Cadastrar Tipo de Serviço
                </h1>

                <p>
                    Cadastre novos tipos de
                    serviços disponíveis na oficina.
                </p>

            </div>

            <div class="input-box">

                <label>
                    Nome do Serviço
                </label>

                <asp:TextBox
                    ID="txtTipoServico"
                    runat="server"
                    CssClass="input-field"
                    placeholder="Ex: Troca de óleo">
                </asp:TextBox>

            </div>

            <div class="btn-area">

                <asp:Button
                    ID="btnCadastrar"
                    runat="server"
                    Text="Cadastrar Serviço"
                    CssClass="btn-cadastrar"
                    OnClick="btnCadastrar_Click"/>

            </div>

        </div>
        <!-- LISTAGEM -->

<div class="table-container">

    <div class="table-header">

        <h2>Tipos de Serviços Cadastrados</h2>

    </div>

    <asp:GridView
        ID="gvServicos"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table"
        DataKeyNames="id_tipo_servico"
        OnRowDeleting="gvServicos_RowDeleting">

        <Columns>

            <asp:BoundField
                DataField="id_tipo_servico"
                HeaderText="ID" />

            <asp:BoundField
                DataField="nm_tipo_servico"
                HeaderText="Nome do Serviço" />

            <asp:CommandField
                ShowDeleteButton="true"
                DeleteText="Excluir" />

        </Columns>

    </asp:GridView>

</div>
    </main>

</div>


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
