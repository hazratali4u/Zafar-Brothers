<%@ Page Title="CORN :: Invoice Wise Sales" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="RptInvoiceWiseSale.aspx.cs" Inherits="Forms_RptInvoiceWiseSale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {

            $("select").searchable();
        }

        function ValidateForm() {

            return true;
        }
        function SelectAllAccountHead() {
            var chkBoxList = document.getElementById('<%= Grid_users.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAll.ClientID %>');
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
        function UnCheckSelectAll() {
            var chkBox = document.getElementById('<%= ChbAll.ClientID %>');
            var chkBoxList = document.getElementById('<%= Grid_users.ClientID %>');
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
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_rpt" runat="server">
                                    <table>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left" colspan="3">
                                            </td>
                                        </tr>
                                        <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="Principal" Visible="false"
                                            Width="70px"></asp:Label>
                                        <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px" CssClass="DropList"
                                            Visible="false">
                                        </asp:DropDownList>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" CssClass="lblbox" Text="Location" Width="94px"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left" style="height: 25px">
                                                <asp:DropDownList ID="drpDistributor" runat="server" CssClass="DropList" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Height="13px" Text="From Date" Width="77px"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left" style="height: 25px">
                                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10" onkeyup="BlockStartDateKeyPress()"
                                                    Width="150px"></asp:TextBox>
                                                <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Height="13px" Text="To Date" Width="76px"></asp:Label></strong>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left" style="height: 25px">
                                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10" onkeyup="BlockEndDateKeyPress()"
                                                    Width="150px"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                                    Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td align="left" style="height: 25px">
                                                <asp:Button ID="btnGetData" runat="server" CssClass="Button" Text="Get Data" Width="90"
                                                    OnClick="btnGetData_Click" />
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
                    </td>
                </tr>
            </table>
            &nbsp;
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="ChbAll" onclick="SelectAllAccountHead()" runat="server" Text="Select All">
                                </asp:CheckBox>
                                <fieldset>
                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="240px" ScrollBars="Vertical">
                                        <asp:GridView ID="Grid_users" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                            Width="99%">
                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                PreviousPageText="Previous" />
                                            <RowStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChbCustomer" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"/>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SALE_INVOICE_ID" HeaderText="SALE_INVOICE_ID">
                                                    <ItemStyle CssClass="HidePanel"/>
                                                    <HeaderStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BILL NO" HeaderText="Document No">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BILL DATE" HeaderText="Document Date" DataFormatString="{0:dd-MMM-yyyy}">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                               
                                                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Qty">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GROSS SALE" HeaderText="Gross Sale" DataFormatString="{0:f2}">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="STANDARD DISCOUNT" HeaderText="Discount" DataFormatString="{0:f2}">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TOTAL_NET_AMOUNT" HeaderText="Net Amount" DataFormatString="{0:f2}">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="RELIZATION" HeaderText="Payment Rcd." DataFormatString="{0:f2}">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                      </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="tblhead" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <asp:Button ID="BtnViewPdf" runat="server" CssClass="Button" OnClick="BtnViewPdf_Click"
                            Text="View PDF" Width="90" />
                        <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                            Width="90" OnClick="btnViewExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
