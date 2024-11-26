<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AutorizaSolicitud.aspx.cs" Inherits="cxpcxc.trf_AutorizaSolicitud" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            return confirm('¿Esta seguro que desea AUTORIZAR la Solicitud ?');
        }

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
                    <asp:ImageButton ID="btnFactura" ImageUrl ="~/img/invoice_i.png" runat="server"  CausesValidation="false" ToolTip="Factura" OnClick="btnFactura_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br/>
        <div id="dvSolicitud" runat ="server">
            <table id ="tblSol" runat ="server" style ="width:90%; margin :0 auto; font-size :14px" >
                <tr>
                    <td colspan ="4" class="Titulos" >
                        <h4><asp:Label ID="lbBeneficiario" runat ="server"  Width ="95%"  Font-Bold="true"></asp:Label></h4>
                    </td>
                </tr>
                <tr>
                    <td style ="width:25%;font-size:14px; font-weight: 700;">Factura:</td>
                    <td colspan ="3">
                        <asp:Label ID="lbFactura" runat ="server" font-size="14px" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Fecha Factura:</td>
                    <td>
                        <asp:Label  ID="lbFhFactura" runat ="server" ></asp:Label>
                    </td>
                    <td style ="width:20%;font-weight: 700;">Importe:</td>
                    <td>
                        <asp:Label ID="lbImporte" runat ="server"  Width ="25%" ></asp:Label>
                    </td>
                </tr>
                <tr><td colspan ="4" style="height :10px;font-weight: 700;"></td></tr>
                <tr>
                    <td style ="vertical-align :top;font-weight: 700">Concepto:</td>
                    <td colspan ="3">
                        <asp:Label  ID="lbConcepto" runat ="server" Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr><td colspan ="4" style="height :10px;"></td></tr>
                <tr>
                    <td style ="font-weight: 700;">Banco:</td>
                    <td>
                        <asp:Label ID="lbBanco" runat ="server"  Width ="90%" ></asp:Label>
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
                <tr style ="visibility :hidden ;font-weight: 700;">
                    <td>Importe Con letra:</td>
                    <td colspan ="3">
                        <asp:Label ID="lbImpLetra" runat ="server" Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Condiciones de pago:</td>
                    <td colspan ="3">
                        <asp:Label ID="lbCodPago" runat ="server" Width ="95%"  ></asp:Label>
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
                        <asp:Button ID="btnRechazar" runat ="server"  Text ="RECHAZAR" CssClass="button" />&nbsp;&nbsp;
                        <asp:Button ID="btnAutorizar" runat ="server"  Text ="AUTORIZAR" CssClass="button" OnClick="btnAutorizar_Click" OnClientClick ="return Confirmar();" CausesValidation="false" Visible="false"/>
                    </td>
                </tr>
            </table>
        </div>
         <asp:Panel ID="pnPopRechazo" runat="server" Style="display: none ;"  CssClass="modalPopup" >
            <table id="tblRechazo" style="width:100%; margin:auto;" >
                <tr><td colspan ="2" class ="Titulos" >RECHAZAR</td></tr>
                <tr><td style ="height :15px"></td></tr>
                <tr>
                    <td style ="width :20%;"><strong>MOTIVO:</strong></td>
                    <td>
                        <asp:DropDownList ID="dpRechazo" runat ="server" Width ="450px"></asp:DropDownList>
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
            CancelControlID="btnCancelar">
        </ajaxToolkit:ModalPopupExtender>
    </fieldset>
</asp:Content>
