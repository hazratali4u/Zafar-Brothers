<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSalePersonPerformance.aspx.cs" Inherits="Forms_frmSalePersonPerformance" Title="CORN :: Saleman Performance Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("#<%=drpDistributor.ClientID %>").searchable();
        }
            function CheckBoxListSelectPrincipal() {
                var chkBoxList = document.getElementById('<%= chbPrincipal.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllPrincipal.ClientID %>');
                var chkBoxCount;
                var i;
                if (chkBox.checked == true) {
                    chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = true;
                    }
                }
                else {
                    chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = false;
                    }
                }
            }


            function CheckBoxListSelectSalePerson() {
                var chkBoxList = document.getElementById('<%= chbSalePerson.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllSalePerson.ClientID %>');
                var chkBoxCount;
                var i;
                if (chkBox.checked == true) {
                    chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = true;
                    }
                }
                else {
                    chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = false;
                    }
                }
            }

            function CheckBoxListSelectSeason() {
                var chkBoxList = document.getElementById('<%= ChbSeason.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllSeason.ClientID %>');
                var chkBoxCount;
                var i;
                if (chkBox.checked == true) {
                    chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = true;
                    }
                }
                else {
                    chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = false;
                    }
                }
            }

            function CheckBoxListSelectRegion() {
                var chkBoxList = document.getElementById('<%= chbRegion.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllRegion.ClientID %>');
                var chkBoxCount;
                var i;
                if (chkBox.checked == true) {
                    chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = true;
                    }
                }
                else {
                    chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = false;
                    }
                }
            }
        

    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td style="padding-left: 5px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_rpt" runat="server">
                                <table>
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="73px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td style="height: 25px" align="left">
                                            <asp:DropDownList ID="drpDistributor" AutoPostBack="true" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllPrincipal" Checked="true" onclick="CheckBoxListSelectPrincipal()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllSalePerson" Checked="true" onclick="CheckBoxListSelectSalePerson()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td align="left" style="height: 25px"></td>
                                        <td align="left" style="height: 25px">
                                            <strong>

                                                <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Supplier" Width="78px"></asp:Label></strong>
                                        </td>
                                        <td align="left" style="height: 25px"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:Panel ID="Panel3" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">
                                                <asp:CheckBoxList ID="chbPrincipal" runat="server"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <strong>

                                                <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Sale Person" Width="78px"></asp:Label></strong>
                                        </td>

                                        <td align="left" style="height: 25px; width: 200px">
                                            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">
                                                <asp:CheckBoxList ID="chbSalePerson" runat="server"></asp:CheckBoxList>
                                            </asp:Panel>

                                        </td>

                                    </tr>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllSeason" Checked="true" onclick="CheckBoxListSelectSeason()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllRegion" Checked="true" onclick="CheckBoxListSelectRegion()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    </tr>
                                   
                                        <td align="left"></td>
                                    <td align="left">
                                        <strong>

                                            <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Season" Width="78px"></asp:Label></strong>
                                    </td>
                                    <td align="left"></td>
                                    <td align="left" style="height: 25px">
                                        <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">
                                            <asp:CheckBoxList ID="ChbSeason" runat="server"></asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                    <td align="left">
                                        <strong>

                                            <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Region" Width="78px"></asp:Label></strong>
                                    </td>

                                    <td align="left" style="height: 25px">
                                        <asp:Panel ID="abc" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">
                                            <asp:CheckBoxList ID="chbRegion" runat="server"></asp:CheckBoxList>
                                        </asp:Panel>

                                    </td>

                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="70px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">&nbsp;<asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                            onkeyup="BlockStartDateKeyPress()" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">&nbsp;<asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10"
                                            onkeyup="BlockEndDateKeyPress()" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                                    <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                                        TargetControlID="txtStartDate">
                                    </cc1:CalendarExtender>
                                    <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                                        TargetControlID="txtEndDate">
                                    </cc1:CalendarExtender>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Width="90" Text="View PDF"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Width="90" Text="View Excel" OnClick="btnViewExcel_Click"
                        CssClass="Button" />
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
