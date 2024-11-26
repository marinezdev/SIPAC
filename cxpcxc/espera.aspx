<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="espera.aspx.cs" Inherits="cxpcxc.espera" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <br />
    <asp:Panel ID="pnDireccion" runat ="server"  Visible ="false" >
        <div style =" text-align:center; vertical-align:middle">
            <div>
                <b><asp:Label ID="lbTotalSolPd" runat ="server" Font-Size="14px" ></asp:Label></b>
            </div>
            <asp:Chart ID="chtSolPendXDias" runat="server" Palette="Pastel" Width="600px" Height="300px"  BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" IsSoftShadows="False" 
                BorderlineDashStyle="Solid" BackColor="211, 223, 240">
                <borderskin skinstyle="Emboss" PageColor="Azure" ></borderskin>
                <Titles>
                    <asp:Title Text="SOLICITUDES PENDIENTES DE PAGO" TextStyle="Shadow" ForeColor="26, 59, 105"></asp:Title>
                </Titles>
                <Series >
                    <asp:Series Name="sreDias" LegendText ="Solicitudes pago"  IsValueShownAsLabel="True" Font="Arial, 10pt, style=Bold" CustomProperties="DrawingStyle=Emboss" ChartArea="ChrtPendDias" Color ="Yellow"  LabelFormat ="##,#.##" MarkerBorderColor="DarkGray" MarkerSize="10" MarkerStyle="Circle" XAxisType="Secondary" LabelBackColor="White" MarkerColor="255, 128, 0" Palette="Pastel" ></asp:Series>
                </Series>
                <legends>
				    <asp:Legend BackColor="Transparent" Docking="Top" Font="Arial, 8pt, style=Bold"  BackSecondaryColor="YellowGreen" LegendStyle="Row" ></asp:Legend>
			    </legends>
                <ChartAreas>
                    <asp:ChartArea Name="ChrtPendDias" BackColor="Transparent" BackGradientStyle="TopBottom" BackSecondaryColor="Transparent" BorderColor="AliceBlue">
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
            <br /><br />
        </div>
    </asp:Panel>
</asp:Content>
