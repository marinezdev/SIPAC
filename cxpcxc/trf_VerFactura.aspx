<%@ Page Title="SIPAC" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_VerFactura.aspx.cs" Inherits="cxpcxc.trf_VerFactura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <fieldset>
        <legend>FACTURA</legend>
        <div id="dvBtnCerrar" style="width: 100%; text-align: right;">
            <asp:Button ID="btnCerrar" runat="server" CssClass="button" OnClick="btnCerrar_Click" Text="Cerrar" CausesValidation="False" />
        </div>
        <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="630px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
            <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
        </asp:Panel>
    </fieldset>
</asp:Content>
