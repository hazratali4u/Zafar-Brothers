<%@ Page Title="SAMS :: GRN View" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmPurchaseReceivedView.aspx.cs" Inherits="Forms_frmPurchaseReceivedView" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="99.9%">
            
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnEnquiryList" runat="server">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <th class="tblhead">
                                                            <td class="tblhead" style="width: 40%;">
                                                                Supplier
                                                                <asp:TextBox ID="txtProduct" runat="server" Width="150px"></asp:TextBox>
                                                                <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />
                                                            </td>
                                                            <td class="tblhead" style="width: 25%;">
                                                            </td>
                                                            <td valign="middle" class="tblhead" style="width: 10%;">
                                                            </td>
                                                            <td align="right" class="tblhead" style="width: 25%;">
                                                                &nbsp;
                                                            </td>
                                                        </th>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                    <tr>
                                                        <td class="tblhead" style="width: 5%;">
                                                            ID
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 25%;">
                                                            Supplier
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 25%;">
                                                            Products
                                                        </td>
                                                        <td class="tblhead" style="width: 7.5%;">
                                                            <asp:LinkButton ForeColor="White" ID="btnDate" runat="server" Text="Doc. Date" OnClick="btnDateSort_Click">
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td class="tblhead" style="width: 20%;">
                                                            Updated By
                                                        </td>
                                                        <td class="tblhead" style="width: 13%;">
                                                            <asp:LinkButton ForeColor="White" ID="btnLastUpdate" runat="server" Text="Last Update"
                                                                OnClick="btnLastUpdate_Click">
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 7.5%;">
                                                            Action
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Repeater ID="rReserveSample" runat="server" OnItemCommand="rReserveSample_ItemCommand">
                                                    <ItemTemplate>
                                                        <div style="background: #EFF3FB">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                        <%# Eval("PURCHASE_ORDER_ID")%>
                                                                    </td>
                                                                    <td style="width: 25%;">
                                                                        <%# Eval("SOLD_FROM")%>
                                                                    </td>
                                                                    <td style="width: 25%;">
                                                                        <%# Eval("ProductDetail")%>
                                                                    </td>
                                                                    <td style="width: 7.5%;">
                                                                        <%# Eval("DOCUMENT_DATE")%>
                                                                    </td>
                                                                    <td style="width: 20%;">
                                                                        <%# Eval("UpdatedBy")%>
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        <%# ((DateTime)Eval("LAST_UPDATE")).ToString("dd-MMM-yyyy hh:mm tt")%>
                                                                    </td>
                                                                    <td style="width: 7.5%;">
                                                                        <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("PURCHASE_ORDER_ID") %>'
                                                                            ToolTip="Edit" Text="edit">
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <div style="background: #ffffff">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                        <%# Eval("PURCHASE_ORDER_ID")%>
                                                                    </td>
                                                                    <td style="width: 25%;">
                                                                        <%# Eval("SOLD_FROM")%>
                                                                    </td>
                                                                    <td style="width: 25%;">
                                                                        <%# Eval("ProductDetail")%>
                                                                    </td>
                                                                    <td style="width: 7.5%;">
                                                                        <%# Eval("DOCUMENT_DATE")%>
                                                                    </td>
                                                                    <td style="width: 20%;">
                                                                        <%# Eval("UpdatedBy")%>
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        <%# ((DateTime)Eval("LAST_UPDATE")).ToString("dd-MMM-yyyy hh:mm tt")%>
                                                                    </td>
                                                                    <td style="width: 7.5%;">
                                                                        <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("PURCHASE_ORDER_ID") %>'
                                                                            ToolTip="Edit" Text="edit">
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                                <table width="100%">
                                                    <tr>
                                                        <td align="right" width="56%">
                                                            <asp:LinkButton ID="linkbtnprev" runat="server" Enabled="False" OnClick="linkbtnprev_Click"
                                                                CausesValidation="false" Visible="false" Style="color: #6C5A10;">Previous</asp:LinkButton>
                                                            &nbsp;<asp:LinkButton ID="linkbtnnext" runat="server" Enabled="False" OnClick="linkbtnnext_Click"
                                                                CausesValidation="false" Visible="false" Style="color: #6C5A10;">Next</asp:LinkButton>
                                                        </td>
                                                        <td align="right" width="44%">
                                                            <asp:Label ID="lblCurrentPageNo" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                            <asp:Label ID="lblOf" runat="server" Text="Of" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                            <asp:Label ID="lblTotalNoOfPages" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                            <asp:Label ID="lblDummy" runat="server" Text="| Total Records:" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                            <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 10px;
                        height: 50px">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                    Width="23px" />
                                Wait Update</ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

