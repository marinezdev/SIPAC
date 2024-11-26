<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_AltaOrdenServicio2.aspx.cs" Inherits="cxpcxc.cxc_AltaOrdenServicio2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
        <script type="text/javascript">
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate('Orden') == true) {
                resultado = confirm('¿Esta seguro que desea REGISTRAR la orden de servicio ?');
                if (resultado) {
                    $("#dvBtns").hide();
                }
            }
            return resultado;
        }
        
        function ValidaPartida() {
            var resultado = false;
            if (Page_ClientValidate('partida') == true) {
                resultado = true;
            }
            return resultado;
        }

        function VistaRegPartidas() {
            var dpSol = document.getElementById('<%= dpTpSolicitud.ClientID %>');
            var pnpartidas = document.getElementById('<%= pnMonto.ClientID %>');
            var Seleccion = dpSol.options[dpSol.selectedIndex].value;
            if (Seleccion == 2) {
                pnpartidas.style.visibility = 'hidden';
            }
            else {
                pnpartidas.style.visibility = 'visible';
            }
            $('#<%=txMonto.ClientID%>').val('0');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server"  ></asp:ScriptManager>
    <fieldset >
        <legend >ORDEN DE VENTA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 75%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;font-size :14px">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td>
                    <div id="dvBtns" style="width:100%; text-align:right;">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar"  CssClass="button" OnClientClick =" return Confirmar();" OnClick="btnRegistrar_Click"/>&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" CssClass="button" OnClick="btnCancelar_Click"/>
                    </div> 
                </td>
            </tr>
        </table><br />
        <div id="dvSelCliente" runat ="server" style ="width :100%" >
            <table id="tblSelCliente" runat="server" style="width:80%;margin:0 auto" >
                <tr>
                    <td><b>Empresa facturación:</b></td>
                    <td>
                        <asp:DropDownList ID="dpEmpresa" runat="server" Enabled="false" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ControlToValidate="dpEmpresa" ErrorMessage="*"  ForeColor="Red" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><b>PDF:</b></td>
                    <td>
                        <span style ="color:orange">Archivos no mayores a 2 MB</span>
                        <br />
                        <asp:FileUpload ID="FileUpload2" runat="server" Width="40%" /><asp:Button ID="BtnSubirPDF" runat="server" Text="Subir PDF" CssClass="button" OnClick="BtnSubirPDF_Click"  /><asp:Label ID="LblMensaje2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>XML:</b></td>
                    <td>
                        <span style ="color:orange">Archivos no mayores a 2 MB</span>
                        <br />
                        <asp:FileUpload ID="FileUpload3" runat="server" Width="40%" /><asp:Button ID="BtnSubirXML" runat="server" Text="Subir XML" CssClass="button" OnClick="BtnSubirXML_Click"  /><asp:Label ID="LblMensaje3" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:15%"><b>Cliente:</b></td>
                    <td><asp:DropDownList ID ="dpCliente" runat ="server" AutoPostBack ="true" OnSelectedIndexChanged="dpCliente_SelectedIndexChanged" CausesValidation ="false" Enabled="false"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCliente" runat="server" ControlToValidate="dpCliente" ErrorMessage="*"  ForeColor="Red" InitialValue="0" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                    </td>
                    
                </tr>
            </table>
        </div><br />
        <asp:UpdatePanel ID ="upCliente" runat ="server" >
            <ContentTemplate >
                <asp:Panel id="pnCliente" runat ="server"  width="100%"  Visible ="false" >
                    <table id="tblCliente" runat ="server" style="width:80%; text-align:left; margin:0 auto;background-color: #F3F9FE; border-color:white;" >
                        <tr>
                            <td style="width:30%;text-align :right;"><b>Nombre:</b></td>
                            <td><asp:Label ID="lbNombre" runat ="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="text-align :right"><b>Rfc:</b></td>
                            <td><asp:Label ID="lbRfc" runat ="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="vertical-align:top;text-align :right"><b>Dirección:</b></td>
                            <td><asp:Label ID="lbDireccion" runat ="server" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="text-align:right"><b>Ciudad:</b></td>
                            <td><asp:Label ID="lbCiudad" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="text-align:right"><b>Estado:</b></td>
                            <td><asp:Label ID="lbEstado" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="text-align:right"><b>Cp:</b></td>
                            <td><asp:Label ID="lbCp" runat ="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style ="text-align:right" ><b>Correo:</b></td>
                            <td><asp:Label ID="lbCorreo" runat ="server" ></asp:Label></td>
                        </tr>
                    </table>
                </asp:Panel><br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id ="dvDatos" runat ="server" > 
            <table style ="width :100%" >
                <tr>
                    <td style ="width :60%">
                        <table id ="tblDatos" runat ="server" style ="width :100%;" >
                            <tr>
                                <td style ="width:160px;text-align:right"><b>Inicio:</b></td>
                                <td style="width:5px" ></td>
                                <td>
                                    <asp:TextBox ID ="txFhInicio" runat ="server" Enabled="false" ></asp:TextBox>
                                    <asp:ImageButton ID="ImgCalInicio" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                                    <ajaxtoolkit:calendarextender ID="ce_txFhInicio" runat="server" TargetControlID="txFhInicio" PopupButtonID="ImgCalInicio" Format="dd/MM/yyyy" Enabled="false" ></ajaxtoolkit:calendarextender>
                                    <asp:RequiredFieldValidator ID="rfvFhInicio" runat="server" ControlToValidate="txFhInicio" ErrorMessage="*"  ForeColor="Red" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style ="text-align:right" ><b>Tipo de Solicitud:</b></td>
                                <td></td>
                                <td><asp:DropDownList ID ="dpTpSolicitud" runat ="server">
                                    <asp:ListItem Text ="Seleccionar" Value="0"></asp:ListItem>
                                    <asp:ListItem Text ="Fijo" Value="1"></asp:ListItem>
                                    <asp:ListItem Text ="Variable" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTpSolicitud" runat="server" ControlToValidate="dpTpSolicitud" ErrorMessage="*"  ForeColor="Red" InitialValue="0" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style ="text-align:right"><b>SubTotal:</b></td>
                                <td style="width:5px" ></td>
                                <td><asp:TextBox ID="txtSubTotal" runat="server" Enabled="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style ="text-align:right"><b>Descuento:</b></td>
                                <td style="width:5px" ></td>
                                <td><asp:TextBox ID="txtDescuento" runat="server" Enabled="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style ="text-align:right"><b>Monto Total:</b></td>
                                <td style="width:5px" ></td>
                                <td>
                                    <asp:Panel ID="pnMonto" runat="server">
                                        <asp:TextBox ID="txMonto" runat ="server" Text="0" Enabled="false"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteMonto" runat="server" TargetControlID="txMonto" FilterMode="ValidChars" ValidChars="1234567890." />
                                        <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txMonto" ErrorMessage="*" ForeColor="Red" InitialValue ="" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style ="text-align:right"><b>Tipo Moneda:</b></td>
                                <td style="width:5px" ></td>
                                <td>
                                    <asp:DropDownList ID ="dpMoneda" runat ="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvMoneda" runat="server" ControlToValidate="dpMoneda" ErrorMessage="*"  ForeColor="Red" InitialValue ="0" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td style ="text-align:right"><b>Condiciones pago:</b></td>
                                <td style="width:5px" ></td>
                                <td>
                                    <asp:DropDownList ID ="dpCodPago" runat ="server"  Width ="250px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCodPago" runat="server" ControlToValidate="dpCodPago" ErrorMessage="*"  ForeColor="Red" InitialValue ="" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <td style ="text-align:right"><b>Tiempo de Crédito:</b></td>
                                <td style="width:5px" ></td>
                                <td>
                                    <asp:TextBox ID ="txFechaPago" runat ="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" />
                                    <ajaxtoolkit:calendarextender ID="Calendarextender1" runat="server" TargetControlID="txFechaPago" PopupButtonID="ImageButton1" Format="dd/MM/yyyy"></ajaxtoolkit:calendarextender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txFechaPago" ErrorMessage="*"  ForeColor="Red" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style ="text-align:right"><b>Proyecto:</b></td>
                                <td style="width:5px" ></td>
                                <td>
                                    <asp:DropDownList ID ="dpProyecto" runat ="server"  Width ="250px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvProyecto" runat="server" ControlToValidate="dpProyecto" ErrorMessage="*"  ForeColor="Red" InitialValue ="" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style ="text-align:right"><b>Servicio:</b></td>
                                <td style="width:5px" ></td>
                                <td>
                                    <asp:DropDownList ID ="dpServicio" runat ="server"  ></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvServicio" runat="server" ControlToValidate="dpServicio" ErrorMessage="*" ForeColor="Red" InitialValue ="" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style ="text-align:right;vertical-align :top"><b>Descripción de servicio:</b></td>
                                <td style="width:5px" ></td>
                                <td>
                                    <asp:TextBox ID="txDescServicio" runat ="server"  TextMode ="MultiLine" MaxLength="100" Rows="4" Width="85%"></asp:TextBox> 
                                    <ajaxToolkit:FilteredTextBoxExtender ID="fteDscSrv" runat="server" TargetControlID="txDescServicio" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890 ,.-#áéíóú/" />
                                    <asp:RequiredFieldValidator ID="rfvDescServicio" runat="server" ControlToValidate="txDescServicio" ErrorMessage="*" ForeColor="Red" InitialValue ="" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style ="text-align:right" ><b>Unidad de Negocio:</b></td>
                                <td  ></td>
                                <td ><asp:Label ID="lbUnidadNegocio" runat ="server" ></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                    <td style ="width:35%; vertical-align :top">
                        <table style ="width :100%">
                            <tr>
                                <td colspan ="3" style ="height:80px">
                                    <p style ="color:#0099FF">Ingrese un numero de partidas mayor si requiere que la orden de venta se genere por mas de una sola vez.</p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan ="3" class ="Titulos" style ="text-align :center">Partidas</td>
                            </tr>
                            <tr  style ="vertical-align :top">
                                <td>Número:</td>
                                <td style ="vertical-align :top" >
                                    <asp:TextBox  ID ="txPeriodos" runat ="server" Text ="1" MaxLength ="2"  Width="60px" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPeriodos" runat="server" ControlToValidate="txPeriodos" ErrorMessage="*"  ForeColor="Red" ValidationGroup="Orden"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="ftePeriodos" runat="server" TargetControlID="txPeriodos" FilterMode="ValidChars" ValidChars="1234567890" />
                                </td>
                                <td>
                                    <div style ="border:solid;border-color:white;width:150px">
                                        <asp:RadioButton  ID ="rbMes" runat ="server"  Text ="Mensual" Checked ="true" GroupName ="perd"  /><br />
                                        <asp:RadioButton  ID ="rdBimestral" runat ="server"  Text ="Bimestral" GroupName ="perd"  /><br />
                                        <asp:RadioButton  ID ="rdSemestral" runat ="server"  Text ="Semestral" GroupName ="perd"  /><br />
                                        <asp:RadioButton  ID ="rdAnual" runat ="server"  Text ="Anual" GroupName ="perd"  />
                                    </div>
                                </td>
                            </tr> 
                        </table>
                    </td>
                </tr>
            </table>
        </div><br />
        <b><asp:CheckBox ID="chkEspecial" runat ="server"  Text ="Cliente Especial" /></b><br /><br />
        <b><asp:CheckBox ID="chkEnvioCorreo" runat ="server" Text ="Enviar por correo al cliente cuando se cargue la factura" /></b><br /><br /> 
        <table id ="tblContrato" runat ="server" style ="width :100%" >
            <tr> 
               <td style ="width :13%"><b>Anotaciones:</b></td>
                <td><asp:TextBox ID ="TxAnotacion" runat ="server" Width ="95%" MaxLength="255" TextMode ="MultiLine" Rows ="3" ></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="fteAnotacion" runat="server" TargetControlID="TxAnotacion" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                </td>
            </tr>
            <tr><td style ="height:15px"></td></tr>
            <tr>
                <td><b>Contrato y/o <br />Orden Compra:</b></td>
                <td>
                    <span style ="color:orange">Archivos no mayores a 2 MB</span>
                    <br />
                    <asp:FileUpload ID="fulContrato" runat="server" Width="80%"  />
                </td>
            </tr>
        </table><br />
        <%--<asp:UpdatePanel ID ="upnPartidas" runat ="server" >
            <ContentTemplate >
                <asp:Panel ID ="pnInresaPartidas" runat ="server" Width="100%"  >
                    <br />
                    <table id ="tblPartidas"  runat ="server"  style =" width :70%; margin: 0 auto" class ="tblConsulta">
                        <tr>
                            <td colspan ="4" class ="Titulos " style ="text-align :center"> Registro de conceptos ó partidas</td>
                        </tr>
                        <tr>
                            <td> <b>Importe sin iva:</b></td>
                            <td colspan ="3" ><b><asp:Label ID ="lbImporte" runat ="server" Text ="0"  Font-Size="Medium" ></asp:Label></b></td>
                        </tr>
                        <tr><td colspan ="4" style ="height :20px"><hr /></td></tr>
                        <tr>
                            <td>Cantidad:</td>
                            <td><asp:TextBox ID ="txCantidad" runat ="server" MaxLength ="3" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate="txCantidad" ErrorMessage="*"  ForeColor="Red" ValidationGroup="partida" ></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteCantidad" runat="server" TargetControlID="txCantidad" FilterMode="ValidChars" ValidChars="1234567890" />
                            </td>
                            <td>Precio:</td>
                            <td><asp:TextBox ID ="txPrecio" runat ="server"  MaxLength ="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txPrecio" ErrorMessage="*"  ForeColor="Red" ValidationGroup="partida"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftePrecio" runat="server" TargetControlID="txPrecio" FilterMode="ValidChars" ValidChars="0123456789." />
                             </td>
                        </tr>
                        <tr>
                            <td>Descripción:</td>
                            <td colspan ="6"><asp:TextBox ID ="txPartida" runat ="server" Width ="95%" MaxLength="255" TextMode ="MultiLine" Rows ="3" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPartida" runat="server" ControlToValidate="txPartida" ErrorMessage="*"  ForeColor="Red" ValidationGroup="partida" ></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftePartida" runat="server" TargetControlID="txPartida" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan ="6" style ="text-align :right">
                                <asp:Button ID ="btnAgregar" runat ="server" Text ="Agregar" CssClass ="button" CausesValidation ="false" OnClick="btnAgregar_Click" OnClientClick ="return ValidaPartida();" ></asp:Button>
                            </td>
                        </tr>
                    </table><br />
                    <asp:Repeater ID="rptPartidas" runat="server" OnItemCommand="rptPartidas_ItemCommand">
                        <HeaderTemplate>
                            <table id="tblPartidas" border="1" style="width: 85%" class="tblFiltrar">
                                <thead>
                                    <th scope="col">Cantidad</th>
                                    <th scope="col">Descripcion</th>
                                    <th scope="col">Precio</th>
                                    <th scope="col"></th>
                                </thead>
                            <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: White; color: #333333">
                                <td style="text-align: center; width: 70px"><b><%# Eval("Cantidad")%></b></td>
                                <td><%# Eval("Descripcion")%></td>
                                <td style="text-align: center; width: 120px"><%# Eval("Precio","{0:0,0.00}")%></td>
                                <td  style="text-align: center; width: 30px">
                                    <asp:ImageButton ID="imgbtnEliminar" runat="server" ImageUrl="~/img/delete.png" CommandName="Eliminar" CommandArgument='<%# Eval("IdOrdenFactura")%>' CausesValidation="false" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></tbody></table></FooterTemplate>
                    </asp:Repeater><br />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel><br />--%>

    </fieldset>
</asp:Content>