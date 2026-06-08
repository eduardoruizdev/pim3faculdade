<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="relatorio.aspx.cs"
Inherits="PIM_3SEMESTRE.Pages.ADM.relatorio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Relatórios - Auto Tech</title>

<link href="../../css/Adm/relatorio.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

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

            <a href="cadastrarfuncionario.aspx">
                <i class="fa-solid fa-user-plus"></i>
                Cadastrar Funcionário
            </a>

            <a href="cadastrarServico.aspx">
                <i class="fa-solid fa-screwdriver-wrench"></i>
                Cadastrar Tipo Serviço
            </a>

            <a href="relatorio.aspx" class="active">
                <i class="fa-solid fa-chart-column"></i>
                Relatórios
            </a>

        </div>

        <div class="help-box">

            <h3>Precisa de ajuda?</h3>

            <p>
                Nossa equipe está pronta
                para te atender.
            </p>

            <button type="button">
                <i class="fa-solid fa-headset"></i>
                Falar com a oficina
            </button>

        </div>

        <a href="../Login/login.aspx" class="logout">
            <i class="fa-solid fa-arrow-right-from-bracket"></i>
            Sair da conta
        </a>

    </aside>

    <!-- CONTEUDO -->
    <main class="content">

        <div class="topo">

            <h1>
                Dashboard Inteligente
            </h1>

            <p>
                Machine Learning e Análise de Dados
            </p>

        </div>

        <!-- CARDS -->
        <div class="cards">

            <div class="card">
                <i class="fa-solid fa-users"></i>

                <div>
                    <span>Clientes</span>

                    <asp:Label
                        ID="lblClientes"
                        runat="server"
                        CssClass="numero"/>
                </div>
            </div>

            <div class="card">
                <i class="fa-solid fa-user-gear"></i>

                <div>
                    <span>Mecânicos</span>

                    <asp:Label
                        ID="lblMecanicos"
                        runat="server"
                        CssClass="numero"/>
                </div>
            </div>

            <div class="card">
                <i class="fa-solid fa-screwdriver-wrench"></i>

                <div>
                    <span>Serviços</span>

                    <asp:Label
                        ID="lblServicos"
                        runat="server"
                        CssClass="numero"/>
                </div>
            </div>

            <div class="card">
                <i class="fa-solid fa-dollar-sign"></i>

                <div>
                    <span>Faturamento</span>

                    <asp:Label
                        ID="lblValor"
                        runat="server"
                        CssClass="numero"/>
                </div>
            </div>

        </div>

        <!-- RELATORIO PRODUTIVIDADE -->
        <div class="box">

            <h2>
                Relatório Mensal de Produtividade
            </h2>

            <asp:GridView
                ID="gvProdutividade"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="tabela">

                <Columns>

                    <asp:BoundField
                        DataField="mes"
                        HeaderText="Mês"/>

                    <asp:BoundField
                        DataField="total_os"
                        HeaderText="OS Concluídas"/>

                    <asp:BoundField
                        DataField="faturamento"
                        HeaderText="Faturamento"/>

                    <asp:BoundField
                        DataField="tempo_medio"
                        HeaderText="Tempo Médio"/>

                </Columns>

            </asp:GridView>

        </div>

        <!-- SERVICOS -->
        <div class="box">

            <h2>
                Ranking dos Serviços Mais Realizados
            </h2>

            <asp:GridView
                ID="gvRankingServicos"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="tabela">

                <Columns>

                    <asp:BoundField
                        DataField="nm_tipo_servico"
                        HeaderText="Tipo Serviço"/>

                    <asp:BoundField
                        DataField="quantidade"
                        HeaderText="Quantidade"/>

                </Columns>

            </asp:GridView>

        </div>

        <!-- CLIENTES -->
        <div class="box">

            <h2>
                Clientes Recorrentes
            </h2>

            <asp:GridView
                ID="gvClientes"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="tabela">

                <Columns>

                    <asp:BoundField
                        DataField="cliente"
                        HeaderText="Cliente"/>

                    <asp:BoundField
                        DataField="quantidade"
                        HeaderText="Quantidade de Serviços"/>

                    <asp:BoundField
                        DataField="total_gasto"
                        HeaderText="Total Gasto"/>

                </Columns>

            </asp:GridView>

        </div>

        <!-- MECANICOS -->
        <div class="box">

            <h2>
                Ranking de Mecânicos
            </h2>

            <asp:GridView
                ID="gvMecanicos"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="tabela">

                <Columns>

                    <asp:BoundField
                        DataField="mecanico"
                        HeaderText="Mecânico"/>

                    <asp:BoundField
                        DataField="quantidade"
                        HeaderText="OS Finalizadas"/>

                </Columns>

            </asp:GridView>

        </div>

        <!-- TABELA -->
        <div class="box">

            <h2>
                Últimos Serviços
            </h2>

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

</body>
</html>
