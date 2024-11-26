<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="pruebas.aspx.cs" Inherits="cxpcxc.pruebas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <script type ="text/javascript">
        $(document).ready(function () {
            $("#texto1").keyup(function () {
                var value = $(this).val();
                var total = $('#<%=lbtotal.ClientID%>').val();      
                  $("#texto2").val(value);
              });
        });

        $(document).ready(function () {
            $("#txPrd").keyup(function () {
                var f = new Date();
                day = fecha.getDate();
                month = fecha.getMonth() + 1;
                year = fecha.getFullYear();

                //month=
                var periodo = $(this).val();
                var total = $('#<%=lbtotal.ClientID%>').val();
                $("#txfhdos").val(day + "/" + month + "/" + year);
            });
        });



        

    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <fieldset>
        <legend>CATALOGO DE PROVEEDORES</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 60%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right; height:auto ">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CausesValidation="false" CssClass="button" OnClick="BtnCerrar_Click" />
                </td>
            </tr>
        </table>
        <div runat ="server" >
            <asp:Label ID="lbtotal" runat ="server"  Text ="1250"></asp:Label>
            <input type="text" id="texto1" value=""/><br/>
             <input type="text" id="texto2" value=""/>
            
        </div><br />
        <div style="border-style: none; border-color: inherit; border-width: 1px; font-weight:bold;" >  
            <asp:Button ID="btnObtenerIP" runat="server" Text="prueba  obtencion ip" CssClass="button" OnClick="btnObtenerIP_Click" /><br /><br />
            Tu dirección IP es: <asp:Label ID="lblIPAddress" runat="server" Text=""></asp:Label><br /><br />  
            Tu computadora/Host Name es: <asp:Label ID="lblHostName" runat="server" Text=""></asp:Label>  
            <br /><br />  
            Tu dirección IP detrás del Proxy es: <asp:Label ID="lblIPBehindProxy" runat="server" Text=""></asp:Label>  
        </div>  
    </fieldset>

    <%--<fieldset>
        <div id="wrapper">
          <div id="header">
              <h1>Menú</h1>
                <ul>
                  <li>Inicio</li>
                  <li>Noticias</li>
                  <li>Artículos</li>
                  <li>Contacto</li>
                </ul>
          </div>
 
          <div id="content">
            <div id="menu">
              <h1>Instrucciones</h1>
 
                <ol>
                  <li>Enchufar correctamente</li>
                  <li>Comprobar conexiones</li>
                  <li>Encender el aparato</li>
                </ol>
            </div>
            ...
          </div>
 
          <div id="footer">
            ...
          </div>
        </div>

    </fieldset>--%>
    <fieldset> 
        <legend>OBJETO DINAMICO </legend>

        <asp:Button  ID ="btnObjDinamico" runat ="server" Text =" Objeto Dinamico" OnClick="btnObjDinamico_Click"/>
    </fieldset>

</asp:Content>
