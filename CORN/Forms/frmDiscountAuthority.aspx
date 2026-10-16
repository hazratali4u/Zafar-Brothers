<%@ Page Title="CORN :: Discount Policy" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmDiscountAuthority.aspx.cs" Inherits="Forms_frmDiscountAuthority" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <div id="right_data">
        <asp:UpdatePanel ID="upPanel" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label>
                                <b>Discount On</b></label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDiscountOn" runat="server" Width="200px" AutoPostBack="true"
                                OnSelectedIndexChanged="drpDiscountOn_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">Item Discount</asp:ListItem>
                                <asp:ListItem Value="2">Extra Discount</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <label style="display:none;">
                                <b>Discount Type</b></label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbldiscountType" runat="server" Visible="false" Width="150px" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="rbldiscountType_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">%age</asp:ListItem>
                                <asp:ListItem Value="2">Value</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <div style="padding-left: 5px;">
                    <asp:Panel Style="border: 1px solid; width: 45%; height: 370px;" runat="server" ID="pnlDetail"
                        ScrollBars="Vertical">
                        <table width="100%" border="0" cellspacing="2" cellpadding="5">
                            <tr>
                                <td style="display: none;">
                                </td>
                                <td class="tblhead" style="width: 14%;">
                                    <label style="color: White;">
                                        <b>Designation</b></label>
                                </td>
                                <td class="tblhead" style="width: 7%;">
                                    <label style="color: White;">
                                       <b> Upto(<%=rbldiscountType.SelectedItem.Text %>)</b></label>
                                </td>
                            </tr>
                        </table>
                        <asp:Repeater ID="rptDiscount" runat="server">
                            <ItemTemplate>
                                <table style="background-color: #ddd" class="table table-striped table-bordered table-hover table-condensed">
                                    <tr>
                                        <td style="display: none;">
                                            <asp:Label ID="lblDesgId" runat="server" Text='<%# Eval("designationId")%>'></asp:Label>
                                        </td>
                                        <td style="width: 60%;">
                                            <%# Eval("designation")%>
                                        </td>
                                        <td style="width: 7%;">
                                            <asp:TextBox ID="txtDiscountAmount" runat="server" Text='<%# Eval("discountAmount")%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                    <div style="padding-top: 5px;">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="button" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
