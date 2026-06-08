<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paginamecanico.aspx.cs" Inherits="PIM_3SEMESTRE.Pages.Mecanico.paginamecanico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>AutoTech - Painel do Mecânico</title>

<link rel="stylesheet" href="../../css/mecanico/paginamecanico.css"/>

<link rel="stylesheet"
href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css"/>

</head>

<body>

<form id="form1" runat="server">

<div class="container">

    <!-- SIDEBAR -->
    <aside class="sidebar">

        <div>

            <div class="logo">

                <h1>
                    <span>AUTO</span>TECH
                </h1>

                <p>
                    SISTEMA DE GESTÃO PARA OFICINAS
                </p>

            </div>

            <div class="menu-title">
                MENU
            </div>

            <nav class="menu">

                <a href="#" class="menu-item active">

                    <i class="fa-regular fa-clipboard"></i>

                    Ordem de Serviço

                </a>
            </nav>

        </div>

        <div>

            <div class="help-box">

                <h3>
                    Precisa de ajuda?
                </h3>

                <p>
                    Fale com a equipe da oficina
                    se precisar de suporte.
                </p>

                <button type="button">

                    <i class="fa-brands fa-whatsapp"></i>

                    Falar com a oficina

                </button>

            </div>

            <a href="../Login/login.aspx" class="logout">

                <i class="fa-solid fa-arrow-right-from-bracket"></i>

                Sair da conta

            </a>

        </div>

    </aside>

    <!-- MAIN -->
    <main class="main-content">

        <!-- TOPO -->
        <header class="topbar">

            <div class="notification">

                <i class="fa-regular fa-bell"></i>

                <span>2</span>

            </div>

            <div class="profile">

                <div>

                    <h4>
                        <%= nomeMecanico %>
                    </h4>

                    <p>
                        Mecânico
                    </p>

                </div>

                <div class="avatar">

                    <%=
                    !string.IsNullOrEmpty(nomeMecanico)
                    ? nomeMecanico.Substring(0,1).ToUpper()
                    : "M"
                    %>

                </div>

            </div>

        </header>

        <!-- BANNER -->
        <section class="banner">

            <div class="overlay"></div>

            <div class="banner-content">

                <div class="title-icon">

                    <i class="fa-regular fa-clipboard"></i>

                    <div>

                        <h1>
                            Ordem de Serviço
                        </h1>

                        <p>
                            Acompanhe todos os serviços atribuídos a você.
                        </p>

                    </div>

                </div>

            </div>

        </section>

        <!-- CONTEÚDO -->
        <section class="content-area">

            <!-- SERVIÇOS -->
            <div class="orders-section">

                <!-- TABS -->
                <div class="tabs">

                    <button class="tab active">
                        Todas
                    </button>

                    <button class="tab">
                        Em andamento
                    </button>

                    <button class="tab">
                        Aguardando peças
                    </button>

                    <button class="tab">
                        Concluídas
                    </button>

                </div>

                <!-- BOX -->
                <div class="orders-box">

                    <div class="header-orders">

                        <h2>
                            Ordens de Serviço
                        </h2>

                        <span class="badge-qtd">
                            Serviços atribuídos
                        </span>

                    </div>

                    <!-- CARDS DINÂMICOS -->
                    <%= cardsServicos %>

                </div>

            </div>

            <!-- DETALHES -->
            <div class="details-panel">

                <%= detalhesServico %>

            </div>

        </section>

    </main>

</div>

</form>
    <script src="https://vlibras.gov.br/app/vlibras-plugin.js"></script>

<script>
    new window.VLibras.Widget('https://vlibras.gov.br/app');
</script>
</body>
</html>
