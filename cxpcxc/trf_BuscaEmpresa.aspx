<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_BuscaEmpresa.aspx.cs" Inherits="cxpcxc.trf_BuscaEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript">
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) {resultado = true; }
            return resultado;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <fieldset>
        <legend> SELECCIONAR LA EMPRESA</legend>
        <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
            <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
        </div><br />
        <div id='DivBuscar' style="width :100%">
            <table id ="tblEmpresa" runat ="server"  style ="width :80%; margin :0 auto">
                <tr>
                    <td style ="width:20%">EMPRESA:</td>
                    <td>
                        <asp:DropDownList ID="dpEmpresa" runat ="server" Width="450px" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ControlToValidate="dpEmpresa" ErrorMessage="*"  ForeColor="Red" InitialValue ="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr><td style ="height :20px"></td></tr>
                <tr>
                    <td colspan ="2" style ="text-align :right"> 
                        <asp:Button ID ="btnAceptar" runat ="server" Text ="Aceptar"  CssClass ="button" OnClick="btnAceptar_Click" OnClientClick="return Confirmar();"  />&nbsp;&nbsp;
                        <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" CausesValidation ="false" />
                    </td>
                </tr>
            </table>
        </div>
</fieldset> 

</asp:Content>
