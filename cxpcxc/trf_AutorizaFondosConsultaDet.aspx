<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AutorizaFondosConsultaDet.aspx.cs" Inherits="cxpcxc.trf_AutorizaFondosConsultaDet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript">
        function validar() {
            var resultado = false;
            if (confirm('¿Esta seguro que desea continuar?')) {
                $("#dvBtns").hide();
                resultado = true;
            }

            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager  ID="SM"  runat="server"  ></asp:ScriptManager>
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server" />
    <div style ="text-align :right">
        <asp:Button ID="btnCerrarConsul" runat="server" Text="Cerrar"  CssClass="button" CausesValidation ="false" OnClick="btnCerrarConsul_Click"  />        
    </div>
    <div id ="Div1"  runat="server" style ="width :100%">
        <table runat ="server" style ="width:100%" >
                <tr>
                    <td style="width:25%"><b>LOTE:</b></td>
                    <td style="width:30%"><asp:Label ID ="lbLote" runat ="server" Font-Size ="18px" ></asp:Label> </td>
                    <td style="width:25%" ><b>T.C.:</b></td>
                    <td style="width:30%"><asp:Label  ID="lbTC" runat ="server" Text ="0" Font-Bold ="true" ></asp:Label></td>
                </tr>
                <tr>
                    <td><b>TOTAL SOLICITADO M.N.:</b></td>
                    <td colspan ="3"><asp:Label  ID="lbTotal" runat ="server" Text ="0" Font-Bold ="true"  ></asp:Label></td>
                </tr>
                <tr>
                    <td><b>TOTAL AUTORIZADO M.N.:</b></td>
                    <td colspan ="3"><asp:Label  ID="lbTotalFd" runat ="server" Text ="0" Font-Bold ="true"  ></asp:Label></td>
                </tr>
            </table>
        </div>
        <asp:Panel ID ="pnSolDet"  runat ="server" Width ="100%" HorizontalAlign="Center"  >
             <b><asp:Label ID ="lbTotSol" runat ="server" /></b>
            <asp:Repeater ID="rpSolDet" runat="server" OnItemCommand="rpSolDet_ItemCommand" OnItemDataBound="rpSolDet_ItemDataBound"  >
                <HeaderTemplate>
                    <table id="tblSolDet" border="1" style ="width :100%" class ="tblFiltrar"  >
                        <thead>
                            <th scope="col">UNIDAD DE NEGOCIO</th>
                            <th scope="col">SOLICITANTE</th>
                            <th scope="col">REGISTRO</th>
                            <th scope="col">NO. FACTURA</th>
                            <th scope="col">FECHA FACTURA</th>
                            <th scope="col">PROVEEDOR</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope ="col">FACTURA</th>
                            <th scope="col">CANTIDAD PAGAR</th>
                            <th scope="col">CANTIDAD FONDOS</th>
                            <th scope="col">APROB.</th>
                            <th scope="col">VER</th>
                            <th scope="col"></th>
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
                        <td><asp:Label ID="lbImporte" runat="server" Text='<%# Eval("Importe","{0:0,0.00}")%>'></asp:Label> </td>
                        <td><asp:Label ID="lbTpMoneda" runat="server" Text='<%# Eval("Moneda")%>'></asp:Label> </td>
                        <td style="text-align :center;"><%# Eval("ConFactura")%></td>
                        <td><%# Eval("ImporteAutorizado","{0:0,0.00}") %></td>
                        <td style="text-align :center;width:100px">
                            <asp:label ID ="lbCantidadPagar" runat ="server" Width ="80%"  Text ='<%# Eval("ImporteFondos","{0:0,0.00}") %>'></asp:label>
                        </td>
                        <td style="text-align :center;width:40px">
                            <asp:Image ID="imgChek"  runat ="server"  ImageUrl="~/img/action_check.png"  />
                        </td>
                        <td style="text-align :center;width:40px">
                            <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' CausesValidation ="false"  />
                        </td>
                    </tr>
                </ItemTemplate>
            <FooterTemplate></tbody></table></FooterTemplate>
        </asp:Repeater>
    </asp:Panel><br /><br />
    <asp:Panel ID="pnAnexaPago" runat="server" Width="100%" Visible ="false">
        <div style ="color :white; Background:darkgreen">AGREGAR COMPROBANTE</div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <table id="tblRegPago" runat="server" style="width: 98%; margin: 0 auto;">
                    <tr>
                        <td colspan ="4" style="width: 75%; text-align: center; color: red;">
                            <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                         </td>
                    </tr>
                    <tr>
                        <td style="width: 17%">Comprobante (PDF):</td>
                        <td>
                            <asp:FileUpload ID="fulComprobante" runat="server" Width="90%" AllowMultiple="true" />
                            <asp:RequiredFieldValidator ID="rfvfulComprobante" runat="server" ControlToValidate="fulComprobante" ErrorMessage="*" ForeColor="Red" ValidationGroup="gpArh"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ID="revComprobante" ControlToValidate="fulComprobante" ValidationGroup="gpArh"
                                ErrorMessage="*" ForeColor="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$" />
                        </td>
                        <td  style="width: 20%">
                            <asp:Button ID="btnCargar" runat="server" CssClass="button" Text="Cargar" OnClick="btnCargar_Click" OnClientClick="return validarArchivo();" /> 
                        </td>
                        <td style="width: 20%;text-align :right">
                            
                            <asp:Button ID="btnNvoComprobante" runat="server" Text="Aceptar" CssClass="button" OnClientClick="return validar();" OnClick="btnNvoComprobante_Click"  />
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

</asp:Content>
