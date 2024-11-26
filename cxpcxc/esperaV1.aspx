<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="esperaV1.aspx.cs" Inherits="cxpcxc.esperaV1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SIPAC</title>
    <link type="text/css" href="css/cxpcxc.css" rel="stylesheet" />
    <link type="text/css" href="css/menuhrz.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-2.2.1.min.js"></script>
    <script src="js/jquery.timers.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="CajaAplicacion" >
            <div style="width: 100%; background-color: #105e8e;">
                <table style ="width:100%">
                    <tr>
                        <td><img src="img/asae.jpg" style="height: 115px; width: 250px" /></td>
                        <td style ="text-align :center; color :white">
                            <h1>SISTEMA DE PAGOS Y COBRANZA</h1><br />
                            &nbsp;
                        </td>
                        <td style ="text-align:right">
                            <img id ="ImgLogo" src="img/logo_asae.png" style="height: 115px; width: 225px;background-color: #EEF3FA;" runat ="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan ="3" style=" height :30px; text-align :right;background-color:#D7E4F2;">
                            <b><asp:Label ID="lbMstNombreUsuario" runat="server" ForeColor="DarkGreen" Font-Size="12px" style="color: #339966"></asp:Label></b> 
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table style="width:100%">
                    <tr>
                        <td>
                            <ul id="navmenu">
                                <!--<li><a href='espera.aspx'><span>Inicio</span></a></li>-->
                                <asp:Literal ID="ltStrMenu" runat="server"></asp:Literal>
                                <li><a href='salir.aspx'><span>Terminar Aplicación</span></a></li>
                            </ul>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width:800px; margin-left: auto; margin-right: auto;">
                <div style="width:800px; margin-left: auto; margin-right: auto;">
                    <br />
                    <table runat ="server" style="width:100%; margin :0 auto;">
                        <tr>
                            <td colspan="2" style="text-align:center">
                                Roles asignados en SIPAC.
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%">
                                <!-- example : cxc_SolicitudesFacturacion.aspx -->
                                    <asp:Repeater ID="rptRegistros" runat="server" OnItemCommand="rptRegistros_ItemCommand" OnItemDataBound="rptRegistros_ItemDataBound">
                                        <HeaderTemplate>
                                            <table id="tblRegistros" border="1" style="width: 100%" class="tblFiltrar">
                                                <thead>
                                                    <th scope="col">Usuario</th>
                                                    <th scope="col">Empresa</th>
                                                    <th scope="col">Perfil</th>
                                                    <th scope="col"></th>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: White; color: #333333">
                                                <td style="text-align: center; width: 100px"><%# Eval("Usuario")%></td>
                                                <td style="text-align: center; width: 200px"><%# Eval("EmpresaNombre")%></td>
                                                <td style="text-align: center; width: 100px"><%# Eval("GrupoNombre")%></td>
                                                <td style="text-align: center; width: 90px">
                                                    <asp:ImageButton ID="imgbtnReg" runat="server" ImageUrl="~/img/foward.png" CommandName="Registrar" CommandArgument='<%# Eval("Id")%>'  />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                                </tbody>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblInformacion" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="dvCajaPie" style="width: 1000px; margin: auto;text-align :center;font-size :12px; color :blue">
            <%--<p><b>Cerrada de Margaritas No 426 <br /> Colonia Ex-Hacienda de Guadalupe Chimalistac <br /> C.P. 01050 México, D.F.</b></p>--%>
            <p><b>-   Seguimos trabajando para atenderte mejor  - <br /> SIPAC </b></p>
        </div>
</form>
</body>
</html>
