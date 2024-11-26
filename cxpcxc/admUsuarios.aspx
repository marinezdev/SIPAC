<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admUsuarios.aspx.cs" Inherits="cxpcxc.admUsuarios" %>
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
    <asp:HiddenField ID ="hdIdUsr" runat ="server" />
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server" />
    <fieldset>
        <legend>CATALOGO DE USUARIOS</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 85%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click"  />
                </td>
            </tr>
        </table><br />
        <div id ="dvEmpresa" runat ="server" style ="width :100%; text-align :center; font-size:17px">
            <asp:Literal  ID ="ltEmpresa" runat ="server" ></asp:Literal>
        </div><br />
        <div id="dvCatProveedores" runat ="server"  style ="width:100%">
            <table id="tblProveedor" runat ="server" style="width:85%; text-align:left; margin:0 auto;" >
                <tr>
                    <td style="width:22%;">NOMBRE:</td>
                    <td>
                        <asp:TextBox ID="txNombre" runat ="server"  MaxLength ="80" Width ="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txNombre" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNombre" runat="server" TargetControlID="txNombre" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                    </td>
                </tr>
                <tr>
                    <td>USUARIO:</td>
                    <td>
                        <asp:TextBox ID="txUsuario" runat ="server"  MaxLength ="32" Width ="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txUsuario" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteRfc" runat="server" TargetControlID="txUsuario" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789áéíóúÁÉÍÓÚ" />
                    </td>
                </tr>
                <tr>
                    <td>CLAVE:</td>
                    <td>
                        <asp:TextBox ID="txClave" runat ="server"  MaxLength ="32" Width ="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txClave" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteClave" runat="server" TargetControlID="txClave" FilterMode="ValidChars" 
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-!#$%&/()=?" />

                    </td>
                </tr>
                
                <tr>
                    <td>UNIDAD DE NEGOCIO:</td>
                    <td>
                        <asp:DropDownList ID="dpUdNegocio" runat ="server" Width ="250px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvUndNegocio" runat="server" ControlToValidate="dpUdNegocio" ErrorMessage="*"  ForeColor="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>PERFIL:</td>
                    <td>
                        <asp:DropDownList ID="dpGrupo" runat ="server" Width ="250px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvGrupo" runat="server" ControlToValidate="dpGrupo" ErrorMessage="*"  ForeColor="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>CORREO:</td>
                    <td>
                        <asp:TextBox ID="txCorreo" runat ="server"  MaxLength ="80" Width ="70%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCorreo" runat="server" TargetControlID="txCorreo" FilterMode="ValidChars" ValidChars="!#$%&*+-./0123456789=?@ABCDEFGHIJKLMNOPQRSTUVWXYZ^_abcdefghijklmnopqrstuvwxyz" />
                        <asp:RequiredFieldValidator ID="rtvCorreo" runat="server" ControlToValidate="txCorreo" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revCorreo" runat="server" 
                            ErrorMessage="*"
                            ControlToValidate="txCorreo"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ForeColor="Red">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td> <asp:CheckBox ID ="chkEstado" runat ="server" Text ="Activo" Checked ="true" ></asp:CheckBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="button" OnClientClick="return Confirmar();" OnClick="btnModificar_Click" Visible="False" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnModCancela" runat="server" Text="Cancelar" CssClass="button" OnClick="btnModCancela_Click" Visible="False" CausesValidation ="false" />
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClientClick="return Confirmar();"  OnClick="btnGuardar_Click" />
                    </td>
                </tr>
            </table>
        </div><br />
        <asp:Panel ID ="pnUsuarios"  runat ="server" Width ="100%" Height ="450px" ScrollBars ="Auto">
            <asp:Repeater ID="rptUsuarios" runat="serveR" OnItemCommand="rptProveedor_ItemCommand" >
                <HeaderTemplate>
                    <table id="tblUsuarios" border="1" style ="width :100%" class ="tblFiltrar"  >
                        <thead>
                            <th scope="col">NOMBRE</th>
                            <th scope="col">USUARIO</th>
                            <th scope="col">PERFIL</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col">EDITAR</th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("Nombre")%></td>
                        <td><%# Eval("Usuario")%></td>
                        <td><%# Eval("Grupo")%></td>
                        <td><%# Eval("Estado")%></td>
                        <td style="text-align:center; width:80px">
                            <asp:ImageButton ID="imgbtnEditar"  runat ="server"  ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("IdUsr")%>'  CausesValidation="false"  ToolTip ="Editar"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        
    </fieldset>

</asp:Content>
