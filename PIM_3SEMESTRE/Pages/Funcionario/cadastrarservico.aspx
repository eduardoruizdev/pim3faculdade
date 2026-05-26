<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cadastrarservico.aspx.cs" Inherits="PIM_3SEMESTRE.Pages.Funcionario.cadastrarservico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

<title>AutoTech - Cadastrar Serviço</title>

<link rel="stylesheet" href="../../css/funcionario/cadastrarservico.css"/>

<link rel="preconnect" href="https://fonts.googleapis.com"/>
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="anonymous"/>

<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700;800&display=swap" rel="stylesheet"/>

<link rel="stylesheet"
href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css"/>

</head>

<body>

<form id="form1" runat="server">

<div class="container">

    <!-- SIDEBAR -->
    <aside class="sidebar">

        <div class="logo">
            <img src="../../img/logo.png"/>
        </div>

        <div class="menu">

            <a href="#" class="active">
                <i class="fa-regular fa-pen-to-square"></i>
                Cadastrar Serviço
            </a>

            <a href="cadastrarcliente.aspx">
                <i class="fa-solid fa-user-plus"></i>
                Cadastrar Cliente
            </a>

            <a href="#">
                <i class="fa-solid fa-rotate"></i>
                Atualizar Status do serviço
            </a>

            <a href="#">
                <i class="fa-regular fa-rectangle-list"></i>
                Visualizar Ordem de serviço
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

        <a href="#" class="logout">
            <i class="fa-solid fa-arrow-right-from-bracket"></i>
            Sair da conta
        </a>

    </aside>

    <!-- CONTEÚDO -->
    <main class="content">

        <!-- TOPO -->
        <div class="topbar">

            <div class="notification">
                <i class="fa-regular fa-bell"></i>
                <span>2</span>
            </div>

            <div class="profile">
                <span>Atendente</span>
                <img src="../../img/user.png"/>
            </div>

        </div>

        <!-- BANNER -->
        <div class="banner">

            <div class="banner-text">

                <h1>Cadastrar Serviço</h1>

                <p>
                    Preencha todas as informações para registrar
                    um novo serviço.
                </p>

            </div>

        </div>

        <!-- FORM -->
        <div class="form-card">

            <!-- SERVIÇO -->
            <div class="section">

                <div class="section-title">
                    <i class="fa-regular fa-file-lines"></i>
                    <h2>1. Informações do Serviço</h2>
                </div>

                <div class="grid-2">

                    <div class="input-group">

                        <label>Tipo de serviço *</label>

                        <asp:DropDownList
                            ID="ddlTipoServico"
                            runat="server">
                        </asp:DropDownList>
                        <div class="input-group">

    <label>Mecânico responsável *</label>

    <asp:DropDownList
        ID="ddlMecanico"
        runat="server">
    </asp:DropDownList>

</div>
                    </div>

                    <div class="input-group">

                        <label>Título do serviço *</label>

                        <asp:TextBox
                            ID="txtTitulo"
                            runat="server"
                            placeholder="Ex.: Revisão preventiva + Troca de óleo do motor">
                        </asp:TextBox>

                    </div>

                </div>

                <div class="input-group">

                    <label>Descrição do serviço *</label>

                    <asp:TextBox
                        ID="txtDescricao"
                        runat="server"
                        TextMode="MultiLine"
                        Rows="5"
                        placeholder="Descreva o serviço que será realizado no veículo...">
                    </asp:TextBox>

                </div>

                <div class="grid-3">

                    <div class="input-group">

                        <label>Data de entrada *</label>

                        <asp:TextBox
                            ID="txtDataEntrada"
                            runat="server"
                            TextMode="Date">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>Previsão de entrega *</label>

                        <asp:TextBox
                            ID="txtDataEntrega"
                            runat="server"
                            TextMode="Date">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>Prioridade</label>

                        <asp:DropDownList
                            ID="ddlPrioridade"
                            runat="server">

                            <asp:ListItem Text="Alta"/>
                            <asp:ListItem Text="Média"/>
                            <asp:ListItem Text="Baixa"/>

                        </asp:DropDownList>

                    </div>

                </div>

            </div>

            <!-- VEÍCULO -->
            <div class="section">

                <div class="section-title">
                    <i class="fa-solid fa-car-side"></i>
                    <h2>2. Informações do Veículo</h2>
                </div>

                <div class="grid-5">

                    <div class="input-group">

                        <label>Placa *</label>

                        <asp:TextBox
                            ID="txtPlaca"
                            runat="server"
                            placeholder="ABC1D23">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>Modelo *</label>

                        <asp:TextBox
                            ID="txtModelo"
                            runat="server"
                            placeholder="Volkswagen Jetta">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>Ano</label>

                        <asp:TextBox
                            ID="txtAno"
                            runat="server"
                            placeholder="2019">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>Cor</label>

                        <asp:DropDownList
                            ID="ddlCor"
                            runat="server">

                            <asp:ListItem Text="Preto"/>
                            <asp:ListItem Text="Branco"/>
                            <asp:ListItem Text="Prata"/>
                            <asp:ListItem Text="Vermelho"/>

                        </asp:DropDownList>

                    </div>

                    <div class="input-group">

                        <label>Quilometragem</label>

                        <asp:TextBox
                            ID="txtKm"
                            runat="server"
                            placeholder="58350">
                        </asp:TextBox>

                    </div>

                </div>

            </div>

            <!-- CLIENTE -->
            <div class="section">

                <div class="section-title">
                    <i class="fa-regular fa-user"></i>
                    <h2>3. Informações do Cliente / Proprietário</h2>
                </div>

                <div class="grid-4">

                    <div class="input-group">

                        <label>Cliente *</label>

                        <asp:DropDownList
                            ID="ddlCliente"
                            runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>

                    <div class="input-group">

                        <label>Telefone</label>

                        <asp:TextBox
                            ID="txtTelefone"
                            runat="server"
                            ReadOnly="true">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>E-mail</label>

                        <asp:TextBox
                            ID="txtEmail"
                            runat="server"
                            ReadOnly="true">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>CPF / CNPJ</label>

                        <asp:TextBox
                            ID="txtCpf"
                            runat="server"
                            ReadOnly="true">
                        </asp:TextBox>

                    </div>

                </div>

            </div>

            <!-- VALORES -->
            <div class="section">

                <div class="section-title">
                    <i class="fa-solid fa-dollar-sign"></i>
                    <h2>4. Valores</h2>
                </div>

                <div class="grid-4">

                    <div class="input-group">

                        <label>Valor da mão de obra</label>

                        <asp:TextBox
                            ID="txtMaoObra"
                            runat="server"
                            AutoPostBack="true"
                            OnTextChanged="CalcularValorTotal"
                            placeholder="250,00">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>Valor das peças</label>

                        <asp:TextBox
                            ID="txtPecas"
                            runat="server"
                            AutoPostBack="true"
                            OnTextChanged="CalcularValorTotal"
                            placeholder="350,00">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>Desconto</label>

                        <asp:TextBox
                            ID="txtDesconto"
                            runat="server"
                            AutoPostBack="true"
                            OnTextChanged="CalcularValorTotal"
                            placeholder="0,00">
                        </asp:TextBox>

                    </div>

                    <div class="input-group">

                        <label>Valor total *</label>

                        <asp:TextBox
                            ID="txtValorTotal"
                            runat="server"
                            ReadOnly="true">
                        </asp:TextBox>

                    </div>

                </div>

            </div>

            <!-- OBS -->
            <div class="section">

                <div class="section-title">
                    <i class="fa-regular fa-note-sticky"></i>
                    <h2>5. Observações</h2>
                </div>

                <div class="input-group">

                    <asp:TextBox
                        ID="txtObservacao"
                        runat="server"
                        TextMode="MultiLine"
                        Rows="4">
                    </asp:TextBox>

                </div>

            </div>

            <!-- BOTÕES -->
            <div class="buttons">

                <button type="button" class="btn-cancelar">
                    Cancelar
                </button>

                <asp:Button
                    ID="btnCadastrar"
                    runat="server"
                    Text="Cadastrar Serviço"
                    CssClass="btn-cadastrar"
                    OnClick="btnCadastrar_Click"/>

            </div>

        </div>

    </main>

</div>

</form>

</body>
</html>