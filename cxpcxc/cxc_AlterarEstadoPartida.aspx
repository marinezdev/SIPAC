<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_AlterarEstadoPartida.aspx.cs" Inherits="cxpcxc.cxc_AlterarEstadoPartida" %>
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
                
        function Confirmar(tipo) {
            var Msg = "";
            if (tipo == "E") { Msg = 'ELIMINAR'; }
            if (tipo == "C") { Msg = 'CANCELAR'; }
            if (tipo == "F") { Msg = 'ENVIAR A FACTURACION'; }
            
            var resultado = false;
            if (confirm('¿Esta seguro que desea ' + Msg + ' la orden?')) {
                $find('popProcesando').show();
                resultado = true;
            }
            return resultado
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
        <legend>MODIFICAR ESTADO ORDEN DE FACTURACION</legend>
        <table id="tblBtns" runat="server" style="width: 100%">
            <tr>
                <td style="width: 90%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table><br />
        <asp:Panel ID ="pnConsulta" runat="server">
            <table id ="tbConsulta" runat ="server" style ="width:80%;margin:0 auto "  class="tblConsulta" >
                <tr>
                    <td style ="width :27%">CLIENTE:</td>
                    <td><asp:DropDownList ID ="dpCliente" runat ="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td><asp:DropDownList ID ="dpEstado" runat ="server" ></asp:DropDownList></td>
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
            </table>
        </asp:Panel><br />
        <asp:Panel ID ="pnOrdFact" runat ="server" ScrollBars ="Auto"  Height ="450px" >
            <asp:Repeater ID="rptOrdFact" runat="server"  OnItemCommand="rptsol_ItemCommand" OnItemDataBound="rptOrdFact_ItemDataBound" >
                <HeaderTemplate>
                    <table id="tblOrdFact" border="1" style="width: 99%" class ="tblFiltrar">
                        <thead>
                            <th scope="col" style ="color :green">REGRESA FACTURAR</th>
                            <th scope="col" style ="color :tomato">CANCELAR</th>
                            <th scope="col" style ="color :red">ELIMINAR</th>
                            <th scope="col">ORDEN FACTURA</th>
                            <th scope="col">CLIENTE</th>
                            <th scope="col">FECHA</th>
                            <th scope="col">FACTURA</th>
                            <th scope="col">SERVICIO</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">ESTADO</th>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style="text-align: center;width: 60px">
                            <asp:ImageButton ID="imgbtnFacturar" runat="server" ImageUrl="~/img/add.png" CommandName="Facturar" CommandArgument='<%# Eval("IdOrdenFactura")%>'  OnClientClick ="return Confirmar('F');" ToolTip ="Regresar a Facturar" />
                        </td>
                        <td style="text-align: center; width: 60px">
                            <asp:ImageButton ID="imgbtnCancelar" runat="server" ImageUrl="~/img/cancelar.png" CommandName="Cancelar" CommandArgument='<%# Eval("IdOrdenFactura")%>' OnClientClick ="return Confirmar('C');"  ToolTip ="Cancelar" />
                        </td>
                        <td style="text-align: center; width: 60px">
                            <asp:ImageButton ID="imgbtnEliminar" runat="server" ImageUrl="~/img/Delete.png" CommandName="Eliminar" CommandArgument='<%# Eval("IdOrdenFactura")%>' OnClientClick ="return Confirmar('E');"  ToolTip ="Elimnar Permanentemente" />
                        </td>
                        <td style ="width :70px;text-align :center"><%# Eval("IdOrdenFactura")%></td>
                        <td><%# Eval("Cliente")%></td>
                        <td style ="width :90px;text-align :center"><asp:Label ID="lbFechafactura" runat="server" Text='<%# Eval("FechaInicio","{0:d}")%>'></asp:Label></td>
                        <td style ="width :70px;text-align :center"><%# Eval("NumFactura")%></td>
                        <td><%# Eval("SERVICIO")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td style ="width :110px;text-align :right"><%# Eval("ImporteVista")%></td>
                        <td style ="width :90px;text-align :center"><%# Eval("TipoMoneda")%></td>
                        <td style ="width :90px"><%# Eval("Estado")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>  
                  
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
        <asp:Literal ID="lt_jsMsg" runat="server"></asp:Literal>
    </fieldset>
</asp:Content>
