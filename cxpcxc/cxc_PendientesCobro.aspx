<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_PendientesCobro.aspx.cs" Inherits="cxpcxc.cxc_PendientesCobro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server"  ></asp:ScriptManager>
    <asp:HiddenField  ID="hdIdEmpresa" runat ="server" />
    <fieldset>
        <legend>CONSULTA PENDIENTES POR COBRAR</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 90%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table><br />
        <asp:Panel ID ="pnConsulta" runat="server">
            <table id ="tbConsulta" runat ="server" style ="width:80%;margin:0 auto "  class ="tblConsulta">
                <tr>
                    <td style ="width :27%">CLIENTE:</td>
                    <td><asp:DropDownList ID ="dpCliente" runat ="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>SERVICIO:</td>
                    <td><asp:DropDownList ID ="dpServicio" runat ="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td><asp:DropDownList ID ="dpEstado" runat ="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>PERIODO :</td>
                   <td>
                       <table style ="width:100%">
                           <tr>
                               <td>MES:</td>
                                <td style ="vertical-align :top" >
                                    <asp:DropDownList ID ="dpMes" runat ="server" Width ="150px" >
                                        <asp:ListItem  Text ="Seleccionar" Value ="0"></asp:ListItem>
                                        <asp:ListItem  Text ="Enero" Value ="1"></asp:ListItem>
                                        <asp:ListItem  Text ="Febrero" Value ="2"></asp:ListItem>
                                        <asp:ListItem  Text ="Marzo" Value ="3"></asp:ListItem>
                                        <asp:ListItem  Text ="Abril" Value ="4"></asp:ListItem>
                                        <asp:ListItem  Text ="Mayo" Value ="5"></asp:ListItem>
                                        <asp:ListItem  Text ="Junio" Value ="6"></asp:ListItem>
                                        <asp:ListItem  Text ="Julio" Value ="7"></asp:ListItem>
                                        <asp:ListItem  Text ="Agosto" Value ="8"></asp:ListItem>
                                        <asp:ListItem  Text ="Septiembre" Value ="9"></asp:ListItem>
                                        <asp:ListItem  Text ="Octubre" Value ="10"></asp:ListItem>
                                        <asp:ListItem  Text ="Noviembre" Value ="11"></asp:ListItem>
                                        <asp:ListItem  Text ="Diciembre" Value ="12"></asp:ListItem>
                                    </asp:DropDownList> 
                                </td>
                               <td style ="width:25px"></td>
                                <td>AÑO:</td>
                                <td style ="vertical-align :top">
                                    <asp:DropDownList ID ="dpAño" runat ="server"  Width ="110px">
                                        <asp:ListItem  Text ="Seleccionar" Value ="0"></asp:ListItem>
                                        <asp:ListItem  Text ="2016" Value ="2016"></asp:ListItem>
                                        <asp:ListItem  Text ="2017" Value ="2017"></asp:ListItem>
                                        <asp:ListItem  Text ="2018" Value ="2018"></asp:ListItem>
                                        <asp:ListItem  Text ="2019" Value ="2019"></asp:ListItem>
                                        <asp:ListItem  Text ="2020" Value ="2020"></asp:ListItem>
                                    </asp:DropDownList>  
                                </td>
                            </tr>
                        </table> 
                    </td>
                </tr>
                <tr>
                    <td colspan ="2"><asp:checkbox ID ="ckcteEspecial" runat ="server" Text ="Clientes Especiales" /> </td>
                </tr>
                <tr>
                    <td colspan ="2" style ="text-align :right">
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click"  />
                    </td>
                </tr>
            </table>
        </asp:Panel><br />
        <asp:Panel  ID ="pnToales" runat ="server"  >
            <table id ="tblTotales" runat ="server" style ="width:100%">
                <tr>
                    <td style ="width:100px"></td>
                    <td><b>TOTAL:</b></td>
                </tr>
                <tr>
                    <td><b>Pesos:</b></td>
                    <td><asp:Label ID ="lbPendientePesos" runat ="server" /></td>
                </tr>
                <tr>
                    <td><b>Dolares:</b></td>
                    <td><asp:Label ID ="lbPendienteDll" runat ="server" /></td>
                </tr>
            </table>
        </asp:Panel><br />
        <asp:Panel ID ="pnOrdFact" runat ="server" Height ="450px"  ScrollBars="Vertical">
            <asp:Repeater ID="rptOrdFact" runat="server" OnItemDataBound="rptOrdFact_ItemDataBound" >
                <HeaderTemplate>
                    <table id="tblOrdFact" border="1" style="width: 100%" class="tblFiltrar">
                        <thead>
                            <th scope="col">UNIDAD NEGOCIO</th>
                            <th scope="col">CLIENTE</th>
                            <th scope="col">FECHA </th>
                            <th scope="col">NO. FACTURA</th>
                            <th scope="col">SERVICIO</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">VENCIMIENTO</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col">SEMAFORO</th>
                            <th scope="col"></th>
                            <th scope="col"></th>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style ="text-align :center"><%# Eval("UnidadNegocio")%></td>
                        <td><%# Eval("Cliente")%></td>
                        <td style ="width :90px;text-align :center"><asp:Label ID="lbFechafactura" runat="server" Text='<%# Eval("FechaInicio","{0:d}")%>'></asp:Label></td>
                        <td style ="width :53px;text-align :center"><%# Eval("NumFactura")%></td>
                        <td style ="width :60px;"><%# Eval("Servicio")%></td>
                        <td style ="width :60px;""><%# Eval("Descripcion")%></td>
                        <td style ="width :85px;text-align :right"><%# Eval("ImporteVista")%></td>
                        <td style ="width :85px;text-align :center"><%# Eval("Vencimiento","{0:d}")%></td>
                        <td style ="width :60px;text-align :center"><%# Eval("TipoMoneda")%></td>
                        <td style ="width :60px"><%# Eval("Estado")%></td>
                        <td style="width :65px;text-align: center">
                            <asp:Image ID="imgVencimiento" runat="server" ImageUrl="~/img/Sem_V.png" />
                        </td>
                        <td style="text-align: center; width: 40px">
                            <asp:ImageButton ID="imgbtnVerFac" runat="server" ImageUrl="~/img/verFac_gr.png" CommandName="VerFac" CommandArgument='<%# Eval("IdOrdenFactura")%>' ToolTip ="Ver Factura" Enabled ="false"  OnClick ="imgbtnVerFac_Click"  />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>  
                 
         <asp:Panel  ID ="pnPopDocumento" runat ="server" Width ="800px" Style="display:none; background-color: #F2F2F2;"   >
             <asp:HiddenField  ID ="hdIdOrdFactura" runat ="server"  />
            <div id="dvDocumento" style="width: 100%; text-align: right;">
                <asp:Button ID="btnCierraDocumento" runat="server" CssClass="button" Text="Cerrar" CausesValidation="False"/>
            </div>
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
