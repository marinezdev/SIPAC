<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AutorizacionPre.aspx.cs" Inherits="cxpcxc.trf_AutorizacionPre" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript">
        
        function validaTpCambio() {
            var continuar = false;
            if (Page_ClientValidate('gpSol') == true) {
                if (Page_ClientValidate('gptc') == true) {
                    //var chk = $("input[type='checkbox']:checked").length;
                    //if (chk != "") {
                        if (confirm('Esta seguro que desea AUTORIZAR  la seleccion, con el tipo de cambio registrado?')) {
                            $("#dvBtns").hide();
                            continuar = true;
                        }
                    //} else { alert('Seleccione las solicitudes.'); }
                } else { alert('Agruege el tipo de cambio.'); }
            } else { alert('Algunas de las solicitudes no cuentan con un importe valido.'); }

            return continuar;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdIdEmpresa" runat="server" />
    <fieldset>
        <legend>SOLICITUD DE FONDOS</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 80%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btnAutorizar" runat="server" Text="Autorizar" CssClass="button" />&nbsp;&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
        <br />
        <div id="dvEmpresa" style ="width :100%; text-align :center"><asp:Literal ID="ltEmpresa" runat="server"></asp:Literal></div><br />
        <asp:UpdatePanel ID="udpSolAutorizacion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnSolicitud" runat="server" Width="100%">
                    <table style="width: 60%;text-align :right;margin :0 auto  ">
                        <tr>
                            <td style ="width :10%" ></td>
                            <td style ="width :35%;text-align :center"  class ="Titulos">TOTAL AUTORIZAR</td>
                            <td style ="width :35%;text-align :center" class ="Titulos">TOTAL DE FONDOS</td>
                        </tr>
                        <tr>
                            <td><b>PESOS:</b></td>
                            <td>
                                <asp:Label ID="lbTotPesos" runat="server" Text="0" Font-Size="Medium"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbTotAutPesos" runat="server" Text="0"  Font-Size="Medium" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><b>DOLARES:</b></td>
                            <td>
                                <asp:Label ID="lbTotDlls" runat="server" Text="0"  Font-Size="Medium" ></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbTotAutDlls" runat="server" Text="0" Font-Size="Medium" ></asp:Label>
                            </td>
                        </tr>
                    </table><br />

                    <asp:Repeater ID="rptSolicitud" runat="server" OnItemDataBound="rptSolicitud_ItemDataBound" >
                        <HeaderTemplate>
                            <table id="tblSol" border="1" style="width: 100%" class="tblFiltrar">
                                <thead>
                                    <th scope="col"></th>
                                    <th scope="col">UNIDAD DE NEGOCIO</th>
                                    <th scope="col">SOLICITANTE</th>
                                    <th scope="col">NO. FACTURA</th>
                                    <th scope="col">FECHA FACTURA</th>
                                    <th scope="col">PROVEEDOR</th>
                                    <th scope="col">DESCRIPCION</th>
                                    <th scope="col">IMPORTE</th>
                                    <th scope="col">MONEDA</th>
                                    <th scope="col">FACTURA</th>
                                    <th scope="col">PRIORIDAD PAGO</th>
                                    <th scope="col">CANTIDAD PAGAR</th>
                                    <th scope="col">FONDOS</th>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: White; color: #333333">
                                <td style="text-align: center; width: 40px">
                                    <asp:Label ID ="lbIdSol" runat="server" Text='<%# Eval("IdSolicitud")%>' />
                                </td>
                                <td><%# Eval("UnidadNegocio")%></td>
                                <td><%# Eval("Solicitante")%></td>
                                <td><%# Eval("Factura")%></td>
                                <td><%# Eval("FechaFactura","{0:d}")%></td>
                                <td><%# Eval("Proveedor")%></td>
                                <td><%# Eval("DescProyecto")%></td>
                                <td><asp:Label ID="lbImporte" runat="server" Text='<%# Eval("Importe","{0:0,0.00}")%>'></asp:Label></td>
                                <td><asp:Label ID="lbTpMoneda" runat="server" Text='<%# Eval("Moneda")%>'></asp:Label></td>
                                <td style="text-align: center;"><%# Eval("ConFactura")%></td>
                                <td style="text-align: center; width: 80px">
                                    <asp:Image ID="imgPrioridad" runat="server" ImageUrl="~/img/flag_green.png" Visible="false" />
                                </td>
                                <td style="text-align: center; width: 100px">
                                    <asp:TextBox ID="txCantidadPagar" runat="server" Width="80%" Text='<%# Eval("CantidadPagar","{0:0,0.00}") %>' OnTextChanged="chkAutorizar_CheckedChanged"  AutoPostBack ="true" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCantidadPagar" runat="server" ControlToValidate="txCantidadPagar" ErrorMessage="*" ForeColor="Red" ValidationGroup="gpSol"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteCantidadPagar" runat="server" TargetControlID="txCantidadPagar" FilterMode="ValidChars" ValidChars="0123456789.," />
                                </td>
                                <td style="text-align: center; width: 40px">
                                    <asp:CheckBox ID="chkAutorizar" runat="server" Checked ="true" OnCheckedChanged="chkAutorizar_CheckedChanged" AutoPostBack="true" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></tbody></table></FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Panel ID="pnPopTpCambio" runat="server" Style="display: none;" Width="310px" CssClass="modalPopup">
            <table id="tblTpCambio" style="width: 98%; margin: auto;">
                <tr>
                    <td colspan="2" class="Titulos">SOLICITUD DE FONDOS</td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 15px"></td>
                </tr>
                <tr>
                    <td style="width: 42%">Tipo de cambio:</td>
                    <td>
                        <asp:TextBox ID="txTpCambio" runat="server" Width="88%" MaxLength="5" Text=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTpCambio" runat="server" ControlToValidate="txTpCambio" ErrorMessage="*" ForeColor="Red" ValidationGroup="gptc"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteTpCambio" runat="server" TargetControlID="txTpCambio" FilterMode="ValidChars" ValidChars="0123456789." />
                    </td>
                </tr>
                <tr>
                    <td style="height: 15px"></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">
                        <div id="dvBtns" style="width: 100%; text-align: right;">  
                            <asp:Button ID="btnAceptaTpCambio" runat="server" Text="Aceptar" CssClass="button" OnClientClick="return validaTpCambio();" OnClick="btnAceptaTpCambio_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancelarTpCambio" runat="server" Text="Cancelar" CssClass="button" CausesValidation="false" />
                        </div> 
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="mpeTpCambio" runat="server"
            BackgroundCssClass="modalBackground"
            DropShadow="true"
            BehaviorID="mpeTpCambio"
            TargetControlID="btnAutorizar"
            PopupControlID="pnPopTpCambio"
            CancelControlID="btnCancelarTpCambio">
        </ajaxToolkit:ModalPopupExtender>

    </fieldset>
</asp:Content>
