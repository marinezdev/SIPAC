<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_SolicitudesCaptura.aspx.cs" Inherits="cxpcxc.trf_SolicitudesCaptura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        
        function Confirmar() {
            return confirm('¿Esta seguro continuar?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <fieldset>
        <legend>SOLICITUDES DE TRANSFERENCIA PARA CAPTURA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
            </tr>
           <tr>
                <td style="text-align: right;">
                    <asp:Button ID="BtnExportar" runat="server" Text="Exportar Lista"  CssClass="button" OnClick="BtnExportar_Click"  />&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br /><br< />
        <asp:Panel ID ="pnSolicitud"  runat ="server" Width ="100%" Height ="450px" ScrollBars ="Vertical" >
            <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand"  >
                <HeaderTemplate>
                    <table id="tblSol" border="1" style ="width :100%"  class ="tblFiltrar"  >
                        <caption >SOLICITUDES </caption>
                        <thead>
                            <th scope="col">REGISTRO</th>
                            <th scope="col">NO. FACTURA</th>
                            <th scope="col">FECHA FACTURA</th>
                            <th scope="col">PROVEEDOR</th>
                            <th scope="col">BANCO</th>
                            <th scope="col">CUENTA</th>
                            <th scope="col">CLABE </th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">CANTIDAD PAGAR</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">FACTURA</th>
                            <th scope="col"></th>
                            <th scope="col"></th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("FechaRegistro","{0:d}")%></td>
                        <td><%# Eval("Factura")%></td>
                        <td><%# Eval("FechaFactura","{0:d}")%></td>
                        <td><%# Eval("Proveedor")%></td>
                        <td><%# Eval("Banco")%></td>
                        <td><%# Eval("Cuenta")%></td>
                        <td><%# Eval("CtaClabe")%></td>
                        <td><%# Eval("Importe","{0:0,0.00}")%></td>
                        <td style="width:100px"><%# Eval("CantidadPagar","{0:0,0.00}")%></td>
                        <td><%# Eval("Moneda")%></td>
                        <td style="text-align :center;"><%# Eval("ConFactura")%></td>
                        <td style="text-align :center;width:35px">
                            <asp:ImageButton ID="ImgVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="Ver" CommandArgument='<%# Eval("IdSolicitud")%>' />
                        </td>
                        <td style="text-align :center;width:60px">
                            <asp:LinkButton ID ="lkAceptar" runat ="server"  Text ="Aceptar"  ToolTip ="Aceptar Captura"  CommandName="Aceptar" CommandArgument='<%# Eval("IdSolicitud")%>' OnClientClick ="return Confirmar();" ></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </fieldset>
</asp:Content>
