<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_AgregarFacturas.aspx.cs" Inherits="cxpcxc.cxc_AgregarFacturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript" >
        function Confirmar(){
            var resultado = false;
            if (confirm('¿Esta seguro que desea REGISTRAR la factura ?')){
                $("#dvBtns").hide(); 
                resultado=true ;
            }
            return resultado;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID ="hdArchivoOrg" runat ="server" />
    <asp:HiddenField ID ="hdRutaArchPdf" runat ="server" />
    <asp:HiddenField ID ="hdRutaArchXML" runat ="server" />
    <asp:HiddenField ID ="hdCFD" runat ="server" />
    <fieldset>
        <legend>INGRESAR ARCHIVOS DE FACTURACION</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 75%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <div id="dvBtns" style="width:100%; text-align:right;">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CausesValidation="false" CssClass="button" Visible ="false" OnClick="btnRegistrar_Click" OnClientClick="return Confirmar();" />&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" CssClass="button" OnClick="btnCancelar_Click"/>
                    </div> 
               </td>
            </tr>
        </table><br />
        <table style ="width:100%">
            <tr>
                <td style ="vertical-align:top; width :50%">
                    <table id ="tblDatos" runat ="server" style ="width:95%;background-color: #F3F9FE; margin :0 auto">
                        <tr>
                            <td  style ="width:20%"><b>Orden servicio:</b></td>
                            <td><asp:Label ID ="lbOrdServicio" runat ="server" Font-Size="Medium"> </asp:Label></td>
                        </tr>
                        <tr>
                            <td> <b>Orden factura:</b></td>
                            <td><asp:Label  ID="lbOrdenFactura" runat ="server" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan ="2" class ="Titulos">
                                <asp:Label ID="lbOrdRfc" runat ="server" ></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lbOrdCliente" runat ="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style ="width:20%"><b>Empresa facturación:</b></td>
                            <td><asp:Label ID ="lbOrdEmpresa" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="width:20%"><b>Fecha:</b></td>
                            <td><asp:Label ID ="lbFechaInicio" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td> <b>Importe :</b></td>
                            <td><asp:Label  ID="lbOrdImporte" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Tipo Moneda:</b></td>
                            <td><asp:Label ID ="lbOrdMoneda" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Tipo de Solicitud:</b></td>
                            <td><asp:Label ID ="lbOrdTpSolicitud" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Condiciones pago:</b></td>
                            <td><asp:Label ID ="lbOrdCodPago" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="vertical-align :top"><b>Anotacion:</b></td>
                            <td><asp:Label ID="lbOrdAnotaciones" runat ="server" Width ="95%" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan ="2">
                                <asp:CheckBox ID ="chkEvioCliente"  runat ="server" Text="Enviar a Cliente" Enabled ="false"/> 
                            </td>
                        </tr>
                    </table>
                </td>
                <td style ="vertical-align :top">
                    <asp:Panel Id="pnCargaArchivo" runat ="server" Width ="100%" >
                        <table style ="width :90%;margin: 0 auto" > 
                            <tr>
                                <td colspan="2" class="Titulos" >AGREGUE LOS ARCHIVOS DE LA FACTURA </td>
                            </tr>
                            <tr>
                                <td style ="width:20%">Factura (PDF):</td>
                                <td><asp:FileUpload ID="fulFactura" runat="server" Width="80%"  />
                                    <asp:RequiredFieldValidator ID="rfvfulFactura" runat="server" ControlToValidate ="fulFactura" ErrorMessage="*" ForeColor ="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="revFactura" ControlToValidate="fulFactura"
                                                    ErrorMessage="*" ForeColor ="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$" />
                                </td>
                            </tr>
                            <tr>
                                <td>Archivo (XML):</td>
                                <td><asp:FileUpload ID="fulXml" runat="server" Width="80%" />
                                    <asp:RequiredFieldValidator ID="rfvfulXml" runat="server" ControlToValidate ="fulXml" ErrorMessage="*" ForeColor ="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="revXml" ControlToValidate="fulXml"
                                                    ErrorMessage="*" ForeColor ="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xml|.XML)$" />
                                </td>
                            </tr>
                            <tr><td colspan ="2" style =" text-align:right"><asp:Button ID="btnCargar" runat ="server" Text ="Cargar" CssClass="button" OnClick="btnCargar_Click" CausesValidation ="false" /></td></tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnDatosXml" runat ="server" Visible ="false"  >
                        <h5 class="Titulos" > DATOS DEL XML</h5>
                        <table id ="tblDatosXml" runat ="server" style ="width:95%; margin :0 auto;" >
                            <tr>
                                <td colspan ="2" class ="SubTitulos" >
                                    <asp:Label ID="lbEmpresa" runat ="server"  ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style ="width:20%;font-weight: 700;">Rfc:</td>
                                <td><asp:Label ID="lbRfc" runat ="server"  Width ="95%" ></asp:Label></td>
                            </tr>
                            <tr><td colspan ="2" style ="height :10px"></td></tr>
                            <tr>
                                <td style ="font-weight: 700;">Factura:</td>
                                <td><asp:Label ID="lbFactura" runat ="server" Font-Size ="14px" width ="95%" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td style ="font-weight: 700;">Fecha Factura:</td>
                                <td><asp:Label  ID="lbFhFactura" runat ="server" width ="95%" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td style ="font-weight: 700;">Importe:</td>
                                <td><asp:Label  ID="lbImporte" runat ="server" ></asp:Label></td>
                            </tr>
                            <tr><td style ="height :10px"></td></tr>
                            <tr>
                                <td><b>Concepto:</b></td>
                                <td><asp:Label  ID="lbConcepto" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan ="2" class ="SubTitulos ">
                                    <b><asp:Label ID="lbCliente" runat ="server"  Width ="95%"  ></asp:Label></b>
                                </td>
                            </tr>
                            <tr>
                                <td><b>Rfc:</b></td>
                                <td><asp:Label  ID="lbRfcCliente" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnFactura"  runat ="server"  Visible ="false"  >
            <h5 class="Titulos"> FACTURA</h5>
            <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="500px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
            </asp:Panel>
        </asp:Panel>
    </fieldset> 
</asp:Content>
