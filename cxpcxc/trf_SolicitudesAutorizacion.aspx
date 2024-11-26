<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_SolicitudesAutorizacion.aspx.cs" Inherits="cxpcxc.trf_SolicitudesAurizacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function validaTpCambio() {
            var continuar = false;
            if (Page_ClientValidate('gpSol') == true) {
                if (Page_ClientValidate('gptc') == true) {
                    var chk = $("input[type='checkbox']:checked").length;
                    if (chk != "") {
                        if (confirm('Esta seguro que desea AUTORIZAR  la seleccion, con el tipo de cambio registrado?')) {
                            $("#dvBtns").hide();
                            continuar = true;
                        }
                    } else { alert('Seleccione las solicitudes.') ;}
                } else { alert('Agruege el tipo de cambio.') ;}
            } else { alert('Algunas de las solicitudes no cuentan con un importe valido.') ;}

            return continuar;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="HdIdEmpresa" runat="server" />
    <asp:HiddenField ID="hdIdUsr" runat="server" />
    <fieldset>
        <legend>AUTORIZACION DE TRANSFERENCIA</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 80%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btnAutorizar" runat="server" Text="Continuar" CssClass="button" OnClick="btnAutorizar_Click1" />&nbsp;&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="pnConsulta" runat="server" Width="100%">
            <table id="tblConsulta" runat="server" style="width: 80%; margin: 0 auto" class ="tblConsulta">
                <tr>
                    <td colspan="3" style="text-align: center" >
                        <b><asp:Literal ID="ltEmpresa" runat="server"></asp:Literal></b>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px">PROVEEDOR:</td>
                    <td colspan ="2">
                        <asp:DropDownList ID="dpProveedor" runat="server"></asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td>UNIDAD DE NEGOCIO:</td>
                    <td colspan ="2">
                        <asp:DropDownList ID="dpUdNegocio" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>SOLICITANTE:</td>
                    <td>
                        <asp:DropDownList ID="dpSolicitante" runat="server"></asp:DropDownList>
                    </td>
                    <td style="text-align:right">
                        <asp:Button ID="btnConsulta" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsulta_Click" CausesValidation="false" />
                    </td>
                </tr>
            </table><br />
        </asp:Panel>
        <br />
        <asp:UpdatePanel ID="udpSolAutorizacion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnContenido" runat="server" Width="100%">
                    <table style="width: 60%;text-align :right;margin :0 auto  ">
                        <tr>
                            <td style ="width :10%" ></td>
                            <td style ="width :35%;text-align :center"  class ="Titulos">TOTAL GENERAL</td>
                            <td style ="width :35%;text-align :center" class ="Titulos">TOTAL DE AUTORIZACION</td>
                        </tr>
                        <tr>
                            <td><b>PESOS:</b></td>
                            <td>
                                <asp:Label ID="lbTotPesos" runat="server" Text="0" Font-Size="Medium"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbTotAutPesos" runat="server" Text="0"  Font-Size="Medium" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><b>DOLARES:</b></td>
                            <td>
                                <asp:Label ID="lbTotDlls" runat="server" Text="0"  Font-Size="Medium" ></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbTotAutDlls" runat="server" Text="0" Font-Size="Medium" ></asp:Label>
                            </td>
                        </tr>
                    </table><br />
                    <asp:Panel ID ="pnSolicitud"  runat ="server" >
                        <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand" OnItemDataBound="rptSolicitud_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tblSol" border="1" style="width: 98%" class="tblFiltrar">
                                    <thead>
                                        <%--<th scope="col">UNIDAD DE NEGOCIO</th>
                                        <th scope="col">SOLICITANTE</th>--%>
                                        <%--<th scope="col">REGISTRO</th>--%>
                                        <th scope="col">NO. FACTURA</th>
                                        <th scope="col">FECHA FACTURA</th>
                                        <th scope="col">PROVEEDOR</th>
                                        <th scope="col">DESCRIPCION</th>
                                        <th scope="col">IMPORTE</th>
                                        <th scope="col">MONEDA</th>
                                        <th scope="col">FACTURA</th>
                                        <th scope="col">PRIORIDAD PAGO</th>
                                        <th scope="col">CANTIDAD PAGAR</th>
                                        <th scope="col">VER</th>
                                        <th scope="col"></th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <%--<td><%# Eval("UnidadNegocio ")%></td>
                                    <td><%# Eval("Solicitante")%></td>--%>
                                    <%--<td><%# Eval("FechaRegistro","{0:d}")%></td>--%>
                                    <td><%# Eval("Factura")%></td>
                                    <td><%# Eval("FechaFactura","{0:d}")%></td>
                                    <td><%# Eval("Proveedor")%></td>
                                    <td><%# Eval("DescProyecto")%></td>
                                    <td>
                                        <asp:Label ID="lbImporte" runat="server" Text='<%# Eval("Importe","{0:0,0.00}")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbTpMoneda" runat="server" Text='<%# Eval("Moneda")%>'></asp:Label>
                                    </td>
                                    <td style="text-align: center;"><%# Eval("ConFactura")%></td>
                                    <td style="text-align: center; width: 80px">
                                        <asp:Image ID="imgPrioridad" runat="server" ImageUrl="~/img/flag_green.png" Visible="false" />
                                    </td>
                                    <td style="text-align: center; width: 100px">
                                        <asp:Label ID="lbCantidadPagar" runat="server" Width="80%" Text='<%# Eval("CantidadPagar","{0:0,0.00}") %>'></asp:Label> 
                                    </td>
                                    <td style="text-align: center; width: 40px">
                                        <asp:ImageButton ID="imgbtnVer" runat="server" ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' CausesValidation="false" />
                                    </td>
                                    <td style="text-align :center;width:80px">
                                        <asp:ImageButton  ID ="btnInactivo" runat ="server" ImageUrl ="~/img/Seleccionar.png" Visible ="true"  CommandArgument='<%# Eval("IdSolicitud")%>' CommandName ="btnInactivo"/>
                                        <asp:ImageButton  ID ="btnActivo" runat ="server" ImageUrl ="~/img/action_check.png" Visible ="false" CommandArgument='<%# Eval("IdSolicitud")%>' CommandName ="btnActivo"  />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </fieldset>
</asp:Content>
