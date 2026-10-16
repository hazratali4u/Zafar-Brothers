<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmImportPhysicalStock.aspx.cs" Inherits="frmImportPhysicalStock" Title="CORN :: Import Physical Stock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {

            $("select").searchable();
        }
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <div style="z-index: 101; left: 486px; width: 100px; position: absolute; top: 191px; height: 100px">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                    Width="22px" />
                                Record Update
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <table>
                        <tbody>
                            <tr>
                                <td align="left" style="width: 59px; height: 25px"></td>
                                <td align="left">
                                    <strong>
                                        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Width="175px"></asp:Label></strong>
                                </td>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td style="height: 25px; width: 59px;" align="left">
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" Width="58px" Text="Type"></asp:Label>
                                    </strong>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlType" runat="server" Width="200px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Text="Physical Stock" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Transfer Out"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Short"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Excess"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td style="height: 25px; width: 59px;" align="left">
                                    <strong>
                                        <asp:Label ID="lblLocation" runat="server" Width="58px" Text="Location"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DrpDistributor" runat="server" Width="200px" CssClass="DropList">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr id="trTransfer" runat="server" visible="false">
                                <td style="height: 25px;" align="left">
                                    <strong>
                                        <asp:Label ID="lblTransferTo" runat="server" Width="65px" Text="Transfer To"></asp:Label></strong>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlTransferTo" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="height: 25px">
                                    <asp:FileUpload ID="txtFile" runat="server" Width="287px" />
                                </td>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 59px; height: 25px"></td>
                                <td align="left">
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="vg"
                                        CssClass="Button" />
                                </td>
                                <td style="height: 10px"></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="height: 25px"></td>
                                <td style="height: 10px"></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
