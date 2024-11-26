<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_CapturaSolicitud.aspx.cs" Inherits="cxpcxc.trf_CapturaSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            return confirm('¿Esta seguro que desea Continuar?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField  ID="hdIdSol" runat="server"  />
    <fieldset>
        <legend>SOLICITUD DE TRANSFERENCIA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:ImageButton ID="btnFactura" ImageUrl ="~/img/invoice_i.png" runat="server" ToolTip="Factura" OnClick="btnFactura_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br/>
        <div id="dvSolicitud" runat ="server">
            <table id ="tblSol" runat ="server" style ="width:90%; margin :0 auto; font-size :14px" >
                <tr>
                    <td colspan ="4" class="Titulos">
                        <h4><asp:Label ID="lbBeneficiario" runat ="server"  Width ="95%"  Font-Bold="true"></asp:Label></h4>
                    </td>
                </tr>
                <tr>
                    <td style ="width:25%;font-weight: 700;">Factura:</td>
                    <td colspan ="3">
                        <asp:Label ID="lbFactura" runat ="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Fecha Factura:</td>
                    <td>
                        <asp:Label  ID="lbFhFactura" runat ="server" ></asp:Label>
                    </td>
                    <td style ="width :20% ;font-weight: 700;">Importe:</td>
                    <td>
                        <asp:Label ID="lbImporte" runat ="server"  Width ="25%" ></asp:Label>
                    </td>
                </tr>
                <tr><td colspan ="4" style="height :10px"></td></tr>
                <tr>
                    <td style ="vertical-align:top ;font-weight: 700;">Concepto:</td>
                    <td colspan ="3">
                        <asp:Label  ID="lbConcepto" runat ="server" Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr><td colspan ="4" style="height :10px"></td></tr>
                <tr>
                    <td style ="font-weight: 700;">Banco:</td>
                    <td>
                        <asp:Label ID="lbBanco" runat ="server"  Width ="90%"  ></asp:Label>
                    </td>
                    <td style ="font-weight: 700;">Cuenta:</td>
                    <td>
                        <asp:Label ID="lbCuenta" runat ="server"  Width ="90%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Cuenta Clabe:</td>
                    <td>
                        <asp:Label ID="lbClabe" runat ="server"  Width ="90%" ></asp:Label>
                    </td>
                    <td style ="font-weight: 700;">Sucursal:</td>
                    <td>
                        <asp:Label ID="lbSucursal" runat ="server"  Width ="90%"></asp:Label>
                    </td>
                </tr>
                <tr><td colspan ="4" style="height :10px"></td></tr>
                <tr>
                    <td style ="font-weight: 700;">Condiciones de pago:</td>
                    <td colspan ="3">
                        <asp:Label ID="lbCodPago" runat ="server" Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Proyecto:</td>
                    <td colspan ="3">
                        <asp:Label ID="lbProyecto" runat ="server" Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Descripcion:</td>
                    <td colspan ="3">
                        <asp:Label ID="lbDecProyecto" runat ="server" Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Moneda:</td>
                    <td colspan ="3">
                        <asp:Label ID="lbMoneda" runat ="server" Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan ="4" style ="text-align :right">
                        <asp:Button ID="btnCapturado" runat ="server"  Text ="Aceptar" CssClass="button" OnClientClick ="return Confirmar();" OnClick="btnCapturado_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
</asp:Content>
