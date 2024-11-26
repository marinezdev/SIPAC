<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_SolConFactura.aspx.cs" Inherits="cxpcxc.trf_SolConFactura" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            if (resultado) { $("#dvBtns").hide(); }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdIdpvd" runat ="server" />
    <asp:HiddenField ID="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID="hdLLaveSol" runat ="server" />
    <fieldset>
        <legend>REGISTRAR SOLICITUD DE TRANSFERENCIA</legend>
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
        </table><br />
        <asp:Panel ID="pnDatosXml" runat ="server" Width ="100%" Visible ="false" Font-Size ="12px" >
            <table id ="tblDatosXml" runat ="server" style ="width:90%; margin :0 auto;" >
                <tr>
                    <td colspan ="4" class="Titulos" >
                        <asp:Label ID="lbProveedor" runat ="server"  Width ="95%" Font-Bold ="true"  ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="width:20%;font-weight: 700;">RFC:</td>
                    <td>
                        <asp:Label ID="lbRfc" runat ="server"  Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr><td colspan ="2" style ="height :10px"></td></tr>
                <tr>
                    <td style ="font-weight: 700;">Factura:</td>
                    <td>
                        <asp:Label ID="lbFactura" runat ="server" Font-Size ="14px" Width ="95%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Fecha Factura:</td>
                    <td>
                        <asp:Label  ID="lbFhFactura" runat ="server" Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Importe:</td>
                    <td>
                        <asp:Label  ID="lbImporte" runat ="server"  Width ="95%" ></asp:Label>
                    </td>
                </tr>
                <tr><td colspan ="2" style ="height :10px"></td></tr>
                <tr>
                    <td style ="vertical-align :top;font-weight: 700;">Concepto:</td>
                    <td>
                        <asp:Label  ID="lbConcepto" runat ="server" Width ="95%" ></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel><br />

        <asp:Panel ID ="pnSelCuenta" runat ="server" Width ="100%" Visible ="false">
            <asp:Repeater ID="rptCuentas" runat="serveR" OnItemCommand="rptProveedor_ItemCommand" >
                <HeaderTemplate>
                    <table id="tblCtas" border="1" style ="width :100%"  class ="tblFiltrar" >
                        <caption ><h4>SELECCIONA LA CUENTA DE DEPOSITO</h4></caption>
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
                            <asp:ImageButton ID="imgbtnSel"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="Seleccionar" CommandArgument='<%# Eval("NoCuenta")%>'  CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        
        <asp:Panel ID="pnCuenta" runat ="server" Width ="100%" Visible ="false" Font-Size ="12px" >
            <table id ="tblCuenta" runat ="server" style ="width:90%; margin :0 auto;" >
                <tr>
                    <td style ="width:20%;font-weight: 700;">Banco:</td>
                    <td>
                        <asp:Label  ID="lbBanco" runat ="server"  Width ="90%"></asp:Label>
                    </td>
                    <td style ="width:20%;font-weight: 700;">Cuenta:</td>
                    <td>
                        <asp:Label  ID="lbCuenta" runat ="server"  Width ="90%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Cuenta Clabe:</td>
                    <td>
                        <asp:Label ID="lbClabe" runat ="server"  Width ="90%"></asp:Label>
                    </td>
                    <td style ="font-weight: 700;">Sucursal:</td>
                    <td>
                        <asp:Label ID="lbSucursal" runat ="server"  Width ="90%" ></asp:Label>
                    </td>
                </tr>
            </table><br />
            <table id ="tblSol" runat ="server" style ="width:90%; margin :0 auto">
                <%--<tr>
                    <td style ="width:20%">Importe Con letra:</td>
                    <td colspan ="3">
                        <asp:TextBox  ID="txImpLetra" runat ="server" Width ="95%" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvImpLetra" runat="server" ControlToValidate="txImpLetra" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteImpLetra" runat="server" TargetControlID="txImpLetra" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789.,/" />
                    </td>
                </tr>--%>
                <tr>
                    <td style ="width:20%">Condiciones de pago:</td>
                    <td colspan ="3">
                        <asp:DropDownList ID="dpCondPago" runat ="server"  Width ="450px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvImpLetra" runat="server" ControlToValidate="dpCondPago" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style ="width:20%">Proyecto:</td>
                    <td colspan ="3">
                        <asp:DropDownList ID="dpProyecto" runat ="server" Width ="450px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvfProyecto" runat="server" ControlToValidate="dpProyecto" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style ="width:20%">Descripcion:</td>
                    <td colspan ="3">
                        <asp:TextBox  ID="txDecProyecto" runat ="server" Width ="95%" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteDecProyecto" runat="server" TargetControlID="txDecProyecto" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                        <asp:RequiredFieldValidator ID="rfvDecProyecto" runat="server" ControlToValidate="txDecProyecto" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style ="width:20%">Tipo de Moneda:</td>
                    <td colspan ="3">
                        <asp:DropDownList ID ="dpTpMoneda" runat ="server" Width ="250px"  >
                            <asp:ListItem  Value ="0"  Text="Seleccionar" > </asp:ListItem>
                            <asp:ListItem  Value ="Pesos"  Text="Pesos"> </asp:ListItem>
                            <asp:ListItem  Value ="Dolares"  Text="Dolares" > </asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvTpMoneda" runat="server" ControlToValidate="dpTpMoneda" ErrorMessage="*"  ForeColor="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan ="4" style ="text-align :right">
                        <div id="dvBtns" style="width:100%; text-align:right;">
                            <asp:Button ID="btnGuardar" runat ="server"  Text ="Guardar"  CssClass="button" OnClick="btnGuardar_Click" OnClientClick ="return Confirmar();"/>
                        </div> 
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>
</asp:Content>
