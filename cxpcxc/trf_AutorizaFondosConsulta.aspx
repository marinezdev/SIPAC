<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AutorizaFondosConsulta.aspx.cs" Inherits="cxpcxc.trf_AutorizaFondosConsulta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager  ID="SM"  runat="server"  ></asp:ScriptManager>
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server"  /> 
    <fieldset>
        <legend>SOLICITUD DE FONDOS</legend>
        <div id="dvSolFondos" runat ="server" style ="width :100%;text-align :right"  >
            <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />        
        </div>
        <asp:Panel ID="pnConsulta" runat ="server"  Width ="100%" >
            <table id="tblConsulta" runat ="server"  style="text-align:left; width :50%; margin : 0 auto" class ="tblConsulta">
                <tr>
                    <td style ="width :100px">FECHA:</td>
                    <td>
                        <table style ="width:100%">
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
                    <td >
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click"  />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td colspan ="2">
                        <asp:DropDownList  ID="dpEstado" runat ="server">
                            <asp:ListItem  Text="Todas"  Value="0" ></asp:ListItem>
                            <asp:ListItem  Text="Autorizar"  Value="10" ></asp:ListItem>
                            <asp:ListItem  Text="Con Fondos"  Value="20" ></asp:ListItem>
                            <asp:ListItem  Text="Autorizados sin deposito"  Value="30" ></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan ="3" style ="text-align :center ;color :red "> 
                        <asp:Literal ID ="ltMsg" runat ="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </asp:Panel><br />
        <asp:Panel ID ="pnSolFondeo"  runat ="server" Width ="100%" Height ="500px" ScrollBars ="Vertical" >
            <asp:Repeater ID="rptSolFondeo" runat="server" OnItemCommand="rptSolFondeo_ItemCommand" OnItemDataBound="rptSolFondeo_ItemDataBound"  >
                <HeaderTemplate>
                    <table id="tblSolFondeo" border="1" style ="width :100%;text-align :center" class ="tblFiltrar"  >
                        <thead>
                            <th scope="col">NO. LOTE</th>
                            <th scope="col">FECHA SOLICITUD</th>
                            <th scope="col">EMPRESA</th>
                            <th scope="col">FECHA AUTORIZACION FONDOS</th>
                            <th scope="col">NO. FACTURAS</th>
                            <th scope="col">IMPORTE FONDOS M.N</th>
                              <th scope="col">ESTADO</th>
                            <th scope="col">DETALLE</th>
                            <th scope="col">COMPROBANTE</th>
                            <th scope="col"></th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("IdFondeo")%></td>
                        <td><%# Eval("FechaRegistro","{0:d}")%></td>
                        <td><%# Eval("Empresa")%></td>
                        <td>
                            <asp:Label ID ="lbFechafd" runat ="server" Text='<%# Eval("FechaFondos","{0:d}")%>'></asp:Label>
                        </td>
                        <td><%# Eval("NoSolicitudes")%></td>
                        <td><%# Eval("Total","{0:0,0.00}")%></td>   
                        <td><%# Eval("Estado")%></td>
                        <td style="text-align :center;width:80px">
                            <asp:ImageButton ID="imgbtnSol"  runat ="server"  ImageUrl="~/img/Detalle.png" CommandName="Detalle" CommandArgument='<%# Eval("IdFondeo")%>' />
                        </td>
                        <td style="text-align :center;width:80px">
                            <asp:ImageButton ID="imgBtnComp"  runat ="server"  ImageUrl="~/img/arDocto.png" CommandName="Comprobante" CommandArgument='<%# Eval("IdFondeo")%>' Visible ="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </fieldset> 
</asp:Content>
