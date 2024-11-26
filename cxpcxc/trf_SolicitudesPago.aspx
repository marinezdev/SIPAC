<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_SolicitudesPago.aspx.cs" Inherits="cxpcxc.trf_SolicitudesPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="css/start.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.jqueryui.min.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            $('#tblSol').DataTable({
                //"stateSave": true,
                "paging": false,
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": { "sFirst": "Primero", "sLast": "Último", "sNext": "Siguiente", "sPrevious": "Anterior" },
                    "oAria": { "sSortAscending": ": Activar para ordenar la columna de manera ascendente", "sSortDescending": ": Activar para ordenar la columna de manera descendente" }
                },
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server"  />
    <fieldset>
        <legend>SOLICITUDES PARA APLICACION DE PAGO </legend>
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
        
        <table id="tblConsulta" style="text-align:left; width :50%; margin : 0 auto" class ="tblConsulta">
            <tr>
                <td style ="width :100px">PROVEEDOR:</td>
                <td>
                    <asp:DropDownList  ID="dpProveedor" runat ="server"  ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>PROYECTO:</td>
                <td>
                    <asp:DropDownList  ID="dpProyecto" runat ="server"  ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan ="2" style ="text-align :right">
                    <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" />
                </td>
            </tr>
        </table><br />
        <asp:Panel ID="pnTotales" runat="server" Width="100%">
            <table style="width: 60%;text-align :center;margin :0 auto  ">
                <tr>
                    <td colspan ="2" style ="text-align :center" class ="Titulos">TOTAL POR PAGAR</td>
                </tr>
                <tr>
                    <td style ="width:35%;"><b>PESOS:</b></td>
                    <td>
                        <asp:Label ID="lbTotPesos" runat="server" Text="0" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>DOLARES:</b></td>
                    <td>
                        <asp:Label ID="lbTotDlls" runat="server" Text="0"  Font-Size="Medium" ></asp:Label>
                    </td>
                </tr>
            </table><br />
        </asp:Panel>
        <asp:Panel ID ="pnSolicitud"  runat ="server" Width ="100%" Height ="450px" ScrollBars ="Vertical" >
            <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand"  >
                <HeaderTemplate>
                    <table id="tblSol" border="1" style ="width :100%" class ="tblFiltrar" >
                        <thead>
                            <th scope="col">REGISTRO</th>
                            <th scope="col">NO. FACTURA</th>
                            <th scope="col">FECHA FACTURA</th>
                            <th scope="col">PROVEEDOR</th>
                            <th scope="col">PROYECTO</th>
                            <th scope="col">MONEDA</th>
                            <th scope="col">FACTURA</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">CANTIDAD PAGAR</th>
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
                        <td><%# Eval("Proyecto")%></td>
                        <td><%# Eval("Moneda")%></td>
                        <td style="text-align :center;"><%# Eval("ConFactura")%></td>
                        <td><%# Eval("Importe","{0:0,0.00}")%></td>
                        <td><%# Eval("CantidadPagar","{0:0,0.00}")%></td>
                        <td style="text-align :center;width:80px">
                            <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdSolicitud")%>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </fieldset>
</asp:Content>
