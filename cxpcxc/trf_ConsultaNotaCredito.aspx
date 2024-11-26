<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_ConsultaNotaCredito.aspx.cs" Inherits="cxpcxc.trf_ConsultaNotaCrcedito" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID ="hdConsulta" runat ="server" />
    <fieldset>
        <legend>NOTAS DE CREDITO</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="text-align: right;">
                    <div id="dvBtns" style="width:100%; text-align:right;">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar"  CssClass="button"  OnClick="btnRegistrar_Click" />&nbsp;&nbsp;
                        <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                    </div>
               </td>
            </tr>
        </table><br />
        <table id="tblConsulta" runat="server" style="width: 60%; margin: 0 auto" class ="tblConsulta">
            <tr>
                <td style="width: 150px">PROVEEDOR:</td>
                <td><asp:DropDownList ID="dpProveedor" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="auto-style1">FECHA REGISTRO:</td>
                <td class="auto-style1">
                    <table>
                        <tr>
                            <td>DEL:</td>
                            <td style ="vertical-align :top" >
                                <asp:TextBox ID="txF_Inicio" runat="server" Width="80px" style="margin-bottom: 0px" ></asp:TextBox>
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
                <td> <asp:LinkButton ID ="lkLimpiar" runat ="server"  Text ="Limpiar" OnClick="lkLimpiar_Click"></asp:LinkButton> </td>
                <td style ="text-align :right">
                    <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
            </tr>
        </table><br />
        <asp:Panel ID ="pnNotaCredito"  runat ="server" Height ="450px" ScrollBars ="Vertical">
            <asp:Repeater ID="rptNotaCredito" runat="server" OnItemCommand="rptNotaCredito_ItemCommand"  >
                <HeaderTemplate>
                    <table id="tblNotaCredito" border="1" style ="width :98%; margin :0 auto" class ="tblFiltrar"  >
                        <thead>
                            <th scope="col">FECHA</th>
                            <th scope="col">PROVEEDOR</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col"></th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("Fecha","{0:d}")%></td>
                        <td><%# Eval("Proveedor")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td><%# Eval("Importe","{0:0,0.00}")%></td>
                        <td ><%# Eval("Moneda")%></td>
                        <td ><%# Eval("Estado")%></td>
                        <td style="text-align :center;width:50px">
                            <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdNotaCredito")%>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </fieldset>
</asp:Content>
