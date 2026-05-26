<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cadastrarcliente.aspx.cs" Inherits="PIM_3SEMESTRE.Pages.Funcionario.cadastrarcliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

<title>AutoTech - Cadastrar Cliente</title>

<link rel="stylesheet" href="../../css/funcionario/cadastrarcliente.css"/>

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

            <a href="#">
                <i class="fa-solid fa-table-columns"></i>
                Painel do Cliente
            </a>

            <a href="#">
                <i class="fa-solid fa-clock-rotate-left"></i>
                Histórico
            </a>

            <a href="#">
                <i class="fa-regular fa-user"></i>
                Meus Dados
            </a>

        </div>

        <div class="menu-title">
            ATENDENTE
        </div>

        <div class="menu">

            <a href="#">
                <i class="fa-regular fa-pen-to-square"></i>
                Cadastrar Serviço
            </a>

            <a href="#" class="active">
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

    <!-- CONTEUDO -->
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

                <h1>Cadastrar Cliente</h1>

                <p>
                    Preencha os dados abaixo para
                    cadastrar um novo cliente.
                </p>

            </div>

        </div>

        <!-- FORMULARIO -->
        <div class="form-card">

            <!-- DADOS -->
            <div class="section">

                <div class="section-title">

                    <i class="fa-regular fa-user"></i>

                    <h2>1. Dados Pessoais</h2>

                </div>

                <div class="grid-3">

                    <div class="input-group">
                        <label>Nome completo *</label>

                        <asp:TextBox
                            ID="txtNome"
                            runat="server"
                            placeholder="Ex.: João Silva">
                        </asp:TextBox>
                    </div>

                    <div class="input-group">
                        <label>CPF *</label>

                        <asp:TextBox
                            ID="txtCpf"
                            runat="server"
                            placeholder="Ex.: 123.456.789-00">
                        </asp:TextBox>
                    </div>

                    <div class="input-group">
                        <label>Data de nascimento</label>

                        <asp:TextBox
                            ID="txtNascimento"
                            runat="server"
                            TextMode="Date">
                        </asp:TextBox>
                    </div>

                </div>

                <div class="grid-2">

                    <div class="input-group">
                        <label>Telefone *</label>

                        <asp:TextBox
                            ID="txtTelefone"
                            runat="server"
                            placeholder="(11) 99999-9999">
                        </asp:TextBox>
                    </div>

                    <div class="input-group">
                        <label>E-mail</label>

                        <asp:TextBox
                            ID="txtEmail"
                            runat="server"
                            placeholder="Ex.: joao@email.com">
                        </asp:TextBox>
                    </div>

                </div>

            </div>

            <!-- ENDERECO -->
            <div class="section">

                <div class="section-title">

                    <i class="fa-solid fa-location-dot"></i>

                    <h2>2. Endereço</h2>

                </div>

                <div class="grid-4">

                    <div class="input-group">
                        <label>CEP</label>

                        <asp:TextBox
                            ID="txtCep"
                            runat="server"
                            placeholder="Ex.: 01234-567">
                        </asp:TextBox>
                    </div>

                    <div class="input-group">
                        <label>Rua</label>

                        <asp:TextBox
                            ID="txtRua"
                            runat="server"
                            placeholder="Ex.: Rua das Flores">
                        </asp:TextBox>
                    </div>

                    <div class="input-group">
                        <label>Número</label>

                        <asp:TextBox
                            ID="txtNumero"
                            runat="server"
                            placeholder="Ex.: 123">
                        </asp:TextBox>
                    </div>

                    <div class="input-group">
                        <label>Complemento</label>

                        <asp:TextBox
                            ID="txtComplemento"
                            runat="server"
                            placeholder="Ex.: Apto 45">
                        </asp:TextBox>
                    </div>

                </div>

                <div class="grid-3">

                    <div class="input-group">
                        <label>Bairro</label>

                        <asp:TextBox
                            ID="txtBairro"
                            runat="server"
                            placeholder="Ex.: Centro">
                        </asp:TextBox>
                    </div>

                    <div class="input-group">
                        <label>Cidade</label>

                        <asp:TextBox
                            ID="txtCidade"
                            runat="server"
                            placeholder="Ex.: São Paulo">
                        </asp:TextBox>
                    </div>

                    <div class="input-group">
                        <label>Estado</label>

                        <asp:DropDownList
                            ID="ddlEstado"
                            runat="server">

                            <asp:ListItem Text="Selecione o estado"/>
                            <asp:ListItem Text="SP"/>
                            <asp:ListItem Text="RJ"/>
                            <asp:ListItem Text="MG"/>
                            <asp:ListItem Text="BA"/>

                        </asp:DropDownList>
                    </div>

                </div>

            </div>

            <!-- OBS -->
            <div class="section">

                <div class="section-title">

                    <i class="fa-regular fa-note-sticky"></i>

                    <h2>3. Observações (opcional)</h2>

                </div>

                <div class="input-group">

                    <label>Observações sobre o cliente</label>

                    <asp:TextBox
                        ID="txtObservacao"
                        runat="server"
                        TextMode="MultiLine"
                        Rows="5"
                        placeholder="Ex.: Cliente preferencial, observações importantes, informações adicionais...">
                    </asp:TextBox>

                </div>

            </div>

            <!-- BOTOES -->
            <div class="buttons">

                <button type="button" class="btn-cancelar">
                    Cancelar
                </button>

             <asp:Button
    ID="btnCadastrar"
    runat="server"
    Text="Cadastrar Cliente"
    CssClass="btn-cadastrar"
    OnClick="btnCadastrar_Click"/>

            </div>

        </div>

    </main>

</div>

</form>

</body>
</html>