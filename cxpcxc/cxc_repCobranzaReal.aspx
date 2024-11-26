<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxc_repCobranzaReal.aspx.cs" Inherits="cxpcxc.cxc_repCobranzaReal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    <asp:HiddenField ID="hdIdEmpresa" runat ="server" />
    <fieldset>
        <legend>REPORTE DE COBRANZA </legend>
        <table style ="width:100%" >
            <tr>
                <td style ="width :80%;color :red;text-align :center "><asp:Literal ID ="ltMsg"  runat ="server" ></asp:Literal></td>
                <td style ="text-align :right"><asp:Button  ID ="btnCerrar" runat ="server" Text="Cerrar" CssClass="button" OnClick="btnCerrar_Click"/></td>
            </tr>
        </table><br />
        <table style ="width :65%; margin :0 auto">
            <tr>
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
                <td style ="text-align :right">
                    <asp:ImageButton ID ="imbtnconsulta"  runat ="server" ImageUrl="~/img/search.png" OnClick="imbtnconsulta_Click"  />
                </td>
            </tr>
        </table><br /><br />
        <asp:Panel ID ="pnGrafica" runat ="server"  Width ="100%" HorizontalAlign="Center"   >
            <table style ="width :830px;margin :0 auto">
                <tr>
                    <td style ="width :25%"><b>TOTAL PESOS</b></td>
                    <td><b><asp:Label  ID="lbTotPesos" runat ="server" Text ="0" ForeColor ="DarkGreen" Font-Size ="16px" ></asp:Label></b></td>
                    <td style ="width :25%"><b>TOTAL DOLARES</b></td>
                    <td><b><asp:Label  ID="lbTotDolares" runat ="server" Text ="0" ForeColor ="DarkGreen" Font-Size ="16px"></asp:Label></b></td>
                </tr>
            </table><br />
            <asp:Chart ID="chtCobranzaReal" runat="server" Palette="Pastel" Width="830px" Height="300px"  BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" IsSoftShadows="False" 
                BorderlineDashStyle="Solid" BackColor="211, 223, 240">
                <borderskin skinstyle="Emboss" PageColor="Azure" ></borderskin>
                <Titles>
                    <asp:Title Text="REPORTE DE COBRANZA" TextStyle="Shadow" ForeColor="26, 59, 105"></asp:Title>
                </Titles>
                <Series >
                    <asp:Series Name="srePesos" LegendText ="Pesos"  ChartType="Column"  IsValueShownAsLabel="True" Font="Arial, 10pt, style=Bold" CustomProperties="DrawingStyle=Emboss" ChartArea="ChrtCobranza" Color ="#dde207"  LabelFormat ="##,#.##" ></asp:Series>
                    <asp:Series Name="sreDolares" LegendText="Dolares" ChartType="Column" IsValueShownAsLabel="True" Font="Arial, 10pt, style=Bold" CustomProperties="DrawingStyle=Emboss " ChartArea="ChrtCobranza" color="#7dca15" LabelFormat ="##,#.##"></asp:Series>
                    </Series>
                <legends>
				    <asp:Legend BackColor="Transparent" Docking="Top" Font="Arial, 8pt, style=Bold" LegendStyle="Row" ></asp:Legend>
			    </legends>
                <ChartAreas>
                    <asp:ChartArea Name="ChrtCobranza" BackColor="Transparent" BackGradientStyle="TopBottom" BackSecondaryColor="Transparent" BorderColor="AliceBlue">
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
        
        <asp:Panel ID ="pnFacturas" runat ="server"  Height ="350px" ScrollBars ="Vertical" Visible ="false"  >
            <asp:Repeater ID ="rpFactutras" runat ="server" >
                <HeaderTemplate>
                    <table id ="tblFactutras" border ="1" style ="width :80%;margin :0 auto" class="tblFiltrar" >
                        <thead>
                            <th scope="col">FECHA</th>
                            <th scope="col">CLIENTE</th>
                            <th scope="col">NO FACTURA</th>
                            <th scope="col">FECHA FACTURA</th>
                            <th scope="col">PROYECTO</th>
                            <th scope="col">IMPORTE</th>
                            <th scope="col">MONEDA</th>
                            
                        </thead>
                    <tbody>     
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333">
                        <td><%# Eval("FechaRegistro")%></td>
                        <td><%# Eval("Cliente")%></td>
                        <td><%# Eval("NumFactura")%></td>
                        <td><%# Eval("FechaFactura ","{0:d}")%></td>
                        <td><%# Eval("Proyecto")%></td>
                        <td style ="text-align :right"><%# Eval("Importe ","{0:0,0.00}")%></td>
                        <td><%# Eval("TipoMoneda")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        
    </fieldset>
</asp:Content>
