<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AgregaMarcaPrioridad.aspx.cs" Inherits="cxpcxc.trf_AgregaMarcaPrioridad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID ="hdIdUsr" runat ="server" />
    <fieldset>
        <legend>PRIORIDAD DE SOLICITUDES</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
            </tr>
           <tr>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br />
        <div id="dvFechas">
            <table id="tblFechas" style="text-align:left; width :70%; margin : 0 auto" class ="tblConsulta">
                <tr>
                    <td>PROVEEDOR:</td>
                    <td>
                        <asp:DropDownList  ID="dpProveedor" runat ="server" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>FECHA REGISTRO:</td>
                    <td>
                       <table>
                           <tr>
                               <td>DEL:</td>
                                <td style ="vertical-align :top" >
                                    <asp:TextBox ID="txF_Inicio" runat="server" Width="80px" style="margin-bottom: 0px"></asp:TextBox>
                                    <asp:ImageButton ID="ImgCalInicio" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                                    <ajaxtoolkit:calendarextender ID="ce_txF_Inicio" runat="server" TargetControlID="txF_Inicio" PopupButtonID="ImgCalInicio" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
                                </td>
                               <td style ="width:15px"></td>
                                <td>AL:</td>
                                <td style ="vertical-align :top">
                                    <asp:TextBox ID="txF_Fin" runat="server" Width="80px"></asp:TextBox>
                                    <asp:ImageButton ID="ImgCalFin" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                                    <ajaxtoolkit:calendarextender ID="ce_txF_Fin" runat="server" TargetControlID="txF_Fin" PopupButtonID="ImgCalFin" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
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
        </div><br />
        <asp:Panel ID ="pnSolicitud"  runat ="server" Width ="100%" Visible ="false" >
            <asp:UpdatePanel ID="udpSolAutorizacion" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style ="width :95%">
                        <tr>
                            <td style="font-weight: 700;width:270px" >TOTAL PESOS:</td>
                           <td><asp:Label  ID="lbTotPesos" runat ="server" Text ="0" Font-Bold ="true" Font-Size ="Large" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-weight: 700;" >TOTAL DOLARES:</td>
                           <td><asp:Label  ID="lbTotDlls" runat ="server" Text ="0" Font-Bold ="true" Font-Size ="Large" ></asp:Label></td>
                        </tr>
                    </table>
                    <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand" OnItemDataBound="rptSolicitud_ItemDataBound"  >
                        <HeaderTemplate>
                            <table id="tblSol" border="1" style ="width :100%" class ="tblFiltrar"  >
                                <thead>
                                    <th scope="col">REGISTRO</th>
                                    <th scope="col">NO. FACTURA</th>
                                    <th scope="col">FECHA FACTURA</th>
                                    <th scope="col">PROVEEDOR</th>
                                    <th scope="col">IMPORTE</th>
                                    <th scope="col">MONEDA</th>
                                    <th scope="col">FACTURA</th>
                                    <th scope="col">SELECIONAR</th>
                                </thead>
                            <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: White; color: #333333">
                                <td><%# Eval("FechaRegistro","{0:d}")%></td>
                                <td><%# Eval("Factura")%></td>
                                <td><%# Eval("FechaFactura","{0:d}")%></td>
                                <td><%# Eval("Proveedor")%></td>
                                <td style="width:80px"><asp:Label  ID="lbImporte" runat ="server" Text ='<%# Eval("Importe","{0:0,0.00}")%>'  ></asp:Label></td>
                                <td style="width:80px"><asp:Label  ID="lbMoneda" runat ="server" Text ='<%# Eval("Moneda")%>'  ></asp:Label></td>
                                <td style="text-align :center;width :90px"><asp:Label ID ="lbConFactura" runat ="server" Text='<%# Eval("ConFactura")%>' ></asp:Label> </td>
                                <td style="text-align :center;width:80px">
                                    <asp:ImageButton  ID ="btnInactivo" runat ="server" ImageUrl ="~/img/Seleccionar.png" Visible ="false"  CommandArgument='<%# Eval("IdSolicitud")%>' CommandName ="btnInactivo"/>
                                    <asp:ImageButton  ID ="btnActivo" runat ="server" ImageUrl ="~/img/flag_green.png" Visible ="false" CommandArgument='<%# Eval("IdSolicitud")%>' CommandName ="btnActivo"  />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></tbody></table></FooterTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </fieldset>
</asp:Content>
