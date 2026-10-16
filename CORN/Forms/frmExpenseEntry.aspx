<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmExpenseEntry.aspx.cs" Inherits="frmExpenseEntry" Title="CORN :: Branch Expense & Salary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
   
    <script language="JavaScript" type="text/javascript">
       

        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
            }
            else if (str == "0") {
                alert('Amount should be greater than zero');
                return false;
            }
        } 
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div style="z-index: 101; left: 537px; width: 100px; position: absolute; top: 80px;
                            height: 83px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                    TargetControlID="txtAmount" ValidChars="0123456789.">
                                </cc1:FilteredTextBoxExtender>
                                <asp:Panel ID="Panel1" runat="server">
                                    <table width="40%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <strong>
                                                        <asp:Label ID="Label5" runat="server" Width="116px" Text="Detail Account Type" Visible="false"></asp:Label></strong>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblDetailAccountType" runat="server" Width="270px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="rblDetailAccountType_SelectedIndexChanged" Visible="false">
                                                        <asp:ListItem Selected="True" Value="55">Branch Indirect Expenses</asp:ListItem>
                                                        <asp:ListItem Value="56">Direct Distribution Expense</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        <asp:Label ID="Label2" runat="server" Width="68px" Text="Voucher No" CssClass="lblbox"></asp:Label></strong>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:DropDownList ID="DrpVoucherNo" runat="server" Width="250px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DrpVoucherNo_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="height: 20px">
                                                    <strong>
                                                        <asp:Label ID="lblfromLocation" runat="server" CssClass="lblbox" Text="Location"
                                                            Width="65px"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:DropDownList ID="drpDistributor" runat="server" Width="250px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        <asp:Label ID="Label62" runat="server" Width="116px" Text="Petty Head" CssClass="lblbox"></asp:Label></strong>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:DropDownList ID="ddlPettyHead" runat="server" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <asp:Label ID="Label1" runat="server" Width="116px" Text="Expense Head" Visible="false"
                                                CssClass="lblbox"></asp:Label>
                                            <asp:DropDownList ID="DrpAccountHead" runat="server" CssClass="DropList" Visible="false"
                                                Width="250px">
                                            </asp:DropDownList>
                                            <tr>
                                                <td align="left" style="height: 20px">
                                                    <strong>
                                                        <asp:Label ID="Label9" runat="server" CssClass="lblbox" Text="Remarks" Width="71px"></asp:Label>
                                                    </strong>&nbsp;
                                                </td>
                                                <td align="left" style="height: 20px">
                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " Width="243px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="height: 20px">
                                                    <strong>
                                                        <asp:Label ID="lblExpense" runat="server" Text="Expense Date"></asp:Label>
                                                    </strong>&nbsp;
                                                </td>
                                                <td align="left" style="height: 20px">
                                                    <asp:TextBox ID="txtExpenseDate" runat="server" Width="163px"></asp:TextBox>
                                                    <asp:ImageButton ID="ibExpenseDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                                                    <cc1:CalendarExtender ID="ceExpenseDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibExpenseDate"
                                                        TargetControlID="txtExpenseDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        <asp:Label ID="Label6" runat="server" Width="63px" Text="Amount" CssClass="lblbox"></asp:Label></strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:TextBox ID="txtAmount" runat="server" Width="163px" CssClass="txtBox "></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender runat="server" ID="fteAmount" TargetControlID="txtAmount"
                                                        ValidChars=".0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td align="left" style="width: 100px;">
                                                </td>
                                                <td align="left" colspan="1" style="height: 20px">
                                                    <asp:Button ID="btnAddNew" runat="server" AccessKey="S" CssClass="Button" Font-Size="8pt"
                                                        OnClick="btnAddNew_Click" Text="Add" Width="90px" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                                &nbsp;&nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <strong>
                            <asp:Label ID="lblRowId" runat="server" Text="Label" Visible="False"></asp:Label></strong>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel12" runat="server" Height="200px" ScrollBars="Vertical" Width="620px"
                                    BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px">
                                    <asp:GridView ID="GrdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" CaptionAlign="Left" CssClass="gridRow2" ForeColor="SteelBlue"
                                        HorizontalAlign="Center" OnRowDeleting="GrdOrder_RowDeleting" OnRowEditing="GrdOrder_RowEditing"
                                        Width="600px">
                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                            PreviousPageText="Previous" />
                                           <AlternatingRowStyle BackColor="#E0E0E0" />
                                        <Columns>
                                            <asp:BoundField DataField="Account_Head_Id" HeaderText="Account Head Id">
                                                <FooterStyle CssClass="HidePanel" />
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Account_Name" HeaderText="Account Head">
                                                <FooterStyle CssClass="HidePanel" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="debit" DataFormatString="{0:F2}" FooterText="Total" HeaderText="Amount">
                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ACCOUNT_PARENT_ID" HeaderText="ACCOUNT_PARENT_ID">
                                                <HeaderStyle CssClass="HidePanel" />
                                                <ItemStyle CssClass="HidePanel" />
                                            </asp:BoundField>
                                             <asp:TemplateField HeaderText="Edit">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Delete">
                                                                    <img src="../images/edit.gif" />
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="45px" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                           
                                                                 <asp:TemplateField HeaderText="Delete" >
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDelete" ToolTip="Delete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                                    CommandName="Delete">
                                                                                    <img src="../images/delete.gif" width="16" height="16">
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="45px" HorizontalAlign="Center">
                                                                            </ItemStyle>
                                                                        </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </asp:Panel>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label61" runat="server" CssClass="lblbox" Text="Total Amount" Width="102px"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMainCash" runat="server" CssClass="txtBox " ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <br />
                                            <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Save"
                                                Width="90px" CssClass="Button" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSave" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
