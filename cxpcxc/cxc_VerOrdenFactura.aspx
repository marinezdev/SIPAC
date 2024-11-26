<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_VerOrdenFactura.aspx.cs" Inherits="cxpcxc.cxc_VerOrdenFactura" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        //function ConfirmarModificacion() {
        //    var resultado = false;
        //    var chk = $("input[type='checkbox']:checked").length;
        //    if (chk != "") {
        //        if (Page_ClientValidate('Actz') == true) { resultado = confirm('¿Esta seguro de continuar?'); }
        //        if (resultado) { $("#dvBtnsMod").hide(); }
        //    } else { alert("Debe agrega almenos un cocepto o partida")}
        //    return resultado;
        //}

        function ConfirmarModificacion() {
            var resultado = false;
                if (Page_ClientValidate('Actz') == true) { resultado = confirm('¿Esta seguro de continuar?'); }
                if (resultado) { $("#dvBtnsMod").hide(); }
            
            return resultado;
        }

        //function ValidaPartida() {
        //    var resultado = false;
        //    if (Page_ClientValidate('partida') == true) { resultado = true; }
        //    return resultado;
        //}

        function ConfirmaEnvio() {
            var resultado=false;

            resultado = confirm('¿Esta seguro de continuar?');

            return resultado;
        }

        function VadlidaFhCompromiso() {
            var resultado = false;
            if (Page_ClientValidate('Compromiso') == true) {
                var resultado = true;
                resultado = confirm('¿Esta seguro de continuar?');
            }
            return resultado;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    
        <asp:MultiView  ID ="mtvContenedor" runat ="server" ActiveViewIndex ="0" >
            <asp:View  Id="vwDatos" runat ="server" >
                <fieldset>
                    <legend>ORDEN DE FACTURA</legend>
                    <table id="tblBtnsDatos" runat="server" style="width: 100%">
                        <tr>
                            <td style="width: 60%; text-align: center; color: red;">
                                <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                            </td>
                            <td style="text-align: right;">
                                <asp:ImageButton  ID="imgBtDocumento" runat="server"  ImageUrl ="~/img/invoice_i.png"  CausesValidation="false" OnClick="imgBtDocumento_Click" />&nbsp;
                                <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                            </td>
                        </tr>
                    </table><br />
                    <table style ="width :100%">
                        <tr>
                            <td>
                                <div id ="dvDatos" runat ="server" >  
                                    <table id ="tblDatos" runat ="server" style ="width :100%;" class ="tblConsulta " >
                                        <tr>
                                            <td colspan ="4" class ="Titulos"><asp:Label ID="lbCliente" runat ="server" ></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td  style ="width:20%"><b>Orden servicio:</b></td>
                                            <td colspan ="3"><asp:Label ID ="lbOrdServicio" runat ="server" Font-Size="Medium"> </asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td> <b>Orden factura:</b></td>
                                            <td colspan ="3"><asp:Label  ID="lbOrdFactura" runat ="server" Font-Size="Medium"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style ="width:20%"><b>Empresa facturación:</b></td>
                                            <td colspan ="3"><asp:Label ID ="lbEmpresa" runat ="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Fecha Factura:</b></td>
                                            <td><asp:Label ID ="lbFhFactura" runat ="server"></asp:Label></td>
                                            <td style ="width:20%"> <b>No. Factura:</b></td>
                                            <td><asp:Label  ID="lbNoFactura" runat ="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Tipo de Solicitud:</b></td>
                                            <td><asp:Label ID ="lbTpSolicitud" runat ="server"></asp:Label></td>
                                            <td style ="width:20%"> <b>Importe:</b></td>
                                            <td><asp:Label  ID="lbImporte" runat ="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><b>Condiciones pago:</b></td>
                                            <td><asp:Label ID ="lbCodPago" runat ="server"></asp:Label></td>
                                            <td><b>Tipo Moneda:</b></td>
                                            <td><asp:Label ID ="lbMoneda" runat ="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style ="vertical-align :top"><b>Descripción:</b></td>
                                            <td colspan ="3"><asp:Label ID="lbDescripcion" runat ="server" Width ="95%" ></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style ="vertical-align :top"><b>Anotación:</b></td>
                                            <td colspan ="3"><asp:Label ID="lbAnotaciones" runat ="server" Width ="95%" ></asp:Label></td>
                                        </tr>
                                        <tr><td style ="height :15px"></td></tr>
                                        <tr>
                                            <td style ="vertical-align :top"><b>Compromiso pago:</b></td>
                                            <td colspan ="3" ><asp:Label ID="lbCompPago" runat ="server" Width ="95%" ForeColor="DarkGreen" Font-Size="Medium" style="font-weight: 700"  ></asp:Label></td>
                                        </tr>
                                    </table>
                                    <br /><b><asp:CheckBox ID="chkEspecial" runat ="server"  Text ="Cliente Especial"  Enabled ="false" /></b>
                                </div>
                            </td>
                            <td style ="width :20%; text-align :center;vertical-align :top">
                                <asp:Button ID ="btnFhCompPago" runat ="server" CssClass ="button" Text ="Compromiso de pago" Width ="190px" /><br /><br />
                                <asp:Panel ID ="pnbtnModificacion" runat ="server" Visible ="false" >
                                    <asp:Button ID ="btnModificar" runat ="server" CssClass ="button" Text ="Modificacion conceptos" Width ="190px" /><br /><br />
                                    <asp:Button ID ="btnEnviarFacturacion" runat ="server" CssClass ="button" Text ="Enviar facturacion-pago"  Width ="190px" OnClick="btnEnviarFacturacion_Click" OnClientClick ="return ConfirmaEnvio();" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID ="pnPagos" runat ="server"  Width ="100%" HorizontalAlign ="Left" >    
                        <asp:Repeater ID="rptPagos" runat="server" OnItemCommand="rptPagos_ItemCommand" >
                            <HeaderTemplate>
                                <table id="tblPagos" border="1" style ="width:300px;" class ="tblFiltrar" >
                                    <caption style="text-align :center;font-weight:700" >COMPROBANTES</caption>
                                    <thead>
                                        <th scope="col">FECHA</th>
                                        <th scope="col"></th>
                                    </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("FechaRegistro")%></td>
                                    <td style="text-align :center;width:40px">
                                        <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="verpago" CommandArgument='<%# Eval("IdDocumento")%>' CausesValidation ="false"  />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater><br />
                    </asp:Panel>
                    
                    <asp:Panel  ID ="pnPopModificar" runat ="server" Width ="750px" Style="display: none;"  CssClass="modalPopup" >
                        <fieldset>
                            <legend>MODIFICAR MONTO</legend>
                            <table id ="tblMoficar" runat ="server" style ="width:95%; margin :0 auto">
                                <tr>
                                    <td colspan ="4" style ="text-align :right">
                                        <div id ="dvBtnsMod" runat ="server" >
                                            <asp:Button ID="btnGuardarModif" runat="server" Text="Guardar" CssClass="button" OnClientClick ="return ConfirmarModificacion();"  OnClick="btnGuardarModif_Click" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" CausesValidation="false"  />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style ="width:25%;"><b>Fecha Factura:</b></td>
                                    <td>
                                        <asp:TextBox  ID="txFhFactura" runat ="server"  ></asp:TextBox>
                                        <asp:ImageButton ID="ibtn_FhFactura" runat="server" CausesValidation="False" ImageUrl="~/img/calendario.png" />
                                        <ajaxToolkit:CalendarExtender ID="ce_FhFactura" runat="server" TargetControlID="txFhFactura" PopupButtonID="ibtn_FhFactura" ClearTime="True" PopupPosition="Right" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfv_FhFactura" runat="server" ControlToValidate="txFhFactura" ErrorMessage="*" ForeColor="Red" ValidationGroup ="Actz"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Monto:</b></td>
                                    <td>
                                        <asp:TextBox ID ="txMonto" runat ="server" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMonto" runat="server" ControlToValidate="txMonto" ErrorMessage="*"  ForeColor="Red" ValidationGroup="Actz" InitialValue="" ></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteMonto" runat="server" TargetControlID="txMonto" FilterMode="ValidChars" ValidChars="1234567890." />
                                    </td>
                                </tr>
                            </table> 
                        </fieldset> 
                    </asp:Panel>

                    <ajaxToolkit:ModalPopupExtender ID="mpeModificar" runat="server"
                        BackgroundCssClass="modalBackground"
                        DropShadow="true"
                        BehaviorID="mpeModificar"
                        TargetControlID="btnModificar"
                        PopupControlID="pnPopModificar"
                        CancelControlID="btnCancelar">
                    </ajaxToolkit:ModalPopupExtender>
                </fieldset> 
                <asp:Panel  ID ="pnPopFechaCompromiso" runat ="server" Width ="340px" Style="display: none;"  CssClass="modalPopup" >
                    <div style ="text-align :right"><asp:Button ID ="btnCierraFhCompPago" runat ="server"  Text ="X" CssClass="button" /></div>
                        <fieldset>
                            <legend>ASIGNAR FECHA COMPROMISO DE PAGO</legend><br />
                            <header ><strong>Fecha:</strong></header>
                            <asp:TextBox  ID="txFhCompromisoPago" runat ="server"   ></asp:TextBox>
                            <asp:ImageButton ID="ibtn_FhCompromisoPago" runat="server" CausesValidation="False" ImageUrl="~/img/calendario.png" />
                            <ajaxToolkit:CalendarExtender ID="ce_FhCompromisoPago" runat="server" TargetControlID="txFhCompromisoPago" PopupButtonID="ibtn_FhCompromisoPago"  PopupPosition="Right" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvFhCompromisoPago" runat="server" ControlToValidate="txFhCompromisoPago" ErrorMessage="*"  ForeColor="Red" ValidationGroup="Compromiso" ></asp:RequiredFieldValidator>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID ="btnAtzFhCompromisoPago" runat ="server" Text="Guardar" CssClass ="button" OnClick="btnAtzFhCompromisoPago_Click" OnClientClick ="return VadlidaFhCompromiso();"  />
                        </fieldset>
                    </asp:Panel>

                    <ajaxToolkit:ModalPopupExtender ID="mpeFhCompPago" runat="server"
                        BackgroundCssClass="modalBackground"
                        DropShadow="true"
                        BehaviorID="mpeFhCompPago"
                        TargetControlID="btnFhCompPago"
                        PopupControlID="pnPopFechaCompromiso"
                        CancelControlID="btnCierraFhCompPago">
                    </ajaxToolkit:ModalPopupExtender>
            </asp:View>
            <asp:View ID ="vwDocumento"  runat ="server" >
                <fieldset>
                    <legend>FACTURA</legend>
                    <div id="dvDocumento" style="width: 100%; text-align: right;">
                        <asp:Button ID="btnCierraDocumento" runat="server" CssClass="button" Text="X" CausesValidation="False" OnClick="btnCierraDocumento_Click" />
                    </div>
                    <asp:Panel ID ="pnFacAsociadas" runat ="server"  ScrollBars="Vertical" Visible ="false">
                        <asp:Repeater ID="rpFacAsociadas" runat="server"  >
                            <HeaderTemplate>
                                <table id="tblFacAsociadas" border="1" style="width: 65%" class="tblFiltrar">
                                    <caption >FACTURAS RELACIONADAS AL PAGO</caption>
                                    <thead>
                                        <th scope="col">FECHA FACTURA</th>
                                        <th scope="col">NO. FACTURA</th>
                                        <th scope="col">DESCRIPCION</th>
                                        <th scope="col">IMPORTE</th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("FechaFactura","{0:d}")%></td>
                                    <td><%# Eval("NumFactura")%></td>
                                    <td><%# Eval("Descripcion")%></td>
                                    <td><%# Eval("Importe","{0:0,0.00}")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel><br />  
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="630px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
                        <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                    </asp:Panel>
                </fieldset>
            </asp:View>
        </asp:MultiView> 
</asp:Content>
