<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="ordemservico.aspx.cs"
Inherits="PIM_3SEMESTRE.Pages.Funcionario.ordemservico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta http-equiv="Content-Type"
          content="text/html; charset=utf-8" />

    <meta name="viewport"
          content="width=device-width, initial-scale=1.0" />

    <title>Ordem de Serviço</title>

    <link rel="stylesheet"
          href="../../css/funcionario/ordemservico.css" />

    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css'
          rel='stylesheet' />

</head>

<body>

<form id="form1" runat="server">

<div class="container">

    <!-- SIDEBAR -->
    <aside class="sidebar">

        <div class="logo">
            <img src="../../img/logo.png" />
        </div>

        <nav class="menu">

            <a href="cadastrarservico.aspx"
               class="menu-item">

                <i class='bx bx-edit'></i>
                <span>Cadastrar Serviço</span>

            </a>

            <a href="cadastrarcliente.aspx"
               class="menu-item">

                <i class='bx bx-user-plus'></i>
                <span>Cadastrar Cliente</span>

            </a>

            <a href="ordemservico.aspx"
               class="menu-item active">

                <i class='bx bx-refresh'></i>
                <span>Ordem de Serviço</span>

            </a>

        </nav>

    </aside>

    <!-- MAIN -->
    <main class="main-content">

        <!-- TOPO -->
        <div class="topbar">

            <h1>
                Ordem de Serviço
            </h1>

        </div>

        <!-- GRID -->
        <div class="content-grid">

            <!-- ESQUERDA -->
            <section class="services-list">

                <div class="search-filter">

                    <div class="search-box">

                        <i class='bx bx-search'></i>

                        <asp:TextBox
                            ID="txtBuscar"
                            runat="server"
                            placeholder="Buscar serviço...">
                        </asp:TextBox>

                    </div>

                    <asp:Button
                        ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="filter-btn"
                        OnClick="btnBuscar_Click" />

                </div>

                <!-- GRID -->
                <asp:GridView
                    ID="gvServicos"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="grid-servicos"
                    DataKeyNames="id_servico"
                    OnSelectedIndexChanged="gvServicos_SelectedIndexChanged"
                    OnRowDeleting="gvServicos_RowDeleting">

                    <Columns>

                        <asp:BoundField
                            DataField="id_servico"
                            HeaderText="OS" />

                        <asp:BoundField
                            DataField="nm_usuario"
                            HeaderText="Cliente" />

                        <asp:BoundField
                            DataField="nm_modelo_veiculo_servico"
                            HeaderText="Veículo" />

                        <asp:BoundField
                            DataField="cd_placa_veiculo_servico"
                            HeaderText="Placa" />

                        <asp:BoundField
                            DataField="st_servico"
                            HeaderText="Status" />

                        <asp:CommandField
                            ShowSelectButton="True"
                            SelectText="Visualizar" />

                        <asp:CommandField
                            ShowDeleteButton="True"
                            DeleteText="Excluir" />

                    </Columns>

                </asp:GridView>

            </section>

            <!-- DIREITA -->
            <section class="details-panel">

                <asp:HiddenField
                    ID="hfIdServico"
                    runat="server" />

                <!-- CABEÇALHO -->
                <div class="details-header">

                    <h2>

                        OS #

                        <asp:Label
                            ID="lblOS"
                            runat="server"
                            Text="-">
                        </asp:Label>

                    </h2>

                    <span class="status andamento">

                        <asp:Label
                            ID="lblStatus"
                            runat="server"
                            Text="-">
                        </asp:Label>

                    </span>

                </div>

                <!-- INFO -->
                <div class="details-card">

                    <h3>
                        Informações do serviço
                    </h3>

                    <div class="info-grid">

                        <!-- VEÍCULO -->
                        <div class="info-item">

                            <i class='bx bx-car'></i>

                            <div>

                                <strong>

                                    <asp:Label
                                        ID="lblModelo"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                </strong>

                                <p>

                                    <asp:Label
                                        ID="lblPlaca"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                    •

                                    <asp:Label
                                        ID="lblCor"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                    •

                                    <asp:Label
                                        ID="lblAno"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                    •

                                    <asp:Label
                                        ID="lblKm"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                </p>

                            </div>

                        </div>

                        <!-- TIPO SERVIÇO -->
                        <div class="info-item">

                            <i class='bx bx-wrench'></i>

                            <div>

                                <strong>
                                    Tipo de serviço
                                </strong>

                                <p>

                                    <asp:Label
                                        ID="lblTipoServico"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                </p>

                            </div>

                        </div>

                        <!-- MECÂNICO -->
                        <div class="info-item">

                            <i class='bx bx-hard-hat'></i>

                            <div>

                                <strong>
                                    Mecânico responsável
                                </strong>

                                <p>

                                    <asp:Label
                                        ID="lblMecanico"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                </p>

                            </div>

                        </div>

                        <!-- CLIENTE -->
                        <div class="info-item">

                            <i class='bx bx-user'></i>

                            <div>

                                <strong>

                                    <asp:Label
                                        ID="lblCliente"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                </strong>

                                <p>

                                    <asp:Label
                                        ID="lblTelefone"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                </p>

                            </div>

                        </div>

                        <!-- VALOR -->
                        <div class="info-item">

                            <i class='bx bx-dollar-circle'></i>

                            <div>

                                <strong>
                                    Valor Total
                                </strong>

                                <p>

                                    <asp:Label
                                        ID="lblValor"
                                        runat="server"
                                        Text="-">
                                    </asp:Label>

                                </p>

                            </div>

                        </div>

                    </div>

                    <!-- DESCRIÇÃO -->
                    <div class="description">

                        <strong>
                            Descrição do serviço
                        </strong>

                        <p>

                            <asp:Label
                                ID="lblDescricao"
                                runat="server"
                                Text="-">
                            </asp:Label>

                        </p>

                    </div>

                </div>

                <!-- ATUALIZAR STATUS -->
                <div class="details-card">

                    <h3>
                        Atualizar Status
                    </h3>

                    <div class="input-group">

                        <label>
                            Novo status
                        </label>

                        <asp:DropDownList
                            ID="ddlStatus"
                            runat="server">

                            <asp:ListItem Text="Recebido"
                                          Value="Recebido" />

                            <asp:ListItem Text="Orçamento aprovado"
                                          Value="Orçamento aprovado" />

                            <asp:ListItem Text="Em andamento"
                                          Value="Em andamento" />

                            <asp:ListItem Text="Finalização"
                                          Value="Finalização" />

                            <asp:ListItem Text="Concluído"
                                          Value="Concluído" />

                            <asp:ListItem Text="Cancelado"
                                          Value="Cancelado" />

                        </asp:DropDownList>

                    </div>

                    <div class="buttons">

                        <asp:Button
                            ID="btnAtualizar"
                            runat="server"
                            Text="Atualizar Status"
                            CssClass="save-btn"
                            OnClick="btnAtualizar_Click" />

                    </div>

                </div>

            </section>

        </div>

    </main>

</div>

</form>

</body>

</html>
