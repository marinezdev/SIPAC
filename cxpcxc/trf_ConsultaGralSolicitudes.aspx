<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_ConsultaGralSolicitudes.aspx.cs" Inherits="cxpcxc.trf_ConsultaGralSolicitudes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID ="hdIdUsr" runat ="server" />
    <asp:HiddenField ID ="hdConsulta" runat ="server" />
    <fieldset>
        <legend>SOLICITUDES DE TRANSFERENCIA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="width: 60%;text-align: center; color: red;"">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right; width:25%">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br />
        <div id="dvFechas">
            <table id="tblFechas" style="text-align:left; width :65%; margin : 0 auto" class ="tblConsulta">
                <tr>
                    <td style ="width:20%">PROVEEDOR:</td>
                    <td colspan ="5">
                        <asp:DropDownList  ID="dpProveedor" runat ="server" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td colspan ="5">
                        <asp:DropDownList  ID="dpEstado" runat ="server" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>NUMERO FACTURA:</td>
                    <td><asp:TextBox ID ="TxNumFactura" runat ="server" MaxLength="15" Width ="200px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>FECHA REGISTRO:</td>
                    <td>
                        <asp:Label  runat ="server" Text ="DEL:" ></asp:Label>&nbsp;
                        <asp:TextBox ID="txF_Inicio" runat="server" Width="80px" style="margin-bottom: 0px"></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="ImgCalInicio" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />&nbsp;
                        <ajaxtoolkit:calendarextender ID="ce_txF_Inicio" runat="server" TargetControlID="txF_Inicio" PopupButtonID="ImgCalInicio" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
                        <asp:Label  runat ="server" Text ="AL:" ></asp:Label>&nbsp;
                        <asp:TextBox ID="txF_Fin" runat="server" Width="80px"></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="ImgCalFin" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />&nbsp;
                        <ajaxtoolkit:calendarextender ID="ce_txF_Fin" runat="server" TargetControlID="txF_Fin" PopupButtonID="ImgCalFin" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
                    </td>
                </tr>
                <tr>
                    <td> <asp:LinkButton ID ="lkLimpiar" runat ="server"  Text ="Limpiar" OnClick="lkLimpiar_Click"></asp:LinkButton> </td>
                    <td style ="text-align :right">
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" />
                    </td>
                </tr>
            </table> 
        </div><br />
        <asp:Panel ID ="pnSolicitud"  runat ="server" Visible ="false" >
            <table style="width: 50%;text-align :right;margin :0 auto  ">
                <tr>
                    
                    <td ></td>
                    <td style ="text-align :center" class ="Titulos">PESOS</td>
                    <td style ="text-align :center" class ="Titulos">DOLARES</td>
                    
                </tr>
                <tr>
                    <td><b>TOTAL</b></td>
                    <td><asp:Label ID="lbTotPesos" runat="server" Text="0" ></asp:Label></td>
                    <td><asp:Label ID="lbTotDlls" runat="server" Text="0" ></asp:Label></td>
                </tr>
            </table><br />
            <div id ="dvExportar" runat ="server" >
                <table style ="width:100%; "">
                    <tr>
                        <td>
                            <b><asp:Label ID ="lbTotalSol" runat ="server"></asp:Label></b>
                        </td>
                        <td style ="text-align:right">
                            <asp:ImageButton ID ="btnExportar" runat ="server" ImageUrl ="~/img/ExpExcel.png" OnClick="btnExportar_Click"  />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel runat ="server" Width ="100%" Height ="450px" ScrollBars ="Vertical" >
                <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand" OnItemDataBound="rptSolicitud_ItemDataBound"  >
                    <HeaderTemplate>
                        <table id="tblSol" border="1" style ="width :100%" class ="tblFiltrar"  >
                            <thead>
                                <th scope="col">REGISTRO</th>
                                <th scope="col">NO. FACTURA</th>
                                <th scope="col">FECHA FACTURA</th>
                                <th scope="col">PROVEEDOR</th>
                                <th scope="col">IMPORTE</th>
                                <th scope="col">CANTIDAD A PAGAR</th>
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
                            <td><%# Eval("FechaRegistro","{0:d}")%></td>
                            <td><%# Eval("Factura")%></td>
                            <td><%# Eval("FechaFactura","{0:d}")%></td>
                            <td><%# Eval("Proveedor")%></td>
                            <td><%# Eval("Importe","{0:0,0.00}")%></td>
                            <td><%# Eval("CantidadPagar","{0:0,0.00}")%></td>
                            <td style="width:80px"><%# Eval("Moneda")%></td>
                            <td style="text-align :center;width:60px">
                                <asp:Image ID ="imgConfactura" runat ="server" ImageUrl ="~/img/Sem_V.png" />
                            </td>
                            <td style="text-align :center;width:80px"><%# Eval("Estado")%></td>
                            <td style="text-align :center;width:50px">
                                <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></tbody></table></FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </asp:Panel>
    </fieldset>
</asp:Content>
