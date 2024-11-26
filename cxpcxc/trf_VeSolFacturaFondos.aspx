<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_VeSolFacturaFondos.aspx.cs" Inherits="cxpcxc.trf_VeSolFacturaFondos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript" >
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField  ID="hdIdSol" runat="server"  />
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
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table>
        <table id ="tblContenedor" runat ="server" style ="width:100%" >
            <tr>
                <td style ="width :45%; vertical-align :top ">
                
                    <asp:Panel ID="pnRechazo" runat ="server" Visible ="false" >
                        <h5 style ="background-color :red;color :white">SOLICITUD RECHAZADA</h5>
                        <asp:label ID="lbMotivoRechazo"  runat ="server" Font-Size ="14px" Font-Bold ="true" style="font-size:14px"></asp:label>
                    </asp:Panel><br />
        
                    <table id ="tblSol" runat ="server" style ="width:98%; margin :0 auto;" >
                        <tr>
                            <td colspan ="2" class="Titulos" >
                                <h4><asp:Label ID="lbProveedor" runat ="server"  Width ="95%" ></asp:Label></h4>
                            </td>
                        </tr>
                        <tr>
                            <td style ="width:20%;font-weight: 700;">RFC:</td>
                            <td><asp:Label ID="lbRfc" runat ="server" font-size="14px" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Factura:</td>
                            <td><asp:Label ID="lbFactura" runat ="server" font-size="14px" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Fecha Factura:</td>
                            <td><asp:Label  ID="lbFhFactura" runat ="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Importe:</td>
                            <td><asp:Label ID="lbImporte" runat ="server"  Width ="25%" ></asp:Label></td>
                        </tr>
                        <tr><td colspan ="4" style="height :10px"></td></tr>
                        <tr>
                            <td style ="vertical-align :top;font-weight: 700;">Concepto:</td>
                            <td colspan ="3"><asp:Label  ID="lbConcepto" runat ="server" Width ="95%"></asp:Label></td>
                        </tr>
                        <tr><td colspan ="4" style="height :10px"></td></tr>
                        <tr>
                            <td style ="font-weight: 700;">Banco:</td>
                            <td><asp:Label ID="lbBanco" runat ="server"  Width ="90%" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Cuenta:</td>
                            <td><asp:Label ID="lbCuenta" runat ="server"  Width ="90%"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Cuenta Clabe:</td>
                            <td><asp:Label ID="lbClabe" runat ="server"  Width ="90%"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Sucursal:</td>
                            <td><asp:Label ID="lbSucursal" runat ="server"  Width ="90%"></asp:Label></td>
                        </tr>
                        <tr><td style="height :10px"></td></tr>
                        <tr>
                            <td style ="font-weight: 700;">Condiciones de pago:</td>
                            <td><asp:Label ID="lbCodPago" runat ="server" Width ="95%" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td  style ="font-weight: 700;">Proyecto:</td>
                            <td colspan ="3"><asp:Label ID="lbProyecto" runat ="server" Width ="95%" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td  style ="font-weight: 700;">Descripcion:</td>
                            <td ><asp:Label ID="lbDecProyecto" runat ="server" Width ="95%" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Moneda:</td>
                            <td ><asp:Label ID="lbMoneda" runat ="server" Width ="95%" ></asp:Label></td>
                        </tr>
                    </table><br />
                </td>
                <td>
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="630px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
                        <asp:Literal ID="ltDocumento" runat="server" Text ="SIN DOCUMENTO" ></asp:Literal>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </fieldset> 
</asp:Content>

