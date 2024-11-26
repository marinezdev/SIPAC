<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="trf_Rep_pendientes.aspx.cs" Inherits="cxpcxc.trf_Rep_pendientes" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:HiddenField ID ="hdIdEmpresa" runat ="server" />
    <asp:ScriptManager ID ="SM" runat ="server" ></asp:ScriptManager>
    <fieldset>
        <legend>PENDIENTES</legend>
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
        </table><br/>
        <asp:UpdatePanel ID ="upSeleccion" runat ="server" >
            <ContentTemplate>
                <table style ="width:50%; margin : 0 auto ">
                    <tr>
                        <td><b>TIPO:</b></td>
                        <td>
                            <asp:DropDownList ID="dpTipo" runat ="server" >
                                <asp:ListItem Value ="G" Text ="GENERAL"></asp:ListItem>
                                <asp:ListItem Value ="D" Text ="DETALLADO"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnConsultar" runat ="server"  ImageUrl ="~/img/search.png" OnClick="btnConsultar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>FECHA FACURA:</b></td>
                        <td>
                            <asp:Panel ID="pnPeriodo" runat ="server" >
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
                            </asp:Panel>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan ="3"><b><asp:CheckBox ID ="chkCompleto" runat ="server" Text ="Reporte completo" OnCheckedChanged="chkCompleto_CheckedChanged" AutoPostBack ="true"  /></b></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel><br /><br />
        <asp:Panel ID ="pnPendientes" runat="server"  Width ="100%"  Height ="470px" ScrollBars ="Auto">
            <rsweb:ReportViewer ID="rpvPendientes" runat="server" Width="100%" Height ="450px" ZoomMode ="PageWidth" ShowFindControls="false" ShowPrintButton="false" ></rsweb:ReportViewer>
        </asp:Panel>
    </fieldset> 
</asp:Content>
