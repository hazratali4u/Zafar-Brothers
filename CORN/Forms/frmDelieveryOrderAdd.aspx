<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmDelieveryOrderAdd.aspx.cs" Inherits="Forms_frmDelieveryOrderAdd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSaveOrder.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;

        function AddQuantity(e) {            
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                if (document.getElementById('<%=txtBarCode.ClientID%>').value != "") {
                    document.getElementById("<%=btnDummay.ClientID %>").click();
                }
            }
        }
        function pageLoad()
        {
            document.getElementById("<%=txtBarCode.ClientID %>").focus();
        }
    </script>
    <div id="right_data">
        <div style="z-index: 101; left: 60%; width: 100px; position: absolute; top: 1%; height: 80px">
            &nbsp;<asp:Panel ID="Panel21" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="31px" />
                        Wait....
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="pnlMaster" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hfIsGstReg" />
                <asp:HiddenField runat="server" ID="hfOrderQty" />
                <asp:HiddenField runat="server" ID="hfZoneId" />
                <asp:HiddenField runat="server" ID="hfGst" />
                <asp:HiddenField runat="server" ID="hfAddGst" />
                <asp:HiddenField runat="server" ID="hfInvoiceDiscount" />
                 <asp:HiddenField runat="server" ID="hfSaleExecutiveID" />
                <table>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="rblType_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="Manual"></asp:ListItem>
                                <asp:ListItem Value="2" Text="By Scan" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBarCode" runat="server" Width="500px" onkeypress="AddQuantity(event);"></asp:TextBox>
                        </td>
                        <td>
                            <span style="display: none;">
                                <asp:Button ID="btnDummay" runat="server" OnClick="btnDummay_Click" />
                            </span>
                        </td>
                    </tr>
                </table>
                <table cellpadding="2px">
                    <tr class="tblhead">
                        <td class="tblhead" style="width: 400px;">
                            <strong>
                                <asp:Label runat="server" ID="lblCustomer" Font-Size="Medium"></asp:Label></strong>
                        </td>
                        <td class="tblhead" colspan="2" style="width: 200px;">
                            <strong>
                                <asp:Label runat="server" ID="lblDate" Font-Size="Medium"></asp:Label></strong>
                        </td>
                        <td class="tblhead" colspan="2" style="width: 200px;">
                            <strong>
                                <asp:Label runat="server" ID="lblDONo" Font-Size="Medium"></asp:Label></strong>
                        </td>
                        <td class="tblhead" colspan="2" style="width: 200px;">
                            <strong>
                                <asp:Label runat="server" ID="lblOrderNo" Font-Size="Medium"></asp:Label></strong>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 65px;">
                            <strong>
                                <label>
                                    Location</label>
                            </strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDistributor" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                         <td style="width: 65px;">
                            <strong>
                                <label>
                                    Stock by </label>
                            </strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpAssignedDistributor" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td>Delivery Method</td>
                        <td> <asp:RadioButtonList ID="rbtnDeliveryMethd" runat="server" RepeatDirection="Horizontal"
                                    Width="230px">
                                    <asp:ListItem Selected="True" Text="Pick" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="By Courier" Value="1"></asp:ListItem>
                                     <asp:ListItem Text="By Van" Value="2"></asp:ListItem>
                                </asp:RadioButtonList></td>


                                <td>
                                
                                </td>
                                <td>
                               
                                </td>
                    </tr>                    
                    <tr style="display: none;">
                        <td>
                            <strong>
                                <label>
                                    Customer</label>
                            </strong>
                        </td>
                        <td>
                            <strong>
                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px">
                                </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td style="width: 120px;">
                            <strong>
                                <label>
                                    Delievery Location</label></strong>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList runat="server" ID="rdbCustomerAddress" Width="270px" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="rdbCustomerAddress_OnSelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">Show Room</asp:ListItem>
                                <asp:ListItem Value="1">Ware House</asp:ListItem>
                                <asp:ListItem Value="2">Sub Dealer</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table style="display: none;">
                    <tr>
                        <td style="width: 122px">
                            <strong>
                                <asp:Label ID="lblAdress" runat="server" Text="Address"></asp:Label></strong>
                        </td>
                        <td>
                            <strong>
                                <asp:Label ID="lblCustomerAddress" runat="server"></asp:Label></strong>
                            <asp:RadioButtonList runat="server" ID="rdbSubDealers" RepeatDirection="Horizontal" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlDetail" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hfStandardDiscount" Value="0" />
                <table width="100%">
                    <tr valign="top">
                        <td style="width: 100%">
                            <fieldset style="width: 600px;">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:Panel ID="Panel2" runat="server" Width="980px" Height="280px" ScrollBars="Vertical"
                                                    BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="SteelBlue" ForeColor="SteelBlue" HorizontalAlign="Center" Width="100%"
                                                        OnRowDataBound="grdOrder_RowDataBound">
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="skuId" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="skuName" HeaderText="Product Name">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Left" Width="440px" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="qty" HeaderText="Order Pack">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="qtyUnit" HeaderText="Order Unit">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Prev Dispatch Pack">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRecQty" runat="server" Width="75px" Text='<%# Eval("RcdQty")%>'
                                                                        Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Prev Dispatch Unit">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRecQtyUnit" runat="server" Width="75px" Text='<%# Eval("RcdQtyUnit")%>'
                                                                        Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dispatch Pack">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCurrentRecQty" runat="server" Width="75px" Text='<%# Eval("CurrentRcdQty")%>'
                                                                        ></asp:TextBox>
                                                                    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
                                                                    <ajaxToolkit:filteredtextboxextender ID="FilteredTextBoxExtender1" runat="server"
                                                                        FilterType="Numbers" TargetControlID="txtCurrentRecQty">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Dispatch Unit">
                                                                <HeaderStyle CssClass="grdHead" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCurrentRecQtyUnit" runat="server" Width="75px" Text='<%# Eval("CurrentRcdQtyUnit")%>'
                                                                        ></asp:TextBox>
                                                                    <ajaxToolkit:filteredtextboxextender ID="FilteredTextBoxExtender2" runat="server"
                                                                        FilterType="Numbers" TargetControlID="txtCurrentRecQtyUnit">
                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="price" HeaderText="Price" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="amount" HeaderText="Amount" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="grdDetail" HorizontalAlign="Right" Width="75px" />
                                                                <HeaderStyle CssClass="grdHead" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RcdQty">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RcdQtyUnit">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CurrentRcdQty">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="standardDiscount" HeaderText="Statndard Discounted" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="discountedPrice" HeaderText="Discounted Price" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="netAmount" HeaderText="netAmount" DataFormatString="{0:f2}">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="units_in_case" HeaderText="units_in_case">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BAR_CODE" HeaderText="BAR_CODE">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SORT" HeaderText="SORT">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="techId">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="purchasePrice">
                                                                <ItemStyle CssClass="HidePanel" />
                                                                <HeaderStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 205px;">
                                            </td>
                                            <td>
                                                <strong style="font-size: 14px;">Total Amount</strong>
                                                <asp:TextBox ID="txtTotalQty" runat="server" Font-Size="14px" Width="100px" Enabled="false"
                                                    Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtTotalAmount" runat="server" Font-Size="14px" Width="100px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
               <tr>
               <td>     
                    <table>
                        <tr style="display: none">
                            <td colspan="1">
                                <fieldset >
                                    <table>
                                       
                                        <tr style="display: none">
                                            <td style="width: 140px; font-size: 16px;">
                                                <strong>Business Deal:</strong>
                                            </td>
                                            <td style="font-size: 16px;">
                                                <asp:Label ID="lblBusinessDeal" runat="server"></asp:Label>
                                                </strong>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td>
                                                <asp:Literal ID="ltrBusinessDealText" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="ltrBusinessDeal" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td colspan="1"></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <fieldset>
                                    <table>
                                     <tr>
                                            <td style="font-size: 16px;">
                                                <strong>Business Limit:</strong>
                                            </td>
                                            <td style="font-size: 16px;">
                                                <asp:Label ID="lblSaleLimit" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 200px; font-size: 16px;">
                                                <strong>Ledger Balance:</strong>
                                            </td>
                                            <td style="font-size: 16px;">
                                                <asp:Label ID="lblLedgerBalance" runat="server"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblOpeningType" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td style="font-size: 16px;">
                                                <strong>Security Cheque Amount:</strong>
                                            </td>
                                            <td style="font-size: 16px;">
                                                <asp:Label ID="lblChequeAmount" runat="server"></asp:Label>
                                                <asp:Label ID="lblOpeningType2" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td style="font-size: 16px;">
                                                <strong>Due Amount:</strong>
                                            </td>
                                            <td style="font-size: 16px;">
                                                <asp:Label ID="lblDueAmount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td style="font-size: 16px;">
                                                <strong>Turnover Ratio:</strong>
                                            </td>
                                            <td style="font-size: 16px;">
                                                <asp:Label ID="lblTurnoverRatio" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td> <asp:Label  runat="server"  ID="width" Width="100px"></asp:Label> </td>
                            <td>
                            <fieldset >
                              
                              <table>
                              <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdbDiscountType" runat="server" RepeatDirection="Horizontal"
                                    Width="130px">
                                    <asp:ListItem Selected="True" Text="%" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Value" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Invoice Discount</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtinvoiceDiscount" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td>
                                <strong>Freight</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFreight" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Remarks </strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" Columns="40" Rows="2" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Net Amount</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNetAmount" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <strong>
                                    <asp:CheckBox ID="chkLimit" runat="server" Font-Size="14px" Text="Skip Limit" />
                                </strong>
                            </td>
                        </tr>
</table>
                            </fieldset>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>
                                <strong>Driver Name </strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDriverName" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>
                                <strong>Cell No </strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCellNo" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>
                                <strong>Truck No </strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTruckNo" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>
                                <strong>Builty No </strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBuilty" runat="server" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                                          </table>
                    </td> </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td colspan="2">
                    <asp:Button AccessKey="S" ID="btnCalculate" runat="server" Width="100px" Font-Size="8pt"
                        Text="Calculate" UseSubmitBehavior="False" OnClick="btnCalculate_Click" CssClass="Button" />
                    <asp:Button AccessKey="S" ID="btnSaveOrder" runat="server" Width="100px" Font-Size="8pt"
                        Text="Save" UseSubmitBehavior="False" OnClick="btnSaveOrder_Click" CssClass="Button" />
                    <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="100px" Font-Size="8pt"
                        Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
