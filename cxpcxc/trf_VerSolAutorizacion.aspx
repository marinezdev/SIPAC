<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_VerSolAutorizacion.aspx.cs" Inherits="cxpcxc.trf_VerSolAutorizacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function ConfirmarRechazo() {
            var resultado = false;
            if (Page_ClientValidate() == true) {resultado= confirm('¿Esta seguro que desea RECHAZAR la Solicitud ?'); }
            return resultado;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField  ID="hdIdSol" runat="server"  />
    <asp:HiddenField  ID="hdIdEmpresa" runat="server"  />
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
                    <asp:ImageButton ID="btnFactura" ImageUrl ="~/img/invoice_i.png" runat="server"  CausesValidation="false" ToolTip="Factura" OnClick="btnFactura_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnRechazar" runat ="server"  Text ="RECHAZAR" CssClass="button" />&nbsp;&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br/>
        <table  id ="tblContenedor" style ="width :100%;">
            <tr>
                <td style ="width :45%;vertical-align :top ">
                    <div id="dvSolicitud" runat ="server">
                        <table id ="tblSol" runat ="server" style ="width:100%; margin :0 auto; font-size :14px" >
                            <tr>
                                <td colspan ="4" class="Titulos" >
                                    <h4><asp:Label ID="lbBeneficiario" runat ="server"  Width ="95%"  Font-Bold="true"></asp:Label></h4>
                                </td>
                            </tr>
                            <tr>
                                <td style ="width:25%"><b>Factura:</b></td>
                                <td colspan ="3"><asp:Label ID="lbFactura" runat ="server" font-size="14px" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Fecha Factura:</b></td>
                                <td><asp:Label  ID="lbFhFactura" runat ="server" ></asp:Label></td>
                                <td style ="width:20%"><b>Importe:</b></td>
                                <td><asp:Label ID="lbImporte" runat ="server"  Width ="25%" ></asp:Label></td>
                            </tr>
                            <tr><td colspan ="4" style="height :10px"></td></tr>
                            <tr>
                                <td><b>Concepto:</b></td>
                                <td colspan ="3"><asp:Label  ID="lbConcepto" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr>
                            <tr><td colspan ="4" style="height :10px;"></td></tr>
                            <tr>
                                <td><b>Banco:</b></td>
                                <td><asp:Label ID="lbBanco" runat ="server"  Width ="90%" ></asp:Label></td>
                                <td><b>Cuenta:</b></td>
                                <td><asp:Label ID="lbCuenta" runat ="server"  Width ="90%" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Cuenta Clabe:</b></td>
                                <td><asp:Label ID="lbClabe" runat ="server"  Width ="90%" ></asp:Label></td>
                                <td><b>Sucursal:</b></td>
                                <td><asp:Label ID="lbSucursal" runat ="server"  Width ="90%"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan ="4" style="height :10px"></td></tr>
                            <tr>
                                <td><b>Condiciones de pago:</b></td>
                                <td colspan ="3"><asp:Label ID="lbCodPago" runat ="server" Width ="95%"  ></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Proyecto:</b></td>
                                <td colspan ="3"><asp:Label ID="lbProyecto" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Descripcion:</b></td>
                                <td colspan ="3"><asp:Label ID="lbDecProyecto" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Moneda:</b></td>
                                <td colspan ="3"><asp:Label ID="lbMoneda" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style ="vertical-align:top">
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="600px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
                        <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        
         <asp:Panel ID="pnPopRechazo" runat="server" Style="display: none;" Width="550px"  CssClass="modalPopup" >
            <table id="tblRechazo" style="width:100%; margin:auto;" >
                <tr><td colspan ="2" class ="Titulos" >RECHAZAR</td></tr>
                <tr><td style ="height :15px"></td></tr>
                <tr>
                    <td style ="width :13%;"><strong>MOTIVO:</strong></td>
                    <td>
                        <asp:DropDownList ID="dpRechazo" runat ="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvRechazo" runat="server" ControlToValidate="dpRechazo" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr><td style ="height :15px"></td></tr>
                <tr>
                    <td colspan="2" style="text-align:right">
                        <asp:Button ID="btnAceptaRechazo" runat="server" Text="Aceptar" CssClass="button" OnClientClick ="return ConfirmarRechazo();" OnClick="btnAceptaRechazo_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" CausesValidation="false"  />
                    </td>
                </tr>
            </table>
        </asp:Panel> 

        <ajaxToolkit:ModalPopupExtender ID="mpeRechazo" runat="server"
            BackgroundCssClass="modalBackground"
            DropShadow="true"
            BehaviorID="mpeRechazo"
            TargetControlID="btnRechazar"
            PopupControlID="pnPopRechazo"
            CancelControlID="btnCancelar"
            x="250">
        </ajaxToolkit:ModalPopupExtender>
    </fieldset>
</asp:Content>
