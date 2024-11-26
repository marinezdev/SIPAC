<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="BuscarBeneficiario.aspx.cs" Inherits="cxpcxc.BuscarBeneficiario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript">
        //$(document).ready(function () {
        //    $(function () {
        //        $("[id*=tblCtas] td").hover(function () {
        //            $("td", $(this).closest("tr")).addClass("hover_row");
        //        }, function () {
        //            $("td", $(this).closest("tr")).removeClass("hover_row");
        //        });
        //    });
        //});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    <asp:HiddenField ID="hdSinFactura" runat ="server" />
    <asp:HiddenField ID="hdIdPvd" runat ="server" />
    <fieldset>
        <legend>SELECCIONAR PROVEEDOR</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="udpCuentas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table id="tblBeneficiario" style="width:85%; margin:auto; " >
                    <tr>
                        <td style ="width :20%">PROVEEDOR:</td>
                        <td>
                            <asp:DropDownList ID="dpProveedor" runat ="server" AutoPostBack ="true" OnSelectedIndexChanged="dpProveedor_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                </table><br />
                <asp:Panel Id="pnCuentas" runat ="server" Width ="100%" Visible ="false" >
                    <table id="tblPrvd" style="width:85%; margin :0 auto" runat ="server">
                        <tr>
                            <td colspan ="2" ><b><asp:Label ID ="lbNombre" runat ="server" Font-Size="Large" ></asp:Label></b></td>
                        </tr>
                        <tr>
                            <td style ="width :15%">RFC:</td>
                            <td style ="width :35%"><asp:Label ID ="lbRfc" runat ="server" Font-Size="Small"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>CONTACTO:</td>
                            <td><asp:Label ID ="lbContacto" runat ="server" Font-Size="Small"></asp:Label></td>
                            <td style ="width :15%">CORREO:</td>
                            <td><asp:Label ID ="lbCorreo" runat ="server" Font-Size="Small"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>TELEFONO:</td>
                            <td>
                                <asp:Label ID ="lbTelefono" runat ="server" Font-Size="Small"></asp:Label>&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>EXT:</td>
                            <td><asp:Label ID ="lbExtencion" runat ="server" Font-Size="Small"></asp:Label></td>
                        </tr>
                    </table><br />
                    <asp:Repeater ID="rptCuentas" runat="server" OnItemCommand="rptCuentas_ItemCommand"  >
                        <HeaderTemplate>
                            <table id="tblCtas" border="1" style ="width :100%;font-size:x-small" class="tblFiltrar" >
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
                                    <asp:ImageButton ID="imgbtnRegistrar"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="Registrar" CommandArgument='<%# Eval("NoCuenta")%>'  CausesValidation="false"  ToolTip ="Registrar"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></tbody></table></FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
