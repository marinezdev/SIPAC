<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_AgregaPagoGrupo.aspx.cs" Inherits="cxpcxc.cxc_AgregaPagoGrupo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript">
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate('gral') == true) {
                resultado = confirm('¿Esta seguro de continuar?');
                if (resultado) { $("#dvBtnsMod").hide(); }
            }
            
            return resultado
        }

        function ValidaArchivo() {
            var resultado = false;
            if (Page_ClientValidate("arh") == true) { resultado = true; }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>AGREGAR COMPROBANTE DE PAGO</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 80%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <div id="dvBtns" style="width: 100%; text-align: right;">  
                        <asp:Button ID="btnRegPago" runat ="server"  Text ="Aceptar" CssClass="button" OnClientClick ="return Confirmar();" OnClick="btnRegPago_Click" Visible="false" />&nbsp;
                        <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                    </div> 
                </td>
            </tr>
        </table>
        <br />
       <table id="tblTotales" runat ="server"  >
           <tr>
               <td>TOTAL PESOS:</td>
               <td><asp:Label ID ="lbTotPesos" runat ="server"  Text ="0" /></td>
           </tr>
           <tr>
               <td>TOTAL DOLARES:</td>
               <td><asp:Label ID ="LbTotDll" runat ="server"  Text ="0" /></td>
           </tr>
       </table><br />
        <asp:Panel ID ="pnfacturas" runat ="server" >
            <asp:Repeater ID="rptOrdFact" runat="server"  >
                <HeaderTemplate>
                    <table id="tblOrdFact" border="1" style="width: 100%" class="tblFiltrar">
                        <thead>
                            <th scope="col">ORDEN VENTA</th>
                            <th scope="col">ORDEN FACTURA</th>
                            <th scope="col">CLIENTE</th>
                            <th scope="col">FECHA</th>
                            <th scope="col">FACTURA</th>
                            <th scope="col">SERVICIO</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">ESTADO</th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style ="width :0px;text-align :center"><asp:Label ID="lbServicio" runat="server" Text='<%# Eval("IdServicio")%>'></asp:Label> </td>
                        <td style ="width :70px;text-align :center"><asp:Label ID="lbIdOrdFac" runat="server" Text='<%# Eval("IdOrdenFactura")%>'></asp:Label> </td>
                        <td style ="width :70px;text-align :center"><asp:Label ID="lbCliente" runat="server" Text='<%# Eval("Cliente")%>'></asp:Label> </td>
                        <td style ="width :90px;text-align :center"><asp:Label ID="FechaInicio" runat="server" Text='<%# Eval("FechaInicio","{0:d}")%>'></asp:Label></td>
                        <td style ="width :70px;text-align :center"><asp:Label ID="lbNumFac" runat="server" Text='<%# Eval("NumFactura")%>'></asp:Label> </td>
                        <td><%# Eval("Servicio")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td style ="width :110px;text-align :right"><asp:Label ID="lbImporte" runat="server" Text='<%# Eval("Importe","{0:0,0.00}")%>'></asp:Label> </td>
                        <td style ="width :90px;text-align :center"><%# Eval("TipoMoneda")%></td>
                        
                        <td style ="width :90px"><%# Eval("Estado")%></td>
                      </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel><br /><br />
        <asp:Panel ID ="pnRegComprobante"  runat ="server" Width ="100%" >
            <table id="tblRegPago" runat ="server"  style ="width:100%; margin :0 auto;" >
                <tr><td colspan ="3" style="color: #FFFFFF; background-color: #4972B5;text-align :center" > COMPROBANTE PAGO</td></tr>
                <tr><td style ="height:8px"></td></tr>
                <tr>
                    <td style ="width :220px"><b>Fecha de Pago:</b></td>
                    <td colspan ="2">
                        <asp:TextBox ID="txF_Pago" runat="server"  style="margin-bottom: 0px"></asp:TextBox>
                        <asp:ImageButton ID="ImgCalF_pago" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                        <ajaxtoolkit:calendarextender ID="ce_txF_Pago" runat="server" TargetControlID="txF_Pago" PopupButtonID="ImgCalF_pago" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
                        <asp:RequiredFieldValidator ID="rfvF_pago" runat="server" ControlToValidate ="txF_Pago" ErrorMessage="*" ForeColor ="Red" ValidationGroup ="gral"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr><td style ="height:8px"></td></tr>
                <tr>
                    <td><b>Comprobante (PDF) Maximo 2 MB:</b></td>
                    <td>
                        <asp:FileUpload ID="fulComprobante" runat ="server" AllowMultiple ="true" Width ="500px" />
                        <asp:RequiredFieldValidator ID="rfvfulComprobante" runat="server" ControlToValidate ="fulComprobante" ErrorMessage="*" ForeColor ="Red" ValidationGroup ="arh"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Button ID ="btnCargar" runat ="server"  CssClass ="button" Text ="Ver archivos" OnClick="btnCargar_Click" OnClientClick ="return ValidaArchivo();"/>&nbsp;
                    </td>
                </tr>
            </table><br /><br />
            <asp:Panel ID="pnlDocumento" runat ="server"  Height="500px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" Visible ="false" >
                <b><asp:Literal ID ="ltNumDoctos" runat ="server" ></asp:Literal></b><br /><br />
                <div style ="width :98%; height :450px; overflow-y:scroll;margin :0 auto" >
                    <asp:Literal ID="ltDocumento" runat="server"></asp:Literal>
                </div>
            </asp:Panel>
        </asp:Panel>
    </fieldset>
</asp:Content>
