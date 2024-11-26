<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_RegistroPagos.aspx.cs" Inherits="cxpcxc.trf_RegistroPagos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="css/start.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.jqueryui.min.js"></script>
    <script type ="text/javascript">
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        //function EndRequestHandler(sender, args) {
        //    if (args.get_error() == undefined) {//Condicion si queremos manejar que hubo un error y no ejecutar nada
        //        EjecutaFuncion();
        //    }

        //    EjecutaFuncion();
        //}


        function vdCalculo() {
            var resultado = (Page_ClientValidate("TC") == true); 
            return resultado;
        }

        function ConfirmarPago() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            return resultado; 
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server"  />
    <asp:HiddenField ID ="hdIdUsr" runat ="server"  />
    <asp:HiddenField ID ="hdTipoMoneda" runat ="server"  />
    <fieldset>
        <legend>REGISTRO DE PAGO A SOLICITUDES</legend>
        <asp:Panel ID="pnSeleccion" runat ="server" >
            <table id="tblBtns" style="width: 100%">
                <tr>
                   <td style="width: 80%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </td> 
                   <td style="text-align: right;">
                       <asp:Button  ID ="btnContinuar" runat ="server" Text ="Continuar" CssClass ="button " OnClick="btnContinuar_Click"  CausesValidation ="false" />&nbsp;
                       <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" CausesValidation ="false"  />
                    </td>
                </tr>
            </table><br />
            
            <table id="tblConsulta" style="text-align:left; width :70%; margin : 0 auto" class ="tblConsulta">
                <tr>
                    <td rowspan ="2" style ="width :200px">
                        <img  alt="img" src="img/imgCxp.png" />
                    </td>
                    <td style ="width :120px">PROVEEDOR:</td>
                    <td ><asp:DropDownList  ID="dpProveedor" runat ="server"  ></asp:DropDownList></td>
                    <td rowspan ="2" style ="text-align :right; vertical-align :top ">
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" />
                    </td>
                </tr>
                <tr>
                    <td>PROYECTO:</td>
                    <td><asp:DropDownList  ID="dpProyecto" runat ="server"  ></asp:DropDownList></td>
                </tr>
            </table><br />
            <table style="width: 80%;text-align :center;margin :0 auto  ">
                <tr style ="text-align :center" >
                    <td class ="Titulos" >TOTAL PAGOS PESOS</td>
                    <td class ="Titulos">TOTAL PAGOS DOLARES:</td>
                </tr>
                <tr>
                    <td><asp:Label ID="lbTotPesos" runat="server" Text="0" Font-Size="Medium"></asp:Label></td>
                    <td><asp:Label ID="lbTotDlls" runat="server" Text="0"  Font-Size="Medium" ></asp:Label></td>
                </tr>
            </table><br />
            
            <asp:Panel ID ="pnSolicitud"  runat ="server" Width ="100%" Height ="450px" ScrollBars ="Vertical" >
                <asp:Label ID ="lbNumSol" runat ="server" ></asp:Label>
                <asp:UpdatePanel ID="udpSolpagos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand" OnItemDataBound="rptSolicitud_ItemDataBound" >
                            <HeaderTemplate>
                                <table id="tblSol" border="1" style ="width :100%" class ="tblFiltrar" >
                                    <thead>
                                        <th scope="col">LOTE</th>
                                        <th scope="col">ID</th>
                                        <th scope="col">REGISTRO</th>
                                        <th scope="col">NO. FACTURA</th>
                                        <th scope="col">FECHA FACTURA</th>
                                        <th scope="col">PROVEEDOR</th>
                                        <th scope="col">PROYECTO</th>
                                        <th scope="col">MONEDA</th>
                                        <th scope="col">IMPORTE</th>
                                        <th scope="col">CANTIDAD PAGAR</th>
                                        <th scope="col"><asp:Image ImageUrl="~/img/action_check.png" runat ="server" /></th>
                                    </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td style ="color :darkorange" ><b><asp:Label ID ="lbLote" runat ="server" Text ='<%# Eval("IdFondeo")%>' ></asp:Label> </b></td>
                                    <td ><asp:Label ID ="lbIdsol" runat ="server" Text ='<%# Eval("IdSolicitud")%>' ></asp:Label> </td>
                                    <td><%# Eval("FechaRegistro","{0:d}")%></td>
                                    <td><%# Eval("Factura")%></td>
                                    <td><%# Eval("FechaFactura","{0:d}")%></td>
                                    <td ><asp:Label ID ="lbPrv" runat ="server" Text ='<%# Eval("Proveedor")%>' ></asp:Label> </td>
                                    <td><%# Eval("Proyecto")%></td>
                                    <td ><asp:Label ID ="lbMoneda" runat ="server" Text ='<%# Eval("Moneda")%>' ></asp:Label> </td>
                                    <td><%# Eval("Importe","{0:0,0.00}")%></td>
                                    <td><%# Eval("CantidadPagar","{0:0,0.00}")%></td>
                                    <td style="text-align :center;width:80px">
                                        <asp:ImageButton  ID ="btnInactivo" runat ="server" ImageUrl ="~/img/Seleccionar.png" Visible ="true" CommandArgument='<%# Eval("IdSolicitud") + "," + Eval("IdFondeo")%>' CommandName ="btnInactivo"/>
                                        <asp:ImageButton  ID ="btnActivo" runat ="server" ImageUrl ="~/img/action_check.png" Visible ="false" CommandArgument='<%# Eval("IdSolicitud")%>' CommandName ="btnActivo" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID ="pnRegistro" runat  ="server"  Visible ="false"  >
            <table style="width: 100%">
                <tr>
                   <td style="width: 90%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsgRegistro" runat="server"></asp:Literal>
                    </td> 
                   <td style="text-align: right;">
                        <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  CssClass="button" CausesValidation ="false" OnClick="btnRegresar_Click"  />
                    </td>
                </tr>
            </table><br />
            <asp:Panel ID ="pnRg1"  runat ="server" Width ="100%" >
                <asp:Repeater ID="rpSolSel" runat="server" >
                    <HeaderTemplate>
                        <table id="tblSolSel" border="1" style ="width :100%" class ="tblFiltrar" >
                            <thead>
                                <thead>
                                    <th scope="col">LOTE</th>
                                    <th scope="col">ID</th>
                                    <th scope="col">REGISTRO</th>
                                    <th scope="col">NO. FACTURA</th>
                                    <th scope="col">FECHA FACTURA</th>
                                    <th scope="col">PROVEEDOR</th>
                                    <th scope="col">PROYECTO</th>
                                    <th scope="col">MONEDA</th>
                                    <th scope="col">IMPORTE</th>
                                    <th scope="col">CANTIDAD PAGAR</th>
                                </thead>
                            </thead>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td ><asp:Label ID ="lbLote" runat ="server" Text ='<%# Eval("IdFondeo")%>' ></asp:Label> </td>
                            <td ><asp:Label ID ="lbIdsol" runat ="server" Text ='<%# Eval("IdSolicitud")%>' ></asp:Label> </td>
                            <td><%# Eval("FechaRegistro","{0:d}")%></td>
                            <td><%# Eval("Factura")%></td>
                            <td><%# Eval("FechaFactura","{0:d}")%></td>
                            <td ><asp:Label ID ="lbPrv" runat ="server" Text ='<%# Eval("Proveedor")%>' ></asp:Label> </td>
                            <td><%# Eval("Proyecto")%></td>
                            <td ><asp:Label ID ="lbMoneda" runat ="server" Text ='<%# Eval("Moneda")%>' ></asp:Label> </td>
                            <td><%# Eval("Importe","{0:0,0.00}")%></td>
                            <td><%# Eval("CantidadPagar","{0:0,0.00}")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></tbody></table></FooterTemplate>
                </asp:Repeater>
            </asp:Panel><br /><br />
            <table  id ="tbPagoSel" style ="width :100%">
                <tr>
                    <td class ="Titulos" colspan ="2">COMPROBANTE</td>
                </tr>
                <tr>
                    <td style ="width :60%;vertical-align :top">
                        <br />
                        <asp:Panel ID="pnUlArchivos" runat ="server"  Width="98%" >
                            <b><asp:Label runat ="server" Text ="Comprobante (PDF) Maximo 2 MB:"></asp:Label><br /></b>
                            <asp:FileUpload ID="fulComprobante" runat ="server" Width="76%"  AllowMultiple ="true"  />
                            <asp:Button ID ="btnVerDocto" runat ="server" Text ="Ver Documento" CssClass="button" OnClick="btnVerDocto_Click"  CausesValidation ="false"/>
                            <br />
                            <b><asp:Label ID ="lbTitNumDoc" runat ="server" Text="DOCUMENTOS CARGADOS:"></asp:Label></b>
                            <b><asp:TextBox ID ="txNumDoctos" runat ="server" Text ="0"  Width ="15px" ReadOnly="True"></asp:TextBox></b>
                            <asp:RequiredFieldValidator ID="rfvNumDoctos" runat="server" ControlToValidate ="txNumDoctos" ErrorMessage="*" ForeColor ="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                            <br />
                            <br />
                            <asp:Panel ID="pnlDocumento" runat ="server"  Height="500px" BorderStyle="Solid" BorderWidth="1px" Visible ="false" ScrollBars="Auto" >
                                <div style ="height :500px; overflow-y:scroll;margin :0 auto" >
                                    <asp:Literal ID="ltDocumento" runat="server"></asp:Literal>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </td>
                    <td style ="width :40%;vertical-align :top">
                        <br />
                        <asp:Panel ID ="pnRegPago"  runat ="server" Width ="100%" BackColor="AliceBlue" >
                            <asp:UpdatePanel ID="udpPago" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>    
                                    <table id="tblRegPago" runat ="server"  style ="width:100%; margin :0 auto;">
                                        <tr>
                                            <td style ="width:30%"><b>Fecha de Pago:</b></td>
                                            <td>
                                                <asp:TextBox ID="txF_Pago" runat="server" Width="80px" style="margin-bottom: 0px"></asp:TextBox>
                                                <asp:ImageButton ID="ImgCalF_pago" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                                                <ajaxtoolkit:calendarextender ID="ce_txF_Pago" runat="server" TargetControlID="txF_Pago" PopupButtonID="ImgCalF_pago" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender>
                                                <asp:RequiredFieldValidator ID="rfvF_Pago" runat="server" ControlToValidate ="txF_Pago" ErrorMessage="*" ForeColor ="Red" ></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan ="2">
                                                <asp:Panel ID="pnTC" runat ="server" Width ="100%" Visible ="false" >
                                                    <table id ="tblTc"  runat ="server" style="width:100%">
                                                        <tr>
                                                            <td  style ="width:30%"><b>Cantidad a Pagar:</b></td>
                                                            <td>
                                                                <asp:Label ID ="lbPagar" runat ="server" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Tipo Cambio:</b></td>
                                                            <td>
                                                                <asp:TextBox ID ="txTc" runat ="server" ></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteTc" runat="server" TargetControlID="txTc" FilterMode="ValidChars" ValidChars="0123456789." />
                                                                <asp:RequiredFieldValidator ID="rfvTc" runat="server" ControlToValidate ="txTc"  ErrorMessage="*" ForeColor="Red" ValidationGroup ="TC" ></asp:RequiredFieldValidator>
                                                                <asp:Button ID ="btnTc" runat ="server" Text ="Calcular" OnClick="btnTc_Click"  CausesValidation="false"/>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><b>Cantidad pesos:</b></td>
                                            <td>
                                                <asp:TextBox  ID ="txCantidad" runat ="server" ValidationGroup ="pag" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="fteCantidad" runat="server" TargetControlID="txCantidad" FilterMode="ValidChars" ValidChars="0123456789." />
                                                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate ="txCantidad"  ErrorMessage="*" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><b>Nota:</b></td>
                                            <td>
                                                <asp:TextBox ID="txNota" runat ="server" MaxLength ="255" Rows="4"  Width ="97%" TextMode ="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan ="2" style ="text-align :right">
                                                <asp:Button ID="btnRegPago" runat ="server"  Text ="Aceptar" CssClass="button" OnClick="btnRegPago_Click" OnClientClick ="return ConfirmarPago();"/>
                                            </td>
                                        </tr>
                                    </table>     
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnRegPago" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>

</asp:Content>
