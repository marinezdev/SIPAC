<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="cxpcxc.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SIPAC</title>
    <link type="text/css" href="css/cxpcxc.css" rel="stylesheet" />
    <style >
        .Contenedor {
            background :#F3F9FE; 
            border: 2px solid #617BB0;
            border-radius: 8px 8px 8px 8px;
            -moz-border-radius: 8px 8px 8px 8px;
        }
    </style>
    <script type="text/javascript">
        document.oncontextmenu = function(){return false}
    </script>
</head>
<body  >
    <form id="form1" runat="server" >
        <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
        <div style ="margin-top:150px">
            <table id ="tblContenido" style="width :585px;margin : 0 auto;" class ="Contenedor" >
                <tr>
                    <td style ="width :20%"><img src="img/logo_asae.png" style="height: 90px; width:270px"  /></td>
                    <td style ="text-align :center;background-color: #102167; color :white; font-size: 30px; font-family:Arial 'Times New Roman',Times, serif" >
                        S&nbsp;I&nbsp;P&nbsp;A&nbsp;C
                    </td>
                </tr>
                <tr>
                    <td>
                         <asp:Image runat ="server" ImageUrl ="img/logo_sipac.png"  />
                    </td>
                    <td class ="CajaLogin">
                        <table id="tblLogin" style="margin:0 auto">
                            <tr>
                                <td colspan="4" style="height:10px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Usuario:</td>
                                <td>
                                    <asp:TextBox ID="txUsuario" runat="server" Width="150px" MaxLength="24" AutoCompleteType="Disabled" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_txUsuario" runat="server" ErrorMessage="*" ControlToValidate="txUsuario" Font-Size="Large" ForeColor="Red" ValidationGroup="valUsuario"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txUsuario" runat="server"
                                        TargetControlID="txUsuario"
                                        FilterMode="ValidChars"
                                        ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890!#$%&.-_?"/>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Contraseña:</td>
                                <td>
                                    <asp:TextBox ID="txClave" runat="server" Width="150px" MaxLength="16" TextMode="Password" AutoCompleteType="Disabled">demo</asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_txClave" runat="server" ErrorMessage="*" ControlToValidate="txClave" Font-Size="Large" ForeColor="Red" ValidationGroup="valUsuario"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txClave" runat="server"
                                        TargetControlID="txClave"
                                        FilterMode="ValidChars"
                                        ValidChars="abcdefghijklmnñopqrstuvwxyz ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890!#$%&.-_?"/>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="2" style="color: red; text-align: center; height:20px;">
                                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="button" ValidationGroup="valUsuario" OnClick="btnAceptar_Click" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height:10px">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
