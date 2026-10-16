<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptStockValuation.aspx.cs" Inherits="Forms_RptStockReconcilation" Title="CORN :: Stock Valuation Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
   <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
     <script language="JavaScript" type="text/javascript">

         function pageLoad() {

             $("select").searchable();
         }

         function CheckBoxListSelect() {
            var chkBoxList = document.getElementById('<%= chbPrincipal.ClientID %>');
            var chkBox = document.getElementById('<%= ChbSelectAll.ClientID %>');
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
          function SelectCategory() {
            var chkBoxList = document.getElementById('<%= chbCategory.ClientID %>');
            var chkBox = document.getElementById('<%= chbSelectCategory.ClientID %>');
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
         function SelectGender() {
            var chkBoxList = document.getElementById('<%= chbGender.ClientID %>');
            var chkBox = document.getElementById('<%= chbselectGender.ClientID %>');
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
          function SelectSeason() {
            var chkBoxList = document.getElementById('<%= chbSeason.ClientID %>');
            var chkBox = document.getElementById('<%= chbselectSeason.ClientID %>');
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
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_rpt" runat="server">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr >
                                            <td align="left">
                                                <strong>Price</strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:RadioButtonList ID="rblReportType" runat="server" Width="200px" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="0">Purchase Price</asp:ListItem>
                                                    <asp:ListItem Value="1">Trade Price</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="250px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>  <asp:CheckBox ID="ChbSelectAll" onclick="CheckBoxListSelect()" runat="server" Width="75px"
                                                        Font-Size="8pt" Text="Select All" ></asp:CheckBox></td>
                                            <td></td><td></td>
                                              <td>  <asp:CheckBox ID="chbSelectCategory" onclick="SelectCategory()" runat="server" Width="75px"
                                                        Font-Size="8pt" Text="Select All" ></asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="78px" Text="Principal" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:Panel runat="server" Width="250px" BorderWidth="1px" Height="150px" ScrollBars="Vertical">
                                                    <asp:CheckBoxList ID="chbPrincipal" runat="server" Width="100%"></asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                              <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="78px" Text="Category" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:Panel runat="server" Width="250px" BorderWidth="1px" Height="150px" ScrollBars="Vertical">
                                                    <asp:CheckBoxList ID="chbCategory" runat="server" Width="100%"></asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td></td>
                                            <td></td>
                                            <td>  <asp:CheckBox ID="chbselectGender" onclick="SelectGender()" runat="server" Width="75px"
                                                        Font-Size="8pt" Text="Select All" ></asp:CheckBox></td>
                                            <td></td><td></td>
                                              <td>  <asp:CheckBox ID="chbselectSeason" onclick="SelectSeason()" runat="server" Width="75px"
                                                        Font-Size="8pt" Text="Select All" ></asp:CheckBox></td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="78px" Text="Gender" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:Panel runat="server" Width="250px" BorderWidth="1px" Height="150px" ScrollBars="Vertical">
                                                    <asp:CheckBoxList ID="chbGender" runat="server" Width="100%"></asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                              <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="78px" Text="Season" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                <asp:Panel runat="server" Width="250px" BorderWidth="1px" Height="150px" ScrollBars="Vertical">
                                                    <asp:CheckBoxList ID="chbSeason" runat="server" Width="100%"></asp:CheckBoxList>
                                                </asp:Panel>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="80px" Height="13px" Text="Stock Date"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="height: 25px" align="left">
                                                &nbsp;<asp:TextBox ID="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server"
                                                    Width="150px" CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
                                        <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtEndDate"
                                            PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                        </cc1:CalendarExtender>
                                    </tbody>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp; &nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" Width="90" CssClass="Button"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" Width="90" CssClass="Button"
                        OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
        &nbsp;
    </div>
</asp:Content>
