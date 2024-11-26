<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_SolicitudesFacturacion.aspx.cs" Inherits="cxpcxc.cxc_OrdenFactura" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function ConfirmaEliminar() {
            var resultado = false;
            if(confirm('¿Esta seguro de continuar, La solicitud se eliminara Permanentemente?')){
                resultado =true;
            }
            return resultado;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdIdEmpresa" runat="server" />
    <fieldset>
        <legend>REGISTRO DE FACTURACIÓN</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 85%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="btnCerrar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="pnConsulta" runat="server" Width="100%">
            <table id="tblConsulta" runat="server" style="width: 80%; margin: 0 auto">
                <tr>
                    <td colspan="3" style="text-align: center" >
                        <b><asp:Literal ID="ltEmpresa" runat="server"></asp:Literal></b>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">CLIENTE:</td>
                    <td colspan ="2">
                        <asp:DropDownList ID="dpCliente" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click"  />
                    </td>
                </tr>
            </table><br />
        </asp:Panel><br />
        <asp:Panel ID ="pnRegistros" runat ="server" Width ="100%" Height ="350px" ScrollBars ="Auto">
            <asp:Repeater ID="rptRegistros" runat="server" OnItemCommand="rptSolicitud_ItemCommand" OnItemDataBound="rptRegistros_ItemDataBound">
                <HeaderTemplate>
                    <table id="tblRegistros" border="1" style="width: 100%" class="tblFiltrar">
                        <thead>
                            <th scope="col" style ="color :red" >ELIMINAR</th>
                            <th scope="col">ORDEN SERVICIO</th>
                            <th scope="col">ORDEN FACTURA</th>
                            <th scope="col">FECHA</th>
                            <th scope="col">CLIENTE</th>
                            <th scope="col">SERVICIO</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col" style ="color :green">AGREGAR FACTURA</th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style="text-align: center; width: 90px">
                            <asp:ImageButton ID="ImgEliminar" runat="server" ImageUrl="~/img/delete.png" CommandName="Eliminar" CommandArgument='<%# Eval("IdOrdenFactura")%>' OnClientClick =" return ConfirmaEliminar();"  />
                        </td>
                        <td style="text-align: center; width: 90px"><b><%# Eval("IdServicio")%></b></td>
                        <td style="text-align: center; width: 90px"><b><%# Eval("IdOrdenFactura")%></b></td>
                        <td><%# Eval("FechaFactura","{0:d}")%></td>
                        <td><%# Eval("Cliente")%></td>
                        <td><%# Eval("Servicio")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td><%# Eval("ImporteVista")%></td>
                        <td style="text-align: center; width: 90px">
                            <asp:ImageButton ID="imgbtnReg" runat="server" ImageUrl="~/img/foward.png" CommandName="Registrar" CommandArgument='<%# Eval("IdOrdenFactura")%>'  />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </fieldset>
</asp:Content>
