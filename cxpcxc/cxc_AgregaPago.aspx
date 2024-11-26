<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_AgregaPago.aspx.cs" Inherits="cxpcxc.cxc_AgregaPago" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            if (resultado) { $("#dvBtnsMod").hide(); }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    <asp:HiddenField ID ="hdAnexarCop" runat ="server"  />
        <asp:MultiView  ID ="mtvContenedor" runat ="server" ActiveViewIndex ="0" >
            <asp:View  Id="vwDatos" runat ="server" >
                <fieldset>
                    <legend>ORDEN DE FACTURA</legend>
                    <table id="tblBtnsDatos" runat="server" style="width: 100%">
                        <tr>
                            <td style="width: 80%; text-align: center; color: red;">
                                <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: right;">
                                <asp:ImageButton  ID="imgBtDocumento" runat="server"  ImageUrl ="~/img/invoice_i.png"  CausesValidation="false" OnClick="imgBtDocumento_Click" />
                                &nbsp;<asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                            </td>
                        </tr>
                    </table><br />
                
                    <div id ="dvDatos" runat ="server" >  
                        <table id ="tblDatos" runat ="server" style ="width :100%;" >
                            <tr>
                                <td colspan ="4" class ="Titulos"><asp:Label ID="lbCliente" runat ="server" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td  style ="width:20%"><b>Orden servicio:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbOrdServicio" runat ="server" Font-Size="Medium"> </asp:Label></td>
                            </tr>
                            <tr>
                                <td> <b>Orden factura:</b></td>
                                <td colspan ="3"><asp:Label  ID="lbOrdFactura" runat ="server" Font-Size="Medium"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style ="width:20%"><b>Empresa facturación:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbEmpresa" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Fecha factura:</b></td>
                                <td><asp:Label ID ="lbFhFactura" runat ="server"></asp:Label></td>
                                <td style ="width:20%"> <b>No. Factura:</b></td>
                                <td><asp:Label  ID="lbNoFactura" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Tipo de Solicitud:</b></td>
                                <td><asp:Label ID ="lbTpSolicitud" runat ="server"></asp:Label></td>
                                <td style ="width:20%"> <b>Importe:</b></td>
                                <td><asp:Label  ID="lbImporte" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style ="width:20%"><b>Condiciones pago:</b></td>
                                <td><asp:Label ID ="lbCodPago" runat ="server"></asp:Label></td>
                                <td><b>Tipo Moneda:</b></td>
                                <td><asp:Label ID ="lbMoneda" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style ="vertical-align :top"><b>Descripción:</b></td>
                                <td colspan ="3"><asp:Label ID="lbDescripcion" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td style ="vertical-align :top"><b>Anotaciones:</b></td>
                                <td colspan ="3"><asp:Label ID="lbAnotaciones" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr>
                            <tr><td style ="height :15px"></td></tr>
                        </table>
                    </div>
                    <table style ="width :100%" >
                        <tr>
                            <td  style ="width :35%">
                                <asp:Panel ID ="pnPagos" runat ="server"  Width ="100%">    
                                    <asp:Repeater ID="rptPagos" runat="server" OnItemCommand="rptPagos_ItemCommand" >
                                        <HeaderTemplate>
                                            <table id="tblPagos" border="1" style ="width :300px"  class ="tblFiltrar" >
                                                <caption style="text-align :center;font-weight:700" >COMPROBANTES</caption>
                                                <thead>
                                                    <th scope="col">FECHA</th>
                                                    <th scope="col"></th>
                                                </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: White; color: #333333">
                                                <td><%# Eval("FechaRegistro")%></td>
                                                <td style="text-align :center;width:40px">
                                                    <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdDocumento")%>' CausesValidation ="false"  />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate></tbody></table></FooterTemplate>
                                    </asp:Repeater><br />
                                </asp:Panel>
                            </td>  
                             <td>
                                <asp:Panel ID ="pnRegComprobante"  runat ="server" Width ="100%" Visible="false" >
                                    <table id="tblRegPago" runat ="server"  style ="width:100%; margin :0 auto;">
                                        <tr><td colspan ="2" style="color: #FFFFFF; background-color: #4972B5;text-align :center" > REGISTRAR COMPROBANTE PAGO</td></tr>
                                        <tr><td style ="height:8px"></td></tr>
                                        <tr>
                                            <td>Comprobante (PDF) Maximo 2 MB:</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="fulComprobante" runat ="server" AllowMultiple ="true" Width="90%"/>
                                                <asp:RequiredFieldValidator ID="rfvfulComprobante" runat="server" ControlToValidate ="fulComprobante" ErrorMessage="*" ForeColor ="Red"></asp:RequiredFieldValidator>
                                                <%--<asp:RegularExpressionValidator runat="server" ID="revComprobante" ControlToValidate="fulComprobante"
                                                    ErrorMessage="*" ForeColor ="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$" />        --%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan ="2" style ="text-align :right">
                                                <asp:Button ID="btnRegPago" runat ="server"  Text ="Aceptar" CssClass="button" OnClientClick ="return Confirmar();" OnClick="btnRegPago_Click" />
                                            </td>
                                        </tr>
                                    </table><br />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    
                </fieldset> 
            </asp:View>
            <asp:View ID ="vwDocumento"  runat ="server" >
                <fieldset>
                    <legend>FACTURA</legend>
                    <div id="dvDocumento" style="width: 100%; text-align: right;">
                        <asp:Button ID="btnCierraDocumento" runat="server" CssClass="button" Text="X" CausesValidation="False" OnClick="btnCierraDocumento_Click" />
                    </div>
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="630px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
                        <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                    </asp:Panel>
                </fieldset>
            </asp:View>
        </asp:MultiView> 
</asp:Content>
