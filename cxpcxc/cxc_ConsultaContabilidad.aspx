<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_ConsultaContabilidad.aspx.cs" Inherits="cxpcxc.cxc_ConsultaContabilidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<script type="text/javascript">
        function Confirmar() {
            var continuar = false;
            if (confirm('Esta seguro que desea Exportar las solicitudes?')) {
                continuar = true;
            }
            return continuar;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager id="SM1" runat ="server"></asp:ScriptManager>
    <fieldset>
        <legend>EXPORTAR CUENTAS COBRADAS </legend>
        
        <asp:Panel ID ="pnExportar" Width ="100%" runat ="server"  >
            <table style ="width:80%; margin :0 auto;">
                <tr>
                    <td colspan ="5" class="Titulos">SELECCIONA  EL AÑO Y MES QUE DESEA EXPORTAR</td>
                </tr>
               <tr><td colspan ="5" style ="height :25px"></td></tr>
                <tr>
                    <td>AÑO:</td>
                    <td>
                        <asp:DropDownList ID ="dpAño" runat ="server"  Width ="80px">
                        <asp:ListItem  Text ="2016" Value ="2016"></asp:ListItem>
                        <asp:ListItem  Text ="2017" Value ="2017"></asp:ListItem>
                        <asp:ListItem  Text ="2018" Value ="2018"></asp:ListItem>
                        <asp:ListItem  Text ="2019" Value ="2019"></asp:ListItem>
                        <asp:ListItem  Text ="2020" Value ="2020"></asp:ListItem>
                        </asp:DropDownList>  
                    </td>
                    <td style ="width:100px"></td>
                    <td>MES:</td>
                    <td>
                        <asp:DropDownList ID ="dpMes" runat ="server" Width ="200px" >
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
                </tr>
                <tr><td colspan ="5" style ="height :25px"></td></tr>
                <tr>
                    <td colspan ="5" style ="text-align: center; color: red;"> 
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan ="5">
                        <div id="dvBtns" style="width:100%; text-align:right;">
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar"  CssClass="button" OnClientClick ="return Confirmar();" OnClick="btnExportar_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel><br /><br />
        
    </fieldset>
</asp:Content>
