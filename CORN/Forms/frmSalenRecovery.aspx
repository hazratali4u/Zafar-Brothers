<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmSalenRecovery.aspx.cs" Inherits="Forms_frmSalenRecovery" Title="CORN :: Sales & Recovery Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
     <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

          <%--  $("#<%=drpSaleForce.ClientID %>").searchable();--%>
        }
           function CheckBoxListSelectSalePerson() {
                var chkBoxList = document.getElementById('<%= chbSaleForce.ClientID %>');
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
        function selectallLocation() {
                var chkBoxList = document.getElementById('<%= chbLocation.ClientID %>');
                var chkBox = document.getElementById('<%= ChbSelectAllLocation.ClientID %>');
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


        function chbselectAllRegion() {
                var chkBoxList = document.getElementById('<%= chbRegion.ClientID %>');
                var chkBox = document.getElementById('<%= chbselectAllRegion.ClientID %>');
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



        function chbselectAllTerritory() {
                var chkBoxList = document.getElementById('<%= chbTerritory.ClientID %>');
                var chkBox = document.getElementById('<%= chbselectAllTerritory.ClientID %>');
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


        function chbselectallZone() {
                var chkBoxList = document.getElementById('<%= chbZone.ClientID %>');
                var chkBox = document.getElementById('<%= chbselectallZone.ClientID %>');
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
                <td style="padding-left:5px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_rpt" runat="server">
                                <table>
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                   
<%--                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="73px"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td style="height: 25px" align="left">
                                         <asp:DropDownList ID="drpDistributor" AutoPostBack="true" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>

                                                <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Sale Person"  Width="78px"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:DropDownList ID="drpSaleForce" runat="server" Width="200px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>

                                                <asp:Label ID="Label1" runat="server" CssClass="lblbox" Text="Region"  Width="78px"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            <asp:Panel ID="abc" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">

                                                <asp:CheckBoxList ID="chbRegion" runat="server"></asp:CheckBoxList>
                                            </asp:Panel>
                                            
                                        </td>
                                    </tr>--%>


                                      <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllLocation" Checked="true" onclick="selectallLocation()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All" AutoPostBack="true" OnCheckedChanged="ChbSelectAllLocation_CheckedChanged"></asp:CheckBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="ChbSelectAllSalePerson" Checked="true" onclick="CheckBoxListSelectSalePerson()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
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
                                            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">

                                                <asp:CheckBoxList ID="chbLocation" AutoPostBack="true" runat="server" OnSelectedIndexChanged="chbLocation_SelectedIndexChanged"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td><strong>

                                            <asp:Label ID="Label5" runat="server" CssClass="lblbox" Text="Sale Person" Width="78px"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" Width="200px" ScrollBars="Auto" Height="150px">

                                                <asp:CheckBoxList ID="chbSaleForce" runat="server"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="chbselectAllRegion" Checked="true" onclick="chbselectAllRegion()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All" AutoPostBack="true" OnCheckedChanged="chbselectAllRegion_CheckedChanged"></asp:CheckBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="chbselectAllTerritory" Checked="true" onclick="chbselectAllTerritory()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All" AutoPostBack="true" OnCheckedChanged="chbselectAllTerritory_CheckedChanged"></asp:CheckBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>

                                                <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Region" Width="78px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:Panel ID="Panel3" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">

                                                <asp:CheckBoxList ID="chbRegion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chbRegion_SelectedIndexChanged"></asp:CheckBoxList>
                                            </asp:Panel>

                                        </td>
                                         <td><strong>

                                            <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Territory" Width="78px"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:Panel ID="Panel4" runat="server" BorderWidth="1px" Width="200px" ScrollBars="Auto" Height="150px">

                                                <asp:CheckBoxList ID="chbTerritory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chbTerritory_SelectedIndexChanged"></asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="chbselectallZone" Checked="true" onclick="chbselectallZone()" runat="server" Width="75px"
                                                Font-Size="8pt" Text="Select All"></asp:CheckBox>
                                        </td>
                                    <tr>
                                        <td align="left"></td>
                                        <td align="left">
                                            <strong>

                                                <asp:Label ID="Label8" runat="server" CssClass="lblbox" Text="Zone" Width="78px"></asp:Label></strong>
                                        </td>
                                        <td align="left"></td>
                                        <td align="left" style="height: 25px">
                                            <asp:Panel ID="Panel5" runat="server" BorderWidth="1px" Width="100%" ScrollBars="Auto" Height="150px">

                                                <asp:CheckBoxList ID="chbZone" runat="server" ></asp:CheckBoxList>
                                            </asp:Panel>

                                        </td>
                                       
                                    </tr>

                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="70px"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            &nbsp;<asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10"
                                                onkeyup="BlockStartDateKeyPress()" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong>
                                        </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="height: 25px">
                                            &nbsp;<asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10"
                                                onkeyup="BlockEndDateKeyPress()" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                Width="16px" />
                                        </td>
                                    </tr>
                                    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
        &nbsp;
    </div>
</asp:Content>
