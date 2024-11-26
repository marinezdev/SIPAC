<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_CambioProveedor.aspx.cs" Inherits="cxpcxc.trf_CambioProveedor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript" >
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) {
                resultado = confirm('¿Esta seguro de modificar el proveedor ?');
                if (resultado) { $("#dvBtns").hide(); }
            }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdIdEmpresa" runat="server" />
    <fieldset>
        <legend>CAMBIAR PROVEEDOR</legend>
        <asp:MultiView ID="mtvContenedor" runat="server" ActiveViewIndex="0">
            <asp:View ID="SelSolicitudes" runat="server">
                <div style ="text-align :right">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click"  CausesValidation ="false" />
                </div>
                <div id="dvFechas">
                    <table id="tblFechas" style="text-align: left; width: 75%; margin: 0 auto" class="tblConsulta">
                        <tr>
                            <td>PROVEEDOR:</td>
                            <td><asp:DropDownList ID="dpProveedor" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>FECHA REGISTRO:</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>DEL:</td>
                                        <td style="vertical-align: top">
                                            <asp:TextBox ID="txF_Inicio" runat="server" Width="80px" Style="margin-bottom: 0px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgCalInicio" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                                            <ajaxToolkit:CalendarExtender ID="ce_txF_Inicio" runat="server" TargetControlID="txF_Inicio" PopupButtonID="ImgCalInicio" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td style="width: 15px"></td>
                                        <td>AL:</td>
                                        <td style="vertical-align: top">
                                            <asp:TextBox ID="txF_Fin" runat="server" Width="80px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgCalFin" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                                            <ajaxToolkit:CalendarExtender ID="ce_txF_Fin" runat="server" TargetControlID="txF_Fin" PopupButtonID="ImgCalFin" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" CausesValidation ="false"    />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <asp:Panel ID="pnSolicitud" runat="server" Width="100%" Height="350px" ScrollBars="Vertical" Visible="false">
                    <b><asp:Label  ID="lbNumSolicitudes" runat ="server" ></asp:Label></b>
                    <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand">
                        <HeaderTemplate>
                            <table id="tblSol" border="1" style="width: 100%" class="tblFiltrar">
                                <thead>
                                    <th scope="col">REGISTRO</th>
                                    <th scope="col">NO. FACTURA</th>
                                    <th scope="col">FECHA FACTURA</th>
                                    <th scope="col">PROVEEDOR</th>
                                    <th scope="col">IMPORTE</th>
                                    <th scope="col">MONEDA</th>
                                    <th scope="col">FACTURA</th>
                                    <th scope="col"></th>
                                    <th scope="col"></th>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: White; color: #333333">
                                <td><%# Eval("FechaRegistro","{0:d}")%></td>
                                <td><%# Eval("Factura")%></td>
                                <td><%# Eval("FechaFactura","{0:d}")%></td>
                                <td><%# Eval("Proveedor")%></td>
                                <td><%# Eval("Importe","{0:0,0.00}")%></td>
                                <td style="width: 80px"><%# Eval("Moneda")%></td>
                                <td style="text-align: center; width: 90px">
                                    <asp:Label ID="lbConFactura" runat="server" Text='<%# Eval("ConFactura")%>'></asp:Label>
                                </td>
                                <td style="text-align: center; width: 50px">
                                    <asp:ImageButton ID="imgbtnVer" runat="server" ImageUrl="~/img/edit.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' CausesValidation ="false"  />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></tbody></table></FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>

            </asp:View>
            <asp:View ID="Solicitud" runat="server">
                <table  style ="width :100%">
                    <tr>
                        <td colspan="2" style="text-align: right">
                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="button" OnClick="btnRegresar_Click" CausesValidation ="false"  />
                        </td>
                    </tr>
                    <tr style ="text-align :center;font-size:12px ">
                        <td><b>PROVEEDOR ACTUAL</b></td>
                        <td><b>NUEVO PROVEEDOR</b></td>
                    </tr>
                    <tr>
                        <td style ="vertical-align :top;width:50% ">
                            <table id="tblSol" runat="server" style="width: 98%;" class="tblConsulta">
                                <tr>
                                    <td colspan ="2" class="Titulos">
                                        <asp:Label ID="lbProveedor" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style ="width :30%"><b>Folio:</b></td>
                                    <td >
                                        <asp:Label ID="lbIdSolicitud" runat="server" ></asp:Label>
                                        <asp:Label ID="lbIdCatProveedor" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Rfc:</b></td>
                                    <td><asp:Label ID="lbRfc" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Factura:</b></td>
                                    <td><asp:Label ID="lbFactura" runat="server" Font-Size="14px"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Fecha Factura:</b></td>
                                    <td><asp:Label ID="lbFhFactura" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Importe:</b></td>
                                    <td><asp:Label ID="lbImporte" runat="server" ></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;"><b>Concepto:</b></td>
                                    <td colspan="2">
                                        <asp:Label ID="lbConcepto" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style ="vertical-align :top ">
                            <asp:UpdatePanel runat ="server"  >
                                <ContentTemplate >
                                    <table id="tblBeneficiario" style="width: 100%; margin: auto;" class ="tblConsulta">
                                        <tr>
                                            <td style ="text-align:center;color :red">
                                                <b><asp:Literal ID="ltMsg" runat="server"></asp:Literal></b>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td style ="height:55px ">
                                                <asp:DropDownList ID="dpCambioProveedor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dpCambioProveedor_SelectedIndexChanged" CausesValidation ="false" ></asp:DropDownList>
                                                <asp:RequiredFieldValidator  ID="rfvdpCambioProv"  runat ="server" ControlToValidate ="dpCambioProveedor" ForeColor="Red" InitialValue="0" ErrorMessage ="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div id="dvBtns" style ="text-align :right">
                                <asp:Button ID="btnModificar" runat ="server" Text ="Modificar" CssClass ="button" OnClick="btnModificar_Click" OnClientClick ="return Confirmar();" />
                            </div>
                        </td>
                    </tr>
                </table><br /><br />
                <asp:Panel ID="pnlDocumento" runat="server" Width="100%" Height="300px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                    <asp:Literal ID="ltDocumento" runat="server"></asp:Literal>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
    </fieldset>
    
</asp:Content>
