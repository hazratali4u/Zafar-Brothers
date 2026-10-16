<%@ Page Title="SAMS :: Purchase Orders" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmPurchaseOrder.aspx.cs" Inherits="Forms_frmPurchaseOrder" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="99.9%">
            <tr>
                <td>
                    <a href="frmPurchaseOrderAdd.aspx?LevelType=3&LevelID=150">Add Purchase Order</a>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnEnquiryList" runat="server">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 5px;">
                                                    <tr>
                                                        <th class="tblhead">
                                                            <td class="tblhead" style="width: 40%;">
                                                                <strong>Supplier </strong>
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
                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                    <tr>
                                                        <td class="tblhead" style="width: 5%;">
                                                            <strong>ID</strong>
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 24.5%;">
                                                            <strong>Supplier</strong>
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 12%;">
                                                            <strong>Invoice No</strong>
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 13%;">
                                                            <strong>Expctd Arival Date</strong>
                                                        </td>
                                                        <td class="tblhead" style="width: 7.5%;">
                                                            <strong>PO. Date</strong>
                                                        </td>
                                                        <td class="tblhead" style="width: 20%;">
                                                            <strong>Updated By</strong>
                                                        </td>
                                                        <td class="tblhead" style="width: 14%;">
                                                            <strong>Last Update</strong>
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 5%;">
                                                            <strong>Action</strong>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Repeater ID="rReserveSample" runat="server" OnItemCommand="rReserveSample_ItemCommand">
                                                    <ItemTemplate>
                                                        <div style="background: #EFF3FB">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                <tr>
                                                                    <td style="width: 5%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-left-width: thin; border-left-style: solid; border-left-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("PURCHASE_ORDER_ID")%>
                                                                    </td>
                                                                    <td style="width: 24.5%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("SUPPLIER")%>
                                                                    </td>
                                                                    <td style="width: 12%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("CONTAINER_NO")%>
                                                                    </td>
                                                                    <td style="width: 13%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("ExDate")%>
                                                                    </td>
                                                                    <td style="width: 7.5%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("DOCUMENT_DATE")%>
                                                                    </td>
                                                                    <td style="width: 20%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("UpdatedBy")%>
                                                                    </td>
                                                                    <td style="width: 14%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# ((DateTime)Eval("LAST_UPDATE")).ToString("dd-MMM-yyyy hh:mm tt")%>
                                                                    </td>
                                                                    <td align="center" style="width: 5%; border-right-width: thin; border-right-style: solid;
                                                                        border-right-color: steelBlue; border-bottom-width: thin; border-bottom-style: solid;
                                                                        border-bottom-color: steelBlue;">
                                                                        <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("PURCHASE_ORDER_ID") %>'
                                                                            ToolTip="Edit">
                                                                            <img alt="" id="Img2" runat="server" src="~/images/edit.gif" />
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
                                                                    <td style="width: 5%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-left-width: thin; border-left-style: solid; border-left-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("PURCHASE_ORDER_ID")%>
                                                                    </td>
                                                                    <td style="width: 24.5%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("SUPPLIER")%>
                                                                    </td>
                                                                    <td style="width: 12%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("CONTAINER_NO")%>
                                                                    </td>
                                                                    <td style="width: 13%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("ExDate")%>
                                                                    </td>
                                                                    <td style="width: 7.5%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("DOCUMENT_DATE")%>
                                                                    </td>
                                                                    <td style="width: 20%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# Eval("UpdatedBy")%>
                                                                    </td>
                                                                    <td style="width: 14%; border-right-width: thin; border-right-style: solid; border-right-color: steelBlue;
                                                                        border-bottom-width: thin; border-bottom-style: solid; border-bottom-color: steelBlue;">
                                                                        <%# ((DateTime)Eval("LAST_UPDATE")).ToString("dd-MMM-yyyy hh:mm tt")%>
                                                                    </td>
                                                                    <td align="center" style="width: 5%; border-right-width: thin; border-right-style: solid;
                                                                        border-right-color: steelBlue; border-bottom-width: thin; border-bottom-style: solid;
                                                                        border-bottom-color: steelBlue;">
                                                                        <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("PURCHASE_ORDER_ID") %>'
                                                                            ToolTip="Edit">
                                                                            <img alt="" id="Img2" runat="server" src="~/images/edit.gif" />
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
