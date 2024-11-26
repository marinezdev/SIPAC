<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_ConsultaOrdFactura_Sol_xCliente.aspx.cs" Inherits="cxpcxc.cxc_ConsultaOrdFactura_Sol_xCliente" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        
        function ConfirmaEnvioCorreo() {
            var resultado=false;
            if (confirm('¿Esta seguro que desea enviar el correo?')) {
                $find('popProcesando').show();
                resultado = true;
            }
            return resultado
        }
    </script>
    <style >
       .modalMsgProcesando {
            width: 150px;
            height: 30px;
            text-align: center;
            background-color: #F2F2F2;
            border-width: 1px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    <fieldset>
        <legend>ORDEN DE FACTURACION</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 90%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table><br />
        <asp:Panel ID ="pnConsulta" runat="server">
            <table id ="tbConsulta" runat ="server" style ="width:80%;margin:0 auto "  class="tblConsulta" >
                <tr>
                    <td style ="width :27%">CLIENTE:</td>
                    <td><asp:DropDownList ID ="dpCliente" runat ="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td><asp:DropDownList ID ="dpEstado" runat ="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan ="2" style ="text-align :right">
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" ToolTip="Buscar..." />
                    </td>
                </tr>
            </table>
        </asp:Panel><br />
        <asp:Panel ID ="pnOrdFact" runat ="server" ScrollBars ="Auto"  Height ="450px" >
            <asp:Repeater ID="rptOrdFact" runat="server" OnItemCommand="rptsol_ItemCommand" OnItemDataBound="rptOrdFact_ItemDataBound" >
                <HeaderTemplate>
                    <table id="tblOrdFact" border="1" style="width: 100%" class="tblFiltrar">
                        <thead>
                            <%--<th scope="col">ORDEN SERVICIO</th>--%>
                            <th scope="col">ORDEN FACTURA</th>
                            <th scope="col">CLIENTE</th>
                            <th scope="col">FECHA</th>
                            <th scope="col">FACTURA</th>
                            <th scope="col">SERVICIO</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col"></th>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <%--<td style ="width :70px;text-align :center"><%# Eval("IdServicio")%></td>--%>
                        <td style ="width :70px;text-align :center"><%# Eval("IdOrdenFactura")%></td>
                        <td><%# Eval("Cliente")%></td>
                        <td style ="width :90px;text-align :center"><asp:Label ID="lbFechafactura" runat="server" Text='<%# Eval("FechaInicio","{0:d}")%>'></asp:Label></td>
                        <td style ="width :70px;text-align :center"><%# Eval("NumFactura")%></td>
                        <td><%# Eval("SERVICIO")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td style ="width :110px;text-align :right"><%# Eval("ImporteVista")%></td>
                        <td style ="width :90px;text-align :center"><%# Eval("TipoMoneda")%></td>
                        <td style ="width :90px"><%# Eval("Estado")%></td>
                        <td style="text-align: center; width: 40px">
                            <asp:ImageButton ID="imgbtnVerFac" runat="server" ImageUrl="~/img/verFac_gr.png" CommandName="VerFac" CommandArgument='<%# Eval("IdOrdenFactura")%>' ToolTip ="ver Factura" Enabled ="false"  OnClick ="imgbtnVerFac_Click" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>  
                 
         <asp:Panel  ID ="pnPopDocumento" runat ="server" Width ="800px" Style="display:none; background-color: #F2F2F2;"   >
            <div id="dvDocumento" style="width: 100%; text-align: right;">
                <asp:Button ID="btnCierraDocumento" runat="server" CssClass="button" Text="X" CausesValidation="False"/>
            </div>
            <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="630px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" >
                <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
            </asp:Panel>
        </asp:Panel>

        <ajaxToolkit:ModalPopupExtender ID="mpePopDocumento" runat="server"
            BackgroundCssClass="modalBackground"
            DropShadow="true"
            BehaviorID="mpePopDocumento"
            TargetControlID="MpeFakeTarget"
            PopupControlID="pnPopDocumento"
            OkControlID ="btnCierraDocumento">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Button ID="MpeFakeTarget" runat="server" CausesValidation="False" Style="display:none" />
        
        <asp:Panel ID="pnlProcesando" runat="server" CssClass ="modalMsgProcesando">
            Procesando...
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="popProcesando" runat="server"
            BackgroundCssClass="modalBackground"
            DropShadow="true"
            BehaviorID="popProcesando"
            TargetControlID="MpeFakeTarget"
            PopupControlID="pnlProcesando">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
    </fieldset>
</asp:Content>
