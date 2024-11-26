<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admInfraestructura.aspx.cs" Inherits="cxpcxc.admInfraestructura" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.jqueryui.min.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            $('#tblInstalaciones').DataTable({
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
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server" />
    <asp:HiddenField ID ="hdIdUsr" runat ="server" />
    <asp:HiddenField ID ="hdConsulta" runat ="server" />
    <fieldset>
        <legend>Instalaciones</legend>
        <table id="tblBtns" style="width: 100%">
            <tr>
                <td style="width: 60%;text-align: center; color: red;"">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right; width:25%">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br />
        <div id="dvFechas">
            <table id="tblFechas" style="text-align:left; width :65%; margin : 0 auto;" class ="tblConsulta">
                <tr>
                    <td style ="width:20%">Clientes:</td>
                    <td colspan ="5">
                        <asp:DropDownList  ID="dpClientes" runat ="server" Width="100%"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style ="width:20%">Clasificación:</td>
                    <td colspan ="5">
                        <asp:DropDownList  ID="dpClasificacion" runat ="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="dpClasificacion_SelectedIndexChanged" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style ="width:20%">Subclasificación:</td>
                    <td colspan ="5">
                        <asp:DropDownList  ID="dpSubClasificacion" runat ="server" Width="100%" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Estado:</td>
                    <td colspan="5">
                        <asp:DropDownList  ID="dpEstado" runat ="server" Width="100%" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>M2:</td>
                    <td colspan="2">
                        <asp:DropDownList  ID="dpFiltroNumerico1" runat ="server" Width="100%" ></asp:DropDownList>
                    </td>
                    <td style="text-align : right; ">Valor:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txM2" runat ="server"  MaxLength ="4" Width ="98%" Style="text-transform: uppercase"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txM2" FilterMode="ValidChars" ValidChars="0123456789" />
                    </td>
                </tr>
                <tr>
                    <td> 
                        <!--<asp:LinkButton ID ="lkLimpiar" runat ="server"  Text ="Limpiar" OnClick="lkLimpiar_Click"></asp:LinkButton> -->
                    </td>
                    <td colspan="5" style ="text-align :right">
                        <asp:ImageButton ID ="imbtLimpiar"  runat ="server" ImageUrl="~/img/clear48.png" OnClick="lkLimpiar_Click" ToolTip="Limpiar criterios de búsqueda" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click" ToolTip="Realizar búsqueda de Instalaciones" />
                    </td>
                </tr>
            </table> 
        </div><br />
        <asp:Panel ID ="pnSolicitud"  runat ="server" Visible ="false" >
            <div id ="dvExportar" runat ="server" style="display:none; " >
                <table style ="width:100%; ">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td style ="text-align:right">
                            <asp:ImageButton ID ="btnExportar" runat ="server" ImageUrl ="~/img/ExpExcel.png" OnClick="btnExportar_Click"  />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel runat ="server" Width ="100%" Height ="450px" ScrollBars ="Vertical" >
                <asp:Repeater ID="rptSolicitud" runat="server" OnItemCommand="rptSolicitud_ItemCommand" OnItemDataBound="rptSolicitud_ItemDataBound"  >
                    <HeaderTemplate>
                        <table id="tblInstalaciones" border="1" style ="width :100%" class ="tblFiltrar"  >
                            <thead>
                                <th scope="col">NOMBRE</th>
                                <th scope="col">M2</th>
                                <th scope="col">CLIENTE</th>
                                <th scope="col">ESTADO</th>
                                <th scope="col">EDO. CTA.</th>
                            </thead>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: White; color: #333333">
                            <td><%# Eval("Nombre")%></td>
                            <td><%# Eval("M2")%></td>
                            <td><%# Eval("Cliente")%></td>
                            <td style="text-align :center;width:60px">
                                <asp:Image ID ="imgOcupacion" runat ="server" ImageUrl ="~/img/Sem_V.png" />
                            </td>
                            <td style="text-align :center;width:50px">
                                <asp:ImageButton ID="imgbtnVer"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="ver" CommandArgument='<%# Eval("IdCliente")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></tbody></table></FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </asp:Panel>
    </fieldset>
</asp:Content>