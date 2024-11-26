<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_VerComprobanteFondos.aspx.cs" Inherits="cxpcxc.trf_VerComprobanteFondos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:HiddenField ID="hdIdEmresa" runat ="server" />
    <asp:HiddenField ID="hdIdFondos" runat ="server" />
    <fieldset>
        <legend>COMPROBANTE DE PAGO</legend>
        <div id="dvBtnCerrar" style="width: 100%; text-align: right;">
            <asp:Button ID="btnCerrar" runat="server" CssClass="button" OnClick="btnCerrar_Click" Text="Cerrar" CausesValidation="False" />
        </div>
        <asp:Panel ID ="pnpaginas" runat ="server"  Visible ="false"  >
            <asp:Label ID ="lbEtiqueta" runat ="server"  Text ="COMPROBANTES: " ></asp:Label>
            <asp:Repeater ID="rpPaginas" runat="server" OnItemCommand="rpPaginas_ItemCommand">
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style="width:65px;text-align: center;">
                            <asp:LinkButton ID="lkDoc" runat ="server" Font-Size="17px"  ForeColor="Blue"  Text ='<%# Eval("IdDocumento")%>'  CommandName ="Docto" CommandArgument='<%# Eval("IdDocumento")%>' ></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel><br />  
        <asp:Panel ID="pnlDoctoPago" runat ="server" Width="100%" Height="630px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
            <asp:Literal ID="ltComprobante" runat="server" ></asp:Literal>
        </asp:Panel>
        
    </fieldset>
</asp:Content>
