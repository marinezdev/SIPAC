<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admEmpresasUDN.aspx.cs" Inherits="cxpcxc.admEmpresasUDN" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
        <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>ASIGNAR UNIDADES DE NEGOCIO A EMPRESAS</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br />

        <asp:Panel ID ="pnConsulta" runat="server">
            <table id ="tbConsulta" runat ="server" style ="width:80%;margin:0 auto">
                <tr>
                    <td style="width:23%">EMPRESA:</td>
                    <td>
                        <asp:DropDownList ID="dpEmpresa" runat ="server" Width="450px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ControlToValidate="dpEmpresa" ErrorMessage="*"  ForeColor="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:left">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="button" OnClick="btnBuscar_Click" />
                    </td>
                </tr>               
                <tr id="trCliente" runat="server" visible="false">
                    <td style ="width:23%">UNIDAD de NEGOCIO:</td>
                    <td><asp:DropDownList ID ="dpUDN" runat="server"></asp:DropDownList></td>
                    <td></td>
                </tr>
                <tr id="trEstado" runat="server" visible="false">
                    <td>ESTADO:</td>
                    <td colspan="2"> <asp:CheckBox ID ="chkActivo" runat ="server" Text="Activo"></asp:CheckBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align:left;">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" Visible="false" CssClass="button" OnClick="btnGuardar_Click" /> 
                        <asp:Button ID="btnGuardarModificado" runat="server" Text="Guardar" CssClass="button" Visible="false"  OnClick="btnGuardarModificado_Click"/>
                    </td>
                    <td></td>
                </tr>

            </table>
        </asp:Panel>
        
        <br />

        <asp:Panel ID ="pnEmpresasUnidadNegocio"  runat ="server" Width="100%">
            <asp:Repeater ID="rptEmpresasUnidadNegocio" runat="serveR" OnItemCommand="rptEmpresasUnidadNegocio_ItemCommand" >
                <HeaderTemplate>
                    <table id="tblEmpresasUnidadNegocio" border="1" style ="width :100%"  class="tblFiltrar" >
                        <thead>
                            <th scope="col">EMPRESA</th>
                            <th scope="col">UNIDAD de NEGOCIO</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col">EDITAR</th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style="width :250px"><%# Eval("nombreempresa","{0:d}")%></td>
                        <td style="width :250px"><%# Eval("tituloudn")%></td>
                        <td style="width: 50px; text-align: center"><%# Eval("activo").ToString() == "True" ? "Activo" : "Inactivo"%></td>
                        <td style="text-align:center; width:80px">
                            <asp:ImageButton ID="imgbtnEditar"  runat ="server"  ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("Idempresa") +","+ Eval("idudn")%>'  CausesValidation="false"  ToolTip ="Editar"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>        
        
    </fieldset>
    <asp:HiddenField ID ="hdIdUDN" runat ="server" />

</asp:Content>
