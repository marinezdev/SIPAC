<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_solRecibos.aspx.cs" Inherits="cxpcxc.trf_solRecibos" %>
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
            if (Page_ClientValidate() == true) { resultado = confirm('¿Esta seguro de continuar?'); }
            if (resultado) { $("#dvBtns").hide(); }
            return resultado;
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdNomArchivo" runat ="server" />
    <asp:HiddenField ID="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID="hdIdProveedor" runat ="server" />
    <fieldset>
        <legend>REGISTRAR SOLICITUD TRANSFERENCIA CON COMPROBANTE</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 75%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" CssClass="button" OnClick="btnCancelar_Click"/>
               </td>
            </tr>
        </table><br />
        <asp:Panel ID="pnProveedor" runat ="server"  Width ="100%">
            <table id ="tblProveedor" runat ="server" style ="width:90%; margin :0 auto">
                <tr>
                    <td colspan ="4" class="Titulos">
                        <asp:Label ID="lbProveedor" runat ="server"  Width ="100%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="width:15%;font-weight: 700;">RFC:</td>
                    <td colspan ="3" >
                        <asp:Label ID="lbRfc" runat ="server"  Width ="100%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td  style ="font-weight: 700;">Banco:</td>
                    <td>
                        <asp:Label  ID="lbBanco" runat ="server"  Width ="90%" ></asp:Label>
                    </td>
                    <td style ="width:15%;font-weight: 700;">Cuenta:</td>
                    <td>
                        <asp:Label  ID="lbCuenta" runat ="server"  Width ="90%" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Cuenta Clabe:</td>
                    <td>
                        <asp:Label ID="lbClabe" runat ="server"  Width ="90%" ></asp:Label>
                    </td>
                    <td style ="font-weight: 700;">Sucursal:</td>
                    <td>
                        <asp:Label ID="lbSucursal" runat ="server"  Width ="90%" ></asp:Label>
                    </td>
                </tr>
            </table><br />
        </asp:Panel>
        
        <asp:Panel id="pnSelRecibo" runat="server" width="100%" >
            <table id ="tblRecibo" runat ="server" style="width:90%; margin : 0 auto " >
                <tr>
                    <td colspan ="4" class="Titulos"> Seleccione el recibo:</td>
                </tr>
                <tr>
                    <td style ="width:20%;font-weight: 700;">RECIBO(PDF):</td>
                    <td><asp:FileUpload ID="fulRecibo" runat="server" Width="80%"  />
                        <asp:RequiredFieldValidator ID="rfvfulRecibo" runat="server" ControlToValidate ="fulRecibo" ErrorMessage="*" ForeColor ="Red" ValidationGroup ="grb"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="revRecibo" ControlToValidate="fulRecibo"
                                        ErrorMessage="*" ForeColor ="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF)$" />
                    </td>
                </tr>
                <tr>
                    <td colspan ="2" style ="text-align :right">
                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CausesValidation="false" ValidationGroup ="grb" CssClass="button" OnClick="btnContinuar_Click" OnClientClick ="return CargarArchivo();"  />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel  ID="pnRegistro" runat ="server" Width ="100%"  Visible ="false"  >
            <table id ="tblSol" runat ="server" style ="width:90%; margin :0 auto">
                <tr>
                    <td colspan ="4" style ="text-align :right">
                        <asp:Button  ID ="btnGuardar" runat ="server" Text ="Guardar" CssClass ="button" OnClick="btnGuardar_Click" OnClientClick ="return Confirmar();"/>
                    </td>
                </tr>
                <tr>
                    <td style ="width:20%;font-weight: 700;">Folio:</td>
                    <td>
                        <asp:TextBox ID="txFactura" runat ="server" MaxLength ="30" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFactura" runat="server" ControlToValidate="txFactura" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteFactura" runat="server" TargetControlID="txFactura" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789-" />
                    </td>
                    <td style ="width:15%;font-weight:700;">Fecha Recibo:</td>
                    <td>
                        <asp:TextBox  ID="txFhRecibo" runat ="server"  ></asp:TextBox>
                        <asp:ImageButton ID="ibtn_FhRecivo" runat="server" CausesValidation="False" ImageUrl="~/img/calendario.png" />
                        <ajaxToolkit:CalendarExtender ID="ce_FhRecibo" runat="server" TargetControlID="txFhRecibo" PopupButtonID="ibtn_FhRecivo" ClearTime="True" PopupPosition="Right" Format="dd/MM/yyyy" ></ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="rfv_FhRecibo" runat="server" ControlToValidate="txFhRecibo" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td  style ="font-weight: 700;">Importe:</td>
                    <td colspan ="3">
                        <asp:TextBox  ID="txImporte" runat ="server"  Width ="25%" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvImporte" runat="server" ControlToValidate="txImporte" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteImporte" runat="server" TargetControlID="txImporte" FilterMode="ValidChars" ValidChars="0123456789." />
                    </td>
                </tr>
                <tr>
                    <td  style ="font-weight: 700;" >Concepto:</td>
                    <td colspan ="3">
                        <asp:TextBox  ID="txConcepto" runat ="server"  TextMode ="MultiLine" Width ="95%" Rows="3"  MaxLength ="255"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvConcepto" runat="server" ControlToValidate="txConcepto" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteConcepto" runat="server" TargetControlID="txConcepto" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                    </td>
                </tr>
                <tr>
                    <td  style ="font-weight: 700;">Condiciones de pago:</td>
                    <td colspan ="3">
                        <asp:DropDownList ID="dpCondPago" runat ="server"  ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dpCondPago" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Proyecto:</td>
                    <td colspan ="3">
                        <asp:DropDownList ID="dpProyecto" runat ="server" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rvf" runat="server" ControlToValidate="dpProyecto" ErrorMessage="*"  ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Descripcion:</td>
                    <td colspan ="3">
                        <asp:TextBox  ID="txDecProyecto" runat ="server" Width ="95%" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteDecProyecto" runat="server" TargetControlID="txDecProyecto" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.," />
                        <asp:RequiredFieldValidator ID="rfvDecProyecto" runat="server" ControlToValidate="txDecProyecto" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style ="font-weight: 700;">Tipo de Moneda:</td>
                    <td colspan ="3">
                        <asp:DropDownList ID ="dpTpMoneda" runat ="server" >
                            <asp:ListItem  Value ="0"  Text="Seleccionar" > </asp:ListItem>
                            <asp:ListItem  Value ="Pesos"  Text="Pesos"> </asp:ListItem>
                            <asp:ListItem  Value ="Dolares"  Text="Dolares" > </asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvTpMoneda" runat="server" ControlToValidate="dpTpMoneda" ErrorMessage="*"  ForeColor="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table><br />
            <h5 class="Titulos"> RECIBO</h5>
            <asp:Panel ID="pnlDocumento" runat ="server" Width="100%" Height="500px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                <asp:Literal ID="ltDocumento" runat="server" ></asp:Literal>
            </asp:Panel>
        </asp:Panel>
    </fieldset>
</asp:Content>
