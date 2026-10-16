<%@ Page Title="CORN :: Sales Return View" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmSaleReturnView.aspx.cs" Inherits="Forms_frmSaleReturnView" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="99.9%">
            <tr>
                <td>
                    <a href="frmSaleReturnDoc.aspx?LevelType=3&LevelID=156" style="font-size: 14px; padding: 4px">
                        Add Sale Return</a>
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
                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                    <tr>
                                                        <th class="tblhead">
                                                            <td class="tblhead" style="width: 40%; font-weight: bold; font-size: 12px;">
                                                                Customer Name
                                                                <asp:TextBox ID="txtProduct" runat="server" Width="190px" Height="18px"></asp:TextBox>
                                                                <telerik:RadButton ID="btnFilter" runat="server" Width="60px" Text="Filter" OnClick="btnFilter_Click" />
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
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                    <tr style="height: 26px;">
                                                        <td class="tblhead" style="width: 8%; font-weight:bold;">
                                                            Invoice ID
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 20%; font-weight:bold;">
                                                            Cusotmer Name
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 15%; font-weight:bold;">
                                                            Net Return
                                                        </td>
                                                        <td class="tblhead" style="width: 11%; font-weight:bold;">
                                                            Document Date
                                                        </td>
                                                        <td class="tblhead" style="width: 17%; font-weight:bold;">
                                                            Last Update
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width: 5.5%; font-weight:bold;">
                                                            Action
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Repeater ID="rptSaleReturn" runat="server" OnItemCommand="rptSaleReturn_ItemCommand">
                                                    <ItemTemplate>
                                                        <div style="background: #EFF3FB">
                                                            <table width="100%" cellspacing="0" cellpadding="2">
                                                                <tr style="height: 22px;">
                                                                    <td style="width: 8%;">
                                                                        <%# Eval("SALE_INVOICE_ID")%>
                                                                    </td>
                                                                    <td style="width: 20%;">
                                                                        <%# Eval("customer_name")%>
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        <%# Eval("NetReturn", "{0:f2}")%>
                                                                    </td>
                                                                    <td style="width: 11%;">
                                                                        <%# Eval("DOCUMENT_DATE","{0:dd-MMM-yyyy}")%>
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        <%# ((DateTime)Eval("LAST_UPDATE")).ToString("dd-MMM-yyyy hh:mm tt")%>
                                                                    </td>
                                                                    <td style="width: 3%;">
                                                                        <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("SALE_INVOICE_ID") %>'
                                                                            ToolTip="Edit">
                                                                            <img src="../images/edit.gif" width="16" height="16" alt="">
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                    </td>
                                                                    <td style="width: 3%;">
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("SALES_RETURN_ID") %>'
                                                                            ToolTip="Delete Sale Return" OnClientClick="return confirm('Are you sure? you are going to delete!');">
                                                                                <img src="../images/delete.gif" width="16" height="16" alt="">
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                  
                                                                </tr>
                                                                  
                                                            </table>
                                                           
                                                        </div>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <div style="background: #ffffff">
                                                            <table width="100%"  cellspacing="0" cellpadding="2">
                                                                <tr style="height: 22px;">
                                                                    <td style="width: 8%;">
                                                                        <%# Eval("SALE_INVOICE_ID")%>
                                                                    </td>
                                                                    <td style="width: 20%;">
                                                                        <%# Eval("customer_name")%>
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        <%#  Eval("NetReturn","{0:f2}")%>
                                                                    </td>
                                                                    <td style="width: 11%;">
                                                                        <%# Eval("DOCUMENT_DATE", "{0:dd-MMM-yyyy}")%>
                                                                    </td>
                                                                    <td style="width: 15%;">
                                                                        <%# ((DateTime)Eval("LAST_UPDATE")).ToString("dd-MMM-yyyy hh:mm tt")%>
                                                                    </td>
                                                                    <td style="width: 3%;">
                                                                        <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("SALE_INVOICE_ID") %>'
                                                                            ToolTip="Edit">
                                                                             <img src="../images/edit.gif" width="16" height="16" alt="">
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                    <td style="width: 3%;">
                                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("SALES_RETURN_ID") %>'
                                                                            ToolTip="Delete Sale Return" OnClientClick="return confirm('Are you sure? you are going to delete!');">
                                                                                <img src="../images/delete.gif" width="16" height="16" alt="">
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
                        height: 100px">
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
