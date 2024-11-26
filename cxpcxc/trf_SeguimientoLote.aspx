<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_SeguimientoLote.aspx.cs" Inherits="cxpcxc.trf_SeguimientoLote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager  ID="SM"  runat="server"  ></asp:ScriptManager>
    <asp:Panel ID ="pnConsulta" runat ="server" >
        <fieldset>
            <legend> SEGUIMIENTO DE LOTES</legend>
            <div id="dvSolFondos" runat ="server" style ="width :100%;text-align :right"  >
                <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />        
            </div>
            <table id="tblConsulta" runat ="server"  style="text-align:left; width :70%; margin : 0 auto" class ="tblConsulta">
                <tr>
                    <td rowspan ="3" style ="width:20px"></td>
                    <td rowspan ="3" >
                        <asp:Image  runat ="server" ImageUrl ="~/img/seg facturas.png" Width ="200px" Height ="160px" />
                    </td>
                    <td>FECHA</td>
                    <td>DEL:</td>
                    <td>
                        <asp:TextBox ID="txF_Inicio" runat="server" Width="80px" style="margin-bottom: 0px"></asp:TextBox>
                        <asp:ImageButton ID="ImgCalInicio" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                        <ajaxtoolkit:calendarextender ID="ce_txF_Inicio" runat="server" TargetControlID="txF_Inicio" PopupButtonID="ImgCalInicio" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
                    </td>
                    <td style ="width:15px"></td>
                    <td>AL:</td>
                    <td >
                        <asp:TextBox ID="txF_Fin" runat="server" Width="80px"></asp:TextBox>
                        <asp:ImageButton ID="ImgCalFin" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                        <ajaxtoolkit:calendarextender ID="ce_txF_Fin" runat="server" TargetControlID="txF_Fin" PopupButtonID="ImgCalFin" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
                    </td>
                    <td>
                       <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" />
                    </td>
                </tr>
                <tr>
                    <td>TIPO:</td>
                    <td colspan ="6"> 
                        <asp:DropDownList ID ="dpTipo" runat ="server" >
                            <asp:ListItem Text ="Todas" Value ="0" ></asp:ListItem>
                            <asp:ListItem Text ="Pagadas" Value ="1" ></asp:ListItem>
                            <asp:ListItem Text ="Pendientes pago" Value ="2" ></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan ="7" style ="text-align :center ;color :red;height:40px ">
                        <asp:Literal ID ="ltMsg" runat ="server"></asp:Literal>
                    </td>
                </tr>
            </table><br />
            <asp:Panel ID ="pnLotes"  runat ="server" Width ="100%" Height ="500px" ScrollBars ="Vertical" >
                <asp:Repeater ID="rptLotes" runat="server" OnItemCommand="rptLotes_ItemCommand" OnItemDataBound="rptLotes_ItemDataBound">
                    <HeaderTemplate>
                        <table id="tblSolFondeo" border="1" style ="width :100%;text-align :center" class ="tblFiltrar"  >
                            <thead>
                                <th scope="col">NO. LOTE</th>
                                <th scope="col">FECHA</th>
                                <th scope="col">MONTO SOLICITADO</th>
                                <th scope="col">MONTO FONDOS</th>
                                <th scope="col">NO. FACTURAS</th>
                                <th scope="col">PAGADAS</th>
                                <th scope="col">NO PAGADAS</th>
                                <th scope="col">ESTADO</th>
                                <th scope="col"></th>
                                <th scope="col"></th>
                            </thead>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("IdFondeo")%></td>
                            <td><%# Eval("FechaRegistro")%></td>
                            <td><%# Eval("Total","{0:0,0.00}")%></td>
                            <td><%# Eval("TotalAprob","{0:0,0.00}")%></td>
                            <td><%# Eval("NoSolicitudes")%></td>
                            <td><%# Eval("Pagadas")%></td>
                            <td><%# Eval("NoPagadas")%></td>
                            <td><%# Eval("Estado")%></td>
                            <td style="text-align :center;width:80px">
                                <asp:Image  ID ="imgSem" runat ="server" ImageUrl ="~/img/action_check.png" />
                            </td>
                            <td style="text-align :center;width:80px">
                                <asp:ImageButton ID="imgbtnDetalle"  runat ="server"  ImageUrl="~/img/Detalle.png" CommandName="Detalle" CommandArgument='<%# Eval("IdFondeo")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></tbody></table></FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </fieldset>
    </asp:Panel>
        
    <asp:Panel  ID ="pnDetalleLote" runat ="server" Visible ="false" >
        <fieldset>
            <legend> DETALLE DE  LOTE</legend>
            <div id="dvDetalle" runat ="server" style ="width :100%;text-align :right"  >
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  CssClass="button" OnClick="btnRegresar_Click" />        
            </div><br />
            <table runat ="server" style ="width:100%" >
                <tr>
                    <td style="width:25%"><b>LOTE:</b></td>
                    <td style="width:30%"><asp:Label ID ="lbLote" runat ="server" Font-Size ="18px" ></asp:Label> </td>
                    <td style="width:25%" ><b>T.C.:</b></td>
                    <td style="width:30%"><asp:Label  ID="lbTC" runat ="server" Text ="0" Font-Bold ="true" ></asp:Label></td>
                    <td style="width:30px" rowspan ="6"></td>
                    <td rowspan ="6">
                        <asp:Image runat ="server" ImageUrl ="~/img/DetalleFact.jpg" Width ="280px" /> 
                    </td>
                </tr>
                <tr>
                    <td><b>TOTAL SOLICITADO M.N.:</b></td>
                    <td colspan ="3"><asp:Label  ID="lbTotal" runat ="server" Text ="0" Font-Bold ="true"  ></asp:Label></td>
                </tr>
                <tr>
                    <td><b>TOTAL AUTORIZADO M.N.:</b></td>
                    <td colspan ="3"><asp:Label  ID="lbTotalFd" runat ="server" Text ="0" Font-Bold ="true"  ></asp:Label></td>
                </tr>
                <tr><td colspan ="2" style ="height :30px"></td>
                </tr>
                <tr style ="color :red">
                    <td><b>PENDIENTES:</b></td>
                    <td colspan ="3"><asp:Label  ID="lbSolPed" runat ="server" Text ="0" Font-Bold ="true" ></asp:Label></td>
                </tr>
                <tr style ="color :red">
                    <td><b>MONTO PENDIENTE PAGO:</b></td>
                    <td colspan ="3"><asp:Label  ID="lbMontoPendientePago" runat ="server" Text ="0" Font-Bold ="true" ></asp:Label></td>
                </tr>
            </table><br />

            <b><asp:Label ID ="lbTotSol" runat ="server" Font-Size="14px"  /></b>
            <asp:Repeater ID="rpSol" runat="server"  OnItemDataBound="rpSol_ItemDataBound"  >
                <HeaderTemplate>
                    <table id="tblSol" border="1" style ="width :100%" class ="tblFiltrar"  >
                        <thead>
                            <th scope="col">REGISTRO</th>
                            <th scope="col">NO. FACTURA</th>
                            <th scope="col">FECHA FACTURA</th>
                            <th scope="col">PROVEEDOR</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">CANTIDAD PAGAR</th>
                            <th scope="col">APROB.</th>
                            <th scope="col">PAGADO</th>
                            <th scope="col">VER</th>
                            <th scope="col"></th>
                            <th scope="col"></th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("FechaRegistro","{0:d}")%></td>
                        <td><%# Eval("Factura")%></td>
                        <td><%# Eval("FechaFactura","{0:d}")%></td>
                        <td><%# Eval("Proveedor")%></td>
                        <td><asp:Label ID="lbImporte" runat="server" Text='<%# Eval("Importe","{0:0,0.00}")%>'></asp:Label> </td>
                        <td><asp:Label ID="lbTpMoneda" runat="server" Text='<%# Eval("Moneda")%>'></asp:Label> </td>
                        <td><%# Eval("ImporteFondos","{0:0,0.00}") %></td>
                        <td style="text-align :center;width:40px">
                            <asp:Image ID="imgChek"  runat ="server"  ImageUrl="~/img/action_check.png"  />
                        </td>
                        <td style="text-align :center;width:40px">
                            <asp:Image ID="imgSem"  runat ="server"  ImageUrl="~/img/action_check.png"  />
                        </td>
                        <td style="text-align :center;width:40px">
                            <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/verFac.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' ToolTip ="Factura" CausesValidation ="false" OnClick="imgbtnVer_Click"   />
                        </td>
                        <td style="text-align :center;width:40px">
                            <asp:ImageButton ID="imgbtnPago"  runat ="server"  ImageUrl="~/img/pago.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>'  ToolTip ="Comprobante pago" OnClick="imgbtnPago_Click"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>

            <asp:Panel  ID ="pnPopDocumento" runat ="server" Width ="900px" Style="display:normal; background-color: #F2F2F2;"   >
                <div id="dvDocumento" style="width: 100%; text-align: right;">
                    <asp:Button ID="btnCierraDocumento" runat="server" CssClass="button" Text="X" CausesValidation="False"/>
                </div><br />
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="530px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ScrollBars ="Auto"  >
                        <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                    </asp:Panel>
            </asp:Panel>

            <ajaxToolkit:ModalPopupExtender ID="mpePopDocumento" runat="server"
                BackgroundCssClass="modalBackground"
                DropShadow="true"
                BehaviorID="mpePopDocumento"
                TargetControlID="MpeFakeTarget"
                PopupControlID="pnPopDocumento"
                OkControlID ="btnCierraDocumento">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Button ID="MpeFakeTarget" runat="server" CausesValidation="False" Style="display:none" />

        </fieldset> 
    </asp:Panel>
</asp:Content>
