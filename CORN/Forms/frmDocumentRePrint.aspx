<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="~/Forms/frmDocumentRePrint.aspx.cs" Inherits="Forms_frmDocumentRePrint" Title="CORN :: Print Sale Document" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("#<%= drpSaleForce.ClientID %>").searchable();
            $("#<%= DrpCustomer.ClientID %>").searchable();
        }

    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="1"></td>
                                            <td align="left" colspan="4">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                     
                                     
                                        <tr>
                                            <td align="left"></td>
                                            <td align="left"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left"></td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left"></td>
                                            <td align="left"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="94px" Text="Sale Force" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left"></td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpSaleForce" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left"></td>
                                            <td align="left"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="94px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left"></td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left"></td>
                                            <td align="left"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="90px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left"></td>
                                            <td align="left" style="height: 25px">&nbsp;<asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                                Width="150px"></asp:TextBox>
                                                <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left"></td>
                                            <td align="left"></td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="left"></td>
                                            <td align="left" style="height: 25px">&nbsp;<asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10"
                                                Width="150px"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                                <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                                    TargetControlID="txtStartDate">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                                    TargetControlID="txtEndDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp; &nbsp;
                        <asp:Button ID="btnGetData" runat="server" Width="90" Text="Get Data" OnClick="btnGetData_Click"
                            CssClass="Button" />
    
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
    </div>
</asp:Content>
