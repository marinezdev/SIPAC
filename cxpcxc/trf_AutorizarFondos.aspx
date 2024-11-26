<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AutorizarFondos.aspx.cs" Inherits="cxpcxc.trf_AutorizarFondos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager  ID="SM"  runat="server"  ></asp:ScriptManager>
    <fieldset>
        <legend>SOLICITUDES DE FONDEO</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
               <td style="width: 85%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td> 
               <td style="text-align: right;">
                    <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click"  />&nbsp;&nbsp;
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />        
                </td>
            </tr>
        </table><br />
        
        <asp:Panel ID ="pnSolFondeo"  runat ="server" Width ="100%" Height ="280px" ScrollBars ="Vertical" >
            <asp:Repeater ID="rptSolFondeo" runat="server" OnItemCommand="rptSolFondeo_ItemCommand" OnItemDataBound="rptSolFondeo_ItemDataBound"  >
                <HeaderTemplate>
                    <table id="tblSolFondeo" border="1" style ="width :100%;text-align :center" class ="tblFiltrar"  >
                        <caption style ="font-size:13px; text-align :center" ><b>SOLICITUDES</b></caption>
                        <thead>
                            <th scope="col">NO. LOTE</th>
                            <th scope="col">FECHA SOLICITUD</th>
                            <th scope="col">EMPRESA</th>
                            <th scope="col">FECHA AUTORIZACION FONDOS</th>
                            <th scope="col">NO. FACTURAS</th>
                            <th scope="col">IMPORTE FONDOS M.N</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col">DETALLE</th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("IdFondeo")%></td>
                        <td><b><%# Eval("FechaRegistro","{0:d}")%></b></td>
                        <td><%# Eval("Empresa")%></td>
                        <td>
                            <asp:Label ID ="lbFechafd" runat ="server" Text='<%# Eval("FechaFondos","{0:d}")%>'></asp:Label>
                        </td>
                        <td><%# Eval("NoSolicitudes")%></td>
                        <td><%# Eval("Total","{0:0,0.00}")%></td>
                        <td><%# Eval("Estado")%></td>
                        <td style="text-align :center;width:80px">
                            <asp:ImageButton ID="imgbtnSol"  runat ="server"  ImageUrl="~/img/Detalle.png" CommandName="Detalle" CommandArgument='<%# Eval("IdFondeo")%>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </fieldset> 
</asp:Content>
