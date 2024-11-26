<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admCatRechazos.aspx.cs" Inherits="cxpcxc.admCatRechazos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript">
       
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Desea continuar?'); }
            return resultado;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>CATALOGO DE RECHAZOS</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 85%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table>
        <div id ="dvEmpresa" runat ="server" style ="width :100%; text-align :center; font-size:17px;">
            <asp:Literal  ID ="ltEmpresa" runat ="server" ></asp:Literal>
        </div><br />
        <div id="dvCampos" runat ="server"  style ="width:100%">
            <table id="tblCampos" runat ="server" style="width:85%; text-align:left; margin:0 auto;" >
                <tr>
                    <td style="width:25%;">MOTIVO DE RECHAZO:</td>
                    <td>
                        <asp:TextBox ID="txRechazo" runat ="server"  MaxLength ="80" Width ="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRechazo" runat="server" ControlToValidate="txRechazo" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxtoolkit:filteredtextboxextender ID="fteRechazo" runat="server" TargetControlID="txRechazo" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                    </td>
                </tr>
                <tr><td colspan ="2" style ="height :10px"></td></tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="button" OnClientClick="return Confirmar();" OnClick="btnModificar_Click" Visible="False" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnModCancela" runat="server" Text="Cancelar" CssClass="button" OnClick="btnModCancela_Click" Visible="False" />
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClientClick="return Confirmar();"  OnClick="btnGuardar_Click" />
                    </td>
                </tr>
            </table>
        </div><br />
        <asp:Panel ID ="pnRechazos"  runat ="server" Width ="100%">
            <asp:Repeater ID="rptRechazos" runat="serveR" OnItemCommand="rptRechazos_ItemCommand">
                <HeaderTemplate>
                    <table id="tblRechazos" border="1" style ="width :80%"  class="tblFiltrar" >
                        <thead>
                            <th scope="col">RECHAZO</th>
                            <th scope="col">EDITAR</th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("Titulo")%></td>
                        <td style="text-align:center; width:80px">
                            <asp:ImageButton ID="imgbtnEditar"  runat ="server"  ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("Id")%>'  CausesValidation="false"  ToolTip ="Editar"/>
                        </td>   
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        
    </fieldset>
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID ="hdIdCat" runat ="server" />
</asp:Content>
