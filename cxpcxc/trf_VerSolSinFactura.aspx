<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_VerSolSinFactura.aspx.cs" Inherits="cxpcxc.trf_VerSolSinFactura" %>
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
        <legend>SOLICITUD DE TRANSFERENCIA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="text-align: right;">
                    <asp:Button ID="btnAregarFactura" runat="server" Text="Agregar Factura"  CssClass="button" OnClick="btnAregarFactura_Click" />&nbsp;
                    <asp:Button ID="btnActualizar" runat="server" Text="Modificar"  CssClass="button" />&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
            </tr>
        </table><br />

        <asp:Panel ID="pnRechazo" runat ="server" Visible ="false" >
            <h5 style ="background-color :red;color :white">SOLICITUD RECHAZADA</h5>
            <asp:label ID="lbMotivoRechazo"  runat ="server" Font-Size ="14px" Font-Bold ="true" style="font-size:14px"></asp:label>
        </asp:Panel><br />
        <table style ="width:100%">
            <tr>
                <td style =" vertical-align:top">
                    <table id ="tblSol" runat ="server" style ="width:98%; margin :0 auto;" >
                        <tr>
                            <td colspan ="4" class="Titulos" >
                                <h4><asp:Label ID="lbProveedor" runat ="server"  Width ="95%"  Font-Bold="true"></asp:Label></h4>
                            </td>
                        </tr>
                        <tr>
                            <td style ="width :25%; vertical-align :top;font-weight: 700;">RFC:</td>
                            <td colspan ="3"><asp:Label  ID="lbRfc" runat ="server" Width ="95%" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Factura:</td>
                            <td colspan ="3"><asp:Label ID="lbFactura" runat ="server"  font-size="14px" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Fecha Factura:</td>
                            <td><asp:Label  ID="lbFhFactura" runat ="server"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Importe:</td>
                            <td><asp:Label ID="lbImporte" runat ="server"  Width ="25%"  ></asp:Label></td>
                        </tr>
                        <tr><td colspan ="2" style="height :10px"></td></tr>
                        <tr>
                            <td style ="vertical-align :top;font-weight: 700;">Concepto:</td>
                            <td><asp:Label  ID="lbConcepto" runat ="server" Width ="95%" ></asp:Label></td>
                        </tr>
                        <tr><td colspan ="2" style="height :10px"></td></tr>
                        <tr>
                            <td style ="font-weight: 700;">Banco:</td>
                            <td><asp:Label ID="lbBanco" runat ="server"  Width ="90%"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Cuenta:</td>
                            <td><asp:Label ID="lbCuenta" runat ="server"  Width ="90%" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Cuenta Clabe:</td>
                            <td><asp:Label ID="lbClabe" runat ="server"  Width ="90%" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Sucursal:</td>
                            <td><asp:Label ID="lbSucursal" runat ="server"  Width ="90%" ></asp:Label></td>
                        </tr>
                        <tr><td colspan ="2" style="height :10px"></td></tr>
                        <tr>
                            <td style ="font-weight: 700;">Condiciones de pago:</td>
                            <td><asp:Label ID="lbCodPago" runat ="server" Width ="95%"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td  style ="font-weight: 700;">Proyecto:</td>
                            <td><asp:Label ID="lbProyecto" runat ="server" Width ="95%"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td  style ="font-weight: 700;">Descripcion:</td>
                            <td><asp:Label ID="lbDecProyecto" runat ="server" Width ="95%"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Moneda:</td>
                            <td><asp:Label ID="lbMoneda" runat ="server" Width ="95%"  ></asp:Label></td>
                        </tr>
                    </table><br />
                    <asp:Panel ID ="pnPagos" runat ="server"  Width ="100%">
                        <asp:Repeater ID="rptComprobantes" runat="server" OnItemCommand="rptComprobantes_ItemCommand" >
                            <HeaderTemplate>
                                <table id="tblComprobantes" style ="width :85%"  class ="tblFiltrar" >
                                    <caption style="text-align :center;font-weight:700" >COMPROBANTES</caption>
                                    <thead>
                                        <th scope="col">FECHA</th>
                                        <th scope="col">CANTIDAD</th>
                                        <th scope="col">TIPO CAMBIO</th>
                                        <th scope="col">PESOS</th>
                                        <th scope="col"></th>
                                    </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td  style="width :35%"><%# Eval("FechaRegistro")%></td>
                                    <td><%# Eval("Cantidad")%></td>
                                    <td><%# Eval("TipoCambio")%></td>
                                    <td><%# Eval("Pesos")%></td>
                                    <td style="text-align :center;width:80px">
                                        <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdDocumento")%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater><br />
                        <table id ="tblNotaPago" style ="width:85%; margin :0 auto">
                            <tr>
                                <td style="width:25%"><strong>Notas de Pago:</strong></td>
                                <td><asp:Label  ID ="lbNotaPago" runat ="server" Width ="100%"></asp:Label> </td>
                            </tr>
                        </table>
                    </asp:Panel><br />
                </td>
                <td style ="vertical-align:top">
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="550px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Visible="false" >
                        <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                    </asp:Panel>
                </td>
            </tr>
        </table>
       <asp:Panel  ID ="pnPopActlz" runat ="server" Width ="100%" Style="display: normal;"  CssClass="modalPopup" >
            <asp:HiddenField  ID ="hdSinFactura" runat ="server"/>
            <h5 class ="Titulos " >Modificar</h5>
            <asp:Panel ID ="pnSinFactura" runat ="server" >
                <table id ="tblSinFactura" runat ="server" style ="width:95%; margin :0 auto">
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
                <tr>
                    <td colspan ="4" style ="text-align :right">
                        <asp:Button ID="btnAceptaCambio" runat="server" Text="Aceptar" CssClass="button" OnClientClick ="return Confirmar();" OnClick="btnAceptaCambio_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" CausesValidation="false"  />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="mpeActlz" runat="server"
            BackgroundCssClass="modalBackground"
            DropShadow="true"
            BehaviorID="mpeActlz"
            TargetControlID="btnActualizar"
            PopupControlID="pnPopActlz"
            CancelControlID="btnCancelar">
        </ajaxToolkit:ModalPopupExtender>
    </fieldset>

</asp:Content>
