<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="cxpcxcSeleccionaEmpresa.aspx.cs" Inherits="cxpcxc.cxpcxcSeleccionaEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <fieldset>
        <legend>SELECCIONAR LA EMPRESA DE TRABAJO</legend>
        <div id="dvBtns" style="width: 100%; text-align: right;">
            <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="button" OnClick="BtnCerrar_Click" CausesValidation="false" />
        </div>
        <br />
        
        <div id="dvCaja_rptSelEmpresa" style="width: 40%; margin:auto;">
            <asp:Repeater ID="rptSelEmpresa" runat="server" OnItemCommand="rptSelEmpresa_ItemCommand">
                <HeaderTemplate>
                    <table id="tblSelEmpresa" class="tblFiltrar" >
                        <thead>
                            <th scope="col">EMPRESAS</th>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="background-color: White; color: #333333;font-size :13px ;color :#102167; height:30px"" >
                        <td>
                            <b><asp:LinkButton ID="lnkBtnSelEmpresa" runat="server" ForeColor="#102167" CommandName="selempresa" CommandArgument='<%# Eval("Id")%>' Text='<%# Eval("Nombre")%>' ></asp:LinkButton></b>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="background-color:#f2f2f2; color: #333333;font-size :13px;height:30px">
                        <td>
                            <b> <asp:LinkButton ID="lnkBtnSelEmpresa" runat="server" ForeColor="#102167" CommandName="selempresa" CommandArgument='<%# Eval("Id")%>' Text='<%# Eval("Nombre")%>' ></asp:LinkButton></b>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate></tbody></table></FooterTemplate>
            </asp:Repeater>
        </div><br />
    </fieldset>
</asp:Content>
