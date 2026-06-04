<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="relatorio.aspx.cs"
Inherits="PIM_3SEMESTRE.Pages.ADM.relatorio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

<meta http-equiv="Content-Type"
content="text/html; charset=utf-8"/>

<title>Relatórios</title>

<link href="../../css/Adm/relatorio.css"
rel="stylesheet"/>

<link rel="stylesheet"
href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css"/>

<link href=
"https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap"
rel="stylesheet"/>

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

          <a href="cadastrarfuncionario.aspx" class="active">
              <i class="fa-solid fa-user-plus"></i>
              Cadastrar Funcionário
          </a>

          <a href="cadastrarServico.aspx">
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

          <button type="button" class="help-btn">
              <i class="fa-solid fa-headset"></i>
              Falar com a oficina
          </button>

      </div>

      <a href="../Login/login.aspx" class="logout">
          <i class="fa-solid fa-arrow-right-from-bracket"></i>
          Sair da conta
      </a>

  </aside>


<main class="content">

<div class="topo">

<h1>Dashboard da Oficina</h1>

</div>

<!-- CARDS -->

<div class="cards">

<div class="card">

<h2>Total Clientes</h2>

<asp:Label
ID="lblClientes"
runat="server"
CssClass="numero"/>

</div>

<div class="card">

<h2>Total Mecânicos</h2>

<asp:Label
ID="lblMecanicos"
runat="server"
CssClass="numero"/>

</div>

<div class="card">

<h2>Total Serviços</h2>

<asp:Label
ID="lblServicos"
runat="server"
CssClass="numero"/>

</div>

<div class="card">

<h2>Valor Total</h2>

<asp:Label
ID="lblValor"
runat="server"
CssClass="numero"/>

</div>

</div>

<!-- TABELA -->

<div class="tabela-box">

<h2>Últimos Serviços</h2>

<asp:GridView
ID="gvServicos"
runat="server"
AutoGenerateColumns="False"
CssClass="tabela">

<Columns>

<asp:BoundField
DataField="id_servico"
HeaderText="ID"/>

<asp:BoundField
DataField="nm_tipo_servico"
HeaderText="Tipo"/>

<asp:BoundField
DataField="cliente"
HeaderText="Cliente"/>

<asp:BoundField
DataField="mecanico"
HeaderText="Mecânico"/>

<asp:BoundField
DataField="st_servico"
HeaderText="Status"/>

<asp:BoundField
DataField="vl_servico"
HeaderText="Valor"/>

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

