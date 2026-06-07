<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cadastrarfuncionario.aspx.cs" Inherits="PIM_3SEMESTRE.Pages.ADM.cadastrarfuncionario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />

    <title>Cadastrar Funcionário</title>

    <link href="../../css/Adm/cadastrarfuncionario.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="anonymous"/>

    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet"/>

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

                    <a href="cadastrarfuncionario.aspx" class="active">
                        <i class="fa-solid fa-user-plus"></i>
                        Cadastrar Funcionário
                    </a>

            

                    <a href="cadastrarServico.aspx">
                        <i class="fa-solid fa-screwdriver-wrench"></i>
                        Cadastrar Tipo Serviço
                    </a>

                    <a href="relatorio.aspx">
                        <i class="fa-regular fa-rectangle-list"></i>
                        Relatórios
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

            <!-- CONTEUDO -->
            <main class="content">

                <!-- TOPO -->
                <div class="topbar">

                    <div class="profile">

                        <span>Administrador</span>

                        <img src="../../img/boneco.png"/>

                    </div>

                </div>

                <!-- FORMULARIO -->
                <div class="form-container">

                    <div class="form-header">

                        <h1>Cadastrar Funcionário / Mecânico</h1>

                        <p>
                            Preencha as informações abaixo para cadastrar um novo usuário no sistema.
                        </p>

                    </div>

                    <div class="form-grid">

                        <!-- NOME -->
                        <div class="input-box full">

                            <label>Nome Completo</label>

                            <asp:TextBox
                                ID="txtNome"
                                runat="server"
                                CssClass="input-field"
                                placeholder="Digite o nome completo">
                            </asp:TextBox>

                        </div>

                        <!-- EMAIL -->
                        <div class="input-box">

                            <label>Email</label>

                            <asp:TextBox
                                ID="txtEmail"
                                runat="server"
                                CssClass="input-field"
                                placeholder="Digite o email">
                            </asp:TextBox>

                        </div>

                        <!-- SENHA -->
                        <div class="input-box">

                            <label>Senha</label>

                            <asp:TextBox
                                ID="txtSenha"
                                runat="server"
                                TextMode="Password"
                                CssClass="input-field"
                                placeholder="Digite a senha">
                            </asp:TextBox>

                        </div>

                        <!-- TIPO -->
                        <div class="input-box">

                            <label>Tipo de Usuário</label>

                            <asp:DropDownList
                                ID="ddlTipoUsuario"
                                runat="server"
                                CssClass="input-field">

                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>

                                <asp:ListItem Text="Funcionário" Value="2"></asp:ListItem>

                                <asp:ListItem Text="Mecânico" Value="3"></asp:ListItem>

                            </asp:DropDownList>

                        </div>

                        <!-- ESPECIALIDADE -->
                        <div class="input-box">

                            <label>Especialidade do Mecânico</label>

                            <asp:TextBox
                                ID="txtEspecialidade"
                                runat="server"
                                CssClass="input-field"
                                placeholder="Ex: Motor, Freio, Suspensão...">
                            </asp:TextBox>

                        </div>

                        <!-- OBSERVAÇÃO -->
                        <div class="input-box full">

                            <label>Observações</label>

                            <asp:TextBox
                                ID="txtObservacao"
                                runat="server"
                                TextMode="MultiLine"
                                Rows="5"
                                CssClass="input-field textarea"
                                placeholder="Digite observações adicionais">
                            </asp:TextBox>

                        </div>
                        <!-- LISTA DE FUNCIONÁRIOS -->
<div class="table-container">

    <asp:GridView
        ID="gvFuncionarios"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table-funcionarios"
        DataKeyNames="id_usuario"
        OnRowDeleting="gvFuncionarios_RowDeleting"
        OnRowDataBound="gvFuncionarios_RowDataBound">

        <Columns>

            <asp:BoundField
                DataField="id_usuario"
                HeaderText="ID" />

            <asp:BoundField
                DataField="nm_usuario"
                HeaderText="Nome" />

            <asp:BoundField
                DataField="nm_email_usuario"
                HeaderText="Email" />

            <asp:BoundField
                DataField="nm_tipo_usuario"
                HeaderText="Tipo" />

            <asp:CommandField
                ShowDeleteButton="True"
                DeleteText="Excluir"
                ControlStyle-CssClass="btn-excluir" />

        </Columns>

    </asp:GridView>

</div>

                    <!-- BOTÃO -->
                    <div class="btn-area">

                        <asp:Button
                            ID="btnCadastrar"
                            runat="server"
                            Text="Cadastrar Usuário"
                            CssClass="btn-cadastrar"
                            OnClick="btnCadastrar_Click"/>

                    </div>

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
