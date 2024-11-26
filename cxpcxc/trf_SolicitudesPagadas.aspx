<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_SolicitudesPagadas.aspx.cs" Inherits="cxpcxc.trf_SolicitudesPagadas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.jqueryui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$('#tblSol').DataTable({
            //    "paging": false,
            //     "language": {
            //         "sProcessing": "Procesando...",
            //         "sLengthMenu": "Mostrar _MENU_ registros",
            //         "sZeroRecords": "No se encontraron resultados",
            //         "sEmptyTable": "Ningún dato disponible en esta tabla",
            //         "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            //         "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            //         "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            //         "sInfoPostFix": "",
            //         "sSearch": "Buscar:",
            //         "sUrl": "",
            //         "sInfoThousands": ",",
            //         "sLoadingRecords": "Cargando...",
            //         "oPaginate": { "sFirst": "Primero", "sLast": "Último", "sNext": "Siguiente", "sPrevious": "Anterior" },
            //         "oAria": { "sSortAscending": ": Activar para ordenar la columna de manera ascendente", "sSortDescending": ": Activar para ordenar la columna de manera descendente" }
            //     },
            // });
         });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat ="server" ></asp:ScriptManager>
    <asp:HiddenField ID="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID="hdConsulta" runat ="server" />
    <fieldset>
        <legend>CONSULTAR SOLICITUDES DE TRANSFERENCIA PAGADAS</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
            </tr>
           <tr>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br />
        <div id="dvFechas">
            <table id="tblFechas" style="text-align:left; width :60%; margin : 0 auto" class ="tblConsulta">
                <tr>
                    <td>PROVEEDOR:</td>
                    <td>
                        <asp:DropDownList  ID="dpProveedor" runat ="server"  ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>FECHA:</td>
                   <td>
                       <table>
                           <tr>
                               <td>DEL:</td>
                                <td style ="vertical-align :top" >
                                    <asp:TextBox ID="txF_Inicio" runat="server" Width="80px" style="margin-bottom: 0px"></asp:TextBox>
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
                            </tr>
                        </table> 
                    </td>
                </tr>
                <tr>
                    <td colspan ="2" style ="text-align :right">
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" />
                    </td>
                </tr>
            </table> 
        </div><br />
        <asp:Panel ID ="pnSolicitud"  runat ="server" Width ="100%" Height ="450px" ScrollBars ="Vertical" Visible ="false" >
            <div id ="dvExp" runat ="server" style ="width :100%;text-align :right"  > 
                <asp:ImageButton ID ="btnExportar" runat ="server" ImageUrl ="~/img/ExpExcel.png" OnClick="btnExportar_Click"  />
            </div>
            <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand" >
                <HeaderTemplate>
                    <table id="tblSol" border="1" style ="width :99%" class ="tblFiltrar"  >
                        <thead>
                            <th scope="col">FECHA PAGO</th>
                            <th scope="col">NO. FACTURA</th>
                            <th scope="col">FECHA FACTURA</th>
                            <th scope="col">PROVEEDOR</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">IMPORTE PAGADO </th>
                            <th scope="col"></th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("FechaPago","{0:d}")%></td>
                        <td><%# Eval("Factura")%></td>
                        <td><%# Eval("FechaFactura","{0:d}")%></td>
                        <td><%# Eval("Proveedor")%></td>
                        <td><%# Eval("Importe","{0:0,0.00}")%></td>
                        <td><%# Eval("Moneda")%></td>
                        <td><%# Eval("ImportePagado","{0:0,0.00}")%></td>
                        <td style="text-align :center;width:50px">
                            <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </fieldset>
</asp:Content>
