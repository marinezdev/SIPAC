<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_VerNotaCredito.aspx.cs" Inherits="cxpcxc.trf_NotaCredito" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function fn_Eliminar() {
            var resultado = false;
            resultado = confirm('¿Esta seguro que desea eliminarla?'); 
            return resultado;
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <fieldset>
        <legend>NOTA DE CREDITO</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 70%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <div id="dvBtns" style="width:100%; text-align:right;">
                        <asp:ImageButton ID="brnImgEliminar" ImageUrl ="~/img/btncancel.png" runat="server"  ToolTip="Eliminar"   OnClientClick="return fn_Eliminar();" OnClick="brnImgEliminar_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                    </div>
               </td>
            </tr>
        </table><br />
        <table style ="width :100%;">
            <tr>
                <td style ="width :50%;vertical-align :top">
                    <table id ="tblSol" runat ="server" style ="width:98%; margin :0 auto;font-size :13px ">
                        <tr>
                            <td  colspan ="2" class ="Titulos" ><asp:Label   ID ="lbProveeedor" runat ="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width :15%" ><b>Folio:</b></td>
                            <td><asp:Label ID ="lbFolio" runat ="server"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width :15%" ><b>Fecha:</b></td>
                            <td><asp:Label ID ="lbFecha" runat ="server"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Importe:</b></td>
                            <td><asp:Label ID ="lbImporte" runat ="server"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Descripción:</b></td>
                            <td><asp:Label ID ="lbdescripcion" runat ="server"  ></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Tipo Moneda:</b></td>
                            <td><asp:Label ID ="lbMoneda" runat ="server"  ></asp:Label></td>
                        </tr>
                        <tr><td><asp:ImageButton ID="ImgVerNotaCredito"  runat ="server"  ImageUrl="~/img/nc.png" OnClick="ImgVerNotaCredito_Click" /></td></tr>
                    </table><br /><br />
                    <asp:Panel ID ="pnSolOrigen" runat ="server" >
                        <h4 style ="color:green"> SOLICITUD QUE LA ORIGINO</h4>
                        <asp:Literal ID="ltSolOrigen" runat ="server" ></asp:Literal><br />
                        <asp:ImageButton ID="imgbtnVerFactOrg"  runat ="server"  ImageUrl="~/img/invoice_i.png" OnClick="imgbtnVerFactOrg_Click" />
                    </asp:Panel><br /><br />

                    <asp:Panel ID ="pnSolAsignadas" runat ="server" Visible ="false"   >
                         <h4 style ="color:green"> SOLICITUD DE APLICACION</h4>
                        <asp:Repeater ID="rptSolAsig" runat="server" OnItemCommand="rptSolAsig_ItemCommand" >
                            <HeaderTemplate>
                                <table id="tblrptSolAsig" border="1" style ="width :90%"  class ="tblFiltrar" >
                                    <thead>
                                        <th scope="col">NO. FACTURA</th>
                                        <th scope="col">FECHA</th>
                                        <th scope="col">CANTIDAD</th>
                                        <th scope="col">DESCRIPCION</th>
                                        <th scope="col">MONEDA</th>
                                        <th scope="col"></th>
                                    </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td><%# Eval("Factura")%></td>
                                    <td><%# Eval("FechaFactura","{0:d}")%></td>
                                    <td><%# Eval("Importe")%></td>
                                    <td><%# Eval("DescProyecto")%></td>
                                    <td><%# Eval("Moneda")%></td>
                                    <td style="text-align :center;width:30px">
                                        <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater><br />
                    </asp:Panel><br /><br />
                </td>
                <td style ="vertical-align :top">
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="500px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                        <div class ="Titulos" >
                            <asp:Label ID ="lbTituloDocto" runat ="server" ></asp:Label>
                        </div>
                        <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                     </asp:Panel>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
