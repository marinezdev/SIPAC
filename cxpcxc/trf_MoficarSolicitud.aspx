<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_MoficarSolicitud.aspx.cs" Inherits="cxpcxc.trf_MoficarSolicitud" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function Confirmar() {
                var resultado = false;
                if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro que desea actualizar la solicitud ?'); }
                return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField  ID="hdIdSol" runat="server"  />
    <fieldset>
        <legend>MODIFICAR SOLICITUD</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="width: 80%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar"  CssClass="button" OnClick="btnAceptar_Click" />&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br />
        <asp:Panel  ID ="pnSolicitud" runat ="server" Width ="100%"  >
            <table id ="tblSinFactura" runat ="server" style ="width:95%; margin :0 auto">
                <tr>
                    <td colspan ="4" class ="Titulos ">
                        <asp:Label ID="lbProveedor" runat ="server"  Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="width:20%;font-weight: 700;">Factura:</td>
                    <td>
                        <asp:TextBox ID="txFactura" runat ="server" MaxLength ="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFactura" runat="server" ControlToValidate="txFactura" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteFactura" runat="server" TargetControlID="txFactura" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789-/" />
                    </td>
                    <td style ="width:15%;font-weight:700;">Fecha Factura:</td>
                    <td>
                        <asp:TextBox  ID="txFhFactura" runat ="server"  ></asp:TextBox>
                        <asp:ImageButton ID="ibtn_FhFactura" runat="server" CausesValidation="False" ImageUrl="~/img/calendario.png" />
                        <ajaxToolkit:CalendarExtender ID="ce_FhFactura" runat="server" TargetControlID="txFhFactura" PopupButtonID="ibtn_FhFactura" ClearTime="True" PopupPosition="Right" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfv_FhFactura" runat="server" ControlToValidate="txFhFactura" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td  style ="font-weight: 700;">Importe:</td>
                    <td colspan ="3">
                        <asp:TextBox  ID="txImporte" runat ="server"  Width ="25%"  MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvImporte" runat="server" ControlToValidate="txImporte" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteImporte" runat="server" TargetControlID="txImporte" FilterMode="ValidChars" ValidChars="0123456789." />
                    </td>
                </tr>
                <tr>
                    <td  style ="font-weight: 700;" >Concepto:</td>
                    <td colspan ="3">
                        <asp:TextBox  ID="txConcepto" runat ="server"  TextMode ="MultiLine" Width ="95%" Rows="5" MaxLength ="254" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvConcepto" runat="server" ControlToValidate="txConcepto" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteConcepto" runat="server" TargetControlID="txConcepto" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                    </td>
                </tr>
            </table> 
        </asp:Panel>
        <asp:Panel   ID ="pnComplementarios" runat ="server" Width ="100%"  >
            <table style =" width:95% ; margin :0 auto">
                <tr>
                    <td style ="width:20%">Condiciones de pago:</td>
                    <td colspan ="3">
                        <asp:DropDownList ID="dpCondPago" runat ="server"  ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvImpLetra" runat="server" ControlToValidate="dpCondPago" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Proyecto:</td>
                    <td colspan ="3">
                        <asp:DropDownList ID="dpProyecto" runat ="server" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvfProyecto" runat="server" ControlToValidate="dpProyecto" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Descripcion:</td>
                    <td colspan ="3">
                        <asp:TextBox  ID="txDecProyecto" runat ="server" Width ="95%" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteDecProyecto" runat="server" TargetControlID="txDecProyecto" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                        <asp:RequiredFieldValidator ID="rfvDecProyecto" runat="server" ControlToValidate="txDecProyecto" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Tipo de Moneda:</td>
                    <td colspan ="3">
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
