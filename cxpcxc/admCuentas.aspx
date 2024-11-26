<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admCuentas.aspx.cs" Inherits="cxpcxc.admCuentas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            return resultado;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID="hdIdPvd" runat ="server" />
    <fieldset>
        <legend>CUENTAS DEL PROVEEDOR</legend>
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
        <div id ="dvProveedor" runat ="server">
            <table id="tblPrvd" style="width:70%; margin :0 auto" runat ="server">
                <tr>
                    <td colspan ="2" ><b><asp:Label ID ="lbNombre" runat ="server" Font-Size="Large" ></asp:Label></b></td>
                </tr>
                <tr>
                    <td style ="width :25%">RFC:</td>
                    <td><asp:Label ID ="lbRfc" runat ="server" Font-Size="Small"></asp:Label></td>
                </tr>
                <tr>
                    <td>CONTACTO:</td>
                    <td><asp:Label ID ="lbContacto" runat ="server" Font-Size="Small"></asp:Label></td>
                </tr>
                <tr>
                    <td>CORREO:</td>
                    <td><asp:Label ID ="lbCorreo" runat ="server" Font-Size="Small"></asp:Label></td>
                </tr>
                <tr>
                    <td>TELEFONO:</td>
                    <td>
                        <asp:Label ID ="lbTelefono" runat ="server" Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID ="lbEtq" runat ="server" Text ="Ext:" Font-Size="Small"></asp:Label>
                        <asp:Label ID ="lbExtencion" runat ="server" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
            </table>
        </div><br />
        <div id="dvCatCuentas" runat ="server"  style ="width:100%">
            <table id="tblCuenta" runat ="server" style="width:70%; text-align:left; margin:0 auto;" >
                <tr>
                    <td style="width:25%;">BANCO:</td>
                    <td style="width:75%">
                        <asp:TextBox ID="txBanco" runat ="server"  MaxLength ="80" Width ="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvBanco" runat="server" ControlToValidate="txBanco" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNBanco" runat="server" TargetControlID="txBanco" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                    </td>
                </tr>
                <tr>
                    <td>CUENTA:</td>
                    <td>
                        <asp:TextBox ID="txCuenta" runat ="server" MaxLength ="20" Width ="90%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCuenta" runat="server" ControlToValidate="txCuenta" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCuenta" runat="server" TargetControlID="txCuenta" FilterMode="ValidChars" ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"  />
                    </td>
                </tr>
                <tr>
                    <td>CLABE:</td>
                    <td>
                        <asp:TextBox ID="txClabe" runat ="server"  MaxLength ="20" Width ="90%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteClabe" runat="server" TargetControlID="txClabe" FilterMode="ValidChars" ValidChars="0123456789" />
                        <asp:RequiredFieldValidator ID="rtvClabe" runat="server" ControlToValidate="txClabe" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>SUCURSAL:</td>
                    <td>
                        <asp:TextBox ID="txSucursal" runat ="server"  MaxLength ="20" Width ="90%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteSucursal" runat="server" TargetControlID="txSucursal" FilterMode="ValidChars" ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" />
                    </td>
                </tr>
                <tr>
                    <td>TIPO DE MONEDA:</td>
                    <td>
                        <asp:DropDownList ID="dpMoneda" runat ="server"  >
                            <asp:ListItem Value="0" Text ="Seleccionar"> </asp:ListItem>
                            <asp:ListItem Value="Pesos" Text ="Pesos"> </asp:ListItem>
                            <asp:ListItem Value="Dólares" Text ="Dólares"> </asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvMoneda" runat="server" ControlToValidate="dpMoneda" ErrorMessage="*"  ForeColor="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;height:40px">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClientClick="return Confirmar();"  OnClick="btnGuardar_Click" />
                    </td>
                </tr>
            </table>
        </div><br />
        <asp:Panel ID ="pnCuentas"  runat ="server" Width ="100%">
            <asp:Repeater ID="rptCuentas" runat="serveR" OnItemCommand="rptProveedor_ItemCommand" >
                <HeaderTemplate>
                    <table id="tblCtas" border="1" style ="width :100%" class ="tblFiltrar"  >
                        <thead>
                            <th scope="col">BANCO</th>
                            <th scope="col">CUENTA</th>
                            <th scope="col">CTA CLABE</th>
                            <th scope="col">SUCURSAL</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col"></th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("Banco")%></td>
                        <td><%# Eval("NoCuenta")%></td>
                        <td><%# Eval("CtaClabe")%></td>
                        <td><%# Eval("Sucursal")%></td>
                        <td><%# Eval("Moneda")%></td>
                        <td style="text-align :center;width:80px">
                            <asp:ImageButton ID="imgbtnQuitar"  runat ="server"  ImageUrl="~/img/delete.png" CommandName="Quitar" CommandArgument='<%# Eval("NoCuenta")%>'  CausesValidation="false"  ToolTip ="Quitar"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>

    </fieldset>
</asp:Content>
