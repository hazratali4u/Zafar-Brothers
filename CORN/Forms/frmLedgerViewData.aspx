<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmLedgerViewData.aspx.cs" Inherits="Forms_frmLedgerViewData" Title="CORN :: Voucher Editing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left" style="width: 100px">
                                    <strong>
                                        <asp:Label ID="lbltoLocation" runat="server" Width="67px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 25px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px">
                                    <strong>
                                        <asp:Label ID="Label3" runat="server" CssClass="lblbox" Text="Voucher Type" Width="104px"></asp:Label></strong>
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList ID="DrpVoucherType" runat="server" Width="200px" CssClass="DropList"
                                    AutoPostBack="True" OnSelectedIndexChanged="DrpVoucherType_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </td>
                                <td style="height: 25px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Voucher Sub Type" Width="104px" ></asp:Label></strong>
                                </td>
                                <td align="left" style="width: 100px">
                                   
                                     <asp:DropDownList ID="DrpVoucherSubType" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="height: 25px">
                                </td>
                            </tr>
                              <tr style="display:none">
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Supplier" Width="104px" ></asp:Label></strong>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:DropDownList ID="DrpPrincipal" runat="server" CssClass="DropList" Width="200px" Visible="true">
                                    </asp:DropDownList>
                                     
                                </td>
                                <td align="left" style="height: 25px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label6" runat="server" CssClass="lblbox" Text="User Name" Width="104px"></asp:Label></strong>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:DropDownList ID="DrpUser" runat="server" CssClass="DropList" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="height: 25px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label4" runat="server" Height="13px" Text="From Date" Width="70px"></asp:Label></strong>
                                </td>
                                <td style="width: 100px" align="left">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtBox" MaxLength="10" onkeyup="BlockStartDateKeyPress()"
                                        Width="190px"></asp:TextBox>
                                </td>
                                <td align="left" style="height: 25px;">
                                    <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="16px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <strong>
                                        <asp:Label ID="Label5" runat="server" Height="13px" Text="To Date" Width="80px"></asp:Label></strong>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtBox " MaxLength="10" onkeyup="BlockEndDateKeyPress()"
                                        Width="191px"></asp:TextBox>
                                </td>
                                <td align="left" style="height: 25px">
                                    <asp:ImageButton ID="ibnEndDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif"
                                        Width="16px" />
                                </td>
                            </tr>
                           
                                   <asp:Label ID="Label1" runat="server" CssClass="lblbox" Visible="false" Text="Status "
                                        Width="90px"></asp:Label>
                                    <asp:RadioButtonList ID="RbdList" runat="server" Width="150px" Visible="false" AutoPostBack="True"
                                        OnSelectedIndexChanged="RbdList_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="FALSE">Un Post Voucher</asp:ListItem>
                                        <asp:ListItem Value="TRUE">Post Voucher</asp:ListItem>
                                    </asp:RadioButtonList>
                            
                        </table>
                        <cc1:CalendarExtender ID="CEStartDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibtnStartDate"
                            TargetControlID="txtStartDate">
                        </cc1:CalendarExtender>
                        <cc1:CalendarExtender ID="CEEndDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibnEndDate"
                            TargetControlID="txtEndDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <table width="300">
                            <tr>
                                <td style="width: 100px; height: 21px;">
                                    <asp:CheckBox ID="ChbSelect" runat="server" Text="All Select" Width="115px" AutoPostBack="True"
                                        OnCheckedChanged="ChbSelect_CheckedChanged" />
                                </td>
                                <td style="width: 100px; height: 21px;">
                                    <asp:Button ID="btnView" runat="server" Font-Size="8pt" OnClick="btnView_Click" Text="View"
                                        CssClass="Button" Width="107px" />
                                </td>
                                <td style="width: 100px; height: 21px;">
                                </td>
                                <td style="height: 21px">
                                    <asp:Button ID="btnDelete" runat="server" Font-Size="8pt" Text="Cancel Voucher" CssClass="Button"
                                        Width="108px" OnClick="btnDelete_Click" />
                                </td>
                                <td style="height: 21px">
                                    <asp:Button ID="btnPrintVoucher" runat="server" Font-Size="8pt" Text="Print Voucher"
                                        CssClass="Button" Width="108px" OnClick="btnPrintVoucher_Click" />
                                </td>
                            </tr>
                        </table>
                        <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                            width: 650px; border-bottom: silver thin inset; height: 251px;">
                            <tbody>
                                <tr>
                                    <td align="left" colspan="5" style="height: 21px">
                                        <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical" Width="750px">
                                            <asp:GridView ID="GrdLedger" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                Width="728px" OnRowEditing="GrdLedger_RowEditing" OnRowDeleting="GrdLedger_RowDeleting"
                                                OnRowCreated="GrdLedger_RowCreated" >
                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                    PreviousPageText="Previous" />
                                                   <AlternatingRowStyle BackColor="#E0E0E0" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Voucher" >
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChbSelect" runat="server" />
                                                        </ItemTemplate>
                                                         <HeaderTemplate>
                                                            <asp:CheckBox ID="ChbSelectHead" runat="server" onclick="javascript:HeaderClick(this);" />
                                                        </HeaderTemplate>
                                                        <ItemStyle CssClass="grdDetail" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VOUCHER_NO" ReadOnly="true" HeaderText="Voucher No">
                                                        <ItemStyle CssClass="grdDetail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Ledger_date" ReadOnly="true" HeaderText="Voucher Date">
                                                        <ItemStyle CssClass="grdDetail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remarks" ReadOnly="true" HeaderText="Remarks">
                                                        <ItemStyle CssClass="grdDetail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VOUCHER_TYPE_ID" ReadOnly="true" HeaderText="VOUCHER_TYPE_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VOUCHER_DATE" ReadOnly="true" HeaderText="VOUCHER_DATE">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                   <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPrint" runat="server" CommandName="Edit" ToolTip="Print"
                                                                ><img alt="" id="Img2" runat="server" src="~/images/view.gif" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" ToolTip="Edit" OnClientClick="javascript:return confirm('Are you sure you want to Edit?');return false;"
                                                                ><img alt="" id="Img1" runat="server" src="~/images/edit.gif" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="grdDetail" HorizontalAlign="Center"/>
                                                    </asp:TemplateField>
                                                </Columns>
                                                 <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                <HeaderStyle CssClass="grdHead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
