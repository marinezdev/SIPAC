<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_OrdenServicioModificar.aspx.cs" Inherits="cxpcxc.cxc_OrdenServicioModificar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">
        function ConfirmaCancelar() {
            var resultado = false;
            var chk = $("input[type='checkbox']:checked").length;
            if (chk != "") {
                if (confirm('Esta seguro que desea continuar ?')) {
                    $("#dvBtnCancela").hide();
                    $find('popProcesando').show();
                    resultado = true;
                }
            } else { alert('Seleccione los registros o partidas.'); }
            return resultado;
        }

        function ConfirmaAgregar() {
            var resultado = false;
            if (confirm('Esta seguro que desea continuar?')) {
                $("#dvBtnAgrega").hide();
                resultado = true;
            }
            return resultado;
        }

        function ConfirmarModMonto() {
            var resultado = false;
            if (confirm('Esta seguro que desea continuar?')) {
                $("#dvModCocepto").hide();
                $find('popProcesando').show();
                resultado = true;
            }
            return resultado;
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
        <legend>ACTUALIZAR ORDEN DE VENTA</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 80%; text-align: center; color: red;">
                    <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table><br />
        
        <div id ="dvDatos" runat ="server" >  
            <table style ="width :100%">
                <tr>
                    <td colspan ="2" class ="Titulos">
                       <asp:Label ID="lbCliente" runat ="server" Font-Size="Medium" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id ="tblDatos" runat ="server" style ="width :100%" >
                            <tr>
                                <td  style ="width:28%"><b>Orden:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbOrdServicio" runat ="server" Font-Size="Medium"> </asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Inicio:</b></td>
                                <td><asp:Label ID ="lbFhInicio" runat ="server" Font-Size="17px" ForeColor="#ff0000" ></asp:Label></td>
                                <td><b>Termino:</b></td>
                                <td><asp:Label ID ="lbFhFin" runat ="server" Font-Size="17px" ForeColor="#ff0000"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Empresa:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbEmpresa" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Tipo solicitud:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbTpSolicitud" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Condiciones pago:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbCodPago" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>    
                                <td><b>Moneda:</b></td>
                                <td colspan ="3"><asp:Label ID ="lbMoneda" runat ="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style ="vertical-align :top"><b>Descripcion:</b></td>
                                <td colspan ="3"><asp:Label ID="lbDescripcion" runat ="server" Width ="95%" ></asp:Label></td>
                            </tr> 
                        </table>
                    </td>
                    <td>
                        <table id ="tblPeriodo" style ="width:100%">
                            <tr>
                                <td colspan ="4" class ="Titulos" style ="text-align :center">Partidas totales:
                                   <asp:Label ID ="lbPeriodos" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Tipo:</b>
                                     <div style ="border:solid;border-color:white;width:150px">
                                        <asp:RadioButton  ID ="rbMes" runat ="server"  Text ="Mensual" Enabled ="false"  /><br />
                                        <asp:RadioButton  ID ="rdBimestral" runat ="server"  Text ="Bimestral" Enabled ="false"  /><br />
                                        <asp:RadioButton  ID ="rdSemestral" runat ="server"  Text ="Semestral"  Enabled ="false" /><br />
                                        <asp:RadioButton  ID ="rdAnual" runat ="server"  Text ="Anual" Enabled ="false"  />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br /><b><asp:CheckBox ID="chkEspecial" runat ="server"  Text ="Cliente Especial"  Enabled ="false" /></b><br />
        </div><br />
        <ajaxToolkit:TabContainer runat ="server" Width ="100%" ActiveTabIndex="0" >
            <ajaxToolkit:TabPanel ID ="tbpnActzDatos" HeaderText ="Actualizar Datos" runat ="server" >
                <ContentTemplate>
                    <div id ="dvModCocepto" style ="text-align:right">
                        <asp:Button ID="btnModMonto" runat ="server" Text ="Aceptar" CssClass ="button" OnClick="btnModMonto_Click" OnClientClick ="return ConfirmarModMonto();" />
                    </div>
                    <table id="tblActzDatos" runat="server" class="tblConsulta" style="width: 85%; margin: 0 auto">
                        <tr runat="server">
                            <td style="vertical-align: top; width: 220px" runat="server"><b>Total:</b></td>
                            <td runat="server">
                                <asp:TextBox ID ="txTotal" runat ="server"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fteTotal" runat="server" TargetControlID="txTotal" ValidChars="0123456789." BehaviorID="_content_fteTotal" />
                            </td>
                        </tr>
                        <tr runat="server">
                            <td style="vertical-align: top; width: 220px" runat="server"><b>Descripción de servicio:</b></td>
                            <td runat="server">
                                <asp:TextBox ID="txDescServicio" runat="server" TextMode="MultiLine" MaxLength="100" Rows="4" Width="85%"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender ID="fteDscSrv" runat="server" TargetControlID="txDescServicio" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890 ,.-#áéíóú/" BehaviorID="_content_fteDscSrv" />
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server"><b>Contrato:</b></td>
                            <td runat="server">
                                <asp:Label ID="lbContrato" runat="server" Text="Archivos no mayores a 2 MB" ForeColor="Blue"></asp:Label>&nbsp; <asp:FileUpload ID="fulContrato" runat="server" Width="80%" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel >

            <ajaxToolkit:TabPanel ID ="tbAgregar" HeaderText ="Agregar  Partidas" runat ="server"   Height ="250px">
                <ContentTemplate ><br /><table style ="width :60%; margin :0 auto"   class ="tblConsulta"><tr><td colspan="2" style ="height:5px"></td></tr><tr><td colspan ="2" style ="color :blue">Proporcione la nueva fecha de termino para agregar las nuevas partidas</td></tr><tr><td colspan="2" style ="height:5px"></td></tr><tr><td><b>FECHA DE TERMINO:</b></td><td><asp:TextBox ID ="txNuevaFhTermino" runat ="server" CssClass ="cal_Theme1" ></asp:TextBox><asp:ImageButton ID="ImgCalNuevaFhTermino" runat="server" ImageUrl="~/img/calendario.png" AlternateText="Click para mostrar el calendario" /><ajaxtoolkit:calendarextender ID="ce_NuevaFhTermino" runat="server"   ClearTime="True" TargetControlID="txNuevaFhTermino" PopupButtonID="ImgCalNuevaFhTermino" Format="dd/MM/yyyy" ></ajaxtoolkit:calendarextender></td></tr><tr><td colspan ="2" style ="text-align :right"><div id="dvBtnAgrega" style="width: 100%; text-align: right;"><asp:Button  ID ="btnAgregar" runat ="server" Text ="Aceptar" CssClass ="button" OnClick="btnAgregar_Click" OnClientClick ="return ConfirmaAgregar();" /></div></td></tr></table></ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID ="tbCancelar" HeaderText ="Cancelar partidas" runat ="server"  >
                <HeaderTemplate>
                    Cancelar/Eliminar Partidas
                </HeaderTemplate>
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="font-size: 18px; color: blue">Selecciona las partidas </td>
                            <td colspan ="2" style ="text-align :center; font-size:16px ">
                                <asp:RadioButton ID="rdEliminar" runat="server" Text="Eliminar" GroupName ="tipo" ForeColor="Red" /> 
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                <asp:RadioButton ID="rdCancelar" runat="server" Text="Cancelar" GroupName ="tipo" ForeColor ="Goldenrod" Checked ="True"  />    
                            </td>
                            <td>
                                <div id="dvBtnCancela" style="width: 100%; text-align: right;">
                                    <asp:Button ID="btnCancelaElimina" runat="server" CssClass="button" Text="Aceptar" Height="30px" OnClick="btnCancelaElimina_Click" OnClientClick=" return ConfirmaCancelar();" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Panel ID="pnOrdFact" runat="server" ScrollBars="Auto" Height="450px">
                        <asp:Repeater ID="rptOrdFact" runat="server" OnItemDataBound="rptOrdFact_ItemDataBound">
                            <HeaderTemplate>
                                <table id="tblOrdFact" border="1" style="width: 100%" class="tblFiltrar">
                                    <thead>
                                        <th scope="col">ORDEN</th>
                                        <th scope="col">FECHA</th>
                                        <th scope="col">NO. FACTURA</th>
                                        <th scope="col">IMPORTE</th>
                                        <th scope="col">ESTADO</th>
                                        <th scope="col"></th>
                                        <th scope="col"></th>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: White; color: #333333">
                                    <td style="text-align: center">
                                        <asp:Label ID="lbOrderFac" runat="server" Text='<%# Eval("IdOrdenFactura")%>'></asp:Label></td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lbFechafactura" runat="server" Text='<%# Eval("FechaInicio","{0:d}")%>'></asp:Label></td>
                                    <td style="text-align: center"><%# Eval("NumFactura")%></td>
                                    <td style="text-align: right"><%# Eval("ImporteVista")%></td>
                                    <td style="width: 90px; text-align: center"><%# Eval("Estado")%></td>
                                    <td style="width: 65px; text-align: center">
                                        <asp:Image ID="imgVencimiento" runat="server" ImageUrl="~/img/Sem_V.png" CommandName="VerPago" CommandArgument='<%# Eval("IdOrdenFactura")%>' /></td>
                                    <td style="width: 65px; text-align: center">
                                        <asp:CheckBox ID="chkMarcar" runat="server" /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>

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
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </fieldset> 
</asp:Content>
