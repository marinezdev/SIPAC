<%@ Page Title="" Language="C#" MasterPageFile="~/cxpcxc.Master" AutoEventWireup="true" CodeBehind="admAcceso.aspx.cs" Inherits="cxpcxc.admAcceso" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type ="text/javascript">
        function lenNoMenos(oSrc, args) { args.IsValid = (args.Value.length > 5); }
        function Confirmar() {
            var resultado = false;
            if (Page_ClientValidate() == true) {
                resultado = confirm("Los datos son correctos?");
                if (resultado) { $("#dvBtns").hide(); }
                return resultado;
            } else { return false; }
        }
    </script>
    <style>
        .mCelTituloCampo {
            width: 30%;
            text-align: right;
            font-weight: 700;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAreaTrabajo" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <fieldset>
        <legend>ACCESO AL SISTEMA</legend>
        <table id="tblBtns" style="width: 100%">
            <tr style="height: 30px">
                <td style="width: 73%">
                    <div id="dvMsg" style="width: 100%; text-align: center; color: red;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </div>
                </td>
                <td style="text-align: right;">
                    <div id="dvBtns" style="width:100%; text-align:right;">
                        
                        <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar"  CssClass="button" OnClick="BtnCerrar_Click" CausesValidation ="false" />
                    </div> 
                </td>
            </tr>
        </table><br />
        <asp:UpdatePanel ID ="upCorreo" runat ="server" >
            <ContentTemplate >
                <asp:Panel id="pnEnvioCorreo" runat ="server" Width ="70%" style ="width:70%; margin :0 auto" visible ="false" >
                    <table style ="width :98%" runat ="server" >
                        <tr>
                            <td class ="Titulos" > NOTIFICACION DE AUTORIZACION POR CORREO</td>
                        </tr>
                        <tr>
                            <td> <asp:RadioButton ID="chkSinNotificar" runat ="server"  Text="Sin Notificación" GroupName="correo" CausesValidation="false" AutoPostBack ="true" OnCheckedChanged="ServicioCorreo_CheckedChanged" /></td>
                        </tr>
                        <tr>
                            <td> <asp:RadioButton ID="chkSol" runat ="server"  Text="Notificar cada solicitud de pago"  GroupName ="correo" CausesValidation="false"  AutoPostBack="true" OnCheckedChanged="ServicioCorreo_CheckedChanged"     /></td>
                        </tr>
                        <tr>
                            <td> <asp:RadioButton ID="chkBloque" runat ="server"  Text="Notificar solicitudes periodicamente" GroupName="correo" CausesValidation="false" AutoPostBack ="true" OnCheckedChanged="ServicioCorreo_CheckedChanged" /></td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel><br />
        <div id="dvDatos" runat ="server"  style ="width:100%">
            <table  id ="tblDatos" runat ="server" style ="width:70%; margin: 0 auto">
                <tr>
                    <td colspan ="2" class ="Titulos" > CONTROL DE ACCESO</td>
                </tr>
                
                <tr>
                    <td colspan ="2"  style ="text-align :center "><asp:Label ID ="lbNombre" runat ="server" ></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2"><span style="color:#93ACCF; font-style: italic; font-size: medium;">USUARIO</span></td>
                </tr>
                <tr>
                    <td class ="mCelTituloCampo" >USUARIO:</td>
                    <td>
                        <asp:Label ID="lbUsrActual" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class ="mCelTituloCampo">NUEVO USUARIO:</td>
                    <td>
                        <asp:TextBox ID="txNvoUsuario" runat="server" MaxLength="32" Width="80%"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftb_txNvoUsuario" runat="server"
                            TargetControlID="txNvoUsuario"
                            FilterMode="ValidChars"
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-!#$%&/()=?">
                        </ajaxToolkit:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rfv_txNvoUsuario" runat="server" ErrorMessage="*" ControlToValidate="txNvoUsuario"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cv_txNvoUsuario" runat="server" ErrorMessage="No menos de 6 caracteres" ControlToValidate="txNvoUsuario" ClientValidationFunction="lenNoMenos" ForeColor="Red" Display="Dynamic"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan ="2"><span style="color:#93ACCF; font-style: italic; font-size: medium;">CLAVE DE ACCESO</span></td>
                </tr>
                <tr>
                    <td class ="mCelTituloCampo">CLAVE ACTUAL:</td>
                    <td>
                        <asp:TextBox ID="txClaveActual" runat ="server"  MaxLength ="32" Width ="80%" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvClaveActual" runat="server" ControlToValidate="txClaveActual" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteClaveActual" runat="server" TargetControlID="txClaveActual" FilterMode="ValidChars" 
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-!#$%&/()=?"/>
                    </td>
                </tr>
                <tr>
                    <td class ="mCelTituloCampo">NUEVA CLAVE:</td>
                    <td>
                        <asp:TextBox ID="txNuevaClave" runat ="server"  MaxLength ="32" Width ="80%" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNuevaClave" runat="server" ControlToValidate="txNuevaClave" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteNuevaClave" runat="server" TargetControlID="txNuevaClave" FilterMode="ValidChars" 
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-!#$%&/()=?" />
                        <asp:CustomValidator ID="cv_txNuevaClave" runat="server" ErrorMessage="No menos de 6 caracteres" ControlToValidate="txNuevaClave" ClientValidationFunction="lenNoMenos" ForeColor="Red" Display="Dynamic"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td class ="mCelTituloCampo">CONFIRMAR CLAVE:</td>
                    <td>
                        <asp:TextBox ID="txConfClave" runat ="server"  MaxLength ="32" Width ="80%" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvConfClave" runat="server" ControlToValidate="txConfClave" ErrorMessage="*"  ForeColor="Red"></asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="fteConfClave" runat="server" TargetControlID="txConfClave" FilterMode="ValidChars" 
                            ValidChars="abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890.-!#$%&/()=?" />
                        <asp:CompareValidator ID ="cv_ConfClave" runat ="server" ErrorMessage="La confirmación no coinside" ControlToValidate="txConfClave" ControlToCompare="txNuevaClave" ForeColor="Red"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan ="2" style ="text-align :right">
                        <asp:Button ID="btnModifica" runat="server" Text="Modificar"  CssClass="button" OnClick="btnModifica_Click"  OnClientClick ="return Confirmar();"/>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
</asp:Content>
