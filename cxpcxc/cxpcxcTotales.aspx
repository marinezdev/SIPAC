<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxpcxcTotales.aspx.cs" Inherits="cxpcxc.cxpcxcTotales" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID ="SM1" runat ="server" ></asp:ScriptManager>
    <fieldset>
        <legend>TOTALES DE PROCESO</legend>
        <table id="tblBtns" runat="server" style="width: 100%" >
            <tr>
                <td style="width: 80%; text-align: center; color: red;">
                    <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                </td>
                <td style="text-align: right;">
                   <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click"/>
                </td>
            </tr>
        </table><br />
        <table style ="width :65%; margin :0 auto" class ="tblConsulta">
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
        </table>
        <br /><br />
        <table style ="width:100%">
            <tr>
                <td style ="width :50%;vertical-align :top ">
                    <asp:Panel ID="pnDatos"  runat ="server" >
                        <table style ="width :98%; margin :0 auto; font-size:15px " class="tblConsulta">
                             <tr>
                                <td style ="width :160px"></td>
                                <td style ="text-align :center" class ="Titulos">COBRANZA</td>
                                <td></td>
                                <td style ="text-align :center" class ="Titulos">PAGOS</td>
                            </tr>
                            <tr><td colspan ="4"></td></tr>
                            <tr>
                                <td style ="text-align :right"><b>DOLARES:</b></td>
                                <td style ="text-align :right"><asp:Label ID="lbcxcDll" runat ="server"  Text ="0" ></asp:Label></td>
                                <td></td>
                                <td style ="text-align :right"><asp:Label ID="lbcxpDll" runat ="server"  Text ="0"></asp:Label></td>
                            </tr>
                            <tr><td colspan ="4"></td></tr>
                        </table>
            
                        <br /><br />
                        <table style ="width :98%; margin :0 auto; font-size:15px " class="tblConsulta">
                            <tr>
                                <td style ="width :160px"></td>
                                <td style ="text-align :center" class ="Titulos">COBRANZA</td>
                                <td></td>
                                <td style ="text-align :center" class ="Titulos">PAGOS</td>
                            </tr>
                            <tr><td colspan ="4"></td></tr>
                            <tr>
                                <td style ="text-align :right"><b>SUBTOTAL:</b></td>
                                <td style ="text-align :right"><asp:Label ID="lbcxcSubtotal" runat ="server"  Text ="0"></asp:Label></td>
                                <td></td>
                                <td style ="text-align :right"><asp:Label ID="lbcxpSubtotal" runat ="server"  Text ="0"></asp:Label></td>
                                </tr>
                            <tr><td colspan ="4"></td></tr>
                            <tr>
                                <td style ="text-align :right"><b>IVA:</b></td>
                                <td style ="text-align :right"><asp:Label ID="lbcxcIva" runat ="server"  Text ="0"></asp:Label></td>
                                <td></td>
                                <td style ="text-align :right"><asp:Label ID="lbcxpIva" runat ="server"  Text ="0"></asp:Label></td>
                                </tr>
                            <tr><td colspan ="4"></td></tr>
                            <tr>
                                <td style ="text-align :right"><b>TOTAL:</b></td>
                                <td style ="text-align :right"><asp:Label ID="lbcxcTotal" runat ="server" Text ="0"></asp:Label></td>
                                <td></td>
                                <td style ="text-align :right"><asp:Label ID="lbcxpTotal" runat ="server"  Text ="0"></asp:Label></td>
                                </tr>
                            <tr><td colspan ="4" style ="height :40px"></td></tr>
                            <tr>
                                <td><b>DIFERENCIA IVA:</b></td>
                                <td colspan ="3"><asp:Label ID="lbDifIva" runat ="server"  Text ="0"></asp:Label></td>
                            </tr>
                            <tr><td colspan ="4" style ="height :20px"></td></tr>
                            <tr>
                                <td></td>
                                <td style ="text-align :right"><asp:Button ID="btncxcDetalles" runat ="server" Text ="Detalles" CssClass ="button" OnClick="btncxcDetalles_Click"/></td>
                                <td></td>
                                <td style ="text-align :right"><asp:Button ID="btncxpDetalles" runat ="server" Text ="Detalles" CssClass ="button" OnClick="btncxpDetalles_Click"/></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td >
                    <asp:Panel ID ="pnGrafica" runat ="server"  Width ="100%" HorizontalAlign="Center"   >
                        <asp:Chart ID="chtTotales" runat="server" Palette="Pastel" Width="500px" Height="300px"  BackGradientStyle="TopBottom" BorderWidth="2px" BorderColor="#1A3B69" IsSoftShadows="False" BorderlineDashStyle="Solid" BackColor="211, 223, 240">
                            <borderskin skinstyle="Emboss" PageColor="Azure" ></borderskin>
                            <Titles>
                                <asp:Title Text="TOTALES" TextStyle="Shadow" ForeColor="26, 59, 105"></asp:Title>
                            </Titles>
                            <Series >
                                <asp:Series Name="sreCXC" LegendText ="COBRANZA" IsValueShownAsLabel="True" Font="Arial, 10pt, style=Bold" CustomProperties="DrawingStyle=Emboss" ChartArea="chAreaTotales" Color ="Green"  LabelFormat ="##,#.##" MarkerBorderColor="DarkGray" MarkerSize="5" MarkerStyle="Circle" XAxisType="Secondary" LabelBackColor="White" MarkerColor="255, 128, 0" ></asp:Series>
                                <asp:Series Name="sreCXP" LegendText="PAGOS"  IsValueShownAsLabel="True" Font="Arial, 10pt, style=Bold" CustomProperties="DrawingStyle=Emboss" ChartArea="chAreaTotales" color="Red" LabelFormat ="##,#.##" MarkerBorderColor="DarkGray" MarkerSize="5" MarkerStyle="Circle" XAxisType="Secondary" LabelBackColor="White" MarkerColor="255, 128, 0"  ></asp:Series>
                            </Series>
                            <legends>
				                <asp:Legend BackColor="Transparent" Docking="Top" Font="Arial, 8pt, style=Bold" LegendStyle="Row"  ></asp:Legend>
			                </legends>
                            <ChartAreas>
                                <asp:ChartArea Name="chAreaTotales" BackSecondaryColor="Transparent" BorderColor="AliceBlue">
                                    <area3dstyle Rotation="10" Inclination="15" />
                                    <AxisY IsMarginVisible="false"   >
                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                    </AxisY>
                                    <AxisX IsLabelAutoFit="False" IsMarginVisible="false" >
                                        <LabelStyle IsEndLabelVisible="true"  />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <br /><br />
        <asp:MultiView ID ="mvContenedor" runat ="server">
            <asp:View ID ="mvInicio" runat ="server"></asp:View>
            <asp:View ID="mvCobranza" runat ="server">
                <div style ="text-align :center; color:green;font-size :14px">LISTADO DE COBRANZA</div>
                <asp:Panel id ="pnCobranza" runat ="server" Height="480px"  ScrollBars ="Vertical" >
                    <asp:Repeater ID ="rpCobranza" runat ="server" >
                        <HeaderTemplate>
                            <table id ="tblCobranza" border ="1" style ="width :80%;margin :0 auto" class="tblFiltrar" >
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
                                <td><%# Eval("Importe ","{0:0,0.00}")%></td>
                                <td><%# Eval("TipoMoneda")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></tbody></table></FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </asp:View>
            <asp:View ID="mvPagos" runat ="server">
                <div style ="text-align :center; color:red; font-size:14px">LISTADO DE PAGOS</div>
                <asp:Panel id ="pnPagos" runat ="server" Height="480px" ScrollBars ="Vertical" >
                    <asp:Repeater ID="rptPagos" runat="server" >
                        <HeaderTemplate>
                            <table id="tblPagos" border="1" style ="width :100%" class ="tblFiltrar"  >
                                <thead>
                                    <th scope="col">FECHA PAGO</th>
                                    <th scope="col">NO. FACTURA</th>
                                    <th scope="col">FECHA FACTURA</th>
                                    <th scope="col">PROVEEDOR</th>
                                    <th scope="col">IMPORTE</th>
                                    <th scope="col">MONEDA</th>
                                    <th scope="col">IMPORTE PAGADO </th>
                                </thead>
                            <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: White; color: #333333">
                                <td><%# Eval("FechaRegistro","{0:d}")%></td>
                                <td><%# Eval("Factura")%></td>
                                <td><%# Eval("FechaFactura","{0:d}")%></td>
                                <td><%# Eval("Proveedor")%></td>
                                <td><%# Eval("Importe","{0:0,0.00}")%></td>
                                <td><%# Eval("Moneda")%></td>
                                <td><%# Eval("ImportePagado","{0:0,0.00}")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></tbody></table></FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
    </fieldset>
</asp:Content>
