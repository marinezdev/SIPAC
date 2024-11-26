<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_AltaSolConFactura.aspx.cs" Inherits="cxpcxc.trf_AltaSolConFactura" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            if (resultado) { $("#dvBtns").hide(); }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID="hdLLaveSol" runat ="server" />
    <asp:HiddenField ID="hdIdpvd" runat ="server" />
    <fieldset>
        <legend>REGISTRAR SOLICITUD DE TRANSFERENCIA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 75%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red; font-size :14px">
                        <asp:Literal ID="ltMsg" runat="server" ></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <div id="dvBtns">
                        <asp:Button ID="btnGuardar" runat ="server"  Text ="Guardar"  CssClass="button" OnClick="btnGuardar_Click" OnClientClick ="return Confirmar();" Visible ="false" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" CssClass="button" OnClick="btnCancelar_Click"/>                    
                    </div> 
                </td>
            </tr>
        </table><br />
        <asp:Panel Id="pnSelecionArchivo" runat ="server" Width ="100%" >
            <table style ="width :80%;margin: 0 auto">
                <tr>
                    <td colspan="2" class="Titulos" >AGREGUE LOS ARCHIVOS DE LA FACTURA </td>
                </tr>
                <tr>
                    <td style ="width:20%">Factura (PDF):</td>
                    <td><asp:FileUpload ID="fulFactura" runat="server" Width="80%"  />
                        <asp:RequiredFieldValidator ID="rfvfulFactura" runat="server" ControlToValidate ="fulFactura" ErrorMessage="*" ForeColor ="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="revFactura" ControlToValidate="fulFactura" ErrorMessage="*" ForeColor ="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$" />
                    </td>
                </tr>
                <tr>
                    <td>Archivo (XML):</td>
                    <td><asp:FileUpload ID="fulXml" runat="server" Width="80%" />
                        <asp:RequiredFieldValidator ID="rfvfulXml" runat="server" ControlToValidate ="fulXml" ErrorMessage="*" ForeColor ="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="revXml" ControlToValidate="fulXml" ErrorMessage="*" ForeColor ="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xml|.XML)$" />
                    </td>
                </tr>
                <tr><td colspan ="2" style =" text-align:right"><asp:Button ID="btnCargar" runat ="server" Text ="Cargar" CssClass="button" OnClick="btnCargar_Click" /></td></tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnDatosXml" runat ="server" Width ="100%" Font-Size ="12px" Visible ="false" >
            <h5 class="Titulos" >DATOS DEL XML</h5>
            <table style =" width:100%;background-color:aliceblue" id ="tblDatosXml"  >
                <tr>
                    <td style ="width:50%">
                        <b>EMISOR:</b><br /><br />
                        <table id="tblEmisor" runat ="server" style ="width:90%; margin :0 auto " >
                            <tr>
                                <td colspan ="2"   style="text-align :center;color:brown"  >
                                    <b><asp:Label ID="lbProveedor" runat ="server"></asp:Label></b><hr />
                                </td>
                            </tr>
                            <tr>
                                <td style ="width:20%"><b>RFC:</b></td>
                                <td><asp:Label ID="lbRfc" runat ="server" ></asp:Label></td>
                            </tr>
                            <tr><td colspan ="2" style ="height :10px"></td></tr>
                            <tr>
                                <td><b>Factura:</b></td>
                                <td><asp:Label ID="lbFactura" runat ="server" Font-Size ="14px" width ="95%" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Fecha:</b></td>
                                <td><asp:Label  ID="lbFhFactura" runat ="server" width ="95%" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Importe:</b></td>
                                <td><asp:Label  ID="lbImporte" runat ="server" ></asp:Label></td>
                            </tr>
                            <tr><td style ="height :10px"></td></tr>
                            <tr>
                                <td style ="vertical-align :top;"><b>Concepto:</b></td>
                                <td><asp:Label  ID="lbConcepto" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                    <td style ="vertical-align:top"><b>RECEPTOR:</b><br /><br />
                        <table id ="tblReceptor" runat ="server" style ="width:90%; margin : 0 auto " >
                            <tr>
                                <td colspan ="2"   style="text-align :center;color:brown"   >
                                    <b><asp:Label ID="lbReceptor" runat ="server"></asp:Label></b><hr />
                                </td>
                            </tr>
                            <tr>
                                <td style ="width:20%"><b>RFC:</b></td>
                                <td><asp:Label ID="lbReceptorRfc" runat ="server" ></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table><br />
            <table style ="width :100%">
                <tr>
                    <td style ="width :50%">
                        <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="600px"  BorderStyle="Solid" BorderWidth="1px" ScrollBars ="Auto" >
                            <div  class="Titulos"> FACTURA</div>
                            <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
                        </asp:Panel>
                    </td>
                    <td  style ="vertical-align :top">
                        <asp:Panel ID ="pnSelCuenta"  runat ="server">
                            <asp:Repeater ID="rptCuentas" runat="server" OnItemCommand="rptCuentas_ItemCommand" >
                                <HeaderTemplate>
                                    <table id="tblCtas" border="1" style ="width :98%;margin :0 auto"   class ="tblFiltrar" >
                                        <caption style ="color :green;font-size :16px" >SELECCIONE LA CUENTA DE DEPOSITO</caption>
                                        <thead>
                                            <th scope="col">BANCO</th>
                                            <th scope="col">CUENTA</th>
                                            <th scope="col">CTA CLABE</th>
                                            <th scope="col">SUCURSAL</th>
                                            <th scope="col">MONEDA</th>
                                            <th scope="col"></th>
                                        </thead>
                                    <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: White; color: #333333">
                                        <td><%# Eval("Banco")%></td>
                                        <td><%# Eval("NoCuenta")%></td>
                                        <td><%# Eval("CtaClabe")%></td>
                                        <td><%# Eval("Sucursal")%></td>
                                        <td><%# Eval("Moneda")%></td>
                                        <td style="text-align :center;width:80px">
                                            <asp:ImageButton ID="imgbtnSel"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="Seleccionar" CommandArgument='<%# Eval("NoCuenta")%>'  CausesValidation="false" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></tbody></table></FooterTemplate>
                            </asp:Repeater>
                        </asp:Panel><br /><br />
                        <asp:Panel ID ="pnComplementario" runat ="server"  Visible ="false"  >
                            <table id ="tblCuenta" runat ="server" style ="width:90%; margin :0 auto;" >
                                <tr>
                                    <td style ="width:20%"><b>Banco:</b></td>
                                    <td><asp:Label  ID="lbBanco" runat ="server"  Width ="90%"></asp:Label></td>
                                    <td style ="width:20%"><b>Cuenta:</b></td>
                                    <td><asp:Label  ID="lbCuenta" runat ="server"  Width ="90%"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td><b>Cuenta Clabe:</b></td>
                                    <td><asp:Label ID="lbClabe" runat ="server"  Width ="90%"></asp:Label></td>
                                    <td><b>Sucursal:</b></td>
                                    <td><asp:Label ID="lbSucursal" runat ="server"  Width ="90%" ></asp:Label></td>
                                </tr>
                            </table><br />
                            <table id ="tblSol" runat ="server" style ="width:98%; margin :0 auto">
                                <tr>
                                    <td style ="width:20%">Condiciones de pago:</td>
                                    <td >
                                        <asp:DropDownList ID="dpCondPago" runat ="server"  Width ="450px"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvImpLetra" runat="server" ControlToValidate="dpCondPago" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style ="width:20%">Proyecto:</td>
                                    <td>
                                        <asp:DropDownList ID="dpProyecto" runat ="server" Width ="450px"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfProyecto" runat="server" ControlToValidate="dpProyecto" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style ="width:20%">Descripcion:</td>
                                    <td>
                                        <asp:TextBox  ID="txDecProyecto" runat ="server" Width ="95%" ></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="fteDecProyecto" runat="server" TargetControlID="txDecProyecto" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                                        <asp:RequiredFieldValidator ID="rfvDecProyecto" runat="server" ControlToValidate="txDecProyecto" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style ="width:20%">Tipo de Moneda:</td>
                                    <td>
                                        <asp:DropDownList ID ="dpTpMoneda" runat ="server" Width ="250px"  >
                                            <asp:ListItem  Value ="0"  Text="Seleccionar" > </asp:ListItem>
                                            <asp:ListItem  Value ="Pesos"  Text="Pesos"> </asp:ListItem>
                                            <asp:ListItem  Value ="Dolares"  Text="Dolares" > </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTpMoneda" runat="server" ControlToValidate="dpTpMoneda" ErrorMessage="*"  ForeColor="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
         </asp:Panel>
    </fieldset>
</asp:Content>
