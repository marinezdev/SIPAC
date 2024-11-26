<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AltaNotaCredito.aspx.cs" Inherits="cxpcxc.trf_AltaNotaCredito" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function CargarArchivo() {
            var resultado = false;
            if (Page_ClientValidate('grb') == true) { resultado = true; }
            if (resultado) { $("#dvBtns").hide(); }
            return resultado;
        }

        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate('gral') == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            if (resultado) { $("#dvBtns").hide(); }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <fieldset>
        <legend>ALTA NOTA DE CREDITO</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 70%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <div id="dvBtns" style="width:100%; text-align:right;">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar"  CssClass="button" CausesValidation="false" OnClientClick ="return Confirmar();" OnClick="btnRegistrar_Click" />&nbsp;&nbsp;
                        <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                    </div>
               </td>
            </tr>
        </table><br />
        <table style ="width :100%;">
            <tr>
                <td style ="width :49%;vertical-align :top">
                    <table id ="tbDatosNota" runat ="server" style ="width:98%; margin :0 auto;">
                        <tr>
                            <td  style ="width:140px"><b>Proveeedor:</b></td>
                            <td >
                                <asp:DropDownList ID="dpProveedor" runat ="server" ></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvProveedor" runat="server" ControlToValidate="dpProveedor" ErrorMessage="*" ForeColor="Red" InitialValue ="0" ValidationGroup ="gral"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Fecha:</b></td>
                            <td>
                                <asp:TextBox  ID="txFecha" runat ="server"  Width="130px"  ></asp:TextBox>
                                <asp:ImageButton ID="ibtn_txFecha" runat="server" CausesValidation="False" ImageUrl="~/img/calendario.png" />
                                <ajaxToolkit:CalendarExtender ID="ce_Fecha" runat="server" TargetControlID="txFecha" PopupButtonID="ibtn_txFecha" ClearTime="True" PopupPosition="Right" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfv_Fecha" runat="server" ControlToValidate="txFecha" ErrorMessage="*" ForeColor="Red" ValidationGroup ="gral"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Importe:</b></td>
                            <td colspan ="3">
                                <asp:TextBox  ID="txImporte" runat ="server"  Width ="25%" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvImporte" runat="server" ControlToValidate="txImporte" ErrorMessage="*"  ForeColor="Red" ValidationGroup ="gral"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteImporte" runat="server" TargetControlID="txImporte" FilterMode="ValidChars" ValidChars="0123456789." />
                            </td>
                        </tr>
                        <tr>
                            <td><b>Tipo de Moneda:</b></td>
                            <td colspan ="3">
                                <asp:DropDownList ID ="dpTpMoneda" runat ="server"  >
                                    <asp:ListItem  Value ="0"  Text="Seleccionar" > </asp:ListItem>
                                    <asp:ListItem  Value ="Pesos"  Text="Pesos"> </asp:ListItem>
                                    <asp:ListItem  Value ="Dolares"  Text="Dolares" > </asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTpMoneda" runat="server" ControlToValidate="dpTpMoneda" ErrorMessage="*"  ForeColor="Red" InitialValue ="0" ValidationGroup ="gral"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Descripcion:</b></td>
                            <td colspan ="3">
                                <asp:TextBox  ID="txDescripcion" runat ="server"  TextMode ="MultiLine" Width ="95%" Rows="5"  MaxLength ="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txDescripcion" ErrorMessage="*"  ForeColor="Red" ValidationGroup ="gral"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteDescripcion" runat="server" TargetControlID="txDescripcion" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                            </td>
                        </tr>
                    </table>
                    <br /><hr /><br />
                    <asp:UpdatePanel ID ="upSelOrigen" runat ="server">
                        <ContentTemplate>
                            <asp:Panel ID ="pnIrOrigen" runat ="server">
                                <div runat ="server" style ="text-align :right">
                                    <asp:Button ID ="btnSelOrigen" runat ="server" Text="Factura que genera la nota de credito"  CausesValidation="false" CssClass ="button " OnClick="btnSelOrigen_Click"/>
                                </div><br />
                                <asp:TextBox ID ="txIdSolicitudOrigen" runat ="server" ReadOnly="True" BackColor="#F1F1F1" BorderStyle="None"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvSolOrg" runat="server" ControlToValidate ="txIdSolicitudOrigen" ErrorMessage="Faltan datos de la Factura que genera la nota" ForeColor ="Red"  ValidationGroup ="gral"></asp:RequiredFieldValidator><br />
                                <asp:Literal ID="ltSolOrigen" runat ="server" ></asp:Literal>
                            </asp:Panel>
                            <asp:Panel ID="pnSelSolOrigen" runat="server" Visible ="false" >
                                <div runat ="server"  style="text-align :right">
                                    <asp:Button ID="btnCancelarOrigen" runat="server" Text="Cancelar" CssClass="button" CausesValidation="false" OnClick="btnCancelarOrigen_Click"  />
                                </div><br />
                                <table style ="width :60%; margin :0 auto ">
                                    <tr>
                                       <td>DEL:</td>
                                        <td style ="vertical-align :top" >
                                            <asp:TextBox ID="txF_Inicio" runat="server" Width="80px" style="margin-bottom: 0px" ></asp:TextBox>
                                            <asp:ImageButton ID="ImgCalInicio" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                                            <ajaxtoolkit:calendarextender ID="ce_txF_Inicio" runat="server" TargetControlID="txF_Inicio" PopupButtonID="ImgCalInicio" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
                                        </td>
                                       <td style ="width:15px"></td>
                                        <td>AL:</td>
                                        <td style ="vertical-align :top">
                                            <asp:TextBox ID="txF_Fin" runat="server" Width="80px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgCalFin" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                                            <ajaxtoolkit:calendarextender ID="ce_txF_Fin" runat="server" TargetControlID="txF_Fin" PopupButtonID="ImgCalFin" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID ="imbtnBuscaOrigen"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnBuscaOrigen_Click" />
                                        </td>
                                    </tr>
                                </table> 
                                <asp:Panel runat ="server" Height ="300px" ScrollBars ="Auto">
                                    <asp:Repeater ID="rptListaOrigen" runat="server" OnItemCommand="rptListaOrigen_ItemCommand" >
                                        <HeaderTemplate>
                                            <table id="tblSol" style ="width :100%;"  class ="tblFiltrar"   >
                                                <thead>
                                                    <th scope="col">FECHA FACTURA</th>
                                                    <th scope="col">FACTURA</th>
                                                    <th scope="col">PROVEEDOR</th>
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
                                                <td><%# Eval("FechaFactura","{0:d}")%></td>
                                                <td><%# Eval("Proveedor")%></td>
                                                <td><%# Eval("Factura")%></td>
                                                <td><%# Eval("DescProyecto")%></td>
                                                <td><%# Eval("Importe","{0:0,0.00}")%></td>
                                                <td ><%# Eval("Moneda")%></td>
                                                <td ><%# Eval("Estado")%></td>
                                                <td style="text-align :center;width:50px">
                                                    <asp:ImageButton ID="btnAceptaOrigen"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="Solicitud" CommandArgument='<%# Eval("IdSolicitud")%>' CausesValidation ="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate></tbody></table></FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                            </asp:Panel> 
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style ="width :2%"></td>
                <td style ="width :49%; vertical-align :top">
                    <table id ="tblDcto" runat ="server" style="width:98%; margin : 0 auto " >
                        <tr>
                            <td colspan ="3" class="Titulos">Documento:</td>
                        </tr>
                        <tr>
                            <td style ="width:80px"><b>PDF:</b></td>
                            <td><asp:FileUpload ID="fulNota" runat="server" Width="80%"/>
                                <asp:RequiredFieldValidator ID="rfvfulNota" runat="server" ControlToValidate ="fulNota" ErrorMessage="*" ForeColor ="Red" ValidationGroup ="grb"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revNota" ControlToValidate="fulNota" ErrorMessage="*" ForeColor ="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$" />
                            </td>
                            <td style ="text-align :right">
                                <asp:Button ID="btnCargar" runat="server" Text="Cargar" CausesValidation="false" ValidationGroup ="grb" CssClass="button" OnClientClick ="return CargarArchivo();" OnClick="btnCargar_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:TextBox ID ="txNombreDocto" runat ="server" ReadOnly="True" BackColor="#F1F1F1" BorderStyle="None"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvNombreDocto" runat="server" ControlToValidate ="txNombreDocto" ErrorMessage="Seleccione archivo" ForeColor ="Red"  ValidationGroup ="gral"></asp:RequiredFieldValidator>
                    <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="580px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                        <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
