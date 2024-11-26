<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_VerComprobante.aspx.cs" Inherits="cxpcxc.trf_VerComprobante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <fieldset>
        <legend>COMPROBANTE DE PAGO</legend>
        <div id="dvBtnCerrar" style="width: 100%; text-align: right;">
            <asp:Button ID="btnCerrar" runat="server" CssClass="button" OnClick="btnCerrar_Click" Text="Cerrar" CausesValidation="False" />
        </div>
        <asp:Panel ID="pnlDoctoPago" runat ="server" Width="100%" Height="630px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
            <asp:Literal ID="ltComprobante" runat="server" ></asp:Literal>
        </asp:Panel>
    </fieldset>
</asp:Content>
