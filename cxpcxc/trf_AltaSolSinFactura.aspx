<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AltaSolSinFactura.aspx.cs" Inherits="cxpcxc.trf_AltaSolSinFactura" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            if (resultado) { $("#dvBtns").hide(); }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:HiddenField ID ="hdIdProveedor"  runat ="server" />
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <fieldset>
        <legend>REGISTRAR SOLICITUD DE TRANSFERENCIA SIN FACTUTURA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 70%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <div id="dvBtns" style="width:100%; text-align:right;">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar"  CssClass="button" OnClick="btnGuardar_Click" OnClientClick ="return Confirmar();" />&nbsp;&nbsp;
                        <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                    </div>
               </td>
            </tr>
        </table><br />
        <asp:Panel ID="pnSolicitud" runat ="server" Width ="100%" Font-Size ="12px" >
            <table id ="tblSolicitud" runat ="server" style ="width:90%; margin :0 auto">
                <tr>
                    <td colspan ="4" class="Titulos">
                        <asp:Label ID="lbProveedor" runat ="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="width:15%"><b>RFC:</b></td>
                    <td colspan ="3" ><asp:Label ID="lbRfc" runat ="server"></asp:Label></td>
                </tr>
                <tr>
                    <td><b>Banco:</b></td>
                    <td><asp:Label  ID="lbBanco" runat ="server" ></asp:Label></td>
                    <td style ="width:15%"><b>Cuenta:</b></td>
                    <td><asp:Label  ID="lbCuenta" runat ="server" ></asp:Label></td>
                </tr>
                <tr>
                    <td><b>Cuenta Clabe:</b></td>
                    <td><asp:Label ID="lbClabe" runat ="server"></asp:Label></td>
                    <td><b>Sucursal:</b></td>
                    <td><asp:Label ID="lbSucursal" runat ="server"></asp:Label></td>
                </tr>
            </table><br />
            <table id ="tblSol" runat ="server" style ="width:90%; margin :0 auto">
                <tr>
                    <td style ="width:15%"><b>Fecha Factura:</b></td>
                    <td>
                        <asp:TextBox  ID="txFhFactura" runat ="server"  ></asp:TextBox>
                        <asp:ImageButton ID="ibtn_FhFactura" runat="server" CausesValidation="False" ImageUrl="~/img/calendario.png" />
                        <ajaxToolkit:CalendarExtender ID="ce_FhFactura" runat="server" TargetControlID="txFhFactura" PopupButtonID="ibtn_FhFactura" ClearTime="True" PopupPosition="Right" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfv_FhFactura" runat="server" ControlToValidate="txFhFactura" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><b>Importe:</b></td>
                    <td colspan ="3">
                        <asp:TextBox  ID="txImporte" runat ="server"  Width ="150px" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvImporte" runat="server" ControlToValidate="txImporte" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteImporte" runat="server" TargetControlID="txImporte" FilterMode="ValidChars" ValidChars="0123456789." />
                    </td>
                </tr>
                <tr>
                    <td><b>Concepto:</b></td>
                    <td colspan ="2">
                        <asp:TextBox  ID="txConcepto" runat ="server"  TextMode ="MultiLine" Width ="95%" Rows="5"  MaxLength ="255"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvConcepto" runat="server" ControlToValidate="txConcepto" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteConcepto" runat="server" TargetControlID="txConcepto" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                    </td>
                </tr>
                <tr>
                    <td><b>Condiciones de pago:</b></td>
                    <td colspan ="2">
                        <asp:DropDownList ID="dpCondPago" runat ="server" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dpCondPago" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><b>Proyecto:</b></td>
                    <td colspan ="2">
                        <asp:DropDownList ID="dpProyecto" runat ="server" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvf" runat="server" ControlToValidate="dpProyecto" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><b>Descripcion:</b></td>
                    <td colspan ="2">
                        <asp:TextBox  ID="txDecProyecto" runat ="server" Width ="95%" MaxLength ="128" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteDecProyecto" runat="server" TargetControlID="txDecProyecto" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                        <asp:RequiredFieldValidator ID="rfvDecProyecto" runat="server" ControlToValidate="txDecProyecto" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><b>Tipo de Moneda:</b></td>
                    <td colspan ="2">
                        <asp:DropDownList ID ="dpTpMoneda" runat ="server"  >
                            <asp:ListItem  Value ="0"  Text="Seleccionar" > </asp:ListItem>
                            <asp:ListItem  Value ="Pesos"  Text="Pesos"> </asp:ListItem>
                            <asp:ListItem  Value ="Dolares"  Text="Dolares" > </asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvTpMoneda" runat="server" ControlToValidate="dpTpMoneda" ErrorMessage="*"  ForeColor="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
</asp:Content>
