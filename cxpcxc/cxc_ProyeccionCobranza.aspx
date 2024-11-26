<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_ProyeccionCobranza.aspx.cs" Inherits="cxpcxc.cxc_ProyeccionCobranza" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    <asp:HiddenField ID ="hdIEmpresa" runat="server" />
    <fieldset>
        <legend>PROYECCION COBRANZA</legend>
        <table style ="width:100%" >
            <tr>
                <td style ="width :80%;color :red;text-align :center "><asp:Literal ID ="ltMsg"  runat ="server" ></asp:Literal></td>
                <td style ="text-align :right"><asp:Button  ID ="btnCerrar" runat ="server" Text="Cerrar" CssClass="button" OnClick="btnCerrar_Click"/></td>
            </tr>
        </table><br />
        <table style ="width :65%; margin :0 auto">
            <tr>
                <td> <asp:Image  runat ="server" ImageUrl ="~/img/imgcxc.png" Width ="160px" Height ="50px" /></td>
                <td>Inicio:</td>
                <td>
                    <asp:TextBox ID ="txFhInicio" runat ="server"  ></asp:TextBox>
                    <asp:ImageButton ID="imbtnInicio" runat ="server"  ImageUrl ="~/img/calendario.png" />
                    <ajaxToolkit:CalendarExtender ID="ce_imbtnInicio" runat="server"  TargetControlID ="txFhInicio" PopupButtonID ="imbtnInicio" Format ="dd/MM/yyyy"/>
                </td>
                <td>Termino:</td>
                <td>
                    <asp:TextBox ID ="txFhTermino" runat ="server"  ></asp:TextBox>
                    <asp:ImageButton ID="imbtnTermino" runat ="server"  ImageUrl ="~/img/calendario.png" />
                    <ajaxToolkit:CalendarExtender ID="ce_imbtnTermino" runat="server"  TargetControlID ="txFhTermino" PopupButtonID ="imbtnTermino" Format ="dd/MM/yyyy"/>
                </td>
            </tr>
            <tr>
                <td colspan ="5" style ="text-align :right">
                    <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click"  />
                </td>
            </tr>
        </table><br />
        <div>
            <asp:LinkButton ID="lkActzFechaComp" runat ="server" OnClick="lkActzFechaComp_Click"   ForeColor ="Red" Font-Size ="14px" ></asp:LinkButton>
        </div><br />
        <asp:Panel ID ="pnGrafica" runat ="server"  Width ="100%" HorizontalAlign="Center"   >
            <asp:Chart ID="chtProyeccion" runat="server" Palette="Pastel" Width="830px" Height="300px"  BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" IsSoftShadows="False" 
                BorderlineDashStyle="Solid" BackColor="211, 223, 240">
                <borderskin skinstyle="Emboss" PageColor="Azure" ></borderskin>
                <Titles>
                    <asp:Title Text="PROYECCION" TextStyle="Shadow" ForeColor="26, 59, 105"></asp:Title>
                </Titles>
                <Series >
                    <asp:Series Name="srePesos" LegendText ="Pesos"  ChartType="Column"  IsValueShownAsLabel="True" Font="Arial, 10pt, style=Bold" CustomProperties="DrawingStyle=Cylinder" ChartArea="ChrPry" Color ="#99b958"  LabelFormat ="##,#.##" ></asp:Series>
                    <asp:Series Name="sreDolares" LegendText="Dolares" ChartType="Column" IsValueShownAsLabel="True" Font="Arial, 10pt, style=Bold" CustomProperties="DrawingStyle=Cylinder" ChartArea="ChrPry" color="#4f82be" LabelFormat ="##,#.##"></asp:Series>
                    </Series>
                <legends>
				    <asp:Legend BackColor="Transparent" Docking="Top" Font="Arial, 8pt, style=Bold" LegendStyle="Row" ></asp:Legend>
			    </legends>
                <ChartAreas>
                    <asp:ChartArea Name="ChrPry" BackColor="Transparent" BackGradientStyle="TopBottom" BackSecondaryColor="Transparent" BorderColor="AliceBlue">
                        <Area3DStyle Enable3D="true" Inclination="35"   />
                        <AxisX2 TextOrientation="Rotated270"></AxisX2>
                                    
                        <AxisY Interval="5" IntervalAutoMode="FixedCount" LabelAutoFitStyle="DecreaseFont" LineColor="64, 64, 64, 64" IsMarginVisible="false" TextOrientation="Auto"   >
                            <MajorGrid LineColor="64, 64, 64, 64" />
                        </AxisY>
                        <AxisX Interval="1" IsMarginVisible="False" LabelAutoFitStyle="DecreaseFont" LineColor="64, 64, 64, 64" TitleFont="Arial, 7pt" TextOrientation="Rotated90" IsLabelAutoFit="False" >
                            <MajorGrid LineColor="64, 64, 64, 64" />
                            <LabelStyle Angle="-50" Font="Microsoft Sans Serif, 8pt" ForeColor="DarkRed"  />
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </asp:Panel><br />

        <asp:UpdatePanel ID ="Proyecccion" runat ="server" >
            <ContentTemplate>
                <div >
                    <asp:Repeater ID ="rpProyeccion" runat ="server" OnItemCommand="rpProyeccion_ItemCommand" OnItemDataBound="rpProyeccion_ItemDataBound" >
                        <HeaderTemplate>
                            <table id ="tblProyeccion" border ="1" style ="width :80%;margin :0 auto" class="tblFiltrar" >
                                <thead>
                                    <th scope="col">SEMANA</th>
                                    <th scope="col">INICIO</th>
                                    <th scope="col">TERMINO</th>
                                    <th scope="col">PESOS</th>
                                    <th scope="col">DOLARES</th>
                                    <th scope="col"></th>
                                </thead>
                            <tbody>     
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: White; color: #333333">
                                <td><%# Eval("Semana")%></td>
                                <td><%# Eval("FechaInicio ","{0:d}")%></td>
                                <td><%# Eval("FechaFinal","{0:d}")%></td>
                                <td style ="text-align :right"><%# Eval("Pesos","{0:0,0.00}")%></td>
                                <td style ="text-align :right"><%# Eval("Dolares","{0:0,0.00}")%></td>
                                <td style ="text-align :center"><asp:LinkButton ID ="lkDetalle" runat ="server"  Text="Detalle"  CommandName ="lkDetalle"></asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></tbody></table></FooterTemplate>
                    </asp:Repeater>
                </div><br />
                <asp:Panel ID="pnDetalles" runat ="server" Visible ="false" >
                    <div style ="text-align:center">
                        <b><asp:Label ID ="lbTitSemana" runat ="server" Font-Size ="16px" ForeColor ="#ff9e3d"></asp:Label></b>
                    </div><br />
                    <table style ="width :100%">
                        <tr>
                            <td style ="width:40%; vertical-align :top; margin-left: 40px;">
                                <asp:Repeater ID ="rpProyecto" runat ="server" >
                                    <HeaderTemplate>
                                        <table id ="tblProyecto" border ="1" style ="width :90%" class="tblFiltrar" >
                                            <caption >TOTALES POR PROYECTO</caption>
                                            <thead>
                                                <th scope="col">PROYECTO</th>
                                                <th scope="col">TIPO MONEDA</th>
                                                <th scope="col">TOTAL</th>
                                            </thead>
                                        <tbody>     
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: White; color: #333333">
                                            <td><%# Eval("Proyecto")%></td>
                                            <td><%# Eval("TipoMoneda")%></td>
                                            <td style ="text-align :right"><%# Eval("Total","{0:0,0.00}")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate></tbody></table></FooterTemplate>
                                </asp:Repeater>
                            </td>
                            <td>
                                <asp:Repeater ID ="rpfacturas" runat ="server" >
                                    <HeaderTemplate>
                                        <table id ="tblFacturas" border ="1" style ="width :90%" class="tblFiltrar" >
                                            <caption> RELACION DE FACTURAS </caption>
                                            <thead>
                                                <th scope="col">FECHA FACTURA</th>
                                                <th scope="col">CLIENTE</th>
                                                <th scope="col">MONEDA</th>
                                                <th scope="col">IMPORTE</th>
                                                <th scope="col">PROYECTO</th>
                                                <th scope="col">CONDICIONES</th>
                                            </thead>
                                        <tbody>     
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: White; color: #333333">
                                            <td><%# Eval("FechaFactura","{0:d}")%></td>
                                            <td><%# Eval("Cliente")%></td>
                                            <td><%# Eval("TipoMoneda")%></td>
                                            <td style ="text-align :right"><%# Eval("Importe","{0:0,0.00}")%></td>
                                            <td><%# Eval("Proyecto")%></td>
                                            <td><%# Eval("CondicionPago")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate></tbody></table></FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                 </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel><br /><br />
    </fieldset>
</asp:Content>
