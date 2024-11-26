<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AgregaArchivosSinFactura.aspx.cs" Inherits="cxpcxc.trf_AgregaArchivosSinFactura" %>
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
    <asp:HiddenField ID="hdNomArchivo" runat ="server" />
    <asp:HiddenField ID="hdIdSol" runat ="server" />
    <asp:HiddenField ID="hdLLaveSol" runat ="server" />
    <fieldset>
        <legend>AGREGAR ARCHIVOS DE LA FACTURA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 75%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CausesValidation="false" CssClass="button" Visible ="false" OnClick="btnAceptar_Click" />&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" CssClass="button" OnClick="btnCancelar_Click"/>
               </td>
            </tr>
        </table><br />
        <asp:Panel Id="pnCargaArchivo" runat ="server" Width ="100%" >
            <table style ="width :80%;margin: 0 auto">
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
                <tr><td colspan ="2" style =" text-align:right"><asp:Button ID="btnCargar" runat ="server" Text ="Cargar" CssClass="button" OnClick="btnCargar_Click" CausesValidation ="false" OnClientClick ="return Confirmar();" /></td></tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnDatosXml" runat ="server" Width ="100%" Visible ="false" Font-Size ="12px" >
            <table style ="width:100%" >
                <tr>
                    <td style ="width :50%" class="Titulos"> DATOS SOLICITUD REGISTRADA</td>
                    <td class="Titulos">DATOS DEL XML</td>
                </tr>
                <tr>
                    <td>
                        <table id ="tblSol" runat ="server" style ="width:95%; margin :0 auto;background-color: #DFFFEF" >
                            <tr>
                                <td colspan ="2" class="SubTitulos" >
                                    <b><asp:Label ID="lbOrgProveedor" runat ="server"></asp:Label></b>
                                </td>
                            </tr>
                            <tr>
                                <td style ="width :25%"><b>RFC:</b></td>
                                <td ><asp:Label  ID="lbOrgRfc" runat ="server" ></asp:Label></td>
                            </tr>
                            <tr><td colspan ="2" style ="height :10px"></td></tr>
                            <tr>
                                <td><b>Factura:</b></td>
                                <td><asp:Label ID="bOrgFactura" runat ="server"  font-size="14px" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Fecha Factura:</b></td>
                                <td><asp:Label  ID="lbOrgFhFactura" runat ="server"  ></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Importe:</b></td>
                                <td><asp:Label ID="lbOrgImporte" runat ="server"></asp:Label></td>
                            </tr>
                            <tr><td style ="height :10px"></td></tr>
                            <tr>
                                <td style ="vertical-align :top;"><b>Concepto:</b></td>
                                <td><asp:Label  ID="lbOrgConcepto" runat ="server"></asp:Label></td>
                            </tr>
                         </table>
                    </td>
                    <td>
                       <table id ="tblDatosXml" runat ="server" style ="width:95%; margin :0 auto;background-color:#F3F9FE" >
                            <tr>
                                <td colspan ="2" class ="SubTitulos">
                                    <b><asp:Label ID="lbProveedor" runat ="server" /></b>
                                </td>
                            </tr>
                            <tr>
                                <td style ="width:25%"><b>RFC:</b></td>
                                <td><asp:Label ID="lbRfc" runat ="server"></asp:Label></td>
                            </tr>
                            <tr><td colspan ="2" style ="height :10px"></td></tr>
                            <tr>
                                <td><b>Factura:</b></td>
                                <td><asp:Label ID="lbFactura" runat ="server" Font-Size ="14px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Fecha Factura:</b></td>
                                <td><asp:Label  ID="lbFhFactura" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Importe:</b></td>
                                <td><asp:Label  ID="lbImporte" runat ="server" ></asp:Label></td>
                            </tr>
                            <tr><td style ="height :10px"></td></tr>
                            <tr>
                                <td style ="vertical-align :top"><b>Concepto:</b></td>
                                <td><asp:Label  ID="lbConcepto" runat ="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
                        
            <h5 class="Titulos"> FACTURA</h5>
            <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="500px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
            </asp:Panel>
        </asp:Panel>
    </fieldset>
</asp:Content>
