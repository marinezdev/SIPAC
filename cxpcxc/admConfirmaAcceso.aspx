<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admConfirmaAcceso.aspx.cs" Inherits="cxpcxc.admConfirmaAcceso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <fieldset>
        <legend>MODIFICAR ACCESO AL SISTEMA</legend>
        <div id="dvAvisoBienVenido" style="width:80%; margin:auto;">
            <div id="dvTitulo" style="width:100%; text-align:center;">
                <h4>La actualización se completó con éxito</h4><br />
            </div>
            <table id="tblMensaje" style="width:100%">
                <tr>
                    <td colspan="2" style="text-align:center;">
                        Por favor ingrese nuevamente al sistema, ahora con su nuevo usuario y clave.
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style1">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;">
                        Usuario:
                    </td>
                    <td>
                        <asp:Label ID="lbUsuario" runat="server"></asp:Label>
                    </td>
                </tr>
           </table>
            <div id="dvBtns" style="width:100%; text-align:right;">
                <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="button" OnClick="btnContinuar_Click" />
            </div>
        </div>
    </fieldset>
</asp:Content>
