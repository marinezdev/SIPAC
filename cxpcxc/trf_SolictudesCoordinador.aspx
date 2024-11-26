<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_SolictudesCoordinador.aspx.cs" Inherits="cxpcxc.trf_SolictudesCoordinador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript" >
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID="hdUndNegocio" runat ="server" />
    <asp:HiddenField ID="hdConsulta" runat ="server" />
    <fieldset>
        <legend>SOLICITUDES DE TRANSFERENCIA</legend>
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
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false"  />
                </td>
            </tr>
        </table><br />
        <div id="dvFechas">
            <table id="tblFechas" style="text-align:left; width :70%; margin : 0 auto" class ="tblConsulta">
                <tr><td colspan ="2" style ="height :20px"></td></tr>
                <tr>
                    <td>SOLICITANTE:</td>
                    <td style="margin-left: 40px">
                        <asp:DropDownList  ID="dpSolicitante" runat ="server" Width ="450px"  ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td>
                        <asp:DropDownList  ID="dpEstado" runat ="server"  ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>FECHA:</td>
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
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" />
                    </td>
                </tr>
            </table> 
        </div><br />
        <asp:Panel ID ="pnSolicitud"  runat ="server" Width ="100%" Height ="450px" ScrollBars ="Auto" Visible ="false" >
            <table style ="width :95%;margin :0 auto ">
                <tr>
                    <td colspan ="2" style ="text-align :right"> 
                        <asp:Button  ID ="btnExporta" runat ="server"  Text ="Exportar" CssClass ="button" OnClick="btnExporta_Click"/>
                    </td>
                </tr>
            </table>
            <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand"  OnItemDataBound="rptSolicitud_ItemDataBound" >
                <HeaderTemplate>
                    <table id="tblSol" border="1" style ="width :100%" class ="tblFiltrar"  >
                        <thead>
                            <th scope="col">UNIDAD DE NEGOCIO</th>
                            <th scope="col">SOLICITANTE</th>
                            <th scope="col">REGISTRO</th>
                            <th scope="col">NO. FACTURA</th>
                            <th scope="col">FECHA FACTURA</th>
                            <th scope="col">PROVEEDOR</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">FACTURA</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col"></th>
                            <th scope="col"></th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("UnidadNegocio ")%></td>
                        <td><%# Eval("Solicitante")%></td>
                        <td><%# Eval("FechaRegistro","{0:d}")%></td>
                        <td><%# Eval("Factura")%></td>
                        <td><%# Eval("FechaFactura","{0:d}")%></td>
                        <td><%# Eval("Proveedor")%></td>
                        <td><%# Eval("Importe","{0:0,0.00}")%></td>
                        <td><%# Eval("Moneda")%></td>
                        <td style="text-align :center;"><asp:Label ID ="lbConFactura" runat ="server" Text='<%# Eval("ConFactura")%>' ></asp:Label> </td>
                        <td><%# Eval("Estado")%></td>
                        <td>
                            <asp:Image ID ="imgEstado" runat ="server" ImageUrl ="~/img/niv_1.png" />
                        </td>
                        <td style="text-align :center;width:80px">
                            <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </fieldset>
</asp:Content>
