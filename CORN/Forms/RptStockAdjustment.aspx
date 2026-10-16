<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptStockAdjustment.aspx.cs" Inherits="Forms_RptStockAdjustment"
    Title="CORN :: Stock Adjustment Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {$("select").searchable();
        }
         
            function SelectAllPrincipal() {
               
                var chkBoxList = document.getElementById('<%= chbPrincipal.ClientID %>');
                var chkBox = document.getElementById('<%= ChbAllPrincipal.ClientID %>');
                if (chkBox.checked == true) {
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");

                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = true;
                    }
                }
                else {
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");

                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = false;
                    }
                }
            }


            function SelectAllCategory() {
               
                var chkBoxList = document.getElementById('<%= chbCategory.ClientID %>');
                var chkBox = document.getElementById('<%= ChbAllCategory.ClientID %>');
                if (chkBox.checked == true) {
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");

                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = true;
                    }
                }
                else {
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");

                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = false;
                    }
                }
            }



        function ChbSelectAllSeason() {
               
                var chkBoxList = document.getElementById('<%= chbSeason.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllSeason.ClientID %>');
                if (chkBox.checked == true) {
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");

                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = true;
                    }
                }
                else {
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");

                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = false;
                    }
                }
        }



        function ChbSelectAllGender() {
               
                var chkBoxList = document.getElementById('<%= chbGender.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllGender.ClientID %>');
                if (chkBox.checked == true) {
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");

                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = true;
                    }
                }
                else {
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");

                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = false;
                    }
                }
            }



            function UnCheckSelectAllprincipal() {
                var chkBox = document.getElementById('<%= ChbAllPrincipal.ClientID %>');
                var chkBoxList = document.getElementById('<%= chbPrincipal.ClientID %>');
                var chkBoxCount = chkBoxList.getElementsByTagName("input");
                var count = 0;
                for (var i = 0; i < chkBoxCount.length; i++) {
                    if (chkBoxCount[i].checked == false) {
                        count += 1;
                    }
                }
                if (count > 0) {
                    chkBox.checked = false;
                }
                else {
                    chkBox.checked = true;
                }
            }


        function UnCheckSelectAllCategory() {
            var chkBox = document.getElementById('<%= ChbAllCategory.ClientID %>');
            var chkBoxList = document.getElementById('<%= chbCategory.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == false) {
                    count += 1;
                }
            }
            if (count > 0) {
                chkBox.checked = false;
            }
            else {
                chkBox.checked = true;
            }
        }
        
    </script>
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="width: 1px; height: 30px" align="left"></td>
                                        <td style="height: 30px" align="left">
                                            <strong>
                                                <asp:Label ID="Label6" runat="server" Width="70px" Text="Report Type"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 30px" align="left"></td>
                                        <td style="width: 203px; height: 30px" align="left">
                                            <asp:DropDownList ID="ddlType" runat="server" Width="210px">
                                                <asp:ListItem Value="0" Text="Both" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="Short"></asp:ListItem>
                                                <asp:ListItem Value="9" Text="Excess"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 30px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 30px" align="left"></td>
                                        <td style="height: 30px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 30px" align="left"></td>
                                        <td style="width: 203px; height: 30px" align="left">
                                            <asp:DropDownList ID="DrpLocation" runat="server" Width="210px" CssClass="DropList"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 30px" align="left"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 24px"></td>
                                        <td style="height: 24px"></td>
                                        <td style="height: 24px"></td>
                                        <td style="height: 24px"> <asp:CheckBox ID="ChbAllPrincipal" onclick="SelectAllPrincipal()" Checked="true" runat="server" Text="All">
                                            </asp:CheckBox></td>
                                        <td style="height: 24px"></td>
                                        
                                        <td style="height: 24px"></td>
                                        <td style="height: 24px"> <asp:CheckBox ID="ChbAllCategory" Checked="true" onclick="SelectAllCategory()" runat="server" Text="All">
                                            </asp:CheckBox></td>
                                        <td style="height: 24px"></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td><strong>
                                            <asp:Label ID="lbltoLocation" runat="server" Width="61px" Text="Supplier" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td><strong>
                                            <asp:Label ID="lblcategory" runat="server" Width="61px" Text="Category" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 33px;" align="left"></td>
                                        <td></td>
                                        <td></td>
                                        <td style="width: 250px; height: 33px" align="left">
                                            <asp:Panel ID="pnlsupplier" runat="server" Width="100%" Height="200px" BorderWidth="1px" ScrollBars="Auto">
                                                <asp:CheckBoxList ID="chbPrincipal" onclick="UnCheckSelectAllprincipal()" runat="server"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 33px" align="left"></td>
                                        <td></td>
                                        <td style="width: 250px; height: 33px" align="left">
                                            <asp:Panel ID="pnlCategory" runat="server" Width="100%" Height="200px" BorderWidth="1px" ScrollBars="Auto">
                                                <asp:CheckBoxList ID="chbCategory" onclick="UnCheckSelectAllCategory()" runat="server"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                       <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllSeason" Checked="true" onclick="ChbSelectAllSeason()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                        <td></td>
                                           <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllGender" Checked="true" onclick="ChbSelectAllGender()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td><strong>
                                            <asp:Label ID="Label4" runat="server" Width="61px" Text="Season" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td><strong>
                                            <asp:Label ID="Label5" runat="server" Width="61px" Text="Gender" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 33px;" align="left"></td>
                                        <td></td>
                                        <td></td>
                                        <td style="width: 250px; height: 33px" align="left">
                                            <asp:Panel ID="pnlSeason" runat="server" Width="100%" Height="200px" BorderWidth="1px" ScrollBars="Auto">
                                                <asp:CheckBoxList ID="chbSeason"  runat="server"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 33px" align="left"></td>
                                        <td></td>
                                        <td style="width: 250px; height: 33px" align="left">
                                            <asp:Panel ID="pnlGender" runat="server" Width="100%" Height="200px" BorderWidth="1px" ScrollBars="Auto">
                                                <asp:CheckBoxList ID="chbGender"  runat="server"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 29px; height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="70px" Text="From Date"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="153px" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                      <tr>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 29px; height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="59px" Text="To Date"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="153px" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntToCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left"></td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImgBntToCalc" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                            &nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
