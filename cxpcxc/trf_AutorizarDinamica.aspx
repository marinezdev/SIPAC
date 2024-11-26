<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc_Grande.Master"  AutoEventWireup="true" CodeBehind="trf_AutorizarDinamica.aspx.cs" Inherits="cxpcxc.trf_AutorizarDinamica" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function vdSolicitudes() {
            var continuar = false;
            if (confirm('Esta seguro que desea AUTORIZAR  la seleccion ?')) {
                $("#dvBtns").hide();
                continuar = true;
            }
            return continuar;
        }

        function validaTpCambio() {
            var continuar = false;
            if (Page_ClientValidate('gptc') == true) {
                $("#dvBtns").hide();
                continuar = true;
            } else { alert('Agruege el tipo de cambio.'); }
            return continuar;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdIdEmpresa" runat="server" />
    <asp:HiddenField ID="hdIdUsr" runat="server" />
    <div style="text-align: right;">
        <asp:Button ID="btnAutorizar" runat="server" Text="Autorizar" CssClass="button"  OnClick="btnAutorizar_Click"  OnClientClick ="return vdSolicitudes();"/>&nbsp;&nbsp;
        <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
    </div>
    <br />
    <asp:UpdatePanel ID="udpSolAutorizacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="tblProceso" runat ="server"  style ="width:100% " >
                <tr>
                    <td style ="width :49%;vertical-align :top">
                        <fieldset>
                            <legend>PENDIENTES</legend>
                                <asp:Panel ID="pnConsulta" runat="server" Width="100%">
                                    <table id="tblConsulta" runat="server" style="width: 80%; margin: 0 auto" class ="tblConsulta">
                                        <tr>
                                            <td style="width: 150px">PROVEEDOR:</td>
                                            <td colspan ="2"><asp:DropDownList ID="dpProveedor" runat="server"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>UNIDAD DE NEGOCIO:</td>
                                            <td colspan ="2"><asp:DropDownList ID="dpUdNegocio" runat="server"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>SOLICITANTE:</td>
                                            <td><asp:DropDownList ID="dpSolicitante" runat="server"></asp:DropDownList></td>
                                            <td style="text-align:right">
                                                <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" CausesValidation="false"  />
                                            </td>    
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: center; color: red;">
                                                <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table><br />
                                </asp:Panel>
                                <br />
                    
                        <table style="width: 60%;text-align :center;margin :0 auto  ">
                            <tr style ="text-align :center " >
                                <td></td>
                                <td class ="Titulos"><b>PESOS:</b></td>
                                <td class ="Titulos"><b>DOLARES:</b></td>
                            </tr>
                            <tr>
                                <td>TOTAL:</td>
                                <td><asp:Label ID="lbTotPesos" runat="server" Text="0" Font-Size="Medium"></asp:Label></td>
                                <td><asp:Label ID="lbTotDlls" runat="server" Text="0"  Font-Size="Medium" ></asp:Label></td>        
                            </tr>
                        </table><br />
                        <asp:Panel runat ="server" ScrollBars ="Vertical"  Height ="600px">
                            <b><asp:Label ID ="lbNumSolicitudes" runat ="server"  ></asp:Label></b>
                            <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand" OnItemDataBound="rptSolicitud_ItemDataBound">
                                <HeaderTemplate>
                                    <table id="tblSol" border="1" style="width: 98%" class="tblFiltrar">
                                        <thead>
                                            <th scope="col">NO. FACTURA</th>
                                            <th scope="col">FECHA FACTURA</th>
                                            <th scope="col">PROVEEDOR</th>
                                            <th scope="col">DESCRIPCION</th>
                                            <th scope="col">IMPORTE</th>
                                            <th scope="col">MONEDA</th>
                                            <th scope="col">PRIORIDAD PAGO</th>
                                            <th scope="col">CANTIDAD PAGAR</th>
                                            <th scope="col">FACTURA</th>
                                            <th scope="col">VER</th>
                                            <th scope="col"></th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td><%# Eval("Factura")%></td>
                                        <td><%# Eval("FechaFactura","{0:d}")%></td>
                                        <td><%# Eval("Proveedor")%></td>
                                        <td><%# Eval("DescProyecto")%></td>
                                        <td><asp:Label ID="lbImporte" runat="server" Text='<%# Eval("Importe","{0:0,0.00}")%>'></asp:Label></td>
                                        <td><asp:Label ID="lbTpMoneda" runat="server" Text='<%# Eval("Moneda")%>'></asp:Label></td>
                                        <td style="text-align: center; width: 80px">
                                            <asp:Image ID="imgPrioridad" runat="server" ImageUrl="~/img/flag_green.png" Visible="false" />
                                            <asp:LinkButton ID ="lkQuitaPrd" runat ="server" Text ="Quitar" Visible ="false"  CommandName ="lkQuitaPrd" CommandArgument='<%# Eval("IdSolicitud")%>' ></asp:LinkButton>
                                        </td>
                                        <td style="text-align: center; width: 100px">
                                            <asp:Label ID="lbCantidadPagar" runat="server" Width="80%" Text='<%# Eval("CantidadPagar","{0:0,0.00}") %>'></asp:Label> 
                                        </td>
                                        <td style="text-align :center;width:60px">
                                            <asp:Image ID ="imgConFactura" runat ="server" ImageUrl ="~/img/Sem_V.png" />
                                        </td>
                                        <td style="text-align: center; width: 40px">
                                            <asp:ImageButton ID="imgbtnVer" runat="server" ImageUrl="~/img/verFac.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' CausesValidation="false" />
                                        </td>
                                        <td style="text-align :center;width:40px">
                                            <asp:ImageButton  ID ="btnAutrz" runat ="server" ImageUrl ="~/img/foward.png" Visible ="true" CommandArgument='<%# Eval("IdSolicitud")%>' CommandName ="btnAutrz"/>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></tbody></table></FooterTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                        </fieldset>
                    </td>
                    <td style ="width :49%;vertical-align :top">
                        <fieldset>
                            <legend>AUTORIZAR</legend>
                            <div runat ="server"  style ="height:130px">
                                <br />
                                <b><asp:CheckBox  ID ="chksinDeposito" runat ="server"  Text ="LOTE SIN DEPOSITO, PAGO CON CUENTA DE DOLARES" Font-Size="15px" ForeColor="Green" /></b>
                            </div><br />
                        <table style="width: 60%;text-align :center;margin :0 auto  ">
                            <tr>
                                <td></td>
                                <td class ="Titulos"><b>PESOS:</b></td>
                                <td class ="Titulos"><b>DOLARES:</b></td>
                            </tr>
                            <tr>
                                <td>TOTAL:</td>
                                <td><asp:Label ID="lbTotAutPesos" runat="server" Text="0"  Font-Size="Medium" ></asp:Label></td>
                                <td><asp:Label ID="lbTotAutDlls" runat="server" Text="0" Font-Size="Medium" ></asp:Label></td>
                            </tr>
                        </table><br />
                        <asp:Panel runat ="server" ScrollBars ="Vertical"  Height ="600px">
                            <asp:Repeater ID="rptAutorizar" runat="server" OnItemCommand="rptAutorizar_ItemCommand" >
                                <HeaderTemplate>
                                    <table id="tblAutorizar" border="1" style="width: 98%" class="tblFiltrar">
                                        <thead>
                                            <th scope="col">NO. FACTURA</th>
                                            <th scope="col">FECHA FACTURA</th>
                                            <th scope="col">PROVEEDOR</th>
                                            <th scope="col">DESCRIPCION</th>
                                            <th scope="col">IMPORTE</th>
                                            <th scope="col">MONEDA</th>
                                            <th scope="col">POR PAGAR</th>
                                            <th scope="col">PRIORIDAD</th>
                                            <th scope="col">CANTIDAD PAGAR</th>
                                            <th></th>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td><%# Eval("Factura")%></td>
                                        <td><%# Eval("FechaFactura","{0:d}")%></td>
                                        <td><%# Eval("Proveedor")%></td>
                                        <td><%# Eval("DescProyecto")%></td>
                                        <td><asp:Label ID="lbImporte" runat="server" Text='<%# Eval("Importe","{0:0,0.00}")%>'></asp:Label></td>
                                        <td><asp:Label ID="lbTpMoneda" runat="server" Text='<%# Eval("Moneda")%>'></asp:Label></td>
                                        <td><asp:Label ID="lbPorPagar" runat="server" Text='<%# Eval("CantidadPagar","{0:0,0.00}") %>'></asp:Label></td>
                                        <td style="text-align: center; width: 80px">
                                            <asp:Image ID="imgPrioridad" runat="server" ImageUrl="~/img/flag_green.png" Visible="false" />
                                        </td>
                                        <td style="text-align: center; width: 100px">
                                            <asp:TextBox ID="txAutorizado" runat="server" Width="80%" Text='<%# Eval("ImporteAutorizado","{0:0,0.00}") %>' OnTextChanged ="txAutorizado_TextChanged"  AutoPostBack ="true" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAutorizado" runat="server" ControlToValidate="txAutorizado" ErrorMessage="*" ForeColor="Red" ValidationGroup="gpSol" ></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfv2Autorizado" runat="server" ControlToValidate="txAutorizado" ErrorMessage="*" ForeColor="Red" ValidationGroup="gpSol" InitialValue ="0" ></asp:RequiredFieldValidator>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="fteAutorizado" runat="server" TargetControlID="txAutorizado" FilterMode="ValidChars" ValidChars="0123456789.," />
                                        </td>
                                        <td style="text-align :center;width:40px">
                                            <asp:ImageButton  ID ="btnQuitar" runat ="server" ImageUrl ="~/img/delete.png" CommandArgument='<%# Eval("IdSolicitud")%>' CommandName ="btnQuitar"/>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></tbody></table></FooterTemplate>
                            </asp:Repeater>
                            </asp:Panel>
                        </fieldset> 
                    </td>
                </tr>
            </table>
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

        <asp:Label ID="lbPop" runat ="server" ></asp:Label>
        <ajaxToolkit:ModalPopupExtender ID="mpeTpCambio" runat="server"
            BackgroundCssClass="modalBackground"
            DropShadow="true"
            BehaviorID="mpeTpCambio"
            TargetControlID="lbPop"
            PopupControlID="pnPopTpCambio"
            CancelControlID="btnCancelarTpCambio">
        </ajaxToolkit:ModalPopupExtender>

</asp:Content>
