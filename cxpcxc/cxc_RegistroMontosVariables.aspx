<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_RegistroMontosVariables.aspx.cs" Inherits="cxpcxc.cxc_RegistroMontosVariables" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    <fieldset>
        <legend>REGISTRO DE MONTOS VARIABLES</legend>
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
            <table id ="tbConsulta" runat ="server" style ="width:85%;margin:0 auto "  class ="tblConsulta" >
                <tr>
                    <td style ="width :27%">CLIENTE:</td>
                    <td><asp:DropDownList ID ="dpCliente" runat ="server" ></asp:DropDownList></td>
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
                    <td colspan ="2" style ="text-align :right">
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click"  />
                    </td>
                </tr>
            </table>
        </asp:Panel><br />
        <asp:Panel ID ="pnOrdFact" runat ="server" >
            <asp:Repeater ID="rptOrdFact" runat="server" OnItemCommand="rptsol_ItemCommand" OnItemDataBound="rptOrdFact_ItemDataBound" >
                <HeaderTemplate>
                    <table id="tblOrdFact" border="1" style="width: 100%" class="tblFiltrar">
                        <thead>
                            <th scope="col">ORDEN FACTURA</th>
                            <th scope="col">CLIENTE</th>
                            <th scope="col">FECHA</th>
                            <th scope="col">FACTURA</th>
                            <th scope="col">SERVICIO</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col">SEMAFORO</th>
                            <th scope="col"></th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style ="width :70px;text-align :center"><%# Eval("IdOrdenFactura")%></td>
                        <td><%# Eval("Cliente")%></td>
                        <td style ="width :90px;text-align :center"><%# Eval("FechaInicio","{0:d}")%></td>
                        <td style ="width :70px;text-align :center"><%# Eval("NumFactura")%></td>
                        <td><%# Eval("Servicio")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td style ="width :110px;text-align :right"><%# Eval("ImporteVista")%></td>
                        <td style ="width :90px;text-align :center"><%# Eval("TipoMoneda")%></td>
                        <td style ="width :90px"><%# Eval("Estado")%></td>
                        <td style="width :65px;text-align: center">
                            <asp:Image ID="imgVencimiento" runat="server" ImageUrl="~/img/Sem_V.png" CommandName="VerPago" CommandArgument='<%# Eval("IdOrdenFactura")%>'/>
                        </td>
                        <td style="text-align: center; width: 40px">
                            <asp:ImageButton ID="imgbtnverDat" runat="server" ImageUrl="~/img/foward.png" CommandName="VerDat" CommandArgument='<%# Eval("IdOrdenFactura")%>'  ToolTip ="ver datos"  />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>  
    </fieldset>
</asp:Content>
