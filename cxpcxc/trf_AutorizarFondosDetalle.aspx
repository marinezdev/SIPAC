<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AutorizarFondosDetalle.aspx.cs" Inherits="cxpcxc.trf_AutorizarFondosDetalle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">

       function AsignarFondos() {
            var resultado = false;
            if (Page_ClientValidate("gpSol") == true) {
                if (confirm('¿Esta seguro que desea continuar?')) {
                    $("#dvBtns").hide();
                    resultado = true;
                }
            } else { alert("Los montos en las solicitudes no son correctos") }
            return resultado;
        }

        
        function validarArchivo() {
            var resultado = false;
            if (Page_ClientValidate("gpArh") == true) {
                resultado = true;
            }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>SOLICITUD DE FONDOS</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="width: 75%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <div id="dvBtns" style="width: 100%; text-align: right;">
                        <asp:Button ID="btnAceptarFodos" runat="server" Text="Aceptar" CssClass="button" OnClientClick="return AsignarFondos();" OnClick="btnAceptarFodos_Click" />&nbsp;  
                        <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <br />
        
        <div id="dvCabecera" runat="server" style="width: 100%">
            <table id="tblCabecera" runat="server" style="width: 100%">
                <tr>
                    <td style="width: 100px"><b>LOTE:</b></td>
                    <td>
                        <asp:Label ID="lblote" runat="server" Font-Size="18px"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="udpSolDet" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnSolDet" runat="server" Width="100%" Height="350px" ScrollBars="Vertical">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 150px"><b>TOTAL M.N.:</b></td>
                            <td style="width: 200px">
                                <asp:Label ID="lbTotal" runat="server" Text="0" Font-Bold="true" Font-Size="Large"></asp:Label></td>
                            <td style="width: 60px"><b>T.C.:</b></td>
                            <td>
                                <asp:Label ID="lbTC" runat="server" Text="0" Font-Bold="true" Font-Size="Large"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:Repeater ID="rptSolDet" runat="server" OnItemCommand="rptSolDet_ItemCommand" OnItemDataBound="rptSolDet_ItemDataBound">
                        <HeaderTemplate>
                            <table id="tblSolDet" border="1" style="width: 100%" class="tblFiltrar">
                                <caption style="font-size: 13px; font-weight: 600">SOLICITUDES</caption>
                                <thead>
                                    <th scope="col">UNIDAD DE NEGOCIO</th>
                                    <th scope="col">SOLICITANTE</th>
                                    <th scope="col">REGISTRO</th>
                                    <th scope="col">NO. FACTURA</th>
                                    <th scope="col">FECHA FACTURA</th>
                                    <th scope="col">PROVEEDOR</th>
                                    <th scope="col">DESCRIPCION</th>
                                    <th scope="col">IMPORTE</th>
                                    <th scope="col">MONEDA</th>
                                    <th scope="col">FACTURA</th>
                                    <th scope="col">CANTIDAD PAGAR</th>
                                    <th scope="col">CANTIDAD FONDEAR</th>
                                    <th scope="col">VER</th>
                                    <th scope="col">ACEPTAR</th>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: White; color: #333333">
                                <td><%# Eval("UnidadNegocio")%></td>
                                <td><%# Eval("Solicitante")%></td>
                                <td><%# Eval("FechaRegistro","{0:d}")%></td>
                                <td><%# Eval("Factura")%></td>
                                <td><%# Eval("FechaFactura","{0:d}")%></td>
                                <td><%# Eval("Proveedor")%></td>
                                <td><%# Eval("DescProyecto ")%></td>
                                <td>
                                    <asp:Label ID="lbImporte" runat="server" Text='<%# Eval("Importe","{0:0,0.00}")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbTpMoneda" runat="server" Text='<%# Eval("Moneda")%>'></asp:Label>
                                </td>
                                <td style="text-align: center;"><%# Eval("ConFactura")%></td>
                                <td><%# Eval("ImporteAutorizado","{0:0,0.00}") %></td>
                                <td style="text-align: center; width: 100px">
                                    <asp:TextBox ID="txCantidadPagar" runat="server" Width="80%" Text='<%# Eval("ImporteAutorizado","{0:0,0.00}") %>' OnTextChanged="chkAutorizar_CheckedChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCantidadPagar" runat="server" ControlToValidate="txCantidadPagar" ErrorMessage="*" ForeColor="Red" ValidationGroup="gpSol"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteCantidadPagar" runat="server" TargetControlID="txCantidadPagar" FilterMode="ValidChars" ValidChars="0123456789.," />
                                </td>
                                <td style="text-align: center; width: 40px">
                                    <asp:ImageButton ID="imgbtnVer" runat="server" ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' CausesValidation="false" />
                                </td>
                                <td style="text-align: center; width: 40px">
                                    <asp:CheckBox ID="chkAutorizar" runat="server" OnCheckedChanged="chkAutorizar_CheckedChanged" AutoPostBack="true" Checked="true" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></tbody></table></FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel> <br /><br />
            
        <asp:Panel ID="pnAnexaPago" runat="server" Width="100%">
            <div style ="color :white; Background:darkgreen;font-size :15px">REGISTRAR  COMPROBANTE</div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table id="tblRegPago" runat="server" style="width: 98%; margin: 0 auto;">
                        <tr>
                            <td style="width: 25%">Comprobante (PDF):</td>
                            <td>
                                <asp:FileUpload ID="fulComprobante" runat="server" Width="90%" AllowMultiple="true" />
                                <asp:RequiredFieldValidator ID="rfvfulComprobante" runat="server" ControlToValidate="fulComprobante" ErrorMessage="*" ForeColor="Red" ValidationGroup="gpArh"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revComprobante" ControlToValidate="fulComprobante" ValidationGroup="gpArh"
                                    ErrorMessage="*" ForeColor="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$" />
                            </td>
                            <td style="width: 25%">
                                <asp:Button ID="btnCargar" runat="server" CssClass="button" Text="Cargar" OnClick="btnCargar_Click" OnClientClick="return validarArchivo();" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Panel ID="pnlDocumento" runat="server" Height="500px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" Visible="false">
                        <b>
                            <asp:Literal ID="ltNumDoctos" runat="server"></asp:Literal></b><br />
                        <br />
                        <div style="width: 95%; height: 450px; overflow-y: scroll; margin: 0 auto">
                            <asp:Literal ID="ltDocumento" runat="server"></asp:Literal>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCargar" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </fieldset>
</asp:Content>
