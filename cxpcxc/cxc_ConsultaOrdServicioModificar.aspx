<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_ConsultaOrdServicioModificar.aspx.cs" Inherits="cxpcxc.cxc_ConsultaOrdServicioModificar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 90%;
            height: 34px;
        }
        .auto-style2 {
            height: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    &nbsp;&nbsp;&nbsp;    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    <asp:HiddenField  ID="hdIdEmpresa" runat ="server" />
    <fieldset>
        <legend>MODIFICAR ORDEN DE VENTA</legend>
        <table id="tblBtns" runat="server" style="width: 100%" >
            <tr>
                <td style="text-align: center; color: red;" class="auto-style1">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;" class="auto-style2">
                    <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
                </td>
            </tr>
        </table><br />
        <asp:Panel ID ="pnConsulta" runat="server">
            <table id ="tbConsulta" runat ="server" style ="width:80%;margin:0 auto " class ="tblConsulta" >
                <tr>
                    <td style ="width :27%">CLIENTE:</td>
                    <td><asp:DropDownList ID ="dpCliente" runat ="server" ></asp:DropDownList></td>
                </tr>
                 <tr>
                    <td>SERVICIO:</td>
                    <td><asp:DropDownList ID ="dpServicio" runat ="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan ="2" style ="text-align :right">
                        <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click"  />
                    </td>
                </tr>
            </table>
        </asp:Panel><br />
        <asp:Panel ID ="pnOrdSrv" runat ="server" Height ="450px" ScrollBars ="Auto" >
            <asp:Repeater ID="rptOrdSrv" runat="server" OnItemCommand="rptOrdSrv_ItemCommand" OnItemDataBound="rptOrdSrv_ItemDataBound" >
                <HeaderTemplate>
                    <table id="tblOrdSrv" border="1" style="width: 100%" class="tblFiltrar">
                        <thead>
                            <th scope="col">INICIO</th>
                            <th scope="col">TERMINO</th>
                            <th scope="col">EMPRESA</th>
                            <th scope="col">CLIENTE</th>
                            <th scope="col">SERVICIO</th>
                            <th scope="col">DESCRIPCION</th>
                            <th scope="col">PARTIDAS</th>
                            <th scope="col">CONTRATO</th>
                            <th scope="col">VER</th>
                         </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td style ="width :90px;text-align :center"><%# Eval("FechaInicio","{0:d}")%></td>
                        <td style ="width :90px;text-align :center"><%# Eval("FechaTermino","{0:d}")%></td>
                        <td><%# Eval("Empresa")%></td>
                        <td><%# Eval("Cliente")%></td>
                        <td><%# Eval("Servicio")%></td>
                        <td><%# Eval("Descripcion")%></td>
                        <td style ="width :60px;text-align :center"><%# Eval("Periodos")%></td>
                        <td style="text-align: center;">
                            <asp:ImageButton ID="imgbtnDescarga" runat="server" ImageUrl="~/img/dwContrato_gr.png" CommandName="Descarga" CommandArgument='<%# Eval("IdServicio")%>' ToolTip ="Descargar contrato" Enabled ="false" />
                        </td>
                        <td style="text-align: center;width :50px">
                            <asp:ImageButton ID="imgbtnverDat" runat="server" ImageUrl="~/img/edit.png" CommandName="VerDat" CommandArgument='<%# Eval("IdServicio")%>' ToolTip ="Ver partidas" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>  
                 
         <asp:Panel  ID ="pnPopDocumento" runat ="server" Width ="800px" Style="display:none; background-color: #F2F2F2;"   >
            <div id="dvDocumento" style="width: 100%; text-align: right;">
                <asp:Button ID="btnCierraDocumento" runat="server" CssClass="button" Text="X" CausesValidation="False"/>
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
