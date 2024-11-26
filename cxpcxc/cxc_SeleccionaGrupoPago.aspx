<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_SeleccionaGrupoPago.aspx.cs" Inherits="cxpcxc.cxc_SeleccionaGrupoPago" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$('#tblOrdFact').DataTable({
            //    "paging": false,
            //    "language": {
            //        "sProcessing": "Procesando...",
            //        "sLengthMenu": "Mostrar _MENU_ registros",
            //        "sZeroRecords": "No se encontraron resultados",
            //        "sEmptyTable": "Ningún dato disponible en esta tabla",
            //        "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            //        "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            //        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            //        "sInfoPostFix": "",
            //        "sSearch": "Buscar:",
            //        "sUrl": "",
            //        "sInfoThousands": ",",
            //        "sLoadingRecords": "Cargando...",
            //        "oPaginate": { "sFirst": "Primero", "sLast": "Último", "sNext": "Siguiente", "sPrevious": "Anterior" },
            //        "oAria": { "sSortAscending": ": Activar para ordenar la columna de manera ascendente", "sSortDescending": ": Activar para ordenar la columna de manera descendente" }
            //    },
            //});
        });


       function Confirmar() {
            var resultado = false;
            if (confirm('¿Esta seguro que desea quitar todas las marcas?')) {
                resultado = true;
            }
            return resultado;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:HiddenField  ID="hdIdUsr" runat ="server" />
    <asp:HiddenField  ID="hdIdEmpresa" runat ="server" />
    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    <fieldset>
        <legend>AGREGAR COMPROBANTE DE PAGO</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 75%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="button" CausesValidation="false" OnClick="btnContinuar_Click" />
                    &nbsp;<asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table><br />
        <asp:Panel ID ="pnConsulta" runat="server">
            <table id ="tbConsulta" runat ="server" style ="width:85%;margin:0 auto "  class ="tblConsulta" >
                <tr>
                    <td style ="width :27%">CLIENTE:</td>
                    <td><asp:DropDownList ID ="dpCliente" runat ="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>SERVICIO:</td>
                    <td><asp:DropDownList ID ="dpServicio" runat ="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>PERIODO :</td>
                   <td>
                       <table style ="width:100%">
                           <tr>
                               <td>MES:</td>
                                <td style ="vertical-align :top" >
                                    <asp:DropDownList ID ="dpMes" runat ="server" Width ="150px" >
                                        <asp:ListItem  Text ="Seleccionar" Value ="0"></asp:ListItem>
                                        <asp:ListItem  Text ="Enero" Value ="1"></asp:ListItem>
                                        <asp:ListItem  Text ="Febrero" Value ="2"></asp:ListItem>
                                        <asp:ListItem  Text ="Marzo" Value ="3"></asp:ListItem>
                                        <asp:ListItem  Text ="Abril" Value ="4"></asp:ListItem>
                                        <asp:ListItem  Text ="Mayo" Value ="5"></asp:ListItem>
                                        <asp:ListItem  Text ="Junio" Value ="6"></asp:ListItem>
                                        <asp:ListItem  Text ="Julio" Value ="7"></asp:ListItem>
                                        <asp:ListItem  Text ="Agosto" Value ="8"></asp:ListItem>
                                        <asp:ListItem  Text ="Septiembre" Value ="9"></asp:ListItem>
                                        <asp:ListItem  Text ="Octubre" Value ="10"></asp:ListItem>
                                        <asp:ListItem  Text ="Noviembre" Value ="11"></asp:ListItem>
                                        <asp:ListItem  Text ="Diciembre" Value ="12"></asp:ListItem>
                                    </asp:DropDownList> 
                                </td>
                               <td style ="width:25px"></td>
                                <td>AÑO:</td>
                                <td style ="vertical-align :top">
                                    <asp:DropDownList ID ="dpAño" runat ="server"  Width ="110px">
                                        <asp:ListItem  Text ="Seleccionar" Value ="0"></asp:ListItem>
                                        <asp:ListItem  Text ="2016" Value ="2016"></asp:ListItem>
                                        <asp:ListItem  Text ="2017" Value ="2017"></asp:ListItem>
                                        <asp:ListItem  Text ="2018" Value ="2018"></asp:ListItem>
                                        <asp:ListItem  Text ="2019" Value ="2019"></asp:ListItem>
                                        <asp:ListItem  Text ="2020" Value ="2020"></asp:ListItem>
                                    </asp:DropDownList>  
                                </td>
                            </tr>
                        </table> 
                    </td>
                </tr>
                <tr>
                    <td colspan ="2" style ="text-align :right">
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click"  />
                    </td>
                </tr>
                <tr><td><asp:LinkButton runat ="server" ID ="lkQuitarMarcas" Text=" Quitar todas las marcas" OnClick="lkQuitarMarcas_Click" OnClientClick ="return Confirmar();"   /></td></tr>
            </table>
        </asp:Panel><br />
        <div>
            <table id ="tblMontos" runat ="server"  style ="width :50%;margin :0 auto;" >
                <tr>
                    <td colspan ="2" style ="text-align :center" ><b>MONTO SELECCIONADO:</b></td>
                </tr>
                <tr>
                    <td style ="width:20%">Pesos:</td>
                    <td><b><asp:Label ID ="lbTotalPesos" runat ="server" Text ="0" Font-Size ="14px"/></b></td>
                </tr>
                <tr>
                    <td>Dolares:</td>
                    <td><b><asp:Label ID ="lbTotalDolares" runat ="server" Text ="0" Font-Size ="14px"/></b></td>
                </tr>
            </table>
        </div><br />
        <asp:Panel ID ="pnOrdFact" runat ="server"  ScrollBars ="Vertical" Height="450px" >
            <div runat ="server" style ="color :blue">
                PARTIDAS:  <b><asp:Label ID ="lbNumPartidas" runat="server"  Font-Size ="12px"  ></asp:Label></b>
            </div><br />
            <asp:Repeater ID="rptOrdFact" runat="server" OnItemCommand="rptOrdFact_ItemCommand" OnItemDataBound="rptOrdFact_ItemDataBound" >
                <HeaderTemplate>
                    <table id="tblOrdFact" border="1" style="width:100%;font-size:8px" class="tblFiltrar" >
                        <thead>
                            <th scope="col">ORDEN FACTURA</th>
                            <th scope="col">CLIENTE</th>
                            <th scope="col">FECHA</th>
                            <th scope="col">FACTURA</th>
                            <th scope="col">SERVICIO</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">ESTADO</th>
                            <th scope="col">SEMAFORO</th>
                            <th scope="col">-</th>
                            <th scope="col">-</th>
                            <th scope="col">-</th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style ="width :70px;text-align :center"><%# Eval("IdOrdenFactura")%></td>
                        <td><%# Eval("Cliente")%></td>
                        <td style ="width :90px;text-align :center"><asp:Label ID="lbFechafactura" runat="server" Text='<%# Eval("FechaInicio","{0:d}")%>'></asp:Label></td>
                        <td style ="width :70px;text-align :center"><%# Eval("NumFactura")%></td>
                        <td><%# Eval("Servicio")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td style ="width :110px;text-align :right"><%# Eval("ImporteVista")%></td>
                        <td style ="width :90px;text-align :center"><%# Eval("TipoMoneda")%></td>
                        <td style ="width :90px"><%# Eval("Estado")%></td>
                        <td style="width :65px;text-align: center">
                            <asp:Image ID="imgVencimiento" runat="server" ImageUrl="~/img/Sem_V.png" CommandName="VerPago" CommandArgument='<%# Eval("IdOrdenFactura")%>'/>
                        </td>
                        <td style="text-align: center; width: 40px">
                            <asp:ImageButton ID="imgbtnVerFac" runat="server" ImageUrl="~/img/verFac_gr.png" CommandName="VerFac" CommandArgument='<%# Eval("IdOrdenFactura")%>' Enabled = "false"  ToolTip ="ver Factura" OnClick ="imgbtnVerFac_Click" />
                            </td>
                        <td style="text-align: center; width: 40px">
                            <asp:ImageButton ID="imgbtnDescarga" runat="server" ImageUrl="~/img/descarga_gr.png" CommandName="Descarga" CommandArgument='<%# Eval("IdOrdenFactura")%>' Enabled = "false"  ToolTip ="Descargar Factura" />
                        </td>
                        <td style="text-align :center;width:80px">
                            <asp:ImageButton  ID ="btnInactivo" runat ="server" ImageUrl ="~/img/Seleccionar.png" Visible ="true"  CommandArgument='<%# Eval("IdOrdenFactura")%>' CommandName ="btnInactivo"/>
                            <asp:ImageButton  ID ="btnActivo" runat ="server" ImageUrl ="~/img/action_check.png" Visible ="false" CommandArgument='<%# Eval("IdOrdenFactura")%>' CommandName ="btnActivo"  />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>  
                 
            <asp:Panel  ID ="pnPopDocumento" runat ="server" Width ="800px" Style="display:none; background-color: #F2F2F2;"   >
            <div id="dvDocumento" style="width: 100%; text-align: right;">
                <asp:Button ID="btnCierraDocumento" runat="server" CssClass="button" Text="Cerrar" CausesValidation="False"/>
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
    </fieldset>
</asp:Content>
