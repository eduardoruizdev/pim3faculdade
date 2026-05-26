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

<body>

<form id="form1" runat="server">

<div class="container">


    <aside class="sidebar">

        <div>
            <div class="logo">
                <img src="../../img/logo.png" alt="Logo"/>
            </div>

            <div class="menu-title">
                MENU
            </div>

            <nav class="menu">

                <a href="#" class="menu-item">
                    <i class='bx bx-home-alt'></i>
                    <span>Painel do Cliente</span>
                </a>

                <a href="#" class="menu-item active">
                    <i class='bx bx-history'></i>
                    <span>Histórico</span>
                </a>

                <a href="#" class="menu-item">
                    <i class='bx bx-user'></i>
                    <span>Meus Dados</span>
                </a>

            </nav>
        </div>

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
            <button type="button" class="filter-btn active-filter">Todos</button>
            <button type="button" class="filter-btn">Em andamento</button>
            <button type="button" class="filter-btn">Concluídos</button>
            <button type="button" class="filter-btn">Cancelados</button>
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

                                <!-- 🔥 DESCRIÇÃO DO SERVIÇO -->
                                <div class="service-date">
                                    Serviço: <%# Eval("ds_servico") %>
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

</body>
</html>