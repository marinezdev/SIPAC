<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_PagoSolicitud.aspx.cs" Inherits="cxpcxc.trf_PagoSolicitud" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            return resultado; 
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField  ID="hdIdSol" runat="server"/>
    <asp:HiddenField ID="hdTotalPagos" runat ="server" />
    <fieldset>
        <legend>SOLICITUD DE TRANSFERENCIA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 80%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:ImageButton ID="btnFactura" ImageUrl ="~/img/invoice_i.png" runat="server" CausesValidation="false" ToolTip="Factura" OnClick="btnFactura_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br />
        <table id="tblContenido" style ="width:100%">
            <tr>
                <td style ="width:50%;vertical-align:top">
                    <table id ="tblSol" runat ="server" style ="width:98%; margin :0 auto">
                        <tr>
                            <td colspan ="2" class="Titulos" ><asp:Label ID="lbBeneficiario" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td  style ="width:25%"><b>Fecha Factura:</b></td>
                            <td><asp:Label  ID="lbFhFactura" runat ="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Factura:</b></td>
                            <td><asp:Label ID="lbFactura" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Importe:</b></td>
                            <td><asp:Label ID="lbImporte" runat ="server"></asp:Label></td>
                        </tr>
                        <tr><td style="height :10px"></td></tr>
                        <tr>
                            <td><b>Concepto:</b></td>
                            <td><asp:Label  ID="lbConcepto" runat ="server"></asp:Label></td>
                        </tr>
                        <tr><td colspan ="2" style="height :10px"></td></tr>
                        <tr>
                            <td><b>Banco:</b></td>
                            <td><asp:Label ID="lbBanco" runat ="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Cuenta:</b></td>
                            <td><asp:Label ID="lbCuenta" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Cuenta Clabe:</b></td>
                            <td><asp:Label ID="lbClabe" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Sucursal:</b></td>
                            <td><asp:Label ID="lbSucursal" runat ="server" ></asp:Label></td>
                        </tr>
                        <tr><td colspan ="2" style="height :10px"></td></tr>
                        <tr>
                            <td><b>Condiciones de pago:</b></td>
                            <td><asp:Label ID="lbCodPago" runat ="server"></asp:Label></td>
                        </tr>
                         <tr>
                            <td><b>Proyecto:</b></td>
                            <td><asp:Label ID="lbProyecto" runat ="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Descripcion:</b></td>
                            <td><asp:Label ID="lbDecProyecto" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Moneda:</b></td>
                            <td><asp:Label ID="lbMoneda" runat ="server"></asp:Label></td>
                        </tr>
                    </table><br />
                
                    <asp:Panel ID ="pnPagos" runat ="server"  Width ="100%">
                        <h3 style ="color:green">COMPROBANTES DE PAGO</h3>
                        <asp:Repeater ID="rptComprobantes" runat="server" OnItemCommand="rptComprobantes_ItemCommand" >
                            <HeaderTemplate>
                                <table id="tblSol" style ="width :75%"  class ="tblFiltrar" >
                                    <thead>
                                        <th scope="col">FECHA</th>
                                        <th scope="col">CANTIDAD</th>
                                        <th scope="col">TIPO CAMBIO</th>
                                        <th scope="col">PESOS</th>
                                        <th scope="col"></th>
                                    </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("FechaRegistro","{0:d}")%></td>
                                    <td><%# Eval("Cantidad")%></td>
                                    <td><%# Eval("TipoCambio")%></td>
                                    <td><%# Eval("Pesos")%></td>
                                    <td style="text-align :center;width:80px">
                                        <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CausesValidation="false" CommandName="ver" CommandArgument='<%# Eval("IdDocumento")%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater><br />
                        <table id ="tblNotaPago" style ="width:100%; margin :0 auto">
                            <tr>
                                <td style="width:25%"><strong>Notas de Pago:</strong></td>
                                <td><asp:Label  ID ="lbNotaPago" runat ="server" Width ="100%"></asp:Label> </td>
                            </tr>
                        </table>
                    </asp:Panel><br /><br />
                    <asp:Panel ID="pnUlArchivos" runat ="server"  Width="100%" >
                        <h5 class ="Titulos" > REGISTRAR COMPROBANTE</h5>
                        <asp:Label runat ="server" Text ="Comprobante (PDF) Maximo 2 MB:"></asp:Label><br />
                        <asp:FileUpload ID="fulComprobante" runat ="server" AllowMultiple ="true" />
                        <asp:RequiredFieldValidator ID="rfvfulComprobante" runat="server" ControlToValidate ="fulComprobante" ErrorMessage="*" ForeColor ="Red"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator runat="server" ID="revComprobante" ControlToValidate="fulComprobante"
                                    ErrorMessage="*" ForeColor ="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$" />--%>        
                        <asp:Button ID ="btnAnexaComprobante" runat ="server" Text ="Aceptar" CssClass="button" OnClientClick ="return Confirmar();" OnClick="btnAnexaComprobante_Click" />
                    </asp:Panel>
                </td>
                <td style ="vertical-align:top">
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="700px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
                        <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                    </asp:Panel>
                </td>
            </tr>    
          </table>
    </fieldset>
</asp:Content>
