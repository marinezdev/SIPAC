<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admProveedor.aspx.cs" Inherits="cxpcxc.admProveedor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.jqueryui.min.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            //$('#tblpvdr').DataTable({
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
            if (Page_ClientValidate() == true) { resultado = confirm('¿Desea continuar?'); }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>CATALOGO DE PROVEEDORES</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table><br />
        <div id ="dvEmpresa" runat ="server" style ="width :100%; text-align :center; font-size:17px;">
            <asp:Literal  ID ="ltEmpresa" runat ="server" ></asp:Literal>
        </div><br />
        <div id="dvCatProveedores" runat ="server"  style ="width:100%">
            <table id="tblProveedor" runat ="server" style="width:85%; text-align:left; margin:0 auto;" >
                <tr>
                    <td style="width:15%;">NOMBRE:</td>
                    <td>
                        <asp:TextBox ID="txNombre" runat ="server"  MaxLength ="80" Width ="90%" Style="text-transform: uppercase" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txNombre" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNombre" runat="server" TargetControlID="txNombre" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                    </td>
                </tr>
                <tr>
                    <td>RFC:</td>
                    <td>
                        <asp:TextBox ID="txRfc" runat ="server"  MaxLength ="16" Width ="90%" Style="text-transform: uppercase"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRfc" runat="server" ControlToValidate="txRfc" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteRfc" runat="server" TargetControlID="txRfc" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789áéíóúÁÉÍÓÚ" />

                    </td>
                </tr>

                <tr>
                    <td style ="vertical-align:top">DIRECCIÓN:</td>
                    <td>
                        <asp:TextBox ID="txDireccion" runat ="server"  MaxLength ="255" Width ="90%" TextMode ="MultiLine" Rows ="3" Style="text-transform: uppercase" ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvDireccion" runat="server" ControlToValidate="txDireccion" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteDireccion" runat="server" TargetControlID="txDireccion" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                    </td>
                </tr>
                <tr>
                    <td>CIUDAD:</td>
                    <td>
                        <asp:TextBox ID="txCiudad" runat ="server"  MaxLength ="128" Width ="90%" Style="text-transform: uppercase"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvCiudad" runat="server" ControlToValidate="txCiudad" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCiudad" runat="server" TargetControlID="txCiudad" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                    </td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td>
                        <asp:DropDownList ID ="dpEstado" runat ="server" >
                            <asp:ListItem  Text="Seleccionar" Value="NA"></asp:ListItem>
                            <asp:ListItem  Text="AGUASCALIENTES" Value="AGUASCALIENTES"></asp:ListItem>
                            <asp:ListItem  Text="BAJA CALIFORNIA" Value="BAJA CALIFORNIA"></asp:ListItem>
                            <asp:ListItem  Text="BAJA CALIFORNIASUR" Value="BAJA CALIFORNIA SUR"></asp:ListItem>
                            <asp:ListItem  Text="CAMPECHE" Value="CAMPECHE"></asp:ListItem>
                            <asp:ListItem  Text="CHIAPAS" Value="CHIAPAS"></asp:ListItem>
                            <asp:ListItem  Text="CHIHUAHUA" Value="CHIHUAHUA"></asp:ListItem>
                            <asp:ListItem  Text="COAHUILA" Value="COAHUILA"></asp:ListItem>
                            <asp:ListItem  Text="COLIMA" Value="COLIMA"></asp:ListItem>
                            <asp:ListItem  Text="DISTRITO FEDERAL" Value="DISTRITO FEDERAL"></asp:ListItem>
                            <asp:ListItem  Text="DURANGO" Value="DURANGO"></asp:ListItem>
                            <asp:ListItem  Text="ESTADO DE MÉXICO" Value="ESTADO DE MÉXICO"></asp:ListItem>
                            <asp:ListItem  Text="GUANAJUATO" Value="GUANAJUATO"></asp:ListItem>
                            <asp:ListItem  Text="GUERRERO" Value="GUERRERO"></asp:ListItem>
                            <asp:ListItem  Text="HIDALGO" Value="HIDALGO"></asp:ListItem>
                            <asp:ListItem  Text="JALISCO" Value="JALISCO"></asp:ListItem>
                            <asp:ListItem  Text="MICHOACÁN" Value="MICHOACÁN"></asp:ListItem>
                            <asp:ListItem  Text="MORELOS" Value="MORELOS"></asp:ListItem>
                            <asp:ListItem  Text="NAYARIT" Value="NAYARIT"></asp:ListItem>
                            <asp:ListItem  Text="NUEVOLEÓN" Value="NUEVOLEÓN"></asp:ListItem>
                            <asp:ListItem  Text="OAXACA" Value="OAXACA"></asp:ListItem>
                            <asp:ListItem  Text="PUEBLA" Value="PUEBLA"></asp:ListItem>
                            <asp:ListItem  Text="QUERÉTARO" Value="QUERÉTARO"></asp:ListItem>
                            <asp:ListItem  Text="QUINTANAROO" Value="QUINTANAROO"></asp:ListItem>
                            <asp:ListItem  Text="SAN LUIS POTOSÍ" Value="SAN LUIS POTOSÍ"></asp:ListItem>
                            <asp:ListItem  Text="SINALOA" Value="SINALOA"></asp:ListItem>
                            <asp:ListItem  Text="SONORA" Value="SONORA"></asp:ListItem>
                            <asp:ListItem  Text="TABASCO" Value="TABASCO"></asp:ListItem>
                            <asp:ListItem  Text="TAMAULIPAS" Value="TAMAULIPAS"></asp:ListItem>
                            <asp:ListItem  Text="TLAXCALA" Value="TLAXCALA"></asp:ListItem>
                            <asp:ListItem  Text="VERACRUZ" Value="VERACRUZ"></asp:ListItem>
                            <asp:ListItem  Text="YUCATÁN" Value="YUCATÁN"></asp:ListItem>
                            <asp:ListItem  Text="ZACATECAS" Value="ZACATECAS"></asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="dpEstado" ErrorMessage="*"  ForeColor="Red" InitialValue ="NA"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>CP:</td>
                    <td>
                        <asp:TextBox ID="txCp" runat ="server"  MaxLength ="5" Width ="100px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvCp" runat="server" ControlToValidate="txCp" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCp" runat="server" TargetControlID="txCp" FilterMode="ValidChars" ValidChars="0123456789" />
                    </td>
                </tr>

                <tr>
                    <td>CONTACTO:</td>
                    <td>
                        <asp:TextBox ID="txContacto" runat ="server" MaxLength ="80" Width ="90%" Style="text-transform: uppercase"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvContacto" runat="server" ControlToValidate="txContacto" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteContacto" runat="server" TargetControlID="txContacto" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,"  />
                    </td>
                </tr>
                <tr>
                    <td>CORREO:</td>
                    <td>
                        <asp:TextBox ID="txCorreo" runat ="server"  MaxLength ="64" Width ="90%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCorreo" runat="server" TargetControlID="txCorreo" FilterMode="ValidChars" ValidChars="!#$%&*+-./0123456789=?@ABCDEFGHIJKLMNOPQRSTUVWXYZ^_abcdefghijklmnopqrstuvwxyz" />
                     <%--   <asp:RequiredFieldValidator ID="rtvCorreo" runat="server" ControlToValidate="txCorreo" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="revCorreo" runat="server" 
                            ErrorMessage="*"
                            ControlToValidate="txCorreo"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ForeColor="Red">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>TELEFONO:</td>
                    <td>
                        <asp:TextBox ID="txTelefono" runat ="server" MaxLength ="15" Width ="30%"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txTelefono" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                        <asp:Label runat="server" Width ="35px" >EXT:</asp:Label>
                        <asp:TextBox ID="txExtencion" runat ="server"  MaxLength ="6" Width ="30%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteTelefono" runat="server" TargetControlID="txTelefono" FilterMode="ValidChars" ValidChars="0123456789"  />
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteExtencion" runat="server" TargetControlID="TxExtencion" FilterType="Numbers"   />
                    </td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td colspan="2"> <asp:CheckBox ID ="chkActivo" runat ="server" Text ="Activo" Checked ="true" ></asp:CheckBox></td>
                </tr>
                <tr>
                    <td colspan="2"> <asp:CheckBox ID ="chkSinFactura" runat ="server" Text ="Permitir Solicitud sin Factura "></asp:CheckBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="button" OnClientClick="return Confirmar();" OnClick="btnModificar_Click" Visible="False" />&nbsp;&nbsp
                        <asp:Button ID="btnModCancela" runat="server" Text="Cancelar" CssClass="button" OnClick="btnModCancela_Click" Visible="False" CausesValidation="false" />
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClientClick="return Confirmar();"  OnClick="btnGuardar_Click" />
                    </td>
                </tr>
            </table>
        </div><br />
        <asp:Panel ID ="pnProveedores"  runat ="server" Width ="100%" Height ="450px" ScrollBars ="Auto">
            <asp:Repeater ID="rptProveedor" runat="serveR" OnItemCommand="rptProveedor_ItemCommand" >
                <HeaderTemplate>
                    <table id="tblpvdr" border="1" style ="width :100%"  class ="tblFiltrar" >
                        <thead>
                            <th scope="col">RFC</th>
                            <th scope="col">NOMBRE</th>
                            <th scope="col">CONTACTO</th>
                            <th scope="col">CORREO</th>
                            <th scope="col">SIN FACTURA</th>
                            <th scope="col">EDITAR</th>
                            <th scope="col">CUENTAS</th>
                        </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("Rfc")%></td>
                        <td><%# Eval("Nombre")%></td>
                        <td><%# Eval("Contacto")%></td>
                        <td><%# Eval("Correo")%></td>
                        <td style="text-align:center;""><%# Eval("SinFactura")%></td>
                        <td style="text-align:center; width:80px">
                            <asp:ImageButton ID="imgbtnEditar"  runat ="server"  ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("Id")%>'  CausesValidation="false"  ToolTip ="Editar"/>
                        </td>
                        <td style="text-align :center;width:80px">
                            <asp:ImageButton ID="imgbtnCtas"  runat ="server"  ImageUrl="~/img/foward.png" CommandName="Cuentas" CommandArgument='<%# Eval("Id")%>'  CausesValidation="false"   ToolTip ="Cuentas"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        
    </fieldset>
    <asp:HiddenField ID ="hdIdProveedor" runat ="server" />
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server" />
</asp:Content>
