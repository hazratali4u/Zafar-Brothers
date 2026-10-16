<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmProductSearch2.aspx.cs" Inherits="Forms_frmProductSearch2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <link href="../css/POStyle2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function ProductSelected(source, eventArgs) {
            document.getElementById("<%=txtSearch.ClientID %>").value = eventArgs.get_text();
        }
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="updatpanel12" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td width="120px">
                            <strong>Item Code or Name</strong>
                        </td>
                        <td width="265px">
                            <asp:TextBox ID="txtSearch" runat="server" Width="250px" Height="20px"></asp:TextBox>
                            <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
                            <cc1:AutoCompleteExtender ID="aceProduct" runat="server" TargetControlID="txtSearch"
                                ServicePath="wsProductList.asmx" MinimumPrefixLength="3" CompletionInterval="500"
                                ContextKey="false" CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListItemCssClass="autocomplete_listItem" EnableCaching="true" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="ProductSelected" FirstRowSelected="true" ServiceMethod="GetPosProducts">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="Button" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <div class="menu2" style="width: 98.5%">
                                <div class="main">
                                    <ul>
                                        <li class="sku-location">&nbsp;&nbsp;Location</li>
                                        <li class="sperator"></li>
                                        <li class="sku-Supplier">Supplier</li>
                                        <li class="sperator"></li>
                                        <li class="sku-c">Item Code</li>
                                        <li class="sperator"></li>
                                        <li class="sku-nameproduct1">Item Name</li>
                                        <li class="sperator"></li>
                                         <li class="sku-Size">UIP</li>
                                        <li class="sperator"></li>
                                        <li class="sku-price">Sale Price</li>
                                        <li class="sperator"></li>
                                        <li class="sku-price1">Closing Stock</li>
                                        <li class="sperator"></li>
                                           <li class="sku-price2">Stock Position</li>
                                        <li class="sperator"></li>
                                    </ul>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div>
                                <asp:Panel ID="Panel2" runat="server" Height="400px" ScrollBars="Vertical" Width="99%"
                                    BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px">
                                    <asp:GridView ID="GrdPurchase" runat="server" ForeColor="SteelBlue" SelectedRowStyle-BackColor="DarkBlue"
                                        AutoGenerateColumns="false" BackColor="White" HorizontalAlign="Center" BorderColor="White"
                                        RowStyle-Height="22px" ShowHeader="false" Width="100%">
                                         <AlternatingRowStyle BackColor="#E0E0E0" />
                                        <Columns>
                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DISTRIBUTOR_NAME" HeaderText="Location">
                                                <ItemStyle CssClass="grdDetail" Width="15%"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField="Principal" HeaderText="Supplier">
                                                <ItemStyle CssClass="grdDetail" Width="10%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                <ItemStyle CssClass="grdDetail" Width="15%"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                <ItemStyle CssClass="grdDetail" Width="25%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PACKSIZE" HeaderText="Size">
                                                <ItemStyle CssClass="grdDetail" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TRADE_PRICE" HeaderText="Unit PRICE" DataFormatString="{0:F2}">
                                                <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CLOSING_STOCK" HeaderText="Closing Stock">
                                                <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                                <asp:BoundField DataField="STOCK_POSITION" HeaderText="Stock Position">
                                                <ItemStyle CssClass="grdDetail" Width="10%" HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" ForeColor="red"></asp:Label>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
