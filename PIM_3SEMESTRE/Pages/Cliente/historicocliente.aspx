<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="historicocliente.aspx.cs" Inherits="PIM_3SEMESTRE.Pages.Cliente.historicocliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0"/>

<title>Histórico de Serviços</title>

<link rel="stylesheet" href="../../css/cliente/historicocliente.css"/>
<link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'/>

</head>

<body class="light-theme">

<form id="form1" runat="server">

<div class="container">


    <aside class="sidebar">

     <div class="logo">
         <img src="../../img/logoempresa.png"/>
     </div>

     <div class="menu-title">
         CLIENTE
     </div>

     <div class="menu">

         <a href="cadastrarfuncionario.aspx" class="active">
             <i class="fa-solid fa-user-plus"></i>
             Historico
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

    <!-- MAIN -->
    <main class="main-content">

        <!-- TOPBAR -->
        <div class="topbar"></div>

        <!-- HERO -->
        <section class="hero">
            <div class="hero-overlay"></div>

            <div class="hero-content">
                <h1>Histórico de Serviços</h1>
                <p>Acompanhe todos os serviços realizados no seu veículo.</p>
            </div>
        </section>

        <!-- FILTROS -->
 <div class="filters">

    <asp:LinkButton
        ID="btnTodos"
        runat="server"
        CssClass="filter-btn active-filter"
        OnClick="btnTodos_Click">
        Todos
    </asp:LinkButton>

    <asp:LinkButton
        ID="btnAndamento"
        runat="server"
        CssClass="filter-btn"
        OnClick="btnAndamento_Click">
        Em andamento
    </asp:LinkButton>

    <asp:LinkButton
        ID="btnConcluidos"
        runat="server"
        CssClass="filter-btn"
        OnClick="btnConcluidos_Click">
        Concluídos
    </asp:LinkButton>

    <asp:LinkButton
        ID="btnCancelados"
        runat="server"
        CssClass="filter-btn"
        OnClick="btnCancelados_Click">
        Cancelados
    </asp:LinkButton>

</div>

        <!-- ========================= -->
        <!-- REPEATER DINÂMICO -->
        <!-- ========================= -->

        <asp:Repeater ID="rptHistorico" runat="server">

            <ItemTemplate>

                <section class="services">

                    <div class="service-card">

                        <div class="service-left">

                            <div class="service-icon andamento-icon">
                                <i class='bx bx-wrench'></i>
                            </div>

                            <div class="service-info">

                                <!-- TÍTULO -->
                                <h2>
                                    <%# Eval("nm_titulo_servico") %>
                                </h2>

                                <!-- OS + STATUS -->
                                <div class="service-status-line">

                                    <span class="os-number">
                                        OS #<%# Eval("id_servico") %>
                                    </span>

                                    <span class="dot"></span>

                                    <span class="status">
                                        <%# Eval("st_servico") %>
                                    </span>

                                </div>

                                <!-- ENTRADA -->
                                <div class="service-date">
                                    <i class='bx bx-calendar'></i>
                                    Entrada: <%# Eval("dt_cadastro_servico") %>
                                </div>

                                <!-- PREVISÃO -->
                                <div class="service-date">
                                    <i class='bx bx-message-rounded'></i>
                                    Previsão: <%# Eval("dt_prevista_entrega_servico") %>
                                </div>

                                <!-- VEÍCULO -->
                                <div class="service-date">
                                    Veículo: <%# Eval("nm_modelo_veiculo_servico") %>
                                    • <%# Eval("cd_placa_veiculo_servico") %>
                                </div>

                                <!-- MECÂNICO -->
                                <div class="service-date">
                                    Mecânico: <%# Eval("nm_mecanico") %>
                                </div>

                               <!-- TIPO DE SERVIÇO -->
<div class="service-date">
    Serviço: <%# Eval("nm_tipo_servico") %>
</div>

                                <!-- 💰 VALOR -->
                                <div class="service-date">
                                    Valor: R$ <%# Eval("vl_servico") %>
                                </div>

                            </div>

                        </div>

                    </div>

                </section>

            </ItemTemplate>

        </asp:Repeater>

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