<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_VerOrdenServicio.aspx.cs" Inherits="cxpcxc.cxc_VerOrdenServicio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
        <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
        <fieldset>
        <legend>ORDEN DE VENTA</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 80%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                   <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click"/>
                </td>
            </tr>
        </table><br />
        <div id ="dvDatos" runat ="server" >  
            <table style ="width :100%">
                <tr>
                    <td colspan ="2" class ="Titulos">
                       <asp:Label ID="lbCliente" runat ="server" Font-Size="Medium" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id ="tblDatos" runat ="server" style ="width :100%;" >
                            <tr>
                                <td  style ="width:28%"><b>Orden:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbOrdServicio" runat ="server" Font-Size="Medium"> </asp:Label></td>
                            </tr>
                            <tr >
                                <td><b>Inicio:</b></td>
                                <td><asp:Label ID ="lbFhInicio" runat ="server" Font-Size="17px" ForeColor="#ff0000"  ></asp:Label></td>
                                <td><b>Termino:</b></td>
                                <td><asp:Label ID ="lbFhFin" runat ="server" Font-Size="17px" ForeColor="#ff0000"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Empresa:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbEmpresa" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Tipo solicitud:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbTpSolicitud" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Condiciones pago:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbCodPago" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>    
                                <td><b>Moneda:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbMoneda" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style ="vertical-align :top"><b>Servicio:</b></td>
                                <td colspan ="3">
                                    <asp:LinkButton ID="lkServicio" runat ="server" OnClick="lkServicio_Click"   ></asp:LinkButton>
                                </td>
                            </tr> 
                            <tr>
                                <td style ="vertical-align :top"><b>Descripcion:</b></td>
                                <td colspan ="3"><asp:Label ID="lbDescripcion" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr> 
                        </table>
                    </td>
                    <td>
                        <table id ="tblPeriodo" style ="width:100%">
                            <tr>
                                <td colspan ="4" class ="Titulos" style ="text-align :center">Partidas totales:
                                <asp:Label ID ="lbPeriodos" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Tipo:</b>
                                     <div style ="border:solid;border-color:white;width:150px">
                                        <asp:RadioButton  ID ="rbMes" runat ="server"  Text ="Mensual" Enabled ="false"  /><br />
                                        <asp:RadioButton  ID ="rdBimestral" runat ="server"  Text ="Bimestral" Enabled ="false"  /><br />
                                        <asp:RadioButton  ID ="rdSemestral" runat ="server"  Text ="Semestral"  Enabled ="false" /><br />
                                        <asp:RadioButton  ID ="rdAnual" runat ="server"  Text ="Anual" Enabled ="false"  />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br /><b><asp:CheckBox ID="chkEspecial" runat ="server"  Text ="Cliente Especial"  Enabled ="false" /></b><br />
        </div><br />
        <asp:Panel ID ="pnOrdFact" runat ="server" ScrollBars ="Auto" Height ="480px" >
            <asp:Repeater ID="rptOrdFact" runat="server"  OnItemDataBound="rptOrdFact_ItemDataBound" >
                <HeaderTemplate>
                    <table id="tblOrdFact" border="1" style="width: 100%" class="tblFiltrar">
                        <thead>
                            <th scope="col">ORDEN FACTURA</th>
                            <th scope="col">FECHA</th>
                            <th scope="col">NO. FACTURA</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">COND. PAGO</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col"></th>
                            <th scope="col"></th>
                            <th scope="col"></th>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style ="width :80px;text-align :center"><%# Eval("IdOrdenFactura")%></td>
                        <td style ="text-align :center"><asp:Label ID="lbFechafactura" runat="server" Text='<%# Eval("FechaInicio","{0:d}")%>'></asp:Label></td>
                        <td style ="text-align :center"><%# Eval("NumFactura")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td style ="text-align :right"><%# Eval("ImporteVista")%></td>
                        <td style ="text-align :center"><%# Eval("CondicionPago")%></td>
                        <td style ="width:90px;text-align :center "><%# Eval("Estado")%></td>
                        <td style="width :65px;text-align: center">
                            <asp:Image ID="imgVencimiento" runat="server" ImageUrl="~/img/Sem_V.png" CommandName="VerPago" CommandArgument='<%# Eval("IdOrdenFactura")%>'/>
                        </td>
                        <td style="text-align: center; width: 40px">
                            <asp:ImageButton ID="imgbtnVerFac" runat="server" ImageUrl="~/img/verFac_gr.png" CommandName="VerFac" CommandArgument='<%# Eval("IdOrdenFactura")%>' ToolTip ="ver Factura" Enabled ="false"  OnClick ="imgbtnVerFac_Click" />
                           </td>
                        <td style="text-align: center; width: 40px">
                            <asp:ImageButton ID="imgbtnCompPago" runat="server" ImageUrl="~/img/nada.png" CommandName="VerPago" CommandArgument='<%# Eval("IdOrdenFactura")%>'  ToolTip ="Ver comprobante pago" Enabled ="false"  OnClick ="imgbtnCompPago_Click"   />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>

        <asp:Panel  ID ="pnPopDocumento" runat ="server" Width ="800px" Style="display:normal; background-color: #F2F2F2;"   >
            <asp:HiddenField  ID ="hdIdOrdFactura" runat ="server"  />
            <div id="dvDocumento" style="width: 100%; text-align: right;">
                <asp:Button ID="btnCierraDocumento" runat="server" CssClass="button" Text="X" CausesValidation="False"/>
            </div>
            <asp:Panel ID ="pnFacAsociadas" runat ="server" Visible ="false">
                 <table style ="width :65% ;margin :0 auto;text-align :center ">
                     <tr><td colspan ="2">FACTURAS RELACIONADAS AL PAGO</td></tr>
                     <tr>
                         <td><asp:Label ID ="lbTotFacAsoc" runat ="server"  /></td>
                     </tr>
                 </table>
                <asp:Repeater ID="rpFacAsociadas" runat="server"  >
                    <HeaderTemplate>
                        <table id="tblFacAsociadas" border="1" style="width: 65%" class="tblFiltrar">
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
                            <td><%# Eval("FechaFactura","{0:d}")%>'</td>
                            <td><%# Eval("NumFactura")%></td>
                            <td><%# Eval("Descripcion")%></td>
                            <td><%# Eval("Importe","{0:0,0.00}")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></tbody></table></FooterTemplate>
                </asp:Repeater>
            </asp:Panel><br />  
             <asp:Panel ID ="pnpaginas" runat ="server"  Visible ="false"  >
                <asp:Label ID ="lbEtiqueta" runat ="server"  Text ="COMPROBANTES: " ></asp:Label>
                <asp:Repeater ID="rpPaginas" runat="server" OnItemCommand="rpPaginas_ItemCommand">
                   <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                                <td style="width:65px;text-align: center;">
                                <asp:LinkButton ID="lkDoc" runat ="server" Font-Size="15px"  ForeColor="Blue"  Text ='<%# Eval("IdDocumento")%>'  CommandName ="Docto" CommandArgument='<%# Eval("IdDocumento")%>' ></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></tbody></table></FooterTemplate>
                </asp:Repeater>
            </asp:Panel><br />  
            <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="630px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
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

</asp:Content>
