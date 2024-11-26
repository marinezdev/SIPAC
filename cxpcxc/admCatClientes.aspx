<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admCatClientes.aspx.cs" Inherits="cxpcxc.admCatClientes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.jqueryui.min.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            //$('#tblCltes').DataTable({
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
        <legend>CATALOGO DE CLIENTES</legend>
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
        <div id="dvDatosClte" runat ="server"  style ="width:100%">
            <table id="tblDatosClte" runat ="server" style="width:95%; text-align:left; margin:0 auto;" >
                <tr>
                    <td style="width:25%;">NOMBRE:</td>
                    <td>
                        <asp:TextBox ID="txNombre" runat ="server"  MaxLength ="80" Width ="90%" Style="text-transform: uppercase"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txNombre" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNombre" runat="server" TargetControlID="txNombre" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                    </td>
                </tr>
                <tr>
                    <td>RFC:</td>
                    <td>
                        <asp:TextBox ID="txRfc" runat ="server"  MaxLength ="16" Width ="200px" Style="text-transform: uppercase"></asp:TextBox>
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
                        <asp:TextBox ID="txCiudad" runat ="server"  MaxLength ="128" Width ="90%" Style="text-transform: uppercase" ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvCiudad" runat="server" ControlToValidate="txCiudad" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCiudad" runat="server" TargetControlID="txCiudad" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,&$#%()-" />
                    </td>
                </tr>
                <tr>
                    <td>ESTADO:</td>
                    <td>
                        <asp:DropDownList ID ="dpEstado" runat ="server" >
                            <asp:ListItem  Text="Seleccionar" Value="NA"></asp:ListItem>
                            <asp:ListItem  Text="Aguascalientes" Value="Aguascalientes"></asp:ListItem>
                            <asp:ListItem  Text="Baja California" Value="Baja California"></asp:ListItem>
                            <asp:ListItem  Text="Baja CaliforniaSur" Value="Baja California Sur"></asp:ListItem>
                            <asp:ListItem  Text="Campeche" Value="Campeche"></asp:ListItem>
                            <asp:ListItem  Text="Chiapas" Value="Chiapas"></asp:ListItem>
                            <asp:ListItem  Text="Chihuahua" Value="Chihuahua"></asp:ListItem>
                            <asp:ListItem  Text="Coahuila" Value="Coahuila"></asp:ListItem>
                            <asp:ListItem  Text="Colima" Value="Colima"></asp:ListItem>
                            <asp:ListItem  Text="Distrito Federal" Value="Distrito Federal"></asp:ListItem>
                            <asp:ListItem  Text="Durango" Value="Durango"></asp:ListItem>
                            <asp:ListItem  Text="Estado de México" Value="Estado de México"></asp:ListItem>
                            <asp:ListItem  Text="Guanajuato" Value="Guanajuato"></asp:ListItem>
                            <asp:ListItem  Text="Guerrero" Value="Guerrero"></asp:ListItem>
                            <asp:ListItem  Text="Hidalgo" Value="Hidalgo"></asp:ListItem>
                            <asp:ListItem  Text="Jalisco" Value="Jalisco"></asp:ListItem>
                            <asp:ListItem  Text="Michoacán" Value="Michoacán"></asp:ListItem>
                            <asp:ListItem  Text="Morelos" Value="Morelos"></asp:ListItem>
                            <asp:ListItem  Text="Nayarit" Value="Nayarit"></asp:ListItem>
                            <asp:ListItem  Text="NuevoLeón" Value="NuevoLeón"></asp:ListItem>
                            <asp:ListItem  Text="Oaxaca" Value="Oaxaca"></asp:ListItem>
                            <asp:ListItem  Text="Puebla" Value="Puebla"></asp:ListItem>
                            <asp:ListItem  Text="Querétaro" Value="Querétaro"></asp:ListItem>
                            <asp:ListItem  Text="QuintanaRoo" Value="QuintanaRoo"></asp:ListItem>
                            <asp:ListItem  Text="San Luis Potosí" Value="San Luis Potosí"></asp:ListItem>
                            <asp:ListItem  Text="Sinaloa" Value="Sinaloa"></asp:ListItem>
                            <asp:ListItem  Text="Sonora" Value="Sonora"></asp:ListItem>
                            <asp:ListItem  Text="Tabasco" Value="Tabasco"></asp:ListItem>
                            <asp:ListItem  Text="Tamaulipas" Value="Tamaulipas"></asp:ListItem>
                            <asp:ListItem  Text="Tlaxcala" Value="Tlaxcala"></asp:ListItem>
                            <asp:ListItem  Text="Veracruz" Value="Veracruz"></asp:ListItem>
                            <asp:ListItem  Text="Yucatán" Value="Yucatán"></asp:ListItem>
                            <asp:ListItem  Text="Zacatecas" Value="Zacatecas"></asp:ListItem>
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
                    <td>CONTACTO PROYECTO:</td>
                    <td>
                        <asp:TextBox ID="txContactoProy" runat ="server" MaxLength ="80" Width ="90%" Style="text-transform: uppercase"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvContactoPry" runat="server" ControlToValidate="txContactoProy" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteContactoPry" runat="server" TargetControlID="txContactoProy" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,"  />
                    </td>
                </tr>
                <tr>
                    <td>CONTACTO FACTURACIÓN:</td>
                    <td>
                        <asp:TextBox ID="txContactoFact" runat ="server" MaxLength ="80" Width ="90%" Style="text-transform: uppercase"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvContactoFact" runat="server" ControlToValidate="txContactoFact" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteContactoFact" runat="server" TargetControlID="txContactoFact" FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz 0123456789áéíóúÁÉÍÓÚ.,"  />
                    </td>
                </tr>
                <tr>
                    <td>CORREO:</td>
                    <td>
                        <asp:TextBox ID="txCorreo" runat ="server"  MaxLength ="64" Width ="90%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteCorreo" runat="server" TargetControlID="txCorreo" FilterMode="ValidChars" ValidChars="!#$%&*+-./0123456789=?@ABCDEFGHIJKLMNOPQRSTUVWXYZ^_abcdefghijklmnopqrstuvwxyz" />
                        <%--<asp:RequiredFieldValidator ID="rtvCorreo" runat="server" ControlToValidate="txCorreo" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>--%>
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
                        <asp:TextBox ID="txTelefono" runat ="server" MaxLength ="15" Width ="200px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txTelefono" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                        <asp:Label runat="server" Width ="35px" >EXT:</asp:Label>
                        <asp:TextBox ID="txExtencion" runat ="server"  MaxLength ="6" Width ="200px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteTelefono" runat="server" TargetControlID="txTelefono" FilterMode="ValidChars" ValidChars="0123456789"  />
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteExtencion" runat="server" TargetControlID="TxExtencion" FilterType="Numbers"   />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="button" OnClientClick="return Confirmar();" OnClick="btnModificar_Click" Visible="False" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnModCancela" runat="server" Text="Cancelar" CssClass="button" OnClick="btnModCancela_Click" Visible="False" CausesValidation ="false" />
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="button" OnClientClick="return Confirmar();"  OnClick="btnGuardar_Click" />
                    </td>
                </tr>
            </table>
        </div><br />
        <asp:Panel ID ="pnClientes"  runat ="server" Width ="100%" Height ="450px" ScrollBars ="Auto">
            <asp:Repeater ID="rptClientes" runat="serveR" OnItemCommand="rptClientes_ItemCommand" >
                <HeaderTemplate>
                    <table id="tblCltes" border="1" style ="width :100%"  class ="tblFiltrar" >
                        <thead>
                            <th scope="col">RFC</th>
                            <th scope="col">NOMBRE</th>
                            <th scope="col">CONTACTO PROYECTO</th>
                            <th scope="col">CORREO</th>
                            <th scope="col">EDITAR</th>
                       </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("Rfc")%></td>
                        <td><%# Eval("Nombre")%></td>
                        <td><%# Eval("ContactoProyecto")%></td>
                        <td><%# Eval("Correo")%></td>
                        <td style="text-align:center; width:80px">
                            <asp:ImageButton ID="imgbtnEditar"  runat ="server"  ImageUrl="~/img/edit.png" CommandName="Editar" CommandArgument='<%# Eval("Id")%>'  CausesValidation="false"  ToolTip ="Editar"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        
    </fieldset>
    <asp:HiddenField ID ="hdIdCte" runat ="server" />
    <%--<asp:HiddenField ID ="hdIdEmpresa" runat ="server" />--%>
</asp:Content>
