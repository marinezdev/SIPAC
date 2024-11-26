<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_SolConFacturaValida.aspx.cs" Inherits="cxpcxc.trf_SolConFacturaValida" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdNomArchivo" runat ="server" />
    <asp:HiddenField ID="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID="hdLLaveSol" runat ="server" />
    <fieldset>
        <legend>REGISTRAR SOLICITUD DE TRANSFERENCIA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 75%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CausesValidation="false" CssClass="button" OnClick="btnContinuar_Click" Visible ="false" />&nbsp;
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
                <tr><td colspan ="2" style =" text-align:right"><asp:Button ID="btnCargar" runat ="server" Text ="Cargar" CssClass="button" OnClick="btnCargar_Click" CausesValidation ="false" /></td></tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnDatosXml" runat ="server" Width ="100%" Visible ="false" Font-Size ="12px" >
            <h5 class="Titulos" >DATOS DEL XML</h5>
            <table style =" width:100%" id ="tblDatosXml" >
                <tr>
                    <td style ="width:50%">
                        <b>EMISOR:</b><br /><br />
                        <table id="tblEmisor" runat ="server" style ="width:90%; margin :0 auto;" >
                            <tr>
                                <td colspan ="2"   style="text-align :center;color:brown"  >
                                    <b><asp:Label ID="lbProveedor" runat ="server"></asp:Label></b><hr />
                                </td>
                            </tr>
                            <tr>
                                <td style ="width:20%"><b>RFC:</b></td>
                                <td>
                                    <asp:Label ID="lbRfc" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr><td colspan ="2" style ="height :10px"></td></tr>
                            <tr>
                                <td><b>Factura:</b></td>
                                <td>
                                    <asp:Label ID="lbFactura" runat ="server" Font-Size ="14px" width ="95%" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>Fecha:</b></td>
                                <td>
                                    <asp:Label  ID="lbFhFactura" runat ="server" width ="95%" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>Importe:</b></td>
                                <td>
                                    <asp:Label  ID="lbImporte" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr><td style ="height :10px"></td></tr>
                            <tr>
                                <td style ="vertical-align :top;"><b>Concepto:</b></td>
                                <td>
                                    <asp:Label  ID="lbConcepto" runat ="server" Width ="95%" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style ="vertical-align:top"><b>RECEPTOR:</b><br /><br />
                        <table id ="tblReceptor" runat ="server" style ="width:90%; margin :0 auto;" >
                            <tr>
                                <td colspan ="2"   style="text-align :center;color:brown"   >
                                    <b><asp:Label ID="lbReceptor" runat ="server"></asp:Label></b><hr />
                                </td>
                            </tr>
                            <tr>
                                <td style ="width:20%"><b>RFC:</b></td>
                                <td>
                                    <asp:Label ID="lbReceptorRfc" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <h6 class="Titulos"> FACTURA</h6>
            <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="400px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
            </asp:Panel>
        </asp:Panel>
    </fieldset>
</asp:Content>
