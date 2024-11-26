<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admCatEmpresas.aspx.cs" Inherits="cxpcxc.admCatEmpresas" %>
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
        <legend>CATALOGO DE EMPRESAS</legend>
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
        </table>
        <div id="dvCatProveedores" runat ="server"  style ="width:100%">
            <table id="tblProveedor" runat ="server" style="width:85%; text-align:left; margin:0 auto;" >
                <tr>
                    <td style="width:20%;">RFC:</td>
                    <td>
                        <asp:TextBox ID="txRfc" runat ="server"  MaxLength ="16" Width ="30%" Style="text-transform: uppercase"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteRfc" runat="server" TargetControlID="txRfc" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789áéíóúÁÉÍÓÚ" />
                    </td>
                </tr>
                <tr>
                    <td>Nombre:</td>
                    <td>
                        <asp:TextBox ID="txNombre" runat ="server"  MaxLength ="80" Width ="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txNombre" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNombre" runat="server" TargetControlID="txNombre" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
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
        <asp:Panel ID ="pnEmpresas"  runat ="server" Width ="100%">
            <asp:Repeater ID="rptEmpresas" runat="serveR" OnItemCommand="rptEmpresas_ItemCommand" >
                <HeaderTemplate>
                    <table id="tblEmpresas" border="1" style ="width :100%"  class="tblFiltrar" >
                        <thead>
                            <th scope="col">FECHA</th>
                            <th scope="col">RFC</th>
                            <th scope="col">NOMBRE</th>
                            <th scope="col">EDITAR</th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style="width :150px"><%# Eval("FechaRegistro","{0:d}")%></td>
                        <td style="width :250px"><%# Eval("Rfc")%></td>
                        <td><%# Eval("Nombre")%></td>
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
</asp:Content>
