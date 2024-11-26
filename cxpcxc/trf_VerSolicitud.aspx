<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_VerSolicitud.aspx.cs" Inherits="cxpcxc.trf_VerSolicitud" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function fn_Eliminar() {
            var resultado = false;
             resultado = confirm('¿Esta seguro que desea eliminar la solicitud ?'); 
            return resultado;
        }
    
        function ConfirmaNotaCredito() {
            var resultado = confirm('¿Esta seguro que desea agregar la nota credito ?');
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
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:ImageButton ID="imgbtnNotacredito" ImageUrl ="~/img/nc.png" runat="server" ToolTip="Nota credito" />&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="brnImgEliminar" ImageUrl ="~/img/btncancel.png" runat="server"  ToolTip="Eliminar"   OnClientClick="return fn_Eliminar();" OnClick="brnImgEliminar_Click" Visible ="false"  />&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnImgModificar" ImageUrl ="~/img/modify.png" runat="server"  ToolTip="Modificar" OnClick="btnImgModificar_Click" Visible ="false"/>&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnFactura" ImageUrl ="~/img/invoice_i.png" runat="server"  ToolTip="Factura" OnClick="btnFactura_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnAregarFactura" runat="server" Text="Agregar Factura"  CssClass="button"  OnClick="btnAregarFactura_Click" />&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br />

        <asp:Panel ID="pnRechazo" runat ="server" Visible ="false" >
            <h5 style ="background-color :red;color :white">SOLICITUD RECHAZADA</h5>
            <asp:label ID="lbMotivoRechazo"  runat ="server" Font-Size ="14px" Font-Bold ="true" style="font-size:14px"></asp:label>
        </asp:Panel><br />
        
        <table id="tblContenido" style ="width:100%">
            <tr>
                <td style ="width:50%;vertical-align:top">
                    <table id ="tblSol" runat ="server" style ="width:98%; margin :0 auto;" >
                        <tr>
                            <td colspan ="2" class="Titulos" >
                                <h4><asp:Label ID="lbProveedor" runat ="server"  Width ="95%"  Font-Bold="true"></asp:Label></h4>
                            </td>
                        </tr>
                        <tr>
                            <td style ="width :25%; vertical-align :top;font-weight: 700;">RFC:</td>
                            <td><asp:Label  ID="lbRfc" runat ="server" Width ="95%" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Fecha Factura:</td>
                            <td><asp:Label  ID="lbFhFactura" runat ="server"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Factura:</td>
                            <td><asp:Label ID="lbFactura" runat ="server"  font-size="14px" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Importe:</td>
                            <td><asp:Label ID="lbImporte" runat ="server"  Width ="25%"  ></asp:Label></td>
                        </tr>
                        <tr><td style="height :10px"></td></tr>
                        <tr>
                            <td style ="vertical-align :top;font-weight: 700;">Concepto:</td>
                            <td><asp:Label  ID="lbConcepto" runat ="server" Width ="95%" ></asp:Label></td>
                        </tr>
                        <tr><td style="height :10px"></td></tr>
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
                        <tr><td style="height :10px"></td></tr>
                        <tr>
                            <td style ="font-weight: 700;">Condiciones de pago:</td>
                            <td><asp:Label ID="lbCodPago" runat ="server" Width ="95%"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;" >Proyecto:</td>
                            <td><asp:Label ID="lbProyecto" runat ="server" Width ="95%"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td  style ="font-weight: 700;">Descripcion:</td>
                            <td><asp:Label ID="lbDecProyecto" runat ="server" Width ="95%"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="font-weight: 700;">Moneda:</td>
                            <td><asp:Label ID="lbMoneda" runat ="server" Width ="95%" Height="16px"  ></asp:Label></td>
                        </tr>
                    </table><br />
                    <asp:Panel ID ="pnPagos" runat ="server">    
                        <h3 style ="color:green">COMPROBANTES DE PAGO</h3>
                        <asp:Repeater ID="rptComprobantes" runat="server" OnItemCommand="rptComprobantes_ItemCommand" >
                            <HeaderTemplate>
                                <table id="tblComprobantes" border="1" style ="width :75%"  class ="tblFiltrar" >
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
        
                        <table id ="tblNotaPago" style ="width:60%; margin :0 auto">
                            <tr>
                                <td style="width:25%"><strong>Notas de Pago:</strong></td>
                                <td><asp:Label  ID ="lbNotaPago" runat ="server" Width ="100%"></asp:Label> </td>
                            </tr>
                        </table>
                    </asp:Panel><br />
                    <asp:Panel ID ="pnNotaCreditoAsignada" runat ="server" Visible ="false" >
                         <h3 style ="color:#FF6600">NOTA DE CREDITO</h3>
                        <asp:Repeater ID="rpNotaCreditoAsignada" runat="server" OnItemCommand="rpNotaCreditoAsignada_ItemCommand" >
                            <HeaderTemplate>
                                <table id="tblNotaCreditoAsignada" border="1" style ="width :75%"  class ="tblFiltrar" >
                                    <thead>
                                        <th scope="col">FECHA REGISTRO</th>
                                        <th scope="col">FECHA NOTA</th>
                                        <th scope="col">DESCRIPCION</th>
                                        <th scope="col">IMPORTE</th>
                                        <th scope="col">ASIGNADO</th>
                                        <th scope="col"></th>
                                    </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("FechaRegistro","{0:d}")%></td>
                                    <td><%# Eval("Fecha","{0:d}")%></td>
                                    <td><%# Eval("Descripcion")%></td>
                                    <td><%# Eval("Importe")%></td>
                                    <td><%# Eval("Monto")%></td>
                                    <td style="text-align :center;width:30px">
                                        <asp:ImageButton ID="imgbtnVerNota"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="verNota" CommandArgument='<%# Eval("IdNotaCredito")%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater><br />
                    </asp:Panel><br />
                </td>
                <td style ="vertical-align:top">
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="600px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
                        <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                    </asp:Panel>
                </td>
            </tr>
        </table>

        <asp:Panel ID="pnPopNCDisponible" runat="server" Style="display: none;" Width="600px" CssClass="modalPopup">
            <div id="dvBtns" style="width: 100%; text-align: right;">  
                <asp:Button ID="btnCancelarNc" runat="server" Text="Cancelar" CssClass="button" CausesValidation="false" />
            </div><br />
            <asp:Repeater ID="rpNotaCreditoDisponibles" runat="server" OnItemCommand="rpNotaCreditoDisponibles_ItemCommand" >
                <HeaderTemplate>
                    <table id="tblNotaCreditoDisponibles" border="1" style ="width :85%"  class ="tblFiltrar" >
                        <caption style ="color :blue" >NOTAS DE CREDITO DISPONIBLES</caption>
                        <thead>
                            <th scope="col">FECHA</th>
                            <th scope="col">PROVEEDOR</th>
                            <th scope="col">CANTIDAD</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col"></th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("Fecha")%></td>
                        <td><%# Eval("Proveedor")%></td>
                        <td><%# Eval("Importe")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td style="text-align :center;width:30px">
                            <asp:ImageButton ID="imgbtnAsignar"  runat ="server"  ImageUrl="~/img/add.png" CommandName="Asignar" CommandArgument='<%# Eval("IdNotaCredito")%>'  OnClientClick ="return ConfirmaNotaCredito();"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater><br />
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="mpeNotacredito" runat="server"
            BackgroundCssClass="modalBackground"
            DropShadow="true"
            BehaviorID="mpeTpCambio"
            PopupControlID="pnPopNCDisponible"
            TargetControlID="imgbtnNotacredito"
            CancelControlID="btnCancelarNc"
            x="250" Y="400">
        </ajaxToolkit:ModalPopupExtender>

    </fieldset>
</asp:Content>